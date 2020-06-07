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
using System.Diagnostics.CodeAnalysis;
using Moq;
using Orion.Players;
using Orion.World;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Events.World.Tiles {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class TileSquareEventTests {
        [Fact]
        public void Ctor_NullWorld_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;

            Assert.Throws<ArgumentNullException>(() => new TileSquareEvent(null!, player, 0, 0, new Tile[0, 0]));
        }

        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var world = new Mock<IWorld>().Object;

            Assert.Throws<ArgumentNullException>(() => new TileSquareEvent(world, null!, 0, 0, new Tile[0, 0]));
        }

        [Fact]
        public void Ctor_NullTiles_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            var world = new Mock<IWorld>().Object;

            Assert.Throws<ArgumentNullException>(() => new TileSquareEvent(world, player, 0, 0, null!));
        }

        [Fact]
        public void Ctor_TilesNotSquareArray_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            var world = new Mock<IWorld>().Object;

            Assert.Throws<ArgumentException>(() => new TileSquareEvent(world, player, 0, 0, new Tile[1, 2]));
        }

        [Fact]
        public void Player_Get() {
            var player = new Mock<IPlayer>().Object;
            var world = new Mock<IWorld>().Object;
            var tiles = new Tile[1, 1];
            var evt = new TileSquareEvent(world, player, 123, 456, tiles);

            Assert.Same(player, evt.Player);
        }

        [Fact]
        public void X_Get() {
            var player = new Mock<IPlayer>().Object;
            var world = new Mock<IWorld>().Object;
            var tiles = new Tile[1, 1];
            var evt = new TileSquareEvent(world, player, 123, 456, tiles);

            Assert.Equal(123, evt.X);
        }

        [Fact]
        public void Y_Get() {
            var player = new Mock<IPlayer>().Object;
            var world = new Mock<IWorld>().Object;
            var tiles = new Tile[1, 1];
            var evt = new TileSquareEvent(world, player, 123, 456, tiles);

            Assert.Equal(456, evt.Y);
        }

        [Fact]
        public void Tiles_Get() {
            var player = new Mock<IPlayer>().Object;
            var world = new Mock<IWorld>().Object;
            var tiles = new Tile[1, 1];
            var evt = new TileSquareEvent(world, player, 123, 456, tiles);

            Assert.Same(tiles, evt.Tiles);
        }

        [Fact]
        public void CancellationReason_Set_Get() {
            var player = new Mock<IPlayer>().Object;
            var world = new Mock<IWorld>().Object;
            var tiles = new Tile[1, 1];
            var evt = new TileSquareEvent(world, player, 123, 456, tiles);

            evt.CancellationReason = "test";

            Assert.Equal("test", evt.CancellationReason);
        }
    }
}
