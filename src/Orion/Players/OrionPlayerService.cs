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
using Orion.Framework;
using Orion.Packets;
using Orion.Packets.Client;
using Orion.Packets.Modules;
using Orion.Packets.Players;
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
            // Construct the `Players` array. Note that the last player should be ignored, as it is not a real player.
            Players = new WrappedReadOnlyList<OrionPlayer, Terraria.Player>(
                Terraria.Main.player.AsMemory(..^1),
                (playerIndex, terrariaPlayer) => new OrionPlayer(playerIndex, terrariaPlayer, kernel, log));

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

            OTAPI.Hooks.Net.ReceiveData = ReceiveDataHandler;
            OTAPI.Hooks.Net.SendBytes = SendBytesHandler;
            OTAPI.Hooks.Player.PreUpdate = PreUpdateHandler;
            OTAPI.Hooks.Net.RemoteClient.PreReset = PreResetHandler;

            Kernel.RegisterHandlers(this, Log);
        }

        public IReadOnlyList<IPlayer> Players { get; }

        public override void Dispose() {
            _ignoreReceiveDataHandler.Dispose();
            _receiveBuffer.Dispose();
            _sendBuffer.Dispose();

            OTAPI.Hooks.Net.ReceiveData = null;
            OTAPI.Hooks.Net.SendBytes = null;
            OTAPI.Hooks.Player.PreUpdate = null;
            OTAPI.Hooks.Net.RemoteClient.PreReset = null;

            Kernel.DeregisterHandlers(this, Log);
        }

        // =============================================================================================================
        // OTAPI hooks
        //

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

            OnReceivePacketHandler handler;
            var span = buffer.readBuffer.AsSpan(start..(start + length));
            if (packetId == (byte)PacketId.Module) {
                var moduleId = Unsafe.ReadUnaligned<ushort>(ref span[1]);
                handler = _onReceiveModuleHandlers[moduleId] ?? OnReceivePacket<ModulePacket<UnknownModule>>;
            } else {
                handler = _onReceivePacketHandlers[packetId] ?? OnReceivePacket<UnknownPacket>;
            }

            handler(buffer, span);
            return OTAPI.HookResult.Cancel;
        }

        private OTAPI.HookResult SendBytesHandler(
                ref int playerIndex, ref byte[] data, ref int offset, ref int size,
                ref Terraria.Net.Sockets.SocketSendCallback _, ref object _2) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Players.Count);
            Debug.Assert(data != null);
            Debug.Assert(offset >= 0 && offset + size <= data.Length);
            Debug.Assert(size > 0);

            OnSendPacketHandler handler;
            var span = data.AsSpan((offset + 2)..(offset + size));
            var packetId = span[0];
            if (packetId == (byte)PacketId.Module) {
                var moduleId = Unsafe.ReadUnaligned<ushort>(ref span[1]);
                handler = _onSendModuleHandlers[moduleId] ?? OnSendPacket<ModulePacket<UnknownModule>>;
            } else {
                handler = _onSendPacketHandlers[packetId] ?? OnSendPacket<UnknownPacket>;
            }

            handler(playerIndex, span);
            return OTAPI.HookResult.Cancel;
        }

        private OTAPI.HookResult PreUpdateHandler(Terraria.Player _, ref int playerIndex) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Players.Count);

            var player = Players[playerIndex];
            var evt = new PlayerTickEvent(player);
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

            var player = Players[remoteClient.Id];
            var evt = new PlayerQuitEvent(player);
            Kernel.Raise(evt, Log);
            return OTAPI.HookResult.Continue;
        }

        // =============================================================================================================
        // Packet event publishers
        //

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

            var sender = Players[buffer.whoAmI];
            var evt = new PacketReceiveEvent<TPacket>(ref packet, sender);
            Kernel.Raise(evt, Log);
            if (evt.IsCanceled()) {
                return;
            }

            // To simulate the receival of the packet, we must swap out the read buffer and reader, and call `GetData()`
            // while ensuring that the next `ReceiveDataHandler()` call is ignored. A thread-local receive buffer is
            // used in case there is some concurrency.
            var oldReadBuffer = buffer.readBuffer;
            var oldReader = buffer.reader;

            // When writing the packet, we need to use the `Client` context since this packet comes from the client.
            var receiveBuffer = _receiveBuffer.Value;
            var newPacketLength = packet.WriteWithHeader(receiveBuffer, (PacketContext)PacketContext.Client);

            _ignoreReceiveDataHandler.Value = true;
            buffer.readBuffer = receiveBuffer;
            buffer.reader = new BinaryReader(new MemoryStream(buffer.readBuffer), Encoding.UTF8);
            buffer.GetData(2, newPacketLength - 2, out _);

            buffer.readBuffer = oldReadBuffer;
            buffer.reader = oldReader;
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

            var receiver = Players[playerIndex];
            var evt = new PacketSendEvent<TPacket>(ref packet, receiver);
            Kernel.Raise(evt, Log);
            if (evt.IsCanceled()) {
                return;
            }

            // Send the packet. A thread-local send buffer is used in case there is some concurrency.
            //
            // When writing the packet, we need to use the `Server` context since this packet comes from the server.
            var sendBuffer = _sendBuffer.Value;
            var newPacketLength = packet.WriteWithHeader(sendBuffer, PacketContext.Server);

            var terrariaClient = Terraria.Netplay.Clients[playerIndex];
            try {
                terrariaClient.Socket.AsyncSend(sendBuffer, 0, newPacketLength, terrariaClient.ServerWriteCallBack);
            } catch (IOException) { }
        }

        // =============================================================================================================
        // Player event publishers
        //

        [EventHandler("orion-players", Priority = EventPriority.Lowest)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Implicitly used")]
        private void OnPlayerJoinPacket(PacketReceiveEvent<PlayerJoinPacket> evt) {
            var player = evt.Sender;
            var evt2 = new PlayerJoinEvent(player);
            Kernel.Raise(evt2, Log);
            evt.CancellationReason = evt2.CancellationReason;
        }

        [EventHandler("orion-players", Priority = EventPriority.Lowest)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Implicitly used")]
        private void OnPlayerHealthPacket(PacketReceiveEvent<PlayerHealthPacket> evt) {
            var player = evt.Sender;
            ref var packet = ref evt.Packet;
            var evt2 = new PlayerHealthEvent(player, packet.Health, packet.MaxHealth);
            Kernel.Raise(evt2, Log);
            evt.CancellationReason = evt2.CancellationReason;
        }

        [EventHandler("orion-players", Priority = EventPriority.Lowest)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Implicitly used")]
        private void OnPlayerPvpPacket(PacketReceiveEvent<PlayerPvpPacket> evt) {
            var player = evt.Sender;
            ref var packet = ref evt.Packet;
            var evt2 = new PlayerPvpEvent(player, packet.IsInPvp);
            Kernel.Raise(evt2, Log);
            evt.CancellationReason = evt2.CancellationReason;
        }

        [EventHandler("orion-players", Priority = EventPriority.Lowest)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Implicitly used")]
        private void OnPlayerManaPacket(PacketReceiveEvent<PlayerManaPacket> evt) {
            var player = evt.Sender;
            ref var packet = ref evt.Packet;
            var evt2 = new PlayerManaEvent(player, packet.Mana, packet.MaxMana);
            Kernel.Raise(evt2, Log);
            evt.CancellationReason = evt2.CancellationReason;
        }

        [EventHandler("orion-players", Priority = EventPriority.Lowest)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Implicitly used")]
        private void OnPlayerTeamPacket(PacketReceiveEvent<PlayerTeamPacket> evt) {
            var player = evt.Sender;
            ref var packet = ref evt.Packet;
            var evt2 = new PlayerTeamEvent(player, packet.Team);
            Kernel.Raise(evt2, Log);
            evt.CancellationReason = evt2.CancellationReason;
        }

        [EventHandler("orion-players", Priority = EventPriority.Lowest)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Implicitly used")]
        private void OnClientUuidPacket(PacketReceiveEvent<ClientUuidPacket> evt) {
            var player = evt.Sender;
            ref var packet = ref evt.Packet;
            var evt2 = new PlayerUuidEvent(player, packet.Uuid);
            Kernel.Raise(evt2, Log);
            evt.CancellationReason = evt2.CancellationReason;
        }

        [EventHandler("orion-players", Priority = EventPriority.Lowest)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Implicitly used")]
        private void OnChatModule(PacketReceiveEvent<ModulePacket<ChatModule>> evt) {
            var player = evt.Sender;
            ref var module = ref evt.Packet.Module;
            var evt2 = new PlayerChatEvent(player, module.ClientCommand, module.ClientMessage);
            Kernel.Raise(evt2, Log);
            evt.CancellationReason = evt2.CancellationReason;
        }
    }
}
