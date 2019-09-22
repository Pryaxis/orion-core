// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using FluentAssertions;
using OTAPI.Tile;
using Xunit;

namespace Orion.World.Tiles {
    public class TileTests {
        [Fact]
        public void GetBlockType_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.type = 1;

            tile.BlockType.Should().Be(BlockType.Stone);
        }

        [Fact]
        public void SetBlockType_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.BlockType = BlockType.Stone;

            itile.type.Should().Be(1);
        }

        [Fact]
        public void GetWallType_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.wall = 1;

            tile.WallType.Should().Be(WallType.Stone);
        }

        [Fact]
        public void SetWallType_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.WallType = WallType.Stone;

            itile.wall.Should().Be(1);
        }

        [Fact]
        public void GetLiquidAmount_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.liquid = 100;

            tile.LiquidAmount.Should().Be(100);
        }

        [Fact]
        public void SetLiquidAmount_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.LiquidAmount = 100;

            itile.liquid.Should().Be(100);
        }

        [Fact]
        public void GetTileHeader_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.sTileHeader = 12345;

            tile.TileHeader.Should().Be(12345);
        }

        [Fact]
        public void SetTileHeader_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.TileHeader = 12345;

            itile.sTileHeader.Should().Be(12345);
        }

        [Fact]
        public void GetTileHeader2_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.bTileHeader = 100;

            tile.TileHeader2.Should().Be(100);
        }

        [Fact]
        public void SetTileHeader2_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.TileHeader2 = 100;

            itile.bTileHeader.Should().Be(100);
        }

        [Fact]
        public void GetTileHeader3_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.bTileHeader2 = 100;

            tile.TileHeader3.Should().Be(100);
        }

        [Fact]
        public void SetTileHeader3_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.TileHeader3 = 100;

            itile.bTileHeader2.Should().Be(100);
        }

        [Fact]
        public void GetTileHeader4_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.bTileHeader3 = 100;

            tile.TileHeader4.Should().Be(100);
        }

        [Fact]
        public void SetTileHeader4_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.TileHeader4 = 100;

            itile.bTileHeader3.Should().Be(100);
        }

        [Fact]
        public void GetBlockFrameX_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.frameX = 12345;

            tile.BlockFrameX.Should().Be(12345);
        }

        [Fact]
        public void SetBlockFrameX_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.BlockFrameX = 12345;

            itile.frameX.Should().Be(12345);
        }

        [Fact]
        public void GetBlockFrameY_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.frameY = 12345;

            tile.BlockFrameY.Should().Be(12345);
        }

        [Fact]
        public void SetBlockFrameY_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.BlockFrameY = 12345;

            itile.frameY.Should().Be(12345);
        }

        [Fact]
        public void GetBlockColor_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.color(PaintColor.DeepRed.Id);

            tile.BlockColor.Should().BeSameAs(PaintColor.DeepRed);
        }

        [Fact]
        public void SetBlockColor_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.BlockColor = PaintColor.DeepRed;

            itile.color().Should().Be(PaintColor.DeepRed.Id);
        }

        [Fact]
        public void SetBlockColor_NullValue_ThrowsArgumentNullException() {
            var tile = new TestTile();
            Action action = () => tile.BlockColor = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsBlockActive_IsCorrect(bool isBlockActive) {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.active(isBlockActive);

            tile.IsBlockActive.Should().Be(isBlockActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsBlockActive_IsCorrect(bool isBlockActive) {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.IsBlockActive = isBlockActive;

            itile.active().Should().Be(isBlockActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsBlockActuated_IsCorrect(bool isBlockActuated) {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.inActive(isBlockActuated);

            tile.IsBlockActuated.Should().Be(isBlockActuated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsBlockActuated_IsCorrect(bool isBlockActuated) {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.IsBlockActuated = isBlockActuated;

            itile.inActive().Should().Be(isBlockActuated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetHasRedWire_IsCorrect(bool hasRedWire) {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.wire(hasRedWire);

            tile.HasRedWire.Should().Be(hasRedWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetHasRedWire_IsCorrect(bool hasRedWire) {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.HasRedWire = hasRedWire;

            itile.wire().Should().Be(hasRedWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetHasBlueWire_IsCorrect(bool hasBlueWire) {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.wire2(hasBlueWire);

            tile.HasBlueWire.Should().Be(hasBlueWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetHasBlueWire_IsCorrect(bool hasBlueWire) {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.HasBlueWire = hasBlueWire;

            itile.wire2().Should().Be(hasBlueWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetHasGreenWire_IsCorrect(bool hasGreenWire) {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.wire3(hasGreenWire);

            tile.HasGreenWire.Should().Be(hasGreenWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetHasGreenWire_IsCorrect(bool hasGreenWire) {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.HasGreenWire = hasGreenWire;

            itile.wire3().Should().Be(hasGreenWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsBlockHalved_IsCorrect(bool isBlockHalved) {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.halfBrick(isBlockHalved);

            tile.IsBlockHalved.Should().Be(isBlockHalved);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsBlockHalved_IsCorrect(bool isBlockHalved) {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.IsBlockHalved = isBlockHalved;

            itile.halfBrick().Should().Be(isBlockHalved);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetHasActuator_IsCorrect(bool hasActuator) {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.actuator(hasActuator);

            tile.HasActuator.Should().Be(hasActuator);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetHasActuator_IsCorrect(bool hasActuator) {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.HasActuator = hasActuator;

            itile.actuator().Should().Be(hasActuator);
        }

        [Fact]
        public void GetSlope_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.slope((byte)Slope.TopLeft);

            tile.Slope.Should().Be(Slope.TopLeft);
        }

        [Fact]
        public void SetSlope_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.Slope = Slope.TopLeft;

            itile.slope().Should().Be((byte)Slope.TopLeft);
        }

        [Fact]
        public void GetWallColor_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.wallColor(PaintColor.DeepRed.Id);

            tile.WallColor.Should().BeSameAs(PaintColor.DeepRed);
        }

        [Fact]
        public void SetWallColor_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.WallColor = PaintColor.DeepRed;

            itile.wallColor().Should().Be(PaintColor.DeepRed.Id);
        }

        [Fact]
        public void SetWallColor_NullValue_ThrowsArgumentNullException() {
            var tile = new TestTile();
            Action action = () => tile.WallColor = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsLava_IsCorrect(bool isLava) {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.lava(isLava);

            tile.IsLava.Should().Be(isLava);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsLava_IsCorrect(bool isLava) {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.IsLava = isLava;

            itile.lava().Should().Be(isLava);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsHoney_IsCorrect(bool isHoney) {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.honey(isHoney);

            tile.IsHoney.Should().Be(isHoney);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsHoney_IsCorrect(bool isHoney) {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.IsHoney = isHoney;

            itile.honey().Should().Be(isHoney);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetHasYellowWire_IsCorrect(bool hasYellowWire) {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.wire4(hasYellowWire);

            tile.HasYellowWire.Should().Be(hasYellowWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetHasYellowWire_IsCorrect(bool hasYellowWire) {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.HasYellowWire = hasYellowWire;

            itile.wire4().Should().Be(hasYellowWire);
        }

        [Fact]
        public void GetLiquidType_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.liquidType((byte)LiquidType.Honey);

            tile.LiquidType.Should().Be(LiquidType.Honey);
        }

        [Fact]
        public void SetLiquidType_IsCorrect() {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.LiquidType = LiquidType.Honey;

            itile.liquidType().Should().Be((byte)LiquidType.Honey);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsCheckingLiquid_IsCorrect(bool isCheckingLiquid) {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.checkingLiquid(isCheckingLiquid);

            tile.IsCheckingLiquid.Should().Be(isCheckingLiquid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsCheckingLiquid_IsCorrect(bool isCheckingLiquid) {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.IsCheckingLiquid = isCheckingLiquid;

            itile.checkingLiquid().Should().Be(isCheckingLiquid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetShouldSkipLiquid_IsCorrect(bool shouldSkipLiquid) {
            var tile = new TestTile();
            var itile = (ITile)tile;
            itile.skipLiquid(shouldSkipLiquid);

            tile.ShouldSkipLiquid.Should().Be(shouldSkipLiquid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetShouldSkipLiquid_IsCorrect(bool shouldSkipLiquid) {
            var tile = new TestTile();
            var itile = (ITile)tile;

            tile.ShouldSkipLiquid = shouldSkipLiquid;

            itile.skipLiquid().Should().Be(shouldSkipLiquid);
        }

        public class TestTile : Tile {
            public override BlockType BlockType { get; set; }
            public override WallType WallType { get; set; }
            public override byte LiquidAmount { get; set; }
            public override short TileHeader { get; set; }
            public override byte TileHeader2 { get; set; }
            public override byte TileHeader3 { get; set; }
            public override byte TileHeader4 { get; set; }
            public override short BlockFrameX { get; set; }
            public override short BlockFrameY { get; set; }
        }
    }
}
