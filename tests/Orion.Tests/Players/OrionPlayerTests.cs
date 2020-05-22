﻿// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using Orion.Events;
using Orion.Events.Packets;
using Orion.Packets;
using Serilog.Core;
using Xunit;

namespace Orion.Players {
    [Collection("TerrariaTestsCollection")]
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionPlayerTests {
        [Fact]
        public void Name_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new Terraria.Player { name = "test" };
            var player = new OrionPlayer(terrariaPlayer, playerService);

            Assert.Equal("test", player.Name);
        }

        [Fact]
        public void Name_SetNullValue_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer, playerService);

            Assert.Throws<ArgumentNullException>(() => player.Name = null!);
        }

        [Fact]
        public void Name_Set() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer, playerService);

            player.Name = "test";

            Assert.Equal("test", terrariaPlayer.name);
        }

        [Fact]
        public void IsInPvp_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new Terraria.Player { hostile = true };
            var player = new OrionPlayer(terrariaPlayer, playerService);

            Assert.True(player.IsInPvp);
        }

        [Fact]
        public void IsInPvp_Set() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer, playerService);

            player.IsInPvp = true;

            Assert.True(terrariaPlayer.hostile);
        }

        [Fact]
        public void Team_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new Terraria.Player { team = 1 };
            var player = new OrionPlayer(terrariaPlayer, playerService);

            Assert.Equal(PlayerTeam.Red, player.Team);
        }

        [Fact]
        public void Team_Set() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer, playerService);

            player.Team = PlayerTeam.Red;

            Assert.Equal(1, terrariaPlayer.team);
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
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(5, terrariaPlayer, playerService);

            var packet = new TestPacket();
            player.SendPacket(ref packet);

            Assert.Empty(socket.SendData);
        }

        [Fact]
        public void SendPacket_EventCanceled() {
            var socket = new TestSocket { Connected = true };
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient {
                Id = 5,
                Socket = socket
            };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(5, terrariaPlayer, playerService);
            kernel.RegisterHandler<PacketSendEvent<TestPacket>>(evt => evt.Cancel(), Logger.None);

            var packet = new TestPacket();
            player.SendPacket(ref packet);

            Assert.Empty(socket.SendData);
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
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(5, terrariaPlayer, playerService);

            var packet = new TestPacket { Value = 100 };
            player.SendPacket(ref packet);

            Assert.Equal(new byte[] { 4, 0, 255, 100 }, socket.SendData);
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

        private struct TestPacket : IPacket {
            public PacketId Id => (PacketId)255;

            public byte Value;

            public void Read(ReadOnlySpan<byte> span, PacketContext context) => throw new NotImplementedException();

            public void Write(ref Span<byte> span, PacketContext context) {
                span[0] = Value;
                span = span[1..];
            }
        }
    }
}
