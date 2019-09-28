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
using Moq;
using OTAPI.Tile;
using Xunit;

namespace Orion.World.Tiles {
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public unsafe class TileToITileBridgeTests {
        [Fact]
        public void Gettype_IsCorrect() {
            var tile = new Tile {BlockType = BlockType.Stone};
            var bridge = new TileToITileBridge(&tile);

            bridge.type.Should().Be((ushort)BlockType.Stone);
        }

        [Fact]
        public void Settype_IsCorrect() {
            var tile = new Tile();
            var bridge = new TileToITileBridge(&tile);

            bridge.type = (ushort)BlockType.Stone;

            tile.BlockType.Should().Be(BlockType.Stone);
        }

        [Fact]
        public void Getwall_IsCorrect() {
            var tile = new Tile {WallType = WallType.Stone};
            var bridge = new TileToITileBridge(&tile);

            bridge.wall.Should().Be((byte)WallType.Stone);
        }

        [Fact]
        public void Setwall_IsCorrect() {
            var tile = new Tile();
            var bridge = new TileToITileBridge(&tile);

            bridge.wall = (byte)WallType.Stone;

            tile.WallType.Should().Be(WallType.Stone);
        }

        [Fact]
        public void Getliquid_IsCorrect() {
            var tile = new Tile {LiquidAmount = 100};
            var bridge = new TileToITileBridge(&tile);

            bridge.liquid.Should().Be(100);
        }

        [Fact]
        public void Setliquid_IsCorrect() {
            var tile = new Tile();
            var bridge = new TileToITileBridge(&tile);

            bridge.liquid = 100;

            tile.LiquidAmount.Should().Be(100);
        }

        [Fact]
        public void GetsTileHeader_IsCorrect() {
            var tile = new Tile {_sTileHeader = 12345};
            var bridge = new TileToITileBridge(&tile);

            bridge.sTileHeader.Should().Be(12345);
        }

        [Fact]
        public void SetsTileHeader_IsCorrect() {
            var tile = new Tile();
            var bridge = new TileToITileBridge(&tile);

            bridge.sTileHeader = 12345;

            tile._sTileHeader.Should().Be(12345);
        }

        [Fact]
        public void GetbTileHeader_IsCorrect() {
            var tile = new Tile {_bTileHeader = 100};
            var bridge = new TileToITileBridge(&tile);

            bridge.bTileHeader.Should().Be(100);
        }

        [Fact]
        public void SetbTileHeader_IsCorrect() {
            var tile = new Tile();
            var bridge = new TileToITileBridge(&tile);

            bridge.bTileHeader = 100;

            tile._bTileHeader.Should().Be(100);
        }

        [Fact]
        public void GetbTileHeader3_IsCorrect() {
            var tile = new Tile {_bTileHeader2 = 100};
            var bridge = new TileToITileBridge(&tile);

            bridge.bTileHeader3.Should().Be(100);
        }

        [Fact]
        public void SetbTileHeader3_IsCorrect() {
            var tile = new Tile();
            var bridge = new TileToITileBridge(&tile);

            bridge.bTileHeader3 = 100;

            tile._bTileHeader2.Should().Be(100);
        }

        [Fact]
        public void GetframeX_IsCorrect() {
            var tile = new Tile {BlockFrameX = 12345};
            var bridge = new TileToITileBridge(&tile);

            bridge.frameX.Should().Be(12345);
        }

        [Fact]
        public void SetframeX_IsCorrect() {
            var tile = new Tile();
            var bridge = new TileToITileBridge(&tile);

            bridge.frameX = 12345;

            tile.BlockFrameX.Should().Be(12345);
        }

        [Fact]
        public void GetframeY_IsCorrect() {
            var tile = new Tile {BlockFrameY = 12345};
            var bridge = new TileToITileBridge(&tile);

            bridge.frameY.Should().Be(12345);
        }

        [Fact]
        public void SetframeY_IsCorrect() {
            var tile = new Tile();
            var bridge = new TileToITileBridge(&tile);

            bridge.frameY = 12345;

            tile.BlockFrameY.Should().Be(12345);
        }

        [Fact]
        public void CopyFrom_IsCorrect() {
            var mockTile = new Mock<ITile>();
            mockTile.SetupGet(t => t.type).Returns(1);
            mockTile.SetupGet(t => t.wall).Returns(2);
            mockTile.SetupGet(t => t.liquid).Returns(3);
            mockTile.SetupGet(t => t.sTileHeader).Returns(12345);
            mockTile.SetupGet(t => t.bTileHeader).Returns(100);
            mockTile.SetupGet(t => t.bTileHeader3).Returns(101);
            mockTile.SetupGet(t => t.frameX).Returns(4);
            mockTile.SetupGet(t => t.frameY).Returns(5);

            var tile = new Tile();
            var bridge = new TileToITileBridge(&tile);

            bridge.CopyFrom(mockTile.Object);

            tile.BlockType.Should().Be((BlockType)1);
            tile.WallType.Should().Be((WallType)2);
            tile.LiquidAmount.Should().Be(3);
            tile._sTileHeader.Should().Be(12345);
            tile._bTileHeader.Should().Be(100);
            tile._bTileHeader2.Should().Be(101);
            tile.BlockFrameX.Should().Be(4);
            tile.BlockFrameY.Should().Be(5);
            mockTile.VerifyGet(t => t.type);
            mockTile.VerifyGet(t => t.wall);
            mockTile.VerifyGet(t => t.liquid);
            mockTile.VerifyGet(t => t.sTileHeader);
            mockTile.VerifyGet(t => t.bTileHeader);
            mockTile.VerifyGet(t => t.bTileHeader3);
            mockTile.VerifyGet(t => t.frameX);
            mockTile.VerifyGet(t => t.frameY);
            mockTile.VerifyNoOtherCalls();
        }

        [Fact]
        public void CopyFrom_Bridge_IsCorrect() {
            var tile = new Tile {
                BlockType = (BlockType)1,
                WallType = (WallType)2,
                LiquidAmount = 3,
                _sTileHeader = 12345,
                _bTileHeader = 100,
                _bTileHeader2 = 101,
                BlockFrameX = 4,
                BlockFrameY = 5
            };
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile();
            var bridge2 = new TileToITileBridge(&tile2);

            bridge2.CopyFrom(bridge);

            tile2.BlockType.Should().Be((BlockType)1);
            tile2.WallType.Should().Be((WallType)2);
            tile2.LiquidAmount.Should().Be(3);
            tile2._sTileHeader.Should().Be(12345);
            tile2._bTileHeader.Should().Be(100);
            tile2._bTileHeader2.Should().Be(101);
            tile2.BlockFrameX.Should().Be(4);
            tile2.BlockFrameY.Should().Be(5);
        }

        [Fact]
        public void ClearEverything_IsCorrect() {
            var tile = new Tile {
                BlockType = (BlockType)1,
                WallType = (WallType)2,
                LiquidAmount = 3,
                _sTileHeader = 12345,
                _bTileHeader = 100,
                _bTileHeader2 = 101,
                BlockFrameX = 4,
                BlockFrameY = 5
            };
            var bridge = new TileToITileBridge(&tile);

            bridge.ClearEverything();

            tile.BlockType.Should().Be(0);
            tile.WallType.Should().Be(0);
            tile.LiquidAmount.Should().Be(0);
            tile._sTileHeader.Should().Be(0);
            tile._bTileHeader.Should().Be(0);
            tile._bTileHeader2.Should().Be(0);
            tile.BlockFrameX.Should().Be(0);
            tile.BlockFrameY.Should().Be(0);
        }

        [Fact]
        public void ClearTile_IsCorrect() {
            var tile = new Tile {
                Slope = Slope.BottomRight,
                IsBlockHalved = true,
                IsBlockActive = true
            };
            var bridge = new TileToITileBridge(&tile);

            bridge.ClearTile();

            tile.Slope.Should().Be(0);
            tile.IsBlockHalved.Should().BeFalse();
            tile.IsBlockActive.Should().BeFalse();
        }

        [Fact]
        public void ResetToType_IsCorrect() {
            var tile = new Tile {
                BlockType = (BlockType)1,
                WallType = (WallType)2,
                LiquidAmount = 3,
                _sTileHeader = 12345,
                _bTileHeader = 100,
                _bTileHeader2 = 101,
                BlockFrameX = 4,
                BlockFrameY = 5
            };
            var bridge = new TileToITileBridge(&tile);

            bridge.ResetToType((ushort)BlockType.Stone);

            tile.BlockType.Should().Be(BlockType.Stone);
            tile.WallType.Should().Be(0);
            tile.LiquidAmount.Should().Be(0);
            tile._sTileHeader.Should().Be(32);
            tile._bTileHeader.Should().Be(0);
            tile._bTileHeader2.Should().Be(0);
            tile.BlockFrameX.Should().Be(0);
            tile.BlockFrameY.Should().Be(0);
        }

        [Fact]
        public void ClearMetadata_IsCorrect() {
            var tile = new Tile {
                BlockType = (BlockType)1,
                WallType = (WallType)2,
                LiquidAmount = 3,
                _sTileHeader = 12345,
                _bTileHeader = 100,
                _bTileHeader2 = 101,
                BlockFrameX = 4,
                BlockFrameY = 5
            };
            var bridge = new TileToITileBridge(&tile);

            bridge.ClearMetadata();

            tile.BlockType.Should().Be((BlockType)1);
            tile.WallType.Should().Be((WallType)2);
            tile.LiquidAmount.Should().Be(0);
            tile._sTileHeader.Should().Be(0);
            tile._bTileHeader.Should().Be(0);
            tile._bTileHeader2.Should().Be(0);
            tile.BlockFrameX.Should().Be(0);
            tile.BlockFrameY.Should().Be(0);
        }

        [Fact]
        public void isTheSameAs_NullTile_ReturnsFalse() {
            var tile = new Tile();
            var bridge = new TileToITileBridge(&tile);

            bridge.isTheSameAs(null).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentSTileHeader_ReturnsFalse() {
            var tile = new Tile {IsBlockActive = true};
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile();
            var bridge2 = new TileToITileBridge(&tile2);

            bridge.isTheSameAs(bridge2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentTypeAndActive_ReturnsFalse() {
            var tile = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Stone
            };
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Dirt
            };
            var bridge2 = new TileToITileBridge(&tile2);

            bridge.isTheSameAs(bridge2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentTypeNotActive_ReturnsTrue() {
            var tile = new Tile {BlockType = BlockType.Stone};
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile {BlockType = BlockType.Dirt};
            var bridge2 = new TileToITileBridge(&tile2);

            bridge.isTheSameAs(bridge2).Should().BeTrue();
        }

        [Fact]
        public void isTheSameAs_DifferentFrameXAndImportant_ReturnsFalse() {
            var tile = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameX = 1
            };
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameX = 2
            };
            var bridge2 = new TileToITileBridge(&tile2);

            bridge.isTheSameAs(bridge2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentFrameYAndImportant_ReturnsFalse() {
            var tile = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameY = 1
            };
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameY = 2
            };
            var bridge2 = new TileToITileBridge(&tile2);

            bridge.isTheSameAs(bridge2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentFramesNotImportant_ReturnsTrue() {
            var tile = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Stone,
                BlockFrameX = 1,
                BlockFrameY = 1
            };
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Stone,
                BlockFrameX = 2,
                BlockFrameY = 2
            };
            var bridge2 = new TileToITileBridge(&tile2);

            bridge.isTheSameAs(bridge2).Should().BeTrue();
        }

        [Fact]
        public void isTheSameAs_DifferentWall_ReturnsFalse() {
            var tile = new Tile {WallType = WallType.Stone};
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile {WallType = WallType.NaturalDirt};
            var bridge2 = new TileToITileBridge(&tile2);

            bridge.isTheSameAs(bridge2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentLiquid_ReturnsFalse() {
            var tile = new Tile {LiquidAmount = 1};
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile {LiquidAmount = 2};
            var bridge2 = new TileToITileBridge(&tile2);

            bridge.isTheSameAs(bridge2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentWallColor_ReturnsFalse() {
            var tile = new Tile {WallColor = PaintColor.Red};
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile {WallColor = PaintColor.DeepRed};
            var bridge2 = new TileToITileBridge(&tile2);

            bridge.isTheSameAs(bridge2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentYellowWire_ReturnsFalse() {
            var tile = new Tile {HasYellowWire = true};
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile();
            var bridge2 = new TileToITileBridge(&tile2);

            bridge.isTheSameAs(bridge2).Should().BeFalse();
        }

        [Fact]
        public void isTheSameAs_DifferentBTileHeader_ReturnsFalse() {
            var tile = new Tile {
                LiquidAmount = 1,
                _bTileHeader = 1
            };
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile {
                LiquidAmount = 1,
                _bTileHeader = 2
            };
            var bridge2 = new TileToITileBridge(&tile2);

            bridge.isTheSameAs(bridge2).Should().BeFalse();
        }

        [Fact]
        public void color_IsCorrect() {
            var tile = new Tile {BlockColor = PaintColor.Red};
            var bridge = new TileToITileBridge(&tile);

            bridge.color().Should().Be((byte)PaintColor.Red);

            bridge.color((byte)PaintColor.DeepRed);

            tile.BlockColor.Should().Be(PaintColor.DeepRed);
        }

        [Fact]
        public void active_IsCorrect() {
            var tile = new Tile {IsBlockActive = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.active().Should().BeTrue();

            bridge.active(false);

            tile.IsBlockActive.Should().BeFalse();
        }

        [Fact]
        public void inActive_IsCorrect() {
            var tile = new Tile {IsBlockActuated = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.inActive().Should().BeTrue();

            bridge.inActive(false);

            tile.IsBlockActuated.Should().BeFalse();
        }

        [Fact]
        public void wire_IsCorrect() {
            var tile = new Tile {HasRedWire = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.wire().Should().BeTrue();

            bridge.wire(false);

            tile.HasRedWire.Should().BeFalse();
        }

        [Fact]
        public void wire2_IsCorrect() {
            var tile = new Tile {HasBlueWire = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.wire2().Should().BeTrue();

            bridge.wire2(false);

            tile.HasBlueWire.Should().BeFalse();
        }

        [Fact]
        public void wire3_IsCorrect() {
            var tile = new Tile {HasGreenWire = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.wire3().Should().BeTrue();

            bridge.wire3(false);

            tile.HasGreenWire.Should().BeFalse();
        }

        [Fact]
        public void halfBrick_IsCorrect() {
            var tile = new Tile {IsBlockHalved = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.halfBrick().Should().BeTrue();

            bridge.halfBrick(false);

            tile.IsBlockHalved.Should().BeFalse();
        }

        [Fact]
        public void actuator_IsCorrect() {
            var tile = new Tile {HasActuator = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.actuator().Should().BeTrue();

            bridge.actuator(false);

            tile.HasActuator.Should().BeFalse();
        }

        [Fact]
        public void slope_IsCorrect() {
            var tile = new Tile {Slope = Slope.BottomRight};
            var bridge = new TileToITileBridge(&tile);

            bridge.slope().Should().Be((byte)Slope.BottomRight);

            bridge.slope((byte)Slope.BottomLeft);

            tile.Slope.Should().Be(Slope.BottomLeft);
        }

        [Fact]
        public void wallColor_IsCorrect() {
            var tile = new Tile {WallColor = PaintColor.Red};
            var bridge = new TileToITileBridge(&tile);

            bridge.wallColor().Should().Be((byte)PaintColor.Red);

            bridge.wallColor((byte)PaintColor.DeepRed);

            tile.WallColor.Should().Be(PaintColor.DeepRed);
        }

        [Fact]
        public void lava_IsCorrect() {
            var tile = new Tile {IsLava = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.lava().Should().BeTrue();

            bridge.lava(false);

            tile.IsLava.Should().BeFalse();
        }

        [Fact]
        public void honey_IsCorrect() {
            var tile = new Tile {IsHoney = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.honey().Should().BeTrue();

            bridge.honey(false);

            tile.IsHoney.Should().BeFalse();
        }

        [Fact]
        public void liquidType_IsCorrect() {
            var tile = new Tile {LiquidType = LiquidType.Lava};
            var bridge = new TileToITileBridge(&tile);

            bridge.liquidType().Should().Be((byte)LiquidType.Lava);

            bridge.liquidType((int)LiquidType.Honey);

            tile.LiquidType.Should().Be(LiquidType.Honey);
        }

        [Fact]
        public void wire4_IsCorrect() {
            var tile = new Tile {HasYellowWire = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.wire4().Should().BeTrue();

            bridge.wire4(false);

            tile.HasYellowWire.Should().BeFalse();
        }

        [Fact]
        public void checkingLiquid_IsCorrect() {
            var tile = new Tile {IsCheckingLiquid = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.checkingLiquid().Should().BeTrue();

            bridge.checkingLiquid(false);

            tile.IsCheckingLiquid.Should().BeFalse();
        }

        [Fact]
        public void skipLiquid_IsCorrect() {
            var tile = new Tile {ShouldSkipLiquid = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.skipLiquid().Should().BeTrue();

            bridge.skipLiquid(false);

            tile.ShouldSkipLiquid.Should().BeFalse();
        }

        [Fact]
        public void nactive_IsCorrect() {
            var tile = new Tile {IsBlockActive = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.nactive().Should().BeTrue();

            tile.IsBlockActuated = true;

            bridge.nactive().Should().BeFalse();
        }

        [Theory]
        [InlineData(Slope.None, false)]
        [InlineData(Slope.TopLeft, true)]
        [InlineData(Slope.TopRight, true)]
        [InlineData(Slope.BottomLeft, false)]
        [InlineData(Slope.BottomRight, false)]
        public void topSlope_IsCorrect(Slope slope, bool value) {
            var tile = new Tile {Slope = slope};
            var bridge = new TileToITileBridge(&tile);

            bridge.topSlope().Should().Be(value);
        }

        [Theory]
        [InlineData(Slope.None, false)]
        [InlineData(Slope.TopLeft, false)]
        [InlineData(Slope.TopRight, false)]
        [InlineData(Slope.BottomLeft, true)]
        [InlineData(Slope.BottomRight, true)]
        public void bottomSlope_IsCorrect(Slope slope, bool value) {
            var tile = new Tile {Slope = slope};
            var bridge = new TileToITileBridge(&tile);

            bridge.bottomSlope().Should().Be(value);
        }

        [Theory]
        [InlineData(Slope.None, false)]
        [InlineData(Slope.TopLeft, true)]
        [InlineData(Slope.TopRight, false)]
        [InlineData(Slope.BottomLeft, true)]
        [InlineData(Slope.BottomRight, false)]
        public void leftSlope_IsCorrect(Slope slope, bool value) {
            var tile = new Tile {Slope = slope};
            var bridge = new TileToITileBridge(&tile);

            bridge.leftSlope().Should().Be(value);
        }

        [Theory]
        [InlineData(Slope.None, false)]
        [InlineData(Slope.TopLeft, false)]
        [InlineData(Slope.TopRight, true)]
        [InlineData(Slope.BottomLeft, false)]
        [InlineData(Slope.BottomRight, true)]
        public void rightSlope_IsCorrect(Slope slope, bool value) {
            var tile = new Tile {Slope = slope};
            var bridge = new TileToITileBridge(&tile);

            bridge.rightSlope().Should().Be(value);
        }

        [Fact]
        public void HasSameSlope_IsCorrect() {
            var tile = new Tile {Slope = Slope.BottomRight};
            var bridge = new TileToITileBridge(&tile);
            var tile2 = new Tile {Slope = Slope.BottomRight};
            var bridge2 = new TileToITileBridge(&tile2);

            bridge.HasSameSlope(bridge2).Should().BeTrue();

            tile2.Slope = Slope.BottomLeft;

            bridge.HasSameSlope(bridge2).Should().BeFalse();
        }

        [Fact]
        public void blockType_IsCorrect() {
            var tile = new Tile();
            var bridge = new TileToITileBridge(&tile);

            bridge.blockType().Should().Be(0);
        }

        [Fact]
        public void blockType_Halved_IsCorrect() {
            var tile = new Tile {IsBlockHalved = true};
            var bridge = new TileToITileBridge(&tile);

            bridge.blockType().Should().Be(1);
        }

        [Fact]
        public void blockType_Slope_IsCorrect() {
            var tile = new Tile {Slope = Slope.BottomRight};
            var bridge = new TileToITileBridge(&tile);

            bridge.blockType().Should().Be((int)Slope.BottomRight + 1);
        }
    }
}
