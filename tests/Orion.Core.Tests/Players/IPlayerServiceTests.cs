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
using Orion.Core.Packets;
using Orion.Core.Packets.Server;
using Orion.Core.Packets.World.Tiles;
using Orion.Core.Utils;
using Orion.Core.World.Tiles;
using Xunit;

namespace Orion.Core.Players
{
    public class IPlayerServiceTests
    {
        [Fact]
        public void BroadcastPacket_NullPlayers_ThrowsArgumentNullException()
        {
            var packet = Mock.Of<IPacket>();

            Assert.Throws<ArgumentNullException>(() => IPlayerServiceExtensions.BroadcastPacket(null!, packet));
        }

        [Fact]
        public void BroadcastPacket()
        {
            var players = Mock.Of<IPlayerService>(p => p.Count == 1 && p[0] == Mock.Of<IPlayer>());
            var packet = Mock.Of<IPacket>();

            players.BroadcastPacket(packet);

            Mock.Get(players[0])
                .Verify(p => p.SendPacket(packet));
        }

        [Fact]
        public void BroadcastMessage_NullPlayers_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => IPlayerServiceExtensions.BroadcastMessage(null!, "test", Color3.White));
        }

        [Fact]
        public void BroadcastMessage_NullMessage_ThrowsArgumentNullException()
        {
            var players = Mock.Of<IPlayerService>();

            Assert.Throws<ArgumentNullException>(() => players.BroadcastMessage(null!, Color3.White));
        }

        [Fact]
        public void BroadcastMessage()
        {
            var players = Mock.Of<IPlayerService>(p => p.Count == 1 && p[0] == Mock.Of<IPlayer>());
            Mock.Get(players[0])
                .Setup(p => p.SendPacket(
                    It.Is<ServerChat>(p => p.Message == "test" && p.Color == Color3.White && p.LineWidth == -1)));

            players.BroadcastMessage("test", Color3.White);

            Mock.Get(players[0]).VerifyAll();
        }

        [Fact]
        public void BroadcastTiles_NullPlayers_ThrowsArgumentNullException()
        {
            var tiles = Mock.Of<ITileSlice>();

            Assert.Throws<ArgumentNullException>(() => IPlayerServiceExtensions.BroadcastTiles(null!, 123, 456, tiles));
        }

        [Fact]
        public void BroadcastTiles_NullTiles_ThrowsArgumentNullException()
        {
            var players = Mock.Of<IPlayerService>();

            Assert.Throws<ArgumentNullException>(() => players.BroadcastTiles(123, 456, null!));
        }

        [Fact]
        public void BroadcastTiles_NonSquareTiles_ThrowsNotSupportedException()
        {
            var players = Mock.Of<IPlayerService>();
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 2);

            Assert.Throws<NotSupportedException>(() => players.BroadcastTiles(123, 456, tiles));
        }

        [Fact]
        public void BroadcastTiles()
        {
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 1);
            var players = Mock.Of<IPlayerService>(p => p.Count == 1 && p[0] == Mock.Of<IPlayer>());
            Mock.Get(players[0])
                .Setup(p => p.SendPacket(It.Is<TileSquare>(p => p.X == 123 && p.Y == 456 && p.Tiles == tiles)));

            players.BroadcastTiles(123, 456, tiles);

            Mock.Get(players[0]).VerifyAll();
        }
    }
}
