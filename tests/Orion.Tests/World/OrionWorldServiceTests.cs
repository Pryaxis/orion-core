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

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
using Orion.World.Tiles;
using OTAPI.Tile;
using Xunit;
using Main = Terraria.Main;

namespace Orion.World {
    [Collection("TerrariaTestsCollection")]
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionWorldServiceTests : IDisposable {
        private readonly IWorldService _worldService;

        public OrionWorldServiceTests() {
            _worldService = new OrionWorldService();
        }

        public void Dispose() => _worldService.Dispose();

        [Fact]
        public void WorldWidth_Get() {
            Main.maxTilesX = 1000;

            _worldService.WorldWidth.Should().Be(1000);
        }

        [Fact]
        public void WorldHeight_Get() {
            Main.maxTilesY = 1000;

            _worldService.WorldHeight.Should().Be(1000);
        }

        [Fact]
        public void CurrentInvasionType_Get() {
            Main.invasionType = (int)InvasionType.Goblins;

            _worldService.CurrentInvasionType.Should().Be(InvasionType.Goblins);
        }

        [Fact]
        public void Maintile_Gettype() {
            _worldService[0, 0] = new Tile { BlockType = BlockType.Stone };

            Main.tile[0, 0].type.Should().Be((ushort)BlockType.Stone);
        }

        [Fact]
        public void Maintile_Settype() {
            Main.tile[0, 0].type = (ushort)BlockType.Stone;

            _worldService[0, 0].BlockType.Should().Be(BlockType.Stone);
        }

        [Fact]
        public void Maintile_Getwall() {
            _worldService[0, 0] = new Tile { WallType = WallType.Stone };

            Main.tile[0, 0].wall.Should().Be((byte)WallType.Stone);
        }

        [Fact]
        public void Maintile_Setwall() {
            Main.tile[0, 0].wall = (byte)WallType.Stone;

            _worldService[0, 0].WallType.Should().Be(WallType.Stone);
        }

        [Fact]
        public void Maintile_Getliquid() {
            _worldService[0, 0] = new Tile { LiquidAmount = 100 };

            Main.tile[0, 0].liquid.Should().Be(100);
        }

        [Fact]
        public void Maintile_Setliquid() {
            Main.tile[0, 0].liquid = 100;

            _worldService[0, 0].LiquidAmount.Should().Be(100);
        }

        [Fact]
        public void Maintile_GetsTileHeader() {
            _worldService[0, 0] = new Tile { _sTileHeader = 12345 };

            Main.tile[0, 0].sTileHeader.Should().Be(12345);
        }

        [Fact]
        public void Maintile_SetsTileHeader() {
            Main.tile[0, 0].sTileHeader = 12345;

            _worldService[0, 0]._sTileHeader.Should().Be(12345);
        }

        [Fact]
        public void Maintile_GetbTileHeader() {
            _worldService[0, 0] = new Tile { _bTileHeader = 100 };

            Main.tile[0, 0].bTileHeader.Should().Be(100);
        }

        [Fact]
        public void Maintile_SetbTileHeader() {
            Main.tile[0, 0].bTileHeader = 100;

            _worldService[0, 0]._bTileHeader.Should().Be(100);
        }

        [Fact]
        public void Maintile_GetbTileHeader3() {
            _worldService[0, 0] = new Tile { _bTileHeader2 = 100 };

            Main.tile[0, 0].bTileHeader3.Should().Be(100);
        }

        [Fact]
        public void Maintile_SetbTileHeader3() {
            Main.tile[0, 0].bTileHeader3 = 100;

            _worldService[0, 0]._bTileHeader2.Should().Be(100);
        }

        [Fact]
        public void Maintile_GetframeX() {
            _worldService[0, 0] = new Tile { BlockFrameX = 12345 };

            Main.tile[0, 0].frameX.Should().Be(12345);
        }

        [Fact]
        public void Maintile_SetframeX() {
            Main.tile[0, 0].frameX = 12345;

            _worldService[0, 0].BlockFrameX.Should().Be(12345);
        }

        [Fact]
        public void Maintile_GetframeY() {
            _worldService[0, 0] = new Tile { BlockFrameY = 12345 };

            Main.tile[0, 0].frameY.Should().Be(12345);
        }

        [Fact]
        public void Maintile_SetframeY() {
            Main.tile[0, 0].frameY = 12345;

            _worldService[0, 0].BlockFrameY.Should().Be(12345);
        }

        [Fact]
        public void Maintile_CopyFrom() {
            var mockTile = new Mock<ITile>();
            mockTile.SetupGet(t => t.type).Returns(1);
            mockTile.SetupGet(t => t.wall).Returns(2);
            mockTile.SetupGet(t => t.liquid).Returns(3);
            mockTile.SetupGet(t => t.sTileHeader).Returns(12345);
            mockTile.SetupGet(t => t.bTileHeader).Returns(100);
            mockTile.SetupGet(t => t.bTileHeader3).Returns(101);
            mockTile.SetupGet(t => t.frameX).Returns(4);
            mockTile.SetupGet(t => t.frameY).Returns(5);

            Main.tile[0, 0].CopyFrom(mockTile.Object);

            _worldService[0, 0].BlockType.Should().Be((BlockType)1);
            _worldService[0, 0].WallType.Should().Be((WallType)2);
            _worldService[0, 0].LiquidAmount.Should().Be(3);
            _worldService[0, 0]._sTileHeader.Should().Be(12345);
            _worldService[0, 0]._bTileHeader.Should().Be(100);
            _worldService[0, 0]._bTileHeader2.Should().Be(101);
            _worldService[0, 0].BlockFrameX.Should().Be(4);
            _worldService[0, 0].BlockFrameY.Should().Be(5);
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
        public void Maintile_CopyFrom_Bridge() {
            _worldService[0, 0] = new Tile {
                BlockType = (BlockType)1,
                WallType = (WallType)2,
                LiquidAmount = 3,
                _sTileHeader = 12345,
                _bTileHeader = 100,
                _bTileHeader2 = 101,
                BlockFrameX = 4,
                BlockFrameY = 5
            };

            Main.tile[0, 1].CopyFrom(Main.tile[0, 0]);

            _worldService[0, 1].BlockType.Should().Be((BlockType)1);
            _worldService[0, 1].WallType.Should().Be((WallType)2);
            _worldService[0, 1].LiquidAmount.Should().Be(3);
            _worldService[0, 1]._sTileHeader.Should().Be(12345);
            _worldService[0, 1]._bTileHeader.Should().Be(100);
            _worldService[0, 1]._bTileHeader2.Should().Be(101);
            _worldService[0, 1].BlockFrameX.Should().Be(4);
            _worldService[0, 1].BlockFrameY.Should().Be(5);
        }

        [Fact]
        public void Maintile_ClearEverything() {
            _worldService[0, 0] = new Tile {
                BlockType = (BlockType)1,
                WallType = (WallType)2,
                LiquidAmount = 3,
                _sTileHeader = 12345,
                _bTileHeader = 100,
                _bTileHeader2 = 101,
                BlockFrameX = 4,
                BlockFrameY = 5
            };

            Main.tile[0, 0].ClearEverything();

            _worldService[0, 0].BlockType.Should().Be(0);
            _worldService[0, 0].WallType.Should().Be(0);
            _worldService[0, 0].LiquidAmount.Should().Be(0);
            _worldService[0, 0]._sTileHeader.Should().Be(0);
            _worldService[0, 0]._bTileHeader.Should().Be(0);
            _worldService[0, 0]._bTileHeader2.Should().Be(0);
            _worldService[0, 0].BlockFrameX.Should().Be(0);
            _worldService[0, 0].BlockFrameY.Should().Be(0);
        }

        [Fact]
        public void Maintile_ClearTile() {
            _worldService[0, 0] = new Tile {
                Slope = Slope.BottomRight,
                IsBlockHalved = true,
                IsBlockActive = true
            };

            Main.tile[0, 0].ClearTile();

            _worldService[0, 0].Slope.Should().Be(0);
            _worldService[0, 0].IsBlockHalved.Should().BeFalse();
            _worldService[0, 0].IsBlockActive.Should().BeFalse();
        }

        [Fact]
        public void Maintile_ResetToType() {
            _worldService[0, 0] = new Tile {
                BlockType = (BlockType)1,
                WallType = (WallType)2,
                LiquidAmount = 3,
                _sTileHeader = 12345,
                _bTileHeader = 100,
                _bTileHeader2 = 101,
                BlockFrameX = 4,
                BlockFrameY = 5
            };

            Main.tile[0, 0].ResetToType((ushort)BlockType.Stone);

            _worldService[0, 0].BlockType.Should().Be(BlockType.Stone);
            _worldService[0, 0].WallType.Should().Be(0);
            _worldService[0, 0].LiquidAmount.Should().Be(0);
            _worldService[0, 0]._sTileHeader.Should().Be(32);
            _worldService[0, 0]._bTileHeader.Should().Be(0);
            _worldService[0, 0]._bTileHeader2.Should().Be(0);
            _worldService[0, 0].BlockFrameX.Should().Be(0);
            _worldService[0, 0].BlockFrameY.Should().Be(0);
        }

        [Fact]
        public void Maintile_ClearMetadata() {
            _worldService[0, 0] = new Tile {
                BlockType = (BlockType)1,
                WallType = (WallType)2,
                LiquidAmount = 3,
                _sTileHeader = 12345,
                _bTileHeader = 100,
                _bTileHeader2 = 101,
                BlockFrameX = 4,
                BlockFrameY = 5
            };

            Main.tile[0, 0].ClearMetadata();

            _worldService[0, 0].BlockType.Should().Be((BlockType)1);
            _worldService[0, 0].WallType.Should().Be((WallType)2);
            _worldService[0, 0].LiquidAmount.Should().Be(0);
            _worldService[0, 0]._sTileHeader.Should().Be(0);
            _worldService[0, 0]._bTileHeader.Should().Be(0);
            _worldService[0, 0]._bTileHeader2.Should().Be(0);
            _worldService[0, 0].BlockFrameX.Should().Be(0);
            _worldService[0, 0].BlockFrameY.Should().Be(0);
        }

        [Fact]
        public void Maintile_isTheSameAs_NullTile_ReturnsFalse() =>
            Main.tile[0, 0].isTheSameAs(null).Should().BeFalse();

        [Fact]
        public void Maintile_isTheSameAs_DifferentSTileHeader_ReturnsFalse() {
            _worldService[0, 0] = new Tile { IsBlockActive = true };
            _worldService[0, 1] = new Tile();

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentTypeAndActive_ReturnsFalse() {
            _worldService[0, 0] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Stone
            };
            _worldService[0, 1] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Dirt
            };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentTypeNotActive_ReturnsTrue() {
            _worldService[0, 0] = new Tile { BlockType = BlockType.Stone };
            _worldService[0, 1] = new Tile { BlockType = BlockType.Dirt };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeTrue();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentFrameXAndImportant_ReturnsFalse() {
            _worldService[0, 0] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameX = 1
            };
            _worldService[0, 1] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameX = 2
            };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentFrameYAndImportant_ReturnsFalse() {
            _worldService[0, 0] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameY = 1
            };
            _worldService[0, 1] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameY = 2
            };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentFramesNotImportant_ReturnsTrue() {
            _worldService[0, 0] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Stone,
                BlockFrameX = 1,
                BlockFrameY = 1
            };
            _worldService[0, 1] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Stone,
                BlockFrameX = 2,
                BlockFrameY = 2
            };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeTrue();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentWall_ReturnsFalse() {
            _worldService[0, 0] = new Tile { WallType = WallType.Stone };
            _worldService[0, 1] = new Tile { WallType = WallType.NaturalDirt };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentLiquid_ReturnsFalse() {
            _worldService[0, 0] = new Tile { LiquidAmount = 1 };
            _worldService[0, 1] = new Tile { LiquidAmount = 2 };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentWallColor_ReturnsFalse() {
            _worldService[0, 0] = new Tile { WallColor = PaintColor.Red };
            _worldService[0, 1] = new Tile { WallColor = PaintColor.DeepRed };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentYellowWire_ReturnsFalse() {
            _worldService[0, 0] = new Tile { HasYellowWire = true };
            _worldService[0, 1] = new Tile();

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentBTileHeader_ReturnsFalse() {
            _worldService[0, 0] = new Tile {
                LiquidAmount = 1,
                _bTileHeader = 1
            };
            _worldService[0, 1] = new Tile {
                LiquidAmount = 1,
                _bTileHeader = 2
            };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_color() {
            _worldService[0, 0] = new Tile { BlockColor = PaintColor.Red };

            Main.tile[0, 0].color().Should().Be((byte)PaintColor.Red);

            Main.tile[0, 0].color((byte)PaintColor.DeepRed);

            _worldService[0, 0].BlockColor.Should().Be(PaintColor.DeepRed);
        }

        [Fact]
        public void Maintile_active() {
            _worldService[0, 0] = new Tile { IsBlockActive = true };

            Main.tile[0, 0].active().Should().BeTrue();

            Main.tile[0, 0].active(false);

            _worldService[0, 0].IsBlockActive.Should().BeFalse();
        }

        [Fact]
        public void Maintile_inActive() {
            _worldService[0, 0] = new Tile { IsBlockActuated = true };

            Main.tile[0, 0].inActive().Should().BeTrue();

            Main.tile[0, 0].inActive(false);

            _worldService[0, 0].IsBlockActuated.Should().BeFalse();
        }

        [Fact]
        public void Maintile_wire() {
            _worldService[0, 0] = new Tile { HasRedWire = true };

            Main.tile[0, 0].wire().Should().BeTrue();

            Main.tile[0, 0].wire(false);

            _worldService[0, 0].HasRedWire.Should().BeFalse();
        }

        [Fact]
        public void Maintile_wire2() {
            _worldService[0, 0] = new Tile { HasBlueWire = true };

            Main.tile[0, 0].wire2().Should().BeTrue();

            Main.tile[0, 0].wire2(false);

            _worldService[0, 0].HasBlueWire.Should().BeFalse();
        }

        [Fact]
        public void Maintile_wire3() {
            _worldService[0, 0] = new Tile { HasGreenWire = true };

            Main.tile[0, 0].wire3().Should().BeTrue();

            Main.tile[0, 0].wire3(false);

            _worldService[0, 0].HasGreenWire.Should().BeFalse();
        }

        [Fact]
        public void Maintile_halfBrick() {
            _worldService[0, 0] = new Tile { IsBlockHalved = true };

            Main.tile[0, 0].halfBrick().Should().BeTrue();

            Main.tile[0, 0].halfBrick(false);

            _worldService[0, 0].IsBlockHalved.Should().BeFalse();
        }

        [Fact]
        public void Maintile_actuator() {
            _worldService[0, 0] = new Tile { HasActuator = true };

            Main.tile[0, 0].actuator().Should().BeTrue();

            Main.tile[0, 0].actuator(false);

            _worldService[0, 0].HasActuator.Should().BeFalse();
        }

        [Fact]
        public void Maintile_slope() {
            _worldService[0, 0] = new Tile { Slope = Slope.BottomRight };

            Main.tile[0, 0].slope().Should().Be((byte)Slope.BottomRight);

            Main.tile[0, 0].slope((byte)Slope.BottomLeft);

            _worldService[0, 0].Slope.Should().Be(Slope.BottomLeft);
        }

        [Fact]
        public void Maintile_wallColor() {
            _worldService[0, 0] = new Tile { WallColor = PaintColor.Red };

            Main.tile[0, 0].wallColor().Should().Be((byte)PaintColor.Red);

            Main.tile[0, 0].wallColor((byte)PaintColor.DeepRed);

            _worldService[0, 0].WallColor.Should().Be(PaintColor.DeepRed);
        }

        [Fact]
        public void Maintile_lava() {
            _worldService[0, 0] = new Tile { IsLava = true };

            Main.tile[0, 0].lava().Should().BeTrue();

            Main.tile[0, 0].lava(false);

            _worldService[0, 0].IsLava.Should().BeFalse();
        }

        [Fact]
        public void Maintile_honey() {
            _worldService[0, 0] = new Tile { IsHoney = true };

            Main.tile[0, 0].honey().Should().BeTrue();

            Main.tile[0, 0].honey(false);

            _worldService[0, 0].IsHoney.Should().BeFalse();
        }

        [Fact]
        public void Maintile_liquidType() {
            _worldService[0, 0] = new Tile { LiquidType = LiquidType.Lava };

            Main.tile[0, 0].liquidType().Should().Be((byte)LiquidType.Lava);

            Main.tile[0, 0].liquidType((int)LiquidType.Honey);

            _worldService[0, 0].LiquidType.Should().Be(LiquidType.Honey);
        }

        [Fact]
        public void Maintile_wire4() {
            _worldService[0, 0] = new Tile { HasYellowWire = true };

            Main.tile[0, 0].wire4().Should().BeTrue();

            Main.tile[0, 0].wire4(false);

            _worldService[0, 0].HasYellowWire.Should().BeFalse();
        }

        [Fact]
        public void Maintile_checkingLiquid() {
            _worldService[0, 0] = new Tile { IsCheckingLiquid = true };

            Main.tile[0, 0].checkingLiquid().Should().BeTrue();

            Main.tile[0, 0].checkingLiquid(false);

            _worldService[0, 0].IsCheckingLiquid.Should().BeFalse();
        }

        [Fact]
        public void Maintile_skipLiquid() {
            _worldService[0, 0] = new Tile { ShouldSkipLiquid = true };

            Main.tile[0, 0].skipLiquid().Should().BeTrue();

            Main.tile[0, 0].skipLiquid(false);

            _worldService[0, 0].ShouldSkipLiquid.Should().BeFalse();
        }

        [Fact]
        public void Maintile_nactive() {
            _worldService[0, 0] = new Tile { IsBlockActive = true };

            Main.tile[0, 0].nactive().Should().BeTrue();

            _worldService[0, 0].IsBlockActuated = true;

            Main.tile[0, 0].nactive().Should().BeFalse();
        }

        [Theory]
        [InlineData(Slope.None, false)]
        [InlineData(Slope.TopLeft, true)]
        [InlineData(Slope.TopRight, true)]
        [InlineData(Slope.BottomLeft, false)]
        [InlineData(Slope.BottomRight, false)]
        public void Maintile_topSlope(Slope slope, bool value) {
            _worldService[0, 0] = new Tile { Slope = slope };

            Main.tile[0, 0].topSlope().Should().Be(value);
        }

        [Theory]
        [InlineData(Slope.None, false)]
        [InlineData(Slope.TopLeft, false)]
        [InlineData(Slope.TopRight, false)]
        [InlineData(Slope.BottomLeft, true)]
        [InlineData(Slope.BottomRight, true)]
        public void Maintile_bottomSlope(Slope slope, bool value) {
            _worldService[0, 0] = new Tile { Slope = slope };

            Main.tile[0, 0].bottomSlope().Should().Be(value);
        }

        [Theory]
        [InlineData(Slope.None, false)]
        [InlineData(Slope.TopLeft, true)]
        [InlineData(Slope.TopRight, false)]
        [InlineData(Slope.BottomLeft, true)]
        [InlineData(Slope.BottomRight, false)]
        public void Maintile_leftSlope(Slope slope, bool value) {
            _worldService[0, 0] = new Tile { Slope = slope };

            Main.tile[0, 0].leftSlope().Should().Be(value);
        }

        [Theory]
        [InlineData(Slope.None, false)]
        [InlineData(Slope.TopLeft, false)]
        [InlineData(Slope.TopRight, true)]
        [InlineData(Slope.BottomLeft, false)]
        [InlineData(Slope.BottomRight, true)]
        public void Maintile_rightSlope(Slope slope, bool value) {
            _worldService[0, 0] = new Tile { Slope = slope };

            Main.tile[0, 0].rightSlope().Should().Be(value);
        }

        [Fact]
        public void Maintile_HasSameSlope() {
            _worldService[0, 0] = new Tile { Slope = Slope.BottomRight };
            _worldService[0, 1] = new Tile { Slope = Slope.BottomRight };

            Main.tile[0, 0].HasSameSlope(Main.tile[0, 1]).Should().BeTrue();

            _worldService[0, 1].Slope = Slope.BottomLeft;

            Main.tile[0, 0].HasSameSlope(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_blockType() {
            _worldService[0, 0] = new Tile();

            Main.tile[0, 0].blockType().Should().Be(0);
        }

        [Fact]
        public void Maintile_blockType_Halved() {
            _worldService[0, 0] = new Tile { IsBlockHalved = true };

            Main.tile[0, 0].blockType().Should().Be(1);
        }

        [Fact]
        public void Maintile_blockType_Slope() {
            _worldService[0, 0] = new Tile { Slope = Slope.BottomRight };

            Main.tile[0, 0].blockType().Should().Be((int)Slope.BottomRight + 1);
        }
    }
}
