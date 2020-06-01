// Copyright (c) 2019 Pryaxis & Orion Contributors
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

namespace Orion.World.Tiles {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class TileTests {
        [Fact]
        public void BlockId_Set_Get() {
            var tile = new Tile();

            tile.BlockId = BlockId.Torches;

            Assert.Equal(BlockId.Torches, tile.BlockId);
        }

        [Fact]
        public void WallId_Set_Get() {
            var tile = new Tile();

            tile.WallId = WallId.Stone;

            Assert.Equal(WallId.Stone, tile.WallId);
        }

        [Fact]
        public void LiquidAmount_Set_Get() {
            var tile = new Tile();

            tile.LiquidAmount = 255;

            Assert.Equal(255, tile.LiquidAmount);
        }

        [Fact]
        public void BlockFrameX_Set_Get() {
            var tile = new Tile();

            tile.BlockFrameX = 1234;

            Assert.Equal(1234, tile.BlockFrameX);
        }

        [Fact]
        public void BlockFrameY_Set_Get() {
            var tile = new Tile();

            tile.BlockFrameY = 5678;

            Assert.Equal(5678, tile.BlockFrameY);
        }

        [Fact]
        public void BlockColor_Set_Get() {
            var tile = new Tile();

            tile.BlockColor = PaintColor.Red;

            Assert.Equal(PaintColor.Red, tile.BlockColor);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsBlockActive_Set_Get(bool isBlockActive) {
            var tile = new Tile();

            tile.IsBlockActive = isBlockActive;

            Assert.Equal(isBlockActive, tile.IsBlockActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsBlockActuated_Set_Get(bool isBlockActuated) {
            var tile = new Tile();

            tile.IsBlockActuated = isBlockActuated;

            Assert.Equal(isBlockActuated, tile.IsBlockActuated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasRedWire_Set_Get(bool hasRedWire) {
            var tile = new Tile();

            tile.HasRedWire = hasRedWire;

            Assert.Equal(hasRedWire, tile.HasRedWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasBlueWire_Set_Get(bool hasBlueWire) {
            var tile = new Tile();

            tile.HasBlueWire = hasBlueWire;

            Assert.Equal(hasBlueWire, tile.HasBlueWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasGreenWire_Set_Get(bool hasGreenWire) {
            var tile = new Tile();

            tile.HasGreenWire = hasGreenWire;

            Assert.Equal(hasGreenWire, tile.HasGreenWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsBlockHalved_Set_Get(bool isBlockHalved) {
            var tile = new Tile();

            tile.IsBlockHalved = isBlockHalved;

            Assert.Equal(isBlockHalved, tile.IsBlockHalved);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasActuator_Set_Get(bool hasActuator) {
            var tile = new Tile();

            tile.HasActuator = hasActuator;

            Assert.Equal(hasActuator, tile.HasActuator);
        }

        [Fact]
        public void Slope_Set_Get() {
            var tile = new Tile();

            tile.Slope = Slope.TopLeft;

            Assert.Equal(Slope.TopLeft, tile.Slope);
        }

        [Fact]
        public void WallColor_Set_Get() {
            var tile = new Tile();

            tile.WallColor = PaintColor.Red;

            Assert.Equal(PaintColor.Red, tile.WallColor);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasYellowWire_Set_Get(bool hasYellowWire) {
            var tile = new Tile();

            tile.HasYellowWire = hasYellowWire;

            Assert.Equal(hasYellowWire, tile.HasYellowWire);
        }

        [Fact]
        public void Liquid_Set_Get() {
            var tile = new Tile();

            tile.Liquid = Liquid.Honey;

            Assert.Equal(Liquid.Honey, tile.Liquid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsCheckingLiquid_Set_Get(bool isCheckingLiquid) {
            var tile = new Tile();

            tile.IsCheckingLiquid = isCheckingLiquid;

            Assert.Equal(isCheckingLiquid, tile.IsCheckingLiquid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ShouldSkipLiquid_Set_Get(bool shouldSkipLiquid) {
            var tile = new Tile();

            tile.ShouldSkipLiquid = shouldSkipLiquid;

            Assert.Equal(shouldSkipLiquid, tile.ShouldSkipLiquid);
        }
    }
}
