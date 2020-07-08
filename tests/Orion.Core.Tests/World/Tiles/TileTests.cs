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
        public void WallColor_Set_Get()
        {
            var tile = new Tile();

            tile.WallColor = PaintColor.Red;

            Assert.Equal(PaintColor.Red, tile.WallColor);
        }

        [Fact]
        public void Equals_HasFrames_ReturnsTrue()
        {
            var tile = new Tile { BlockId = BlockId.Torches, BlockFrameX = 1, BlockFrameY = 2 };
            var tile2 = new Tile { BlockId = BlockId.Torches, BlockFrameX = 1, BlockFrameY = 2 };

            Assert.True(tile.Equals((object)tile2));
            Assert.True(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_DoesNotHaveFrames_ReturnsTrue()
        {
            var tile = new Tile { BlockId = BlockId.Stone, BlockFrameX = 1, BlockFrameY = 2 };
            var tile2 = new Tile { BlockId = BlockId.Stone, BlockFrameX = 2, BlockFrameY = 2 };

            Assert.True(tile.Equals((object)tile2));
            Assert.True(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_NoLiquid_ReturnsTrue()
        {
            var tile = new Tile { Liquid = new Liquid(LiquidType.Water, 0) };
            var tile2 = new Tile { Liquid = new Liquid(LiquidType.Lava, 0) };

            Assert.True(tile.Equals((object)tile2));
            Assert.True(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_HasLiquid_ReturnsTrue()
        {
            var tile = new Tile { Liquid = new Liquid(LiquidType.Water, 255) };
            var tile2 = new Tile { Liquid = new Liquid(LiquidType.Water, 255) };

            Assert.True(tile.Equals((object)tile2));
            Assert.True(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_WrongType_ReturnsFalse()
        {
            var tile = new Tile();

            Assert.False(tile.Equals(0));
        }

        [Fact]
        public void Equals_BlockIdNotEqual_ReturnsFalse()
        {
            var tile = new Tile { BlockId = BlockId.None };
            var tile2 = new Tile { BlockId = BlockId.Dirt };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_WallIdNotEqual_ReturnsFalse()
        {
            var tile = new Tile { WallId = WallId.None };
            var tile2 = new Tile { WallId = WallId.Stone };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_LiquidNotEqual_ReturnsFalse()
        {
            var tile = new Tile { Liquid = new Liquid(LiquidType.Water, 255) };
            var tile2 = new Tile { Liquid = new Liquid(LiquidType.Water, 127) };
            var tile3 = new Tile { Liquid = new Liquid(LiquidType.Honey, 255) };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals((object)tile3));
            Assert.False(tile.Equals(tile2));
            Assert.False(tile.Equals(tile3));
        }

        [Fact]
        public void Equals_BlockFramesNotEqual_HasFrames_ReturnsFalse()
        {
            var tile = new Tile { BlockId = BlockId.Torches, BlockFrameX = 1, BlockFrameY = 1 };
            var tile2 = new Tile { BlockId = BlockId.Torches, BlockFrameX = 2, BlockFrameY = 1 };
            var tile3 = new Tile { BlockId = BlockId.Torches, BlockFrameX = 1, BlockFrameY = 2 };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals((object)tile3));
            Assert.False(tile.Equals(tile2));
            Assert.False(tile.Equals(tile3));
        }

        [Fact]
        public void Equals_BlockColorNotEqual_ReturnsFalse()
        {
            var tile = new Tile { BlockColor = PaintColor.Red };
            var tile2 = new Tile { BlockColor = PaintColor.Blue };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_BlockShapeNotEqual_ReturnsFalse()
        {
            var tile = new Tile { BlockShape = BlockShape.Normal };
            var tile2 = new Tile { BlockShape = BlockShape.Halved };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_HasRedWireNotEqual_ReturnsFalse()
        {
            var tile = new Tile { HasRedWire = false };
            var tile2 = new Tile { HasRedWire = true };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_HasBlueWireNotEqual_ReturnsFalse()
        {
            var tile = new Tile { HasBlueWire = false };
            var tile2 = new Tile { HasBlueWire = true };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_HasGreenWireNotEqual_ReturnsFalse()
        {
            var tile = new Tile { HasGreenWire = false };
            var tile2 = new Tile { HasGreenWire = true };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_HasYellowWireNotEqual_ReturnsFalse()
        {
            var tile = new Tile { HasYellowWire = false };
            var tile2 = new Tile { HasYellowWire = true };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_HasActuatorNotEqual_ReturnsFalse()
        {
            var tile = new Tile { HasActuator = false };
            var tile2 = new Tile { HasActuator = true };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_IsBlockActuatedNotEqual_ReturnsFalse()
        {
            var tile = new Tile { IsBlockActuated = false };
            var tile2 = new Tile { IsBlockActuated = true };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals(tile2));
        }

        [Fact]
        public void Equals_WallColorNotEqual_ReturnsFalse()
        {
            var tile = new Tile { WallColor = PaintColor.Red };
            var tile2 = new Tile { WallColor = PaintColor.Blue };

            Assert.False(tile.Equals((object)tile2));
            Assert.False(tile.Equals(tile2));
        }

        [Fact]
        public void GetHashCode_Equals_HasFrames_AreEqual()
        {
            var tile = new Tile { BlockId = BlockId.Torches, BlockFrameX = 1, BlockFrameY = 2 };
            var tile2 = new Tile { BlockId = BlockId.Torches, BlockFrameX = 1, BlockFrameY = 2 };

            Assert.Equal(tile.GetHashCode(), tile2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_Equals_DoesNotHaveFrames_AreEqual()
        {
            var tile = new Tile { BlockId = BlockId.Stone, BlockFrameX = 1, BlockFrameY = 2 };
            var tile2 = new Tile { BlockId = BlockId.Stone, BlockFrameX = 2, BlockFrameY = 2 };

            Assert.Equal(tile.GetHashCode(), tile2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_Equals_NoLiquid_AreEqual()
        {
            var tile = new Tile { Liquid = new Liquid(LiquidType.Water, 0) };
            var tile2 = new Tile { Liquid = new Liquid(LiquidType.Lava, 0) };

            Assert.Equal(tile.GetHashCode(), tile2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_Equals_HasLiquid_AreEqual()
        {
            var tile = new Tile { Liquid = new Liquid(LiquidType.Water, 255) };
            var tile2 = new Tile { Liquid = new Liquid(LiquidType.Water, 255) };

            Assert.Equal(tile.GetHashCode(), tile2.GetHashCode());
        }
    }
}
