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

using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Orion.Core.World.Tiles
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class TileTests
    {
        [Fact]
        public void BlockId_Set_Get()
        {
            var tile = new Tile();

            tile.BlockId = BlockId.Torches;

            Assert.Equal(BlockId.Torches, tile.BlockId);
        }

        [Fact]
        public void WallId_Set_Get()
        {
            var tile = new Tile();

            tile.WallId = WallId.Stone;

            Assert.Equal(WallId.Stone, tile.WallId);
        }

        [Fact]
        public void Liquid_Set_Get()
        {
            var tile = new Tile();

            tile.Liquid = new Liquid(LiquidType.Water, 123);

            Assert.Equal(new Liquid(LiquidType.Water, 123), tile.Liquid);
        }

        [Fact]
        public void BlockFrameX_Set_Get()
        {
            var tile = new Tile();

            tile.BlockFrameX = 1234;

            Assert.Equal(1234, tile.BlockFrameX);
        }

        [Fact]
        public void BlockFrameY_Set_Get()
        {
            var tile = new Tile();

            tile.BlockFrameY = 5678;

            Assert.Equal(5678, tile.BlockFrameY);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsBlockActive_Set_Get(bool value)
        {
            var tile = new Tile();

            tile.IsBlockActive = value;

            Assert.Equal(value, tile.IsBlockActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsBlockActuated_Set_Get(bool value)
        {
            var tile = new Tile();

            tile.IsBlockActuated = value;

            Assert.Equal(value, tile.IsBlockActuated);
        }

        [Fact]
        public void BlockColor_Set_Get()
        {
            var tile = new Tile();

            tile.BlockColor = PaintColor.Red;

            Assert.Equal(PaintColor.Red, tile.BlockColor);
        }

        [Theory]
        [InlineData(BlockShape.Normal)]
        [InlineData(BlockShape.Halved)]
        [InlineData(BlockShape.TopRight)]
        [InlineData(BlockShape.TopLeft)]
        [InlineData(BlockShape.BottomRight)]
        [InlineData(BlockShape.BottomLeft)]
        public void BlockShape_Set_Get(BlockShape value)
        {
            var tile = new Tile();

            tile.BlockShape = value;

            Assert.Equal(value, tile.BlockShape);
        }

        [Fact]
        public void WallColor_Set_Get()
        {
            var tile = new Tile();

            tile.WallColor = PaintColor.Red;

            Assert.Equal(PaintColor.Red, tile.WallColor);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasRedWire_Set_Get(bool value)
        {
            var tile = new Tile();

            tile.HasRedWire = value;

            Assert.Equal(value, tile.HasRedWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasBlueWire_Set_Get(bool value)
        {
            var tile = new Tile();

            tile.HasBlueWire = value;

            Assert.Equal(value, tile.HasBlueWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasGreenWire_Set_Get(bool value)
        {
            var tile = new Tile();

            tile.HasGreenWire = value;

            Assert.Equal(value, tile.HasGreenWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasYellowWire_Set_Get(bool value)
        {
            var tile = new Tile();

            tile.HasYellowWire = value;

            Assert.Equal(value, tile.HasYellowWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasActuator_Set_Get(bool value)
        {
            var tile = new Tile();

            tile.HasActuator = value;

            Assert.Equal(value, tile.HasActuator);
        }
    }
}
