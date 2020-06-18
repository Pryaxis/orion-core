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
using Xunit;

namespace Orion.Core.World.Tiles
{
    public class TileSliceTests
    {
        [Fact]
        public void Ctor_NegativeWidth_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TileSlice(-1, 2));
        }

        [Fact]
        public void Ctor_NegativeHeight_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TileSlice(1, -1));
        }

        [Fact]
        public void Item_Get()
        {
            var tiles = new TileSlice(1, 2);

            tiles[0, 0].BlockId = BlockId.Stone;

            Assert.Equal(BlockId.Stone, tiles[0, 0].BlockId);
        }

        [Fact]
        public void Width_Get()
        {
            var tiles = new TileSlice(1, 2);

            Assert.Equal(1, tiles.Width);
        }

        [Fact]
        public void Height_Get()
        {
            var tiles = new TileSlice(1, 2);

            Assert.Equal(2, tiles.Height);
        }
    }
}
