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
using Moq;
using Orion.Core.DataStructures;
using Orion.Core.Packets;
using Orion.Core.Packets.Server;
using Orion.Core.Packets.World.Tiles;
using Orion.Core.World.Tiles;
using Xunit;

namespace Orion.Core.Players
{
    public class PlayerTests
    {
        private delegate void ServerDisconnectCallback(ref ServerDisconnectPacket packet);
        private delegate void ServerChatCallback(ref ServerChatPacket packet);
        private delegate void TileSquareCallback(ref TileSquarePacket packet);

        [Fact]
        public void SendPacket_NullPlayer_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => PlayerExtensions.SendPacket(null!, new TestPacket()));
        }

        [Fact]
        public void SendPacket()
        {
            var mockPlayer = new Mock<IPlayer>();

            mockPlayer.Object.SendPacket(new TestPacket());

            mockPlayer.Verify(p => p.SendPacket(ref It.Ref<TestPacket>.IsAny));
        }

        [Fact]
        public void Disconnect_NullPlayer_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => PlayerExtensions.Disconnect(null!, "test"));
        }

        [Fact]
        public void Disconnect_NullReason_ThrowsArgumentNullException()
        {
            var player = Mock.Of<IPlayer>();

            Assert.Throws<ArgumentNullException>(() => player.Disconnect(null!));
        }

        [Fact]
        public void Disconnect()
        {
            var mockPlayer = new Mock<IPlayer>();
            mockPlayer
                .Setup(p => p.SendPacket(ref It.Ref<ServerDisconnectPacket>.IsAny))
                .Callback((ServerDisconnectCallback)((ref ServerDisconnectPacket packet) =>
                {
                    Assert.Equal("test", packet.Reason);
                }));

            mockPlayer.Object.Disconnect("test");

            mockPlayer.Verify(p => p.SendPacket(ref It.Ref<ServerDisconnectPacket>.IsAny));
        }

        [Fact]
        public void SendMessage_NullPlayer_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => PlayerExtensions.SendMessage(null!, "test", Color3.White));
        }

        [Fact]
        public void SendMessage_NullReason_ThrowsArgumentNullException()
        {
            var player = Mock.Of<IPlayer>();

            Assert.Throws<ArgumentNullException>(() => player.SendMessage(null!, Color3.White));
        }

        [Fact]
        public void SendMessage()
        {
            var mockPlayer = new Mock<IPlayer>();
            mockPlayer
                .Setup(p => p.SendPacket(ref It.Ref<ServerChatPacket>.IsAny))
                .Callback((ServerChatCallback)((ref ServerChatPacket packet) =>
                {
                    Assert.Equal("test", packet.Message);
                    Assert.Equal(Color3.White, packet.Color);
                    Assert.Equal(-1, packet.LineWidth);
                }));

            mockPlayer.Object.SendMessage("test", Color3.White);

            mockPlayer.Verify(p => p.SendPacket(ref It.Ref<ServerChatPacket>.IsAny));
        }

        [Fact]
        public void SendTiles_NullPlayer_ThrowsArgumentNullException()
        {
            var tiles = Mock.Of<ITileSlice>();

            Assert.Throws<ArgumentNullException>(() => PlayerExtensions.SendTiles(null!, 123, 456, tiles));
        }

        [Fact]
        public void SendTiles_NullTiles_ThrowsArgumentNullException()
        {
            var player = Mock.Of<IPlayer>();

            Assert.Throws<ArgumentNullException>(() => player.SendTiles(123, 456, null!));
        }

        [Fact]
        public void SendTiles_NonSquareTiles_ThrowsNotSupportedException()
        {
            var player = Mock.Of<IPlayer>();
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 2);

            Assert.Throws<NotSupportedException>(() => player.SendTiles(123, 456, tiles));
        }

        [Fact]
        public void SendTiles()
        {
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 1);
            var mockPlayer = new Mock<IPlayer>();
            mockPlayer
                .Setup(p => p.SendPacket(ref It.Ref<TileSquarePacket>.IsAny))
                .Callback((TileSquareCallback)((ref TileSquarePacket packet) =>
                {
                    Assert.Equal(123, packet.X);
                    Assert.Equal(456, packet.Y);
                    Assert.Same(tiles, packet.Tiles);
                }));

            mockPlayer.Object.SendTiles(123, 456, tiles);

            mockPlayer.Verify(p => p.SendPacket(ref It.Ref<TileSquarePacket>.IsAny));
        }

        private struct TestPacket : IPacket
        {
            public PacketId Id => throw new NotImplementedException();
            public int Read(Span<byte> span, PacketContext context) => throw new NotImplementedException();
            public int Write(Span<byte> span, PacketContext context) => throw new NotImplementedException();
        }
    }
}
