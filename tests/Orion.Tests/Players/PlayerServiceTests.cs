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
using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Moq;
using Orion.Packets;
using Orion.Packets.World;
using Orion.Utils;
using Xunit;

namespace Orion.Players {
    public class PlayerServiceTests {
        [Fact]
        public void BroadcastPacket() {
            var mockPlayer = new Mock<IPlayer>();
            var mockPlayers = new Mock<IReadOnlyArray<IPlayer>>();
            mockPlayers.SetupGet(p => p.Count).Returns(1);
            mockPlayers.SetupGet(p => p[0]).Returns(mockPlayer.Object);
            var mockPlayerService = new Mock<IPlayerService>();
            mockPlayerService.SetupGet(ps => ps.Players).Returns(mockPlayers.Object);

            var packet = new TestPacket();
            mockPlayerService.Object.BroadcastPacket(packet);

            mockPlayerService.VerifyGet(ps => ps.Players);
            mockPlayers.VerifyGet(p => p.Count, Times.AtLeastOnce());
            mockPlayers.VerifyGet(p => p[0]);
            mockPlayer.Verify(p => p.SendPacket(packet));
            mockPlayerService.VerifyNoOtherCalls();
        }

        [Fact]
        public void BroadcastPacket_NullPlayerService_ThrowsArgumentNullException() {
            var packet = new TestPacket();
            Action action = () => PlayerServiceExtensions.BroadcastPacket(null, packet);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void BroadcastPacket_NullPacket_ThrowsArgumentNullException() {
            var playerService = new Mock<IPlayerService>().Object;
            Action action = () => playerService.BroadcastPacket(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void BroadcastMessage() {
            var mockPlayer = new Mock<IPlayer>();
            var mockPlayers = new Mock<IReadOnlyArray<IPlayer>>();
            mockPlayers.SetupGet(p => p.Count).Returns(1);
            mockPlayers.SetupGet(p => p[0]).Returns(mockPlayer.Object);
            var mockPlayerService = new Mock<IPlayerService>();
            mockPlayerService.SetupGet(ps => ps.Players).Returns(mockPlayers.Object);

            mockPlayerService.Object.BroadcastMessage("test", Color.White);

            mockPlayerService.VerifyGet(ps => ps.Players);
            mockPlayers.VerifyGet(p => p.Count, Times.AtLeastOnce());
            mockPlayers.VerifyGet(p => p[0]);
            mockPlayer.Verify(p => p.SendPacket(
                It.Is<ChatPacket>(cp => cp.ChatColor == Color.White && cp.ChatText == "test")));
            mockPlayerService.VerifyNoOtherCalls();
        }

        [Fact]
        public void BroadcastMessage_NullPlayerService_ThrowsArgumentNullException() {
            Action action = () => PlayerServiceExtensions.BroadcastMessage(null, "", Color.White);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void BroadcastMessage_NullPacket_ThrowsArgumentNullException() {
            var playerService = new Mock<IPlayerService>().Object;
            Action action = () => playerService.BroadcastMessage(null, Color.White);

            action.Should().Throw<ArgumentNullException>();
        }

        private class TestPacket : Packet {
            public override PacketType Type => throw new NotImplementedException();

            private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
                throw new NotImplementedException();

            private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
                throw new NotImplementedException();
        }
    }
}
