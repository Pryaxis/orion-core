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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Orion.Collections;
using Orion.Entities;
using Orion.Events;
using Orion.Events.Packets;
using Orion.Events.Players;
using Orion.Packets;
using Orion.Packets.Client;
using Orion.Packets.Modules;
using Orion.Packets.Players;
using Serilog;

namespace Orion.Players {
    [Service("orion-players")]
    internal sealed class OrionPlayerService : OrionService, IPlayerService {
        private delegate void OnReceivePacketHandler(Terraria.MessageBuffer buffer, Span<byte> span);
        private delegate void OnSendPacketHandler(int playerIndex, Span<byte> span);

        private static readonly MethodInfo ReceivePacketMethod =
            typeof(OrionPlayerService)
                .GetMethod(nameof(OnReceivePacket), BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly MethodInfo SendPacketMethod =
            typeof(OrionPlayerService).GetMethod(nameof(OnSendPacket), BindingFlags.NonPublic | BindingFlags.Instance);

        private readonly ThreadLocal<bool> _ignoreReceiveDataHandler = new ThreadLocal<bool>();
        private readonly OnReceivePacketHandler?[] _onReceivePacketHandlers = new OnReceivePacketHandler?[256];
        private readonly OnReceivePacketHandler?[] _onReceiveModuleHandlers = new OnReceivePacketHandler?[65536];
        private readonly ThreadLocal<byte[]> _receiveBuffer =
            new ThreadLocal<byte[]>(() => new byte[ushort.MaxValue]);

        private readonly OnSendPacketHandler?[] _onSendPacketHandlers = new OnSendPacketHandler?[256];
        private readonly OnSendPacketHandler?[] _onSendModuleHandlers = new OnSendPacketHandler?[65536];
        private readonly ThreadLocal<byte[]> _sendBuffer =
            new ThreadLocal<byte[]>(() => new byte[ushort.MaxValue]);

        public IReadOnlyArray<IPlayer> Players { get; }

        public OrionPlayerService(OrionKernel kernel, ILogger log) : base(kernel, log) {
            Debug.Assert(kernel != null);
            Debug.Assert(log != null);

            // Construct the `Players` array. Note that the last player should be ignored, as it is not a real player.
            Players = new WrappedReadOnlyArray<OrionPlayer, Terraria.Player>(
                Terraria.Main.player.AsMemory(..^1),
                (playerIndex, terrariaPlayer) => new OrionPlayer(playerIndex, terrariaPlayer, this));

            // Construct the `_onReceivePacketHandlers` and `_onSendPacketHandlers` arrays ahead of time.
            for (var i = 1; i < 140; ++i) {
                var packetType = ((PacketId)i).Type();
                _onReceivePacketHandlers[i] =
                    (OnReceivePacketHandler)ReceivePacketMethod
                        .MakeGenericMethod(packetType)
                        .CreateDelegate(typeof(OnReceivePacketHandler), this);
                _onSendPacketHandlers[i] =
                    (OnSendPacketHandler)SendPacketMethod
                        .MakeGenericMethod(packetType)
                        .CreateDelegate(typeof(OnSendPacketHandler), this);
            }

            // Construct the `_onReceiveModuleHandlers` and `_onSendModuleHandlers` arrays ahead of time.
            for (var i = 0; i < 11; ++i) {
                var moduleType = ((ModuleId)i).Type();
                var packetType = typeof(ModulePacket<>).MakeGenericType(moduleType);
                _onReceiveModuleHandlers[i] =
                    (OnReceivePacketHandler)ReceivePacketMethod
                        .MakeGenericMethod(packetType)
                        .CreateDelegate(typeof(OnReceivePacketHandler), this);
                _onSendModuleHandlers[i] =
                    (OnSendPacketHandler)SendPacketMethod
                        .MakeGenericMethod(packetType)
                        .CreateDelegate(typeof(OnSendPacketHandler), this);
            }

            OTAPI.Hooks.Net.ReceiveData = ReceiveDataHandler;
            OTAPI.Hooks.Net.SendBytes = SendBytesHandler;
            OTAPI.Hooks.Net.RemoteClient.PreReset = PreResetHandler;
        }

        public override void Dispose() {
            _ignoreReceiveDataHandler.Dispose();

            OTAPI.Hooks.Net.ReceiveData = null;
            OTAPI.Hooks.Net.SendBytes = null;
            OTAPI.Hooks.Net.RemoteClient.PreReset = null;
        }

        private OTAPI.HookResult ReceiveDataHandler(
                Terraria.MessageBuffer buffer, ref byte packetId, ref int _, ref int start, ref int length) {
            Debug.Assert(buffer != null);
            Debug.Assert(buffer.whoAmI >= 0 && buffer.whoAmI < Players.Count);
            Debug.Assert(start >= 0 && start + length <= buffer.readBuffer.Length);

            // Check `_ignoreReceiveDataHandler` to prevent an infinite loop if `GetData()` is called in
            // `ReceivePacket`. A thread-local value is used in case there is some concurrency.
            if (_ignoreReceiveDataHandler.Value) {
                _ignoreReceiveDataHandler.Value = false;
                return OTAPI.HookResult.Continue;
            }

            var span = buffer.readBuffer.AsSpan(start..(start + length));
            if (packetId == (byte)PacketId.Module) {
                var moduleId = Unsafe.ReadUnaligned<ushort>(ref buffer.readBuffer[start + 1]);
                _onReceiveModuleHandlers[moduleId]?.Invoke(buffer, span);
            } else {
                _onReceivePacketHandlers[packetId]?.Invoke(buffer, span);
            }
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

            // While this may seem inefficient, these typeof comparisons get optimized in each reified generic method by
            // the JIT.
            if (typeof(TPacket) == typeof(PlayerJoinPacket)) {
                var evt = new PlayerJoinEvent(player);
                Kernel.Raise(evt, Log);
                return evt.IsCanceled();
            } else if (typeof(TPacket) == typeof(PlayerPvpPacket)) {
                var evt = new PlayerPvpEvent(player, ref Unsafe.As<TPacket, PlayerPvpPacket>(ref packet));
                Kernel.Raise(evt, Log);
                return evt.IsCanceled();
            } else if (typeof(TPacket) == typeof(PlayerTeamPacket)) {
                var evt = new PlayerTeamEvent(player, ref Unsafe.As<TPacket, PlayerTeamPacket>(ref packet));
                Kernel.Raise(evt, Log);
                return evt.IsCanceled();
            } else if (typeof(TPacket) == typeof(ClientUuidPacket)) {
                var evt = new PlayerUuidEvent(player, ref Unsafe.As<TPacket, ClientUuidPacket>(ref packet));
                Kernel.Raise(evt, Log);
                return evt.IsCanceled();
            } else if (typeof(TPacket) == typeof(ModulePacket<ChatModule>)) {
                var evt = new PlayerChatEvent(player, ref Unsafe.As<TPacket, ModulePacket<ChatModule>>(ref packet));
                Kernel.Raise(evt, Log);
                return evt.IsCanceled();
            } else {
                return false;
            }
        }

        private void OnReceiveModule<TModule>(Terraria.MessageBuffer buffer, Span<byte> span) {
            Debug.Assert(buffer != null);
            Debug.Assert(span.Length > 0);
        }

        private OTAPI.HookResult SendBytesHandler(
                ref int playerIndex, ref byte[] data, ref int offset, ref int size,
                ref Terraria.Net.Sockets.SocketSendCallback _, ref object _2) {
            Debug.Assert(playerIndex >= 0 && playerIndex < Players.Count);
            Debug.Assert(data != null);
            Debug.Assert(offset >= 0 && offset + size <= data.Length);

            var span = data.AsSpan((offset + 2)..(offset + size));
            var packetId = data[offset + 2];
            if (packetId == (byte)PacketId.Module) {
                var moduleId = Unsafe.ReadUnaligned<ushort>(ref data[offset + 3]);
                _onSendModuleHandlers[moduleId]?.Invoke(playerIndex, span);
            } else {
                _onSendPacketHandlers[packetId]?.Invoke(playerIndex, span);
            }
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
    }
}
