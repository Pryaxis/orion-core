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
using System.Runtime.CompilerServices;
using Moq;
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

        [Fact]
        public void Slice_NullTiles_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => TileSliceExtensions.Slice(null!, 0, 0, 1, 2));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        public void Slice_OutOfRangeStartX_ThrowsArgumentOutOfRangeException(int startX)
        {
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 10 && t.Height == 10);

            Assert.Throws<ArgumentOutOfRangeException>(() => tiles.Slice(startX, 5, 1, 2));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        public void Slice_OutOfRangeStartY_ThrowsArgumentOutOfRangeException(int startY)
        {
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 10 && t.Height == 10);

            Assert.Throws<ArgumentOutOfRangeException>(() => tiles.Slice(5, startY, 1, 2));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(6)]
        public void Slice_OutOfRangeWidth_ThrowsArgumentOutOfRangeException(int width)
        {
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 10 && t.Height == 10);

            Assert.Throws<ArgumentOutOfRangeException>(() => tiles.Slice(5, 5, width, 2));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(6)]
        public void Slice_OutOfRangeHeight_ThrowsArgumentOutOfRangeException(int height)
        {
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 10 && t.Height == 10);

            Assert.Throws<ArgumentOutOfRangeException>(() => tiles.Slice(5, 5, 1, height));
        }

        [Fact]
        public void Slice_Item_Get()
        {
            // Use the concrete `TileSlice` here since we can't mock ref returns right now.
            var tiles = new TileSlice(10, 10);

            var slice = tiles.Slice(5, 5, 1, 2);

            Assert.True(Unsafe.AreSame(ref tiles[5, 6], ref slice[0, 1]));
        }

        [Fact]
        public void Slice_Width()
        {
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 10 && t.Height == 10);

            var slice = tiles.Slice(5, 5, 1, 2);

            Assert.Equal(1, slice.Width);
        }

        [Fact]
        public void Slice_Height()
        {
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 10 && t.Height == 10);

            var slice = tiles.Slice(5, 5, 1, 2);

            Assert.Equal(2, slice.Height);
        }
    }
}
