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
using Orion.Core.Packets.World.Tiles;
using Orion.Core.World.Tiles;
using Xunit;

namespace Orion.Core.Players {
    public class PlayerServiceTests {
        private delegate void ServerChatCallback(ref ServerChatPacket packet);
        private delegate void TileSquareCallback(ref TileSquarePacket packet);

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
            var playerService = Mock.Of<IPlayerService>(
                ps => ps.Players == Mock.Of<IReadOnlyList<IPlayer>>(p => p.Count == 1 && p[0] == mockPlayer.Object));

            var packet = new TestPacket();
            playerService.BroadcastPacket(ref packet);

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
            var playerService = Mock.Of<IPlayerService>(
                ps => ps.Players == Mock.Of<IReadOnlyList<IPlayer>>(p => p.Count == 1 && p[0] == mockPlayer.Object));

            var packet = new TestPacket();
            playerService.BroadcastPacket(packet);

            mockPlayer.Verify(p => p.SendPacket(ref It.Ref<TestPacket>.IsAny));
        }

        [Fact]
        public void BroadcastMessage_NullPlayerService_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(
                () => PlayerServiceExtensions.BroadcastMessage(null!, "test", Color3.White));
        }

        [Fact]
        public void BroadcastMessage_NullMessage_ThrowsArgumentNullException() {
            var playerService = Mock.Of<IPlayerService>();

            Assert.Throws<ArgumentNullException>(() => playerService.BroadcastMessage(null!, Color3.White));
        }

        [Fact]
        public void BroadcastMessage() {
            var mockPlayer = new Mock<IPlayer>();
            mockPlayer
                .Setup(p => p.SendPacket(ref It.Ref<ServerChatPacket>.IsAny))
                .Callback((ServerChatCallback)((ref ServerChatPacket packet) => {
                    Assert.Equal("test", packet.Message);
                    Assert.Equal(Color3.White, packet.Color);
                    Assert.Equal(-1, packet.LineWidth);
                }));
            var playerService = Mock.Of<IPlayerService>(
                ps => ps.Players == Mock.Of<IReadOnlyList<IPlayer>>(p => p.Count == 1 && p[0] == mockPlayer.Object));

            playerService.BroadcastMessage("test", Color3.White);

            mockPlayer.Verify(p => p.SendPacket(ref It.Ref<ServerChatPacket>.IsAny));
        }

        [Fact]
        public void BroadcastTiles_NullPlayerService_ThrowsArgumentNullException() {
            var tiles = Mock.Of<ITileSlice>();

            Assert.Throws<ArgumentNullException>(() => PlayerServiceExtensions.BroadcastTiles(null!, 123, 456, tiles));
        }

        [Fact]
        public void BroadcastTiles_NullTiles_ThrowsArgumentNullException() {
            var playerService = Mock.Of<IPlayerService>();

            Assert.Throws<ArgumentNullException>(() => playerService.BroadcastTiles(123, 456, null!));
        }

        [Fact]
        public void BroadcastTiles_NonSquareTiles_ThrowsNotSupportedException() {
            var playerService = Mock.Of<IPlayerService>();
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 2);

            Assert.Throws<NotSupportedException>(() => playerService.BroadcastTiles(123, 456, tiles));
        }

        [Fact]
        public void BroadcastTiles() {
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 1);
            var mockPlayer = new Mock<IPlayer>();
            mockPlayer
                .Setup(p => p.SendPacket(ref It.Ref<TileSquarePacket>.IsAny))
                .Callback((TileSquareCallback)((ref TileSquarePacket packet) => {
                    Assert.Equal(123, packet.X);
                    Assert.Equal(456, packet.Y);
                    Assert.Same(tiles, packet.Tiles);
                }));
            var playerService = Mock.Of<IPlayerService>(
                ps => ps.Players == Mock.Of<IReadOnlyList<IPlayer>>(p => p.Count == 1 && p[0] == mockPlayer.Object));

            playerService.BroadcastTiles(123, 456, tiles);

            mockPlayer.Verify(p => p.SendPacket(ref It.Ref<TileSquarePacket>.IsAny));
        }

        private struct TestPacket : IPacket {
            public PacketId Id => throw new NotImplementedException();
            public int Read(Span<byte> span, PacketContext context) => throw new NotImplementedException();
            public int Write(Span<byte> span, PacketContext context) => throw new NotImplementedException();
        }
    }
}
