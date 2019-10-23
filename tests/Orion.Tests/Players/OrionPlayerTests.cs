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
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new TerrariaPlayer { name = "test" };
            var player = new OrionPlayer(playerService, terrariaPlayer);

            player.Name.Should().Be("test");
        }

        [Fact]
        public void Name_Set() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(playerService, terrariaPlayer);

            player.Name = "test";

            terrariaPlayer.name.Should().Be("test");
        }

        [Fact]
        public void Name_SetNullValue_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(playerService, terrariaPlayer);
            Action action = () => player.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Team_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new TerrariaPlayer { team = (int)PlayerTeam.Red };
            var player = new OrionPlayer(playerService, terrariaPlayer);

            player.Team.Should().Be(PlayerTeam.Red);
        }

        [Fact]
        public void Team_Set() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(playerService, terrariaPlayer);

            player.Team = PlayerTeam.Red;

            terrariaPlayer.team.Should().Be((int)PlayerTeam.Red);
        }

        [Fact]
        public void Stats_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(playerService, terrariaPlayer);

            player.Stats.Should().NotBeNull();
        }

        [Fact]
        public void Inventory_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(playerService, terrariaPlayer);

            player.Inventory.Should().NotBeNull();
        }

        [Fact]
        public void SendPacket() {
            var socket = new TestSocket { Connected = true };
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient {
                Id = 5,
                Socket = socket
            };
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(playerService, 5, terrariaPlayer);
            var packet = new ChatPacket { Text = "test" };

            var isRun = false;
            kernel.RegisterHandler<PacketSendEvent>(e => {
                isRun = true;
                e.Receiver.Should().BeSameAs(player);
                e.Packet.Should().BeEquivalentTo(packet);
            });

            player.SendPacket(packet);

            socket.SendData.Should().BeEquivalentTo(14, 0, 107, 0, 0, 0, 0, 4, 116, 101, 115, 116, 255, 255);
            isRun.Should().BeTrue();
        }

        [Fact]
        public void SendPacket_NotConnected() {
            var socket = new TestSocket { Connected = false };
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient {
                Id = 5,
                Socket = socket
            };
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(playerService, 5, terrariaPlayer);
            var packet = new ChatPacket { Text = "test" };

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
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(playerService, 5, terrariaPlayer);
            var packet = new ChatPacket { Text = "test" };
            kernel.RegisterHandler<PacketSendEvent>(e => e.Cancel());

            player.SendPacket(packet);

            socket.SendData.Should().BeNull();
        }

        [Fact]
        public void SendPacket_NullPacket_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new TerrariaPlayer();
            var player = new OrionPlayer(playerService, terrariaPlayer);
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
