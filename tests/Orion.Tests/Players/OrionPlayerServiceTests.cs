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
using System.Linq;
using Orion.Events;
using Orion.Events.Packets;
using Orion.Events.Players;
using Orion.Packets;
using Orion.Packets.Players;
using Orion.Packets.Server;
using Serilog.Core;
using Xunit;

namespace Orion.Players {
    [Collection("TerrariaTestsCollection")]
    public class OrionPlayerServiceTests {
        private static readonly byte[] ServerConnectBytes;

        static OrionPlayerServiceTests() {
            var bytes = new byte[100];
            var span = bytes.AsSpan();
            var packet = new ServerConnectPacket { Version = "Terraria" + Terraria.Main.curRelease };
            packet.WriteWithHeader(ref span, PacketContext.Client);

            var packetLength = bytes.Length - span.Length;
            ServerConnectBytes = bytes[..packetLength];
        }

        [Fact]
        public void Players_Item_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);

            var player = playerService.Players[1];

            Assert.Equal(1, player.Index);
            Assert.Equal(Terraria.Main.player[1], ((OrionPlayer)player).Wrapped);
        }

        [Fact]
        public void Players_Item_GetMultipleTimes_ReturnsSameInstance() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);

            var player = playerService.Players[0];
            var player2 = playerService.Players[0];

            Assert.Same(player2, player);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Players_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);

            Assert.Throws<IndexOutOfRangeException>(() => playerService.Players[index]);
        }

        [Fact]
        public void Players_GetEnumerator() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);

            var players = playerService.Players.ToList();

            for (var i = 0; i < players.Count; ++i) {
                Assert.Equal(Terraria.Main.player[i], ((OrionPlayer)players[i]).Wrapped);
            }
        }

        [Fact]
        public void PacketReceive_EventTriggered() {
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PacketReceiveEvent<ServerConnectPacket>>(evt => {
                isRun = true;
                Assert.Same(playerService.Players[5], evt.Sender);
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, ServerConnectBytes);

            Assert.True(isRun);
            Assert.Equal(1, Terraria.Netplay.Clients[5].State);
        }

        [Fact]
        public void PacketReceive_EventCanceled() {
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PacketReceiveEvent<ServerConnectPacket>>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, ServerConnectBytes);

            Assert.Equal(0, Terraria.Netplay.Clients[5].State);
        }

        [Fact]
        public void PacketReceive_PlayerPvpEventTriggered() {
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerPvpEvent>(evt => {
                isRun = true;
                Assert.Same(playerService.Players[5], evt.Player);
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerPvpPacketTests.Bytes);

            Assert.True(isRun);
        }

        [Fact]
        public void PacketReceive_PlayerPvpEventCanceled() {
            // Set `State` to 10 so that the PvP packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient {
                Id = 5,
                State = 10
            };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PlayerPvpEvent>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerPvpPacketTests.Bytes);

            Assert.False(Terraria.Main.player[5].hostile);
        }

        [Fact]
        public void PacketSend_EventTriggered() {
            var socket = new TestSocket { Connected = true };
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient {
                Id = 5,
                Socket = socket
            };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PacketSendEvent<ServerConnectPacket>>(evt => {
                isRun = true;
                Assert.Same(playerService.Players[5], evt.Receiver);
                Assert.Equal("Terraria" + Terraria.Main.curRelease, evt.Packet.Version);
            }, Logger.None);

            Terraria.NetMessage.SendData((byte)PacketId.ServerConnect, 5);

            Assert.True(isRun);
            Assert.Equal(ServerConnectBytes, socket.SendData);
        }

        [Fact]
        public void PacketSend_EventCanceled() {
            var socket = new TestSocket { Connected = true };
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient {
                Id = 5,
                Socket = socket
            };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PacketSendEvent<ServerConnectPacket>>(evt => evt.Cancel(), Logger.None);

            Terraria.NetMessage.SendData((byte)PacketId.ServerConnect, 5);

            Assert.Empty(socket.SendData);
        }

        private class TestSocket : Terraria.Net.Sockets.ISocket {
            public bool Connected { get; set; }
            public byte[] SendData { get; private set; } = Array.Empty<byte>();

            public void AsyncReceive(
                byte[] data, int offset, int size, Terraria.Net.Sockets.SocketReceiveCallback callback,
                object? state = null) =>
                    throw new NotImplementedException();
            public void AsyncSend(
                byte[] data, int offset, int size, Terraria.Net.Sockets.SocketSendCallback callback,
                object? state = null) =>
                    SendData = data[offset..(offset + size)];
            public void Close() => throw new NotImplementedException();
            public void Connect(Terraria.Net.RemoteAddress address) => throw new NotImplementedException();
            public Terraria.Net.RemoteAddress GetRemoteAddress() => throw new NotImplementedException();
            public bool IsConnected() => Connected;
            public bool IsDataAvailable() => throw new NotImplementedException();
            public void SendQueuedPackets() => throw new NotImplementedException();
            public bool StartListening(Terraria.Net.Sockets.SocketConnectionAccepted callback) =>
                throw new NotImplementedException();
            public void StopListening() => throw new NotImplementedException();
        }
    }
}
