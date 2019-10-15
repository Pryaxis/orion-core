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
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
using Orion.Events;
using Orion.Events.Players;
using Orion.Packets.World;
using Serilog.Core;
using Terraria.Net;
using Terraria.Net.Sockets;
using Xunit;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    [Collection("TerrariaTestsCollection")]
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionPlayerTests {
        [Fact]
        public void Name_Get() {
            var mockPlayerService = new Mock<IPlayerService>();
            var terrariaPlayer = new TerrariaPlayer { name = "test" };
            var player = new OrionPlayer(mockPlayerService.Object, terrariaPlayer);

            player.Name.Should().Be("test");
        }

        [Fact]
        public void Name_Set() {
            var mockPlayerService = new Mock<IPlayerService>();
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(mockPlayerService.Object, terrariaPlayer);

            player.Name = "test";

            terrariaPlayer.name.Should().Be("test");
        }

        [Fact]
        public void Name_Set_NullValue_ThrowsArgumentNullException() {
            var mockPlayerService = new Mock<IPlayerService>();
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(mockPlayerService.Object, terrariaPlayer);
            Action action = () => player.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Team_Get() {
            var mockPlayerService = new Mock<IPlayerService>();
            var terrariaPlayer = new TerrariaPlayer { team = (int)PlayerTeam.Red };
            var player = new OrionPlayer(mockPlayerService.Object, terrariaPlayer);

            player.Team.Should().Be(PlayerTeam.Red);
        }

        [Fact]
        public void Team_Set() {
            var mockPlayerService = new Mock<IPlayerService>();
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(mockPlayerService.Object, terrariaPlayer);

            player.Team = PlayerTeam.Red;

            terrariaPlayer.team.Should().Be((int)PlayerTeam.Red);
        }

        [Fact]
        public void Stats_Get() {
            var mockPlayerService = new Mock<IPlayerService>();
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(mockPlayerService.Object, terrariaPlayer);

            player.Stats.Should().NotBeNull();
        }
        
        [Fact]
        public void Inventory_Get() {
            var mockPlayerService = new Mock<IPlayerService>();
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(mockPlayerService.Object, terrariaPlayer);

            player.Inventory.Should().NotBeNull();
        }

        [Fact]
        public void SendPacket() {
            var socket = new TestSocket { Connected = true };
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient {
                Id = 5,
                Socket = socket
            };
            var mockPlayerService = new Mock<IPlayerService>();
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(mockPlayerService.Object, 5, terrariaPlayer);
            var packet = new ChatPacket { ChatText = "test" };

            var isRun = false;
            var packetSend = new EventHandlerCollection<PacketSendEventArgs>();
            packetSend.RegisterHandler((sender, args) => {
                isRun = true;
                args.Receiver.Should().BeSameAs(player);
                args.Packet.Should().BeEquivalentTo(packet);
            });
            mockPlayerService.Setup(ps => ps.PacketSend).Returns(packetSend);

            player.SendPacket(packet);

            socket.SendData.Should().BeEquivalentTo(14, 0, 107, 0, 0, 0, 0, 4, 116, 101, 115, 116, 0, 0);
            isRun.Should().BeTrue();
        }

        [Fact]
        public void SendPacket_NotConnected() {
            var socket = new TestSocket { Connected = false };
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient {
                Id = 5,
                Socket = socket
            };
            var mockPlayerService = new Mock<IPlayerService>();
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(mockPlayerService.Object, 5, terrariaPlayer);
            var packet = new ChatPacket { ChatText = "test" };

            player.SendPacket(packet);

            socket.SendData.Should().BeNull();
        }

        [Fact]
        public void SendPacket_Canceled() {
            var socket = new TestSocket { Connected = true };
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient {
                Id = 5,
                Socket = socket
            };
            var mockPlayerService = new Mock<IPlayerService>();
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(mockPlayerService.Object, 5, terrariaPlayer);
            var packet = new ChatPacket { ChatText = "test" };

            var packetSend = new EventHandlerCollection<PacketSendEventArgs>();
            packetSend.RegisterHandler((sender, args) => args.Cancel());
            mockPlayerService.Setup(ps => ps.PacketSend).Returns(packetSend);

            player.SendPacket(packet);
            
            socket.SendData.Should().BeNull();
        }

        [Fact]
        public void SendPacket_NullPacket_ThrowsArgumentNullException() {
            var mockPlayerService = new Mock<IPlayerService>();
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(mockPlayerService.Object, terrariaPlayer);
            Action action = () => player.SendPacket(null);

            action.Should().Throw<ArgumentNullException>();
        }

        private class TestSocket : ISocket {
            public bool Connected { get; set; }
            public byte[] SendData { get; private set; }

            public void AsyncReceive(byte[] data, int offset, int size, SocketReceiveCallback callback,
                object state = null) => throw new NotImplementedException();

            public void AsyncSend(byte[] data, int offset, int size, SocketSendCallback callback,
                object state = null) => SendData = data[offset..(offset + size)];

            public void Close() => throw new NotImplementedException();
            public void Connect(RemoteAddress address) => throw new NotImplementedException();
            public RemoteAddress GetRemoteAddress() => throw new NotImplementedException();
            public bool IsConnected() => Connected;
            public bool IsDataAvailable() => throw new NotImplementedException();
            public void SendQueuedPackets() => throw new NotImplementedException();
            public bool StartListening(SocketConnectionAccepted callback) => throw new NotImplementedException();
            public void StopListening() => throw new NotImplementedException();
        }
    }
}
