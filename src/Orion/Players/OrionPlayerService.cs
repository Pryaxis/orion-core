// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Orion.Collections;
using Orion.Events;
using Orion.Events.Packets;
using Orion.Events.Players;
using Orion.Events.World.Tiles;
using Orion.Framework;
using Orion.Packets;
using Orion.Packets.Client;
using Orion.Packets.Modules;
using Orion.Packets.Players;
using Orion.Packets.World.Tiles;
using Orion.World.Tiles;
using Serilog;

namespace Orion.Players {
    [Binding("orion-players", Author = "Pryaxis", Priority = BindingPriority.Lowest)]
    internal sealed class OrionPlayerService : OrionService, IPlayerService {
        private delegate void OnReceivePacketHandler(Terraria.MessageBuffer buffer, Span<byte> span);
        private delegate void OnSendPacketHandler(int playerIndex, Span<byte> span);

        private static readonly MethodInfo _onReceivePacket =
            typeof(OrionPlayerService)
                .GetMethod(nameof(OnReceivePacket), BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo _onSendPacket =
            typeof(OrionPlayerService)
                .GetMethod(nameof(OnSendPacket), BindingFlags.NonPublic | BindingFlags.Instance); 

        private readonly ThreadLocal<bool> _ignoreReceiveDataHandler = new ThreadLocal<bool>();
        private readonly OnReceivePacketHandler?[] _onReceivePacketHandlers = new OnReceivePacketHandler?[256];
        private readonly OnReceivePacketHandler?[] _onReceiveModuleHandlers = new OnReceivePacketHandler?[65536];
        private readonly ThreadLocal<byte[]> _receiveBuffer =
            new ThreadLocal<byte[]>(() => new byte[ushort.MaxValue]);

        private readonly OnSendPacketHandler?[] _onSendPacketHandlers = new OnSendPacketHandler?[256];
        private readonly OnSendPacketHandler?[] _onSendModuleHandlers = new OnSendPacketHandler?[65536];
        private readonly ThreadLocal<byte[]> _sendBuffer =
            new ThreadLocal<byte[]>(() => new byte[ushort.MaxValue]);

        public OrionPlayerService(OrionKernel kernel, ILogger log) : base(kernel, log) {
            Debug.Assert(kernel != null);
            Debug.Assert(log != null);

            OnReceivePacketHandler MakeOnReceivePacketHandler(Type packetType) =>
                (OnReceivePacketHandler)_onReceivePacket
                    .MakeGenericMethod(packetType)
                    .CreateDelegate(typeof(OnReceivePacketHandler), this);
            OnSendPacketHandler MakeOnSendPacketHandler(Type packetType) =>
                (OnSendPacketHandler)_onSendPacket
                    .MakeGenericMethod(packetType)
                    .CreateDelegate(typeof(OnSendPacketHandler), this);

            // Construct the `_onReceivePacketHandlers` and `_onSendPacketHandlers` arrays ahead of time.
            foreach (var packetId in (PacketId[])Enum.GetValues(typeof(PacketId))) {
                var packetType = packetId.Type();
                _onReceivePacketHandlers[(byte)packetId] = MakeOnReceivePacketHandler(packetType);
                _onSendPacketHandlers[(byte)packetId] = MakeOnSendPacketHandler(packetType);
            }

            // Construct the `_onReceiveModuleHandlers` and `_onSendModuleHandlers` arrays ahead of time.
            foreach (var moduleId in (ModuleId[])Enum.GetValues(typeof(ModuleId))) {
                var packetType = typeof(ModulePacket<>).MakeGenericType(moduleId.Type());
                _onReceiveModuleHandlers[(ushort)moduleId] = MakeOnReceivePacketHandler(packetType);
                _onSendModuleHandlers[(ushort)moduleId] = MakeOnSendPacketHandler(packetType);
            }

            // Construct the `Players` array. Note that the last player should be ignored, as it is not a real player.
            Players = new WrappedReadOnlyList<OrionPlayer, Terraria.Player>(
                Terraria.Main.player.AsMemory(..^1),
                (playerIndex, terrariaPlayer) => new OrionPlayer(playerIndex, terrariaPlayer, this));

            OTAPI.Hooks.Net.ReceiveData = ReceiveDataHandler;
            OTAPI.Hooks.Net.SendBytes = SendBytesHandler;
            OTAPI.Hooks.Player.PreUpdate = PreUpdateHandler;
            OTAPI.Hooks.Net.RemoteClient.PreReset = PreResetHandler;
        }

        public IReadOnlyList<IPlayer> Players { get; }

        public override void Dispose() {
            _ignoreReceiveDataHandler.Dispose();

            OTAPI.Hooks.Net.ReceiveData = null;
            OTAPI.Hooks.Net.SendBytes = null;
            OTAPI.Hooks.Player.PreUpdate = null;
            OTAPI.Hooks.Net.RemoteClient.PreReset = null;
        }

        private OTAPI.HookResult ReceiveDataHandler(
                Terraria.MessageBuffer buffer, ref byte packetId, ref int _, ref int start, ref int length) {
            Debug.Assert(buffer != null);
            Debug.Assert(buffer.whoAmI >= 0 && buffer.whoAmI < Players.Count);
            Debug.Assert(start >= 0 && start + length <= buffer.readBuffer.Length);
            Debug.Assert(length > 0);

            // Check `_ignoreReceiveDataHandler` to prevent an infinite loop if `GetData()` is called in
            // `ReceivePacket`. A thread-local value is used in case there is some concurrency.
            if (_ignoreReceiveDataHandler.Value) {
                _ignoreReceiveDataHandler.Value = false;
                return OTAPI.HookResult.Continue;
            }

            var span = buffer.readBuffer.AsSpan(start..(start + length));
            OnReceivePacketHandler handler;
            if (packetId == (byte)PacketId.Module) {
                var moduleId = Unsafe.ReadUnaligned<ushort>(ref buffer.readBuffer[start + 1]);
                handler = _onReceiveModuleHandlers[moduleId] ?? OnReceivePacket<ModulePacket<UnknownModule>>;
            } else {
                handler = _onReceivePacketHandlers[packetId] ?? OnReceivePacket<UnknownPacket>;
            }

            handler(buffer, span);
            return OTAPI.HookResult.Cancel;
        }

        private void OnReceivePacket<TPacket>(Terraria.MessageBuffer buffer, Span<byte> span)
                where TPacket : struct, IPacket {
            Debug.Assert(buffer != null);
            Debug.Assert(span.Length > 0);

            // When reading the packet, we need to use the `Server` context since this packet should be read as the
            // server. Ignore the first byte as it is the packet ID.
            var packet = new TPacket();
            var packetLength = packet.Read(span[1..], PacketContext.Server);
            Debug.Assert(packetLength == span.Length - 1);

            // If `TPacket` is `UnknownPacket`, then we need to set the `Id` property appropriately.
            if (typeof(TPacket) == typeof(UnknownPacket)) {
                Unsafe.As<TPacket, UnknownPacket>(ref packet).Id = (PacketId)span[0];
            }

            var player = Players[buffer.whoAmI];
            var evt = new PacketReceiveEvent<TPacket>(ref packet, player);
            Kernel.Raise(evt, Log);
            if (evt.IsCanceled() || OnReceivePacketEvent(player, ref packet)) {
                return;
            }

            // To simulate the receival of the packet, we must swap out the read buffer and reader, and call `GetData()`
            // while ensuring that the next `ReceiveDataHandler()` call is ignored. A thread-local receive buffer is
            // used in case there is some concurrency.
            var oldReadBuffer = buffer.readBuffer;
            var oldReader = buffer.reader;

            // When writing the packet, we need to use the `Client` context since this packet comes from the client.
            var newPacketLength = packet.WriteWithHeader(_receiveBuffer.Value, PacketContext.Client);

            _ignoreReceiveDataHandler.Value = true;
            buffer.readBuffer = _receiveBuffer.Value;
            buffer.reader = new BinaryReader(new MemoryStream(buffer.readBuffer), Encoding.UTF8);
            buffer.GetData(2, newPacketLength - 2, out _);

            buffer.readBuffer = oldReadBuffer;
            buffer.reader = oldReader;
        }

        private bool OnReceivePacketEvent<TPacket>(IPlayer player, ref TPacket packet) where TPacket : struct, IPacket {
            Debug.Assert(player != null);

            static ref TOtherPacket As<TOtherPacket>(ref TPacket packet)
                => ref Unsafe.As<TPacket, TOtherPacket>(ref packet);

            return packet.Id switch {
                PacketId.PlayerJoin => RaisePlayerEvent(player, ref As<PlayerJoinPacket>(ref packet)),
                PacketId.PlayerHealth => RaisePlayerEvent(player, ref As<PlayerHealthPacket>(ref packet)),
                PacketId.TileModify => RaisePlayerEvent(player, ref As<TileModifyPacket>(ref packet)),
                PacketId.PlayerPvp => RaisePlayerEvent(player, ref As<PlayerPvpPacket>(ref packet)),
                PacketId.PlayerMana => RaisePlayerEvent(player, ref As<PlayerManaPacket>(ref packet)),
                PacketId.PlayerTeam => RaisePlayerEvent(player, ref As<PlayerTeamPacket>(ref packet)),
                PacketId.ClientUuid => RaisePlayerEvent(player, ref As<ClientUuidPacket>(ref packet)),
                PacketId.Module => true switch {
                    _ when typeof(TPacket) == typeof(ModulePacket<ChatModule>) =>
                        RaisePlayerEvent(player, ref As<ModulePacket<ChatModule>>(ref packet)),
                    _ => false
                },
                _ => false
            };
        }

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Standardization")]
        private bool RaisePlayerEvent(IPlayer player, ref PlayerJoinPacket packet) {
            var evt = new PlayerJoinEvent(player);
            Kernel.Raise(evt, Log);
            return evt.IsCanceled();
        }

        private bool RaisePlayerEvent(IPlayer player, ref PlayerHealthPacket packet) {
            var evt = new PlayerHealthEvent(player, packet.Health, packet.MaxHealth);
            Kernel.Raise(evt, Log);
            return evt.IsCanceled();
        }

        private bool RaisePlayerEvent(IPlayer player, ref TileModifyPacket packet) {
            bool RaiseBlockBreakEvent(ref TileModifyPacket packet, bool shouldSuppressItems) {
                var evt = new BlockBreakEvent(player, packet.X, packet.Y, packet.IsFailure, shouldSuppressItems);
                Kernel.Raise(evt, Log);
                return evt.IsCanceled();
            }

            bool RaiseBlockPlaceEvent(ref TileModifyPacket packet, bool isReplacement) {
                var evt = new BlockPlaceEvent(
                    player, packet.X, packet.Y, packet.BlockId, packet.BlockStyle, isReplacement);
                Kernel.Raise(evt, Log);
                return evt.IsCanceled();
            }

            bool RaiseWallBreakEvent(ref TileModifyPacket packet) {
                var evt = new WallBreakEvent(player, packet.X, packet.Y, packet.IsFailure);
                Kernel.Raise(evt, Log);
                return evt.IsCanceled();
            }

            bool RaiseWallPlaceEvent(ref TileModifyPacket packet, bool isReplacement) {
                var evt = new WallPlaceEvent(player, packet.X, packet.Y, packet.WallId, isReplacement);
                Kernel.Raise(evt, Log);
                return evt.IsCanceled();
            }

            bool RaiseWiringBreakEvent(ref TileModifyPacket packet, Wiring wiring) {
                var evt = new WiringBreakEvent(player, packet.X, packet.Y, wiring);
                Kernel.Raise(evt, Log);
                return evt.IsCanceled();
            }

            bool RaiseWiringPlaceEvent(ref TileModifyPacket packet, Wiring wiring) {
                var evt = new WiringPlaceEvent(player, packet.X, packet.Y, wiring);
                Kernel.Raise(evt, Log);
                return evt.IsCanceled();
            }

            return packet.Modification switch {
                TileModification.BreakBlock => RaiseBlockBreakEvent(ref packet, false),
                TileModification.PlaceBlock => RaiseBlockPlaceEvent(ref packet, false),
                TileModification.BreakWall => RaiseWallBreakEvent(ref packet),
                TileModification.PlaceWall => RaiseWallPlaceEvent(ref packet, false),
                TileModification.BreakBlockNoItems => RaiseBlockBreakEvent(ref packet, true),
                TileModification.PlaceRedWire => RaiseWiringPlaceEvent(ref packet, Wiring.Red),
                TileModification.BreakRedWire => RaiseWiringBreakEvent(ref packet, Wiring.Red),
                TileModification.PlaceActuator => RaiseWiringPlaceEvent(ref packet, Wiring.Actuator),
                TileModification.BreakActuator => RaiseWiringBreakEvent(ref packet, Wiring.Actuator),
                TileModification.PlaceBlueWire => RaiseWiringPlaceEvent(ref packet, Wiring.Blue),
                TileModification.BreakBlueWire => RaiseWiringBreakEvent(ref packet, Wiring.Blue),
                TileModification.PlaceGreenWire => RaiseWiringPlaceEvent(ref packet, Wiring.Green),
                TileModification.BreakGreenWire => RaiseWiringBreakEvent(ref packet, Wiring.Green),
                TileModification.PlaceYellowWire => RaiseWiringPlaceEvent(ref packet, Wiring.Yellow),
                TileModification.BreakYellowWire => RaiseWiringBreakEvent(ref packet, Wiring.Yellow),
                TileModification.ReplaceBlock => RaiseBlockPlaceEvent(ref packet, true),
                TileModification.ReplaceWall => RaiseWallPlaceEvent(ref packet, true),
                _ => false
            };
        }

        private bool RaisePlayerEvent(IPlayer player, ref PlayerPvpPacket packet) {
            var evt = new PlayerPvpEvent(player, packet.IsInPvp);
            Kernel.Raise(evt, Log);
            return evt.IsCanceled();
        }

        private bool RaisePlayerEvent(IPlayer player, ref PlayerManaPacket packet) {
            var evt = new PlayerManaEvent(player, packet.Mana, packet.MaxMana);
            Kernel.Raise(evt, Log);
            return evt.IsCanceled();
        }

        private bool RaisePlayerEvent(IPlayer player, ref PlayerTeamPacket packet) {
            var evt = new PlayerTeamEvent(player, packet.Team);
            Kernel.Raise(evt, Log);
            return evt.IsCanceled();
        }

        private bool RaisePlayerEvent(IPlayer player, ref ClientUuidPacket packet) {
            var evt = new PlayerUuidEvent(player, packet.Uuid);
            Kernel.Raise(evt, Log);
            return evt.IsCanceled();
        }

        private bool RaisePlayerEvent(IPlayer player, ref ModulePacket<ChatModule> packet) {
            var evt = new PlayerChatEvent(player, packet.Module.ClientCommand, packet.Module.ClientMessage);
            Kernel.Raise(evt, Log);
            return evt.IsCanceled();
        }

        private OTAPI.HookResult SendBytesHandler(
                ref int playerIndex, ref byte[] data, ref int offset, ref int size,
                ref Terraria.Net.Sockets.SocketSendCallback _, ref object _2) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Players.Count);
            Debug.Assert(data != null);
            Debug.Assert(offset >= 0 && offset + size <= data.Length);

