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
using Orion.Packets.Players;
using Orion.Packets.Server;
using Serilog;

namespace Orion.Players {
    [Service("orion-players")]
    internal sealed class OrionPlayerService : OrionService, IPlayerService {
        private delegate void ReceivePacketHandler(Terraria.MessageBuffer buffer, ReadOnlySpan<byte> span);
        private delegate void SendPacketHandler(int playerIndex, ReadOnlySpan<byte> span);

        private static readonly MethodInfo ReceivePacketMethod =
            typeof(OrionPlayerService).GetMethod(nameof(ReceivePacket), BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly MethodInfo SendPacketMethod =
            typeof(OrionPlayerService).GetMethod(nameof(SendPacket), BindingFlags.NonPublic | BindingFlags.Instance);

        private readonly ThreadLocal<bool> _ignoreReceiveDataHandler = new ThreadLocal<bool>();
        private readonly ReceivePacketHandler[] _receivePacketHandlers = new ReceivePacketHandler[256];
        private readonly ThreadLocal<byte[]> _receiveBuffer =
            new ThreadLocal<byte[]>(() => new byte[ushort.MaxValue]);

        private readonly SendPacketHandler[] _sendPacketHandlers = new SendPacketHandler[256];
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

            // Construct the `_receivePacketHandlers` and `_sendPacketHandlers` arrays ahead of time.
            for (var i = 0; i < 256; ++i) {
                var packetType = ((PacketId)i).Type();
                var receivePacketMethod = ReceivePacketMethod.MakeGenericMethod(packetType);
                _receivePacketHandlers[i] =
                    (ReceivePacketHandler)receivePacketMethod.CreateDelegate(typeof(ReceivePacketHandler), this);

                var sendPacketMethod = SendPacketMethod.MakeGenericMethod(packetType);
                _sendPacketHandlers[i] =
                    (SendPacketHandler)sendPacketMethod.CreateDelegate(typeof(SendPacketHandler), this);
            }

            OTAPI.Hooks.Net.ReceiveData = ReceiveDataHandler;
            OTAPI.Hooks.Net.SendBytes = SendBytesHandler;
        }

        public override void Dispose() {
            _ignoreReceiveDataHandler.Dispose();

            OTAPI.Hooks.Net.ReceiveData = null;
            OTAPI.Hooks.Net.SendBytes = null;
        }

        private OTAPI.HookResult ReceiveDataHandler(
                Terraria.MessageBuffer buffer, ref byte packetId, ref int _, ref int start, ref int length) {
            Debug.Assert(buffer != null);
            Debug.Assert(buffer.whoAmI >= 0 && buffer.whoAmI < Players.Count);
            Debug.Assert(start >= 0 && start + length <= buffer.readBuffer.Length);

            // Check `_ignoreReceiveDataHandler` to prevent an infinite loop if `GetData()` is called in
            // `ReceivePacket`.
            if (_ignoreReceiveDataHandler.Value) {
                _ignoreReceiveDataHandler.Value = false;
                return OTAPI.HookResult.Continue;
            }

            _receivePacketHandlers[packetId](buffer, buffer.readBuffer.AsSpan(start..(start + length)));
            return OTAPI.HookResult.Cancel;
        }

        private void ReceivePacket<TPacket>(Terraria.MessageBuffer buffer, ReadOnlySpan<byte> span)
                where TPacket : struct, IPacket {
            // When reading the packet, we need to use the `Server` context since this packet should be read as the
            // server. Ignore the first byte as it is the packet ID.
            var packet = new TPacket();
            packet.Read(span[1..], PacketContext.Server);

            // If `TPacket` is `UnknownPacket`, then we need to set the `Id` property appropriately.
            if (typeof(TPacket) == typeof(UnknownPacket)) {
                Unsafe.As<TPacket, UnknownPacket>(ref packet).Id = (PacketId)span[0];
            }

            var player = Players[buffer.whoAmI];
            var evt = new PacketReceiveEvent<TPacket>(ref packet, player);
            Kernel.Raise(evt, Log);
            if (evt.IsCanceled()) {
                return;
            } else if (ReceivePacketEvent(player, ref evt.Packet)) {
                return;
            }

            // To simulate the receival of the packet, we must swap out the read buffer and reader, and call `GetData()`
            // while ensuring that the next `ReceiveDataHandler()` call is ignored. A thread-local receive buffer is
            // used in case there is some concurrency.
            var oldReadBuffer = buffer.readBuffer;
            var oldReader = buffer.reader;

            // When writing the packet, we need to use the `Client` context since this packet comes from the client.
            var receiveSpan = _receiveBuffer.Value.AsSpan();
            evt.Packet.WriteWithHeader(ref receiveSpan, PacketContext.Client);
            var packetLength = _receiveBuffer.Value.Length - receiveSpan.Length;

            _ignoreReceiveDataHandler.Value = true;
            buffer.readBuffer = _receiveBuffer.Value;
            buffer.reader = new BinaryReader(new MemoryStream(buffer.readBuffer), Encoding.UTF8);
            buffer.GetData(sizeof(ushort), packetLength - sizeof(ushort), out _);

            buffer.readBuffer = oldReadBuffer;
            buffer.reader = oldReader;
        }

        private bool ReceivePacketEvent<TPacket>(IPlayer player, ref TPacket packet) where TPacket : struct, IPacket {
            // While this may seem inefficient, these typeof comparisons get optimized in each reified generic method by
            // the JIT.
            if (typeof(TPacket) == typeof(PlayerPvpPacket)) {
                var evt = new PlayerPvpEvent(player, ref Unsafe.As<TPacket, PlayerPvpPacket>(ref packet));
                Kernel.Raise(evt, Log);
                return evt.IsCanceled();
            } else {
                return false;
            }
        }

        private OTAPI.HookResult SendBytesHandler(
                ref int remoteClient, ref byte[] data, ref int offset, ref int size,
                ref Terraria.Net.Sockets.SocketSendCallback _, ref object _2) {
            Debug.Assert(remoteClient >= 0 && remoteClient < Players.Count);
            Debug.Assert(data != null);
            Debug.Assert(offset >= 0 && offset + size <= data.Length);

            var packetId = data[offset + sizeof(ushort)];
            _sendPacketHandlers[packetId](remoteClient, data.AsSpan((offset + sizeof(ushort))..(offset + size)));
            return OTAPI.HookResult.Cancel;
        }

        private void SendPacket<TPacket>(int playerIndex, ReadOnlySpan<byte> span) where TPacket : struct, IPacket {
            // When reading the packet, we need to use the `Client` context since this packet should be read as the
            // client. Ignore the first byte as it is the packet ID.
            var packet = new TPacket();
            packet.Read(span[1..], PacketContext.Client);

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
            var sendSpan = _sendBuffer.Value.AsSpan();
            evt.Packet.WriteWithHeader(ref sendSpan, PacketContext.Server);
            var packetLength = _sendBuffer.Value.Length - sendSpan.Length;

            var terrariaClient = Terraria.Netplay.Clients[playerIndex];
            terrariaClient.Socket.AsyncSend(_sendBuffer.Value, 0, packetLength, terrariaClient.ServerWriteCallBack);
        }
    }
}
