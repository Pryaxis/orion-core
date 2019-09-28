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

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Orion.World.Tiles {
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class TileTests {
        [Fact]
        public void SetGetBlockColor_IsCorrect() {
            var tile = new Tile();

            tile.BlockColor = PaintColor.Red;
            tile.BlockColor.Should().Be(PaintColor.Red);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetGetIsBlockActive_IsCorrect(bool isBlockActive) {
            var tile = new Tile();

            tile.IsBlockActive = isBlockActive;
            tile.IsBlockActive.Should().Be(isBlockActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetGetIsBlockActuated_IsCorrect(bool isBlockActuated) {
            var tile = new Tile();

            tile.IsBlockActuated = isBlockActuated;
            tile.IsBlockActuated.Should().Be(isBlockActuated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetGetHasRedWire_IsCorrect(bool hasRedWire) {
            var tile = new Tile();

            tile.HasRedWire = hasRedWire;
            tile.HasRedWire.Should().Be(hasRedWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetGetHasBlueWire_IsCorrect(bool hasBlueWire) {
            var tile = new Tile();

            tile.HasBlueWire = hasBlueWire;
            tile.HasBlueWire.Should().Be(hasBlueWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetGetHasGreenWire_IsCorrect(bool hasGreenWire) {
            var tile = new Tile();

            tile.HasGreenWire = hasGreenWire;
            tile.HasGreenWire.Should().Be(hasGreenWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetGetIsBlockHalved_IsCorrect(bool isBlockHalved) {
            var tile = new Tile();

            tile.IsBlockHalved = isBlockHalved;
            tile.IsBlockHalved.Should().Be(isBlockHalved);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetGetHasActuator_IsCorrect(bool hasActuator) {
            var tile = new Tile();

            tile.HasActuator = hasActuator;
            tile.HasActuator.Should().Be(hasActuator);
        }

        [Fact]
        public void SetGetSlope_IsCorrect() {
            var tile = new Tile();

            tile.Slope = Slope.TopLeft;
            tile.Slope.Should().Be(Slope.TopLeft);
        }

        [Fact]
        public void SetGetWallColor_IsCorrect() {
            var tile = new Tile();

            tile.WallColor = PaintColor.Red;
            tile.WallColor.Should().Be(PaintColor.Red);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetGetIsLava_IsCorrect(bool isLava) {
            var tile = new Tile();

            tile.IsLava = isLava;
            tile.IsLava.Should().Be(isLava);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetGetIsHoney_IsCorrect(bool isHoney) {
            var tile = new Tile();

            tile.IsHoney = isHoney;
            tile.IsHoney.Should().Be(isHoney);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetGetHasYellowWire_IsCorrect(bool hasYellowWire) {
            var tile = new Tile();

            tile.HasYellowWire = hasYellowWire;
            tile.HasYellowWire.Should().Be(hasYellowWire);
        }

        [Fact]
        public void SetGetLiquidType_IsCorrect() {
            var tile = new Tile();

            tile.LiquidType = LiquidType.Honey;
            tile.LiquidType.Should().Be(LiquidType.Honey);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetGetIsCheckingLiquid_IsCorrect(bool isCheckingLiquid) {
            var tile = new Tile();

            tile.IsCheckingLiquid = isCheckingLiquid;
            tile.IsCheckingLiquid.Should().Be(isCheckingLiquid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetGetShouldSkipLiquid_IsCorrect(bool shouldSkipLiquid) {
            var tile = new Tile();

            tile.ShouldSkipLiquid = shouldSkipLiquid;
            tile.ShouldSkipLiquid.Should().Be(shouldSkipLiquid);
        }

        [Fact]
        public void isTheSameAs_DifferentSTileHeader_ReturnsFalse() {
            var tile = new Tile {IsBlockActive = true};
            var tile2 = new Tile();

            tile.IsTheSameAs(tile2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentTypeAndActive_ReturnsFalse() {
            var tile = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Stone
            };
            var tile2 = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Dirt
            };

            tile.IsTheSameAs(tile2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentTypeNotActive_ReturnsTrue() {
            var tile = new Tile {BlockType = BlockType.Stone};
            var tile2 = new Tile {BlockType = BlockType.Dirt};

            tile.IsTheSameAs(tile2).Should().BeTrue();
        }

        [Fact]
        public void isTheSameAs_DifferentFrameXAndImportant_ReturnsFalse() {
            var tile = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameX = 1
            };
            var tile2 = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameX = 2
            };

            tile.IsTheSameAs(tile2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentFrameYAndImportant_ReturnsFalse() {
            var tile = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameY = 1
            };
            var tile2 = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameY = 2
            };

            tile.IsTheSameAs(tile2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentFramesNotImportant_ReturnsTrue() {
            var tile = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Stone,
                BlockFrameX = 1,
                BlockFrameY = 1
            };
            var tile2 = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Stone,
                BlockFrameX = 2,
                BlockFrameY = 2
            };

            tile.IsTheSameAs(tile2).Should().BeTrue();
        }

        [Fact]
        public void isTheSameAs_DifferentWall_ReturnsFalse() {
            var tile = new Tile {WallType = WallType.Stone};
            var tile2 = new Tile {WallType = WallType.NaturalDirt};

            tile.IsTheSameAs(tile2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentLiquid_ReturnsFalse() {
            var tile = new Tile {LiquidAmount = 1};
            var tile2 = new Tile {LiquidAmount = 2};

            tile.IsTheSameAs(tile2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentWallColor_ReturnsFalse() {
            var tile = new Tile {WallColor = PaintColor.Red};
            var tile2 = new Tile {WallColor = PaintColor.DeepRed};

            tile.IsTheSameAs(tile2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentYellowWire_ReturnsFalse() {
            var tile = new Tile {HasYellowWire = true};
            var tile2 = new Tile();

            tile.IsTheSameAs(tile2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentBTileHeader_ReturnsFalse() {
            var tile = new Tile {
                LiquidAmount = 1,
                _bTileHeader = 1
            };
            var tile2 = new Tile {
                LiquidAmount = 1,
                _bTileHeader = 2
            };

            tile.IsTheSameAs(tile2).Should().BeFalse();
        }
    }
}