            var span = data.AsSpan((offset + 2)..(offset + size));
            var packetId = span[0];
            OnSendPacketHandler handler;
            if (packetId == (byte)PacketId.Module) {
                var moduleId = Unsafe.ReadUnaligned<ushort>(ref span[1]);
                handler = _onSendModuleHandlers[moduleId] ?? OnSendPacket<ModulePacket<UnknownModule>>;
            } else {
                handler = _onSendPacketHandlers[packetId] ?? OnSendPacket<UnknownPacket>;
            }

            handler(playerIndex, span);
            return OTAPI.HookResult.Cancel;
        }

        private void OnSendPacket<TPacket>(int playerIndex, Span<byte> span) where TPacket : struct, IPacket {
            Debug.Assert(playerIndex >= 0 && playerIndex < Players.Count);
            Debug.Assert(span.Length > 0);

            // When reading the packet, we need to use the `Client` context since this packet should be read as the
            // client. Ignore the first byte as it is the packet ID.
            var packet = new TPacket();
            var packetLength = packet.Read(span[1..], PacketContext.Client);
            Debug.Assert(packetLength == span.Length - 1);

            // If `TPacket` is `UnknownPacket`, then we need to set the `Id` property appropriately.
            if (typeof(TPacket) == typeof(UnknownPacket)) {
                Unsafe.As<TPacket, UnknownPacket>(ref packet).Id = (PacketId)span[0];
            }

            var evt = new PacketSendEvent<TPacket>(ref packet, Players[playerIndex]);
            Kernel.Raise(evt, Log);
            if (evt.IsCanceled()) {
                return;
            }

            // Send the packet. A thread-local send buffer is used in case there is some concurrency.
            //
            // When writing the packet, we need to use the `Server` context since this packet comes from the server.
            var newPacketLength = packet.WriteWithHeader(_sendBuffer.Value, PacketContext.Server);

            var terrariaClient = Terraria.Netplay.Clients[playerIndex];
            terrariaClient.Socket.AsyncSend(_sendBuffer.Value, 0, newPacketLength, terrariaClient.ServerWriteCallBack);
        }

