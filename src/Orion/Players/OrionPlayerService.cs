// Copyright (c) 2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Orion.Events;
using Orion.Events.Players;
using Orion.Packets;
using Orion.Packets.Modules;
using Orion.Packets.Players;
using Orion.Utils;
using OTAPI;
using Serilog;
using Main = Terraria.Main;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    [Service("orion-players")]
    internal sealed class OrionPlayerService : OrionService, IPlayerService {
        private readonly ThreadLocal<bool> _shouldIgnoreNextReceiveData = new ThreadLocal<bool>();

        private readonly ThreadLocal<byte[]> _dirtyReadBuffer =
            new ThreadLocal<byte[]>(() => new byte[ushort.MaxValue]);

        private readonly ThreadLocal<byte[]> _dirtyWriteBuffer =
            new ThreadLocal<byte[]>(() => new byte[ushort.MaxValue]);

        private readonly IDictionary<PacketType, Action<PacketReceiveEvent>> _packetReceiveHandlers;

        public IReadOnlyArray<IPlayer> Players { get; }

        public OrionPlayerService(OrionKernel kernel, ILogger log) : base(kernel, log) {
            Debug.Assert(log != null, "log should not be null");
            Debug.Assert(Main.player != null, "Terraria players should not be null");

            _packetReceiveHandlers = new Dictionary<PacketType, Action<PacketReceiveEvent>> {
                [PacketType.PlayerConnect] = PlayerConnectHandler,
                [PacketType.PlayerData] = PlayerDataHandler,
                [PacketType.PlayerInventorySlot] = PlayerInventorySlotHandler,
                [PacketType.PlayerJoin] = PlayerJoinHandler,
                [PacketType.PlayerSpawn] = PlayerSpawnHandler,
                [PacketType.PlayerInfo] = PlayerInfoHandler,
                [PacketType.PlayerHealth] = PlayerHealthHandler,
                [PacketType.PlayerPvp] = PlayerPvpHandler,
                [PacketType.PlayerHealEffect] = PlayerHealEffectHandler,
                [PacketType.PlayerZones] = PlayerZonesHandler,
                [PacketType.PlayerPasswordResponse] = PlayerPasswordResponseHandler,
                [PacketType.PlayerMana] = PlayerManaHandler,
                [PacketType.PlayerManaEffect] = PlayerManaEffectHandler,
                [PacketType.PlayerTeam] = PlayerTeamHandler,
                [PacketType.PlayerUuid] = PlayerUuidHandler,
                [PacketType.PlayerTeleportationPotion] = PlayerTeleportationPotionHandler,
                [PacketType.Module] = ModuleHandler
            };

            // Ignore the last player since it is used as a failure slot.
            Players = new WrappedReadOnlyArray<OrionPlayer, TerrariaPlayer>(
                Main.player.AsMemory(..^1),
                (playerIndex, terrariaPlayer) => new OrionPlayer(this, playerIndex, terrariaPlayer));

            Hooks.Net.ReceiveData = ReceiveDataHandler;
            Hooks.Net.SendBytes = SendBytesHandler;
            Hooks.Net.RemoteClient.PreReset = PreResetHandler;
        }

        public override void Dispose() {
            _shouldIgnoreNextReceiveData.Dispose();
            _dirtyReadBuffer.Dispose();
            _dirtyWriteBuffer.Dispose();

            Hooks.Net.ReceiveData = null;
            Hooks.Net.SendBytes = null;
            Hooks.Net.RemoteClient.PreReset = null;
        }

        private HookResult ReceiveDataHandler(
                Terraria.MessageBuffer buffer, ref byte packetId, ref int readOffset, ref int start, ref int length) {
            Debug.Assert(buffer != null, "buffer should not be null");
            Debug.Assert(buffer.whoAmI >= 0 && buffer.whoAmI < Players.Count, "buffer should have a valid index");

            if (_shouldIgnoreNextReceiveData.Value) {
                _shouldIgnoreNextReceiveData.Value = false;
                return HookResult.Continue;
            }

            // Offset start and length by two since the packet length field is not included.
            var stream = new MemoryStream(buffer.readBuffer, start - 2, length + 2);
            var sender = Players[buffer.whoAmI];
            var packet = Packet.ReadFromStream(stream, PacketContext.Server);
            var e = new PacketReceiveEvent(sender, packet);
            Kernel.RaiseEvent(e, Log);

            if (e.IsCanceled()) {
                return HookResult.Cancel;
            } else if (_packetReceiveHandlers.TryGetValue(packet.Type, out var handler)) {
                handler(e);
                if (e.IsCanceled()) {
                    return HookResult.Cancel;
                }
            }

            if (!e.IsDirty) {
                return HookResult.Continue;
            }

            // To simulate the receival of the dirty packet, we:
            // 1) Save the old read buffer and reader.
            // 2) Swap in the new read buffer and reader.
            // 3) Call GetData() while making sure that we ignore the next ReceiveDataHandler call.
            // 4) Restore the old read buffer and reader.
            var oldReadBuffer = buffer.readBuffer;
            var oldReader = buffer.reader;

            var newStream = new MemoryStream(_dirtyReadBuffer.Value);
            packet.WriteToStream(newStream, PacketContext.Client);
            buffer.readBuffer = _dirtyReadBuffer.Value;
            buffer.reader = new BinaryReader(newStream);

            _shouldIgnoreNextReceiveData.Value = true;
            buffer.GetData(2, (int)(newStream.Position - 2), out _);

            buffer.readBuffer = oldReadBuffer;
            buffer.reader = oldReader;
            return HookResult.Cancel;
        }

        private HookResult SendBytesHandler(
                ref int remoteClient, ref byte[] data, ref int offset, ref int size,
                ref Terraria.Net.Sockets.SocketSendCallback callback, ref object state) {
            Debug.Assert(remoteClient >= 0 && remoteClient < Players.Count, "remote client should be a valid index");

            var stream = new MemoryStream(data, offset, size);
            var receiver = Players[remoteClient];
            var packet = Packet.ReadFromStream(stream, PacketContext.Client);
            var e = new PacketSendEvent(receiver, packet);
            Kernel.RaiseEvent(e, Log);

            if (e.IsCanceled()) {
                return HookResult.Cancel;
            }

            var newStream = new MemoryStream(_dirtyWriteBuffer.Value);
            packet.WriteToStream(newStream, PacketContext.Server);
            data = _dirtyWriteBuffer.Value;
            offset = 0;
            size = (int)newStream.Position;
            return HookResult.Continue;
        }

        private HookResult PreResetHandler(Terraria.RemoteClient remoteClient) {
            Debug.Assert(remoteClient != null, "remote client should not be null");
            Debug.Assert(remoteClient.Id >= 0 && remoteClient.Id < Players.Count,
                "remote client should have a valid index");

            // Check if the client was active since this gets called when setting up RemoteClient as well.
            if (!remoteClient.IsActive) {
                return HookResult.Continue;
            }

            var player = Players[remoteClient.Id];
            var e = new PlayerQuitEvent(player);
            Kernel.RaiseEvent(e, Log);
            return HookResult.Continue;
        }

        private void PlayerConnectHandler(PacketReceiveEvent e_) {
            var packet = (PlayerConnectPacket)e_.Packet;
            var e = new PlayerConnectEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerDataHandler(PacketReceiveEvent e_) {
            var packet = (PlayerDataPacket)e_.Packet;
            var e = new PlayerDataEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerInventorySlotHandler(PacketReceiveEvent e_) {
            var packet = (PlayerInventorySlotPacket)e_.Packet;
            var e = new PlayerInventoryEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerJoinHandler(PacketReceiveEvent e_) {
            var e = new PlayerJoinEvent(e_.Sender);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerSpawnHandler(PacketReceiveEvent e_) {
            var packet = (PlayerSpawnPacket)e_.Packet;
            var e = new PlayerSpawnEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerInfoHandler(PacketReceiveEvent e_) {
            var packet = (PlayerInfoPacket)e_.Packet;
            var e = new PlayerInfoEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerHealthHandler(PacketReceiveEvent e_) {
            var packet = (PlayerHealthPacket)e_.Packet;
            var e = new PlayerHealthEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerPvpHandler(PacketReceiveEvent e_) {
            var packet = (PlayerPvpPacket)e_.Packet;
            var e = new PlayerPvpEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerHealEffectHandler(PacketReceiveEvent e_) {
            var packet = (PlayerHealEffectPacket)e_.Packet;
            var e = new PlayerHealEffectEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerZonesHandler(PacketReceiveEvent e_) {
            var packet = (PlayerZonesPacket)e_.Packet;
            var e = new PlayerZonesEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerPasswordResponseHandler(PacketReceiveEvent e_) {
            var packet = (PlayerPasswordResponsePacket)e_.Packet;
            var e = new PlayerPasswordEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerManaHandler(PacketReceiveEvent e_) {
            var packet = (PlayerManaPacket)e_.Packet;
            var e = new PlayerManaEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerManaEffectHandler(PacketReceiveEvent e_) {
            var packet = (PlayerManaEffectPacket)e_.Packet;
            var e = new PlayerManaEffectEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerTeamHandler(PacketReceiveEvent e_) {
            var packet = (PlayerTeamPacket)e_.Packet;
            var e = new PlayerTeamEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerUuidHandler(PacketReceiveEvent e_) {
            var packet = (PlayerUuidPacket)e_.Packet;
            var e = new PlayerUuidEvent(e_.Sender, packet);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void PlayerTeleportationPotionHandler(PacketReceiveEvent e_) {
            var e = new PlayerTeleportEvent(e_.Sender);
            Kernel.RaiseEvent(e, Log);
            e_.CancellationReason = e.CancellationReason;
        }

        private void ModuleHandler(PacketReceiveEvent e_) {
            var module = ((ModulePacket)e_.Packet).Module;
            if (module is ChatModule chatModule) {
                var e = new PlayerChatEvent(e_.Sender, chatModule);
                Kernel.RaiseEvent(e, Log);
                e_.CancellationReason = e.CancellationReason;
            }
        }
    }
}
