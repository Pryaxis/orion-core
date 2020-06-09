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
using Moq;
using Orion.Core.DataStructures;
using Orion.Core.Packets;
using Orion.Core.Packets.Server;
using Xunit;

namespace Orion.Core.Players {
    public class PlayerServiceTests {
        private delegate void ChatCallback(ref ServerChatPacket packet);

        [Fact]
        public void BroadcastPacket_Ref_NullPlayerService_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => {
                var packet = new TestPacket();
                PlayerServiceExtensions.BroadcastPacket(null!, ref packet);
            });
        }

        [Fact]
        public void BroadcastPacket_Ref() {
            var mockPlayer = new Mock<IPlayer>();
            var mockPlayers = new Mock<IReadOnlyList<IPlayer>>();
            mockPlayers.Setup(p => p.Count).Returns(1);
            mockPlayers.Setup(p => p[0]).Returns(mockPlayer.Object);
            var mockPlayerService = new Mock<IPlayerService>();
            mockPlayerService.Setup(ps => ps.Players).Returns(mockPlayers.Object);

            var packet = new TestPacket();
            mockPlayerService.Object.BroadcastPacket(ref packet);

            mockPlayer.Verify(p => p.SendPacket(ref It.Ref<TestPacket>.IsAny));
        }

        [Fact]
        public void BroadcastPacket_NullPlayerService_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(
                () => PlayerServiceExtensions.BroadcastPacket(null!, new TestPacket()));
        }

        [Fact]
        public void BroadcastPacket() {
            var mockPlayer = new Mock<IPlayer>();
            var mockPlayers = new Mock<IReadOnlyList<IPlayer>>();
            mockPlayers.Setup(p => p.Count).Returns(1);
            mockPlayers.Setup(p => p[0]).Returns(mockPlayer.Object);
            var mockPlayerService = new Mock<IPlayerService>();
            mockPlayerService.Setup(ps => ps.Players).Returns(mockPlayers.Object);

            var packet = new TestPacket();
            mockPlayerService.Object.BroadcastPacket(packet);

            mockPlayer.Verify(p => p.SendPacket(ref It.Ref<TestPacket>.IsAny));
        }

        [Fact]
        public void BroadcastMessage_NullPlayerService_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(
                () => PlayerServiceExtensions.BroadcastMessage(null!, "test", Color3.White));
        }

        [Fact]
        public void BroadcastMessage_NullMessage_ThrowsArgumentNullException() {
            var playerService = new Mock<IPlayerService>().Object;

            Assert.Throws<ArgumentNullException>(
                () => PlayerServiceExtensions.BroadcastMessage(playerService, null!, Color3.White));
        }

        [Fact]
        public void BroadcastMessage() {
            var mockPlayer = new Mock<IPlayer>();
            mockPlayer
                .Setup(p => p.SendPacket(ref It.Ref<ServerChatPacket>.IsAny))
                .Callback((ChatCallback)((ref ServerChatPacket packet) => {
                    Assert.Equal("test", packet.Message);
                    Assert.Equal(Color3.White, packet.Color);
                    Assert.Equal(-1, packet.LineWidth);
                }));
            var mockPlayers = new Mock<IReadOnlyList<IPlayer>>();
            mockPlayers.Setup(p => p.Count).Returns(1);
            mockPlayers.Setup(p => p[0]).Returns(mockPlayer.Object);
            var mockPlayerService = new Mock<IPlayerService>();
            mockPlayerService.Setup(ps => ps.Players).Returns(mockPlayers.Object);

            mockPlayerService.Object.BroadcastMessage("test", Color3.White);

            mockPlayer.Verify(p => p.SendPacket(ref It.Ref<ServerChatPacket>.IsAny));
        }

        private struct TestPacket : IPacket {
            public PacketId Id => throw new NotImplementedException();
            public int Read(Span<byte> span, PacketContext context) => throw new NotImplementedException();
            public int Write(Span<byte> span, PacketContext context) => throw new NotImplementedException();
        }
    }
}