        private OTAPI.HookResult PreUpdateHandler(Terraria.Player terrariaPlayer, ref int _) {
            Debug.Assert(terrariaPlayer != null);

            var evt = new PlayerTickEvent(GetPlayer(terrariaPlayer));
            Kernel.Raise(evt, Log);
            return evt.IsCanceled() ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private OTAPI.HookResult PreResetHandler(Terraria.RemoteClient remoteClient) {
            Debug.Assert(remoteClient != null);
            Debug.Assert(remoteClient.Id >= 0 && remoteClient.Id < Players.Count);

            // Check if the client was active since this gets called when setting up RemoteClient as well.
            if (!remoteClient.IsActive) {
                return OTAPI.HookResult.Continue;
            }

            var evt = new PlayerQuitEvent(Players[remoteClient.Id]);
            Kernel.Raise(evt, Log);
            return OTAPI.HookResult.Continue;
        }

        // Gets an `IPlayer` which corresponds to the given Terraria player. Retrieves the `IPlayer` from the `Players`
        // array, if possible.
        private IPlayer GetPlayer(Terraria.Player terrariaPlayer) {
            Debug.Assert(terrariaPlayer != null);

            var playerIndex = terrariaPlayer.whoAmI;
            Debug.Assert(playerIndex >= 0 && playerIndex < Players.Count);

            var isConcrete = terrariaPlayer == Terraria.Main.player[playerIndex];
            return isConcrete ? Players[playerIndex] : new OrionPlayer(terrariaPlayer, this);
        }
    }
}
