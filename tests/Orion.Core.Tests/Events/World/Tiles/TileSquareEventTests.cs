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
using Orion.Core.Players;
using Orion.Core.World;
using Orion.Core.World.Tiles;
using Xunit;

namespace Orion.Core.Events.World.Tiles {
    public class TileSquareEventTests {
        [Fact]
        public void Ctor_NullWorld_ThrowsArgumentNullException() {
            var player = Mock.Of<IPlayer>();
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 1);

            Assert.Throws<ArgumentNullException>(() => new TileSquareEvent(null!, player, 0, 0, tiles));
        }

        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var world = Mock.Of<IWorld>();
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 1);

            Assert.Throws<ArgumentNullException>(() => new TileSquareEvent(world, null!, 0, 0, tiles));
        }

        [Fact]
        public void Ctor_NullTiles_ThrowsArgumentNullException() {
            var player = Mock.Of<IPlayer>();
            var world = Mock.Of<IWorld>();

            Assert.Throws<ArgumentNullException>(() => new TileSquareEvent(world, player, 0, 0, null!));
        }

        [Fact]
        public void Ctor_TilesNotSquareArray_ThrowsArgumentNullException() {
            var player = Mock.Of<IPlayer>();
            var world = Mock.Of<IWorld>();
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 2);

            Assert.Throws<ArgumentException>(() => new TileSquareEvent(world, player, 0, 0, tiles));
        }

        [Fact]
        public void Player_Get() {
            var player = Mock.Of<IPlayer>();
            var world = Mock.Of<IWorld>();
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 1);
            var evt = new TileSquareEvent(world, player, 123, 456, tiles);

            Assert.Same(player, evt.Player);
        }

        [Fact]
        public void X_Get() {
            var player = Mock.Of<IPlayer>();
            var world = Mock.Of<IWorld>();
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 1);
            var evt = new TileSquareEvent(world, player, 123, 456, tiles);

            Assert.Equal(123, evt.X);
        }

        [Fact]
        public void Y_Get() {
            var player = Mock.Of<IPlayer>();
            var world = Mock.Of<IWorld>();
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 1);
            var evt = new TileSquareEvent(world, player, 123, 456, tiles);

            Assert.Equal(456, evt.Y);
        }

        [Fact]
        public void Tiles_Get() {
            var player = Mock.Of<IPlayer>();
            var world = Mock.Of<IWorld>();
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 1);
            var evt = new TileSquareEvent(world, player, 123, 456, tiles);

            Assert.Same(tiles, evt.Tiles);
        }
    }
}
