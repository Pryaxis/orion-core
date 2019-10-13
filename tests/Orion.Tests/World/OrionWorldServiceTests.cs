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

using FluentAssertions;
using Moq;
using Orion.World.Tiles;
using OTAPI.Tile;
using Serilog.Core;
using Xunit;
using Main = Terraria.Main;

namespace Orion.World {
    [Collection("TerrariaTestsCollection")]
    public class OrionWorldServiceTests {
        [Fact]
        public void WorldWidth_Get() {
            using var worldService = new OrionWorldService(Logger.None);
            Main.maxTilesX = 1000;

            worldService.WorldWidth.Should().Be(1000);
        }

        [Fact]
        public void WorldHeight_Get() {
            using var worldService = new OrionWorldService(Logger.None);
            Main.maxTilesY = 1000;

            worldService.WorldHeight.Should().Be(1000);
        }

        [Fact]
        public void CurrentInvasionType_Get() {
            using var worldService = new OrionWorldService(Logger.None);
            Main.invasionType = (int)InvasionType.Goblins;

            worldService.CurrentInvasionType.Should().Be(InvasionType.Goblins);
        }

        [Fact]
        public void Maintile_Gettype() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { BlockType = BlockType.Stone };

            Main.tile[0, 0].type.Should().Be((ushort)BlockType.Stone);
        }

        [Fact]
        public void Maintile_Settype() {
            using var worldService = new OrionWorldService(Logger.None);
            Main.tile[0, 0].type = (ushort)BlockType.Stone;

            worldService[0, 0].BlockType.Should().Be(BlockType.Stone);
        }

        [Fact]
        public void Maintile_Getwall() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { WallType = WallType.Stone };

            Main.tile[0, 0].wall.Should().Be((byte)WallType.Stone);
        }

        [Fact]
        public void Maintile_Setwall() {
            using var worldService = new OrionWorldService(Logger.None);
            Main.tile[0, 0].wall = (byte)WallType.Stone;

            worldService[0, 0].WallType.Should().Be(WallType.Stone);
        }

        [Fact]
        public void Maintile_Getliquid() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { LiquidAmount = 100 };

            Main.tile[0, 0].liquid.Should().Be(100);
        }

        [Fact]
        public void Maintile_Setliquid() {
            using var worldService = new OrionWorldService(Logger.None);
            Main.tile[0, 0].liquid = 100;

            worldService[0, 0].LiquidAmount.Should().Be(100);
        }

        [Fact]
        public void Maintile_GetsTileHeader() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { _sTileHeader = 12345 };

            Main.tile[0, 0].sTileHeader.Should().Be(12345);
        }

        [Fact]
        public void Maintile_SetsTileHeader() {
            using var worldService = new OrionWorldService(Logger.None);
            Main.tile[0, 0].sTileHeader = 12345;

            worldService[0, 0]._sTileHeader.Should().Be(12345);
        }

        [Fact]
        public void Maintile_GetbTileHeader() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { _bTileHeader = 100 };

            Main.tile[0, 0].bTileHeader.Should().Be(100);
        }

        [Fact]
        public void Maintile_SetbTileHeader() {
            using var worldService = new OrionWorldService(Logger.None);
            Main.tile[0, 0].bTileHeader = 100;

            worldService[0, 0]._bTileHeader.Should().Be(100);
        }

        [Fact]
        public void Maintile_GetbTileHeader3() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { _bTileHeader2 = 100 };

            Main.tile[0, 0].bTileHeader3.Should().Be(100);
        }

        [Fact]
        public void Maintile_SetbTileHeader3() {
            using var worldService = new OrionWorldService(Logger.None);
            Main.tile[0, 0].bTileHeader3 = 100;

            worldService[0, 0]._bTileHeader2.Should().Be(100);
        }

        [Fact]
        public void Maintile_GetframeX() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { BlockFrameX = 12345 };

            Main.tile[0, 0].frameX.Should().Be(12345);
        }

        [Fact]
        public void Maintile_SetframeX() {
            using var worldService = new OrionWorldService(Logger.None);
            Main.tile[0, 0].frameX = 12345;

            worldService[0, 0].BlockFrameX.Should().Be(12345);
        }

        [Fact]
        public void Maintile_GetframeY() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { BlockFrameY = 12345 };

            Main.tile[0, 0].frameY.Should().Be(12345);
        }

        [Fact]
        public void Maintile_SetframeY() {
            using var worldService = new OrionWorldService(Logger.None);
            Main.tile[0, 0].frameY = 12345;

            worldService[0, 0].BlockFrameY.Should().Be(12345);
        }

        [Fact]
        public void Maintile_CopyFrom() {
            using var worldService = new OrionWorldService(Logger.None);
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

            worldService[0, 0].BlockType.Should().Be((BlockType)1);
            worldService[0, 0].WallType.Should().Be((WallType)2);
            worldService[0, 0].LiquidAmount.Should().Be(3);
            worldService[0, 0]._sTileHeader.Should().Be(12345);
            worldService[0, 0]._bTileHeader.Should().Be(100);
            worldService[0, 0]._bTileHeader2.Should().Be(101);
            worldService[0, 0].BlockFrameX.Should().Be(4);
            worldService[0, 0].BlockFrameY.Should().Be(5);
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
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile {
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

            worldService[0, 1].BlockType.Should().Be((BlockType)1);
            worldService[0, 1].WallType.Should().Be((WallType)2);
            worldService[0, 1].LiquidAmount.Should().Be(3);
            worldService[0, 1]._sTileHeader.Should().Be(12345);
            worldService[0, 1]._bTileHeader.Should().Be(100);
            worldService[0, 1]._bTileHeader2.Should().Be(101);
            worldService[0, 1].BlockFrameX.Should().Be(4);
            worldService[0, 1].BlockFrameY.Should().Be(5);
        }

        [Fact]
        public void Maintile_ClearEverything() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile {
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

            worldService[0, 0].BlockType.Should().Be(0);
            worldService[0, 0].WallType.Should().Be(0);
            worldService[0, 0].LiquidAmount.Should().Be(0);
            worldService[0, 0]._sTileHeader.Should().Be(0);
            worldService[0, 0]._bTileHeader.Should().Be(0);
            worldService[0, 0]._bTileHeader2.Should().Be(0);
            worldService[0, 0].BlockFrameX.Should().Be(0);
            worldService[0, 0].BlockFrameY.Should().Be(0);
        }

        [Fact]
        public void Maintile_ClearTile() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile {
                Slope = Slope.BottomRight,
                IsBlockHalved = true,
                IsBlockActive = true
            };

            Main.tile[0, 0].ClearTile();

            worldService[0, 0].Slope.Should().Be(0);
            worldService[0, 0].IsBlockHalved.Should().BeFalse();
            worldService[0, 0].IsBlockActive.Should().BeFalse();
        }

        [Fact]
        public void Maintile_ResetToType() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile {
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

            worldService[0, 0].BlockType.Should().Be(BlockType.Stone);
            worldService[0, 0].WallType.Should().Be(0);
            worldService[0, 0].LiquidAmount.Should().Be(0);
            worldService[0, 0]._sTileHeader.Should().Be(32);
            worldService[0, 0]._bTileHeader.Should().Be(0);
            worldService[0, 0]._bTileHeader2.Should().Be(0);
            worldService[0, 0].BlockFrameX.Should().Be(0);
            worldService[0, 0].BlockFrameY.Should().Be(0);
        }

        [Fact]
        public void Maintile_ClearMetadata() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile {
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

            worldService[0, 0].BlockType.Should().Be((BlockType)1);
            worldService[0, 0].WallType.Should().Be((WallType)2);
            worldService[0, 0].LiquidAmount.Should().Be(0);
            worldService[0, 0]._sTileHeader.Should().Be(0);
            worldService[0, 0]._bTileHeader.Should().Be(0);
            worldService[0, 0]._bTileHeader2.Should().Be(0);
            worldService[0, 0].BlockFrameX.Should().Be(0);
            worldService[0, 0].BlockFrameY.Should().Be(0);
        }

        [Fact]
        public void Maintile_isTheSameAs_NullTile_ReturnsFalse() {
            using var worldService = new OrionWorldService(Logger.None);

            Main.tile[0, 0].isTheSameAs(null).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentSTileHeader_ReturnsFalse() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { IsBlockActive = true };
            worldService[0, 1] = new Tile();

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentTypeAndActive_ReturnsFalse() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Stone
            };
            worldService[0, 1] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Dirt
            };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentTypeNotActive_ReturnsTrue() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { BlockType = BlockType.Stone };
            worldService[0, 1] = new Tile { BlockType = BlockType.Dirt };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeTrue();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentFrameXAndImportant_ReturnsFalse() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameX = 1
            };
            worldService[0, 1] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameX = 2
            };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentFrameYAndImportant_ReturnsFalse() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameY = 1
            };
            worldService[0, 1] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Torches,
                BlockFrameY = 2
            };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentFramesNotImportant_ReturnsTrue() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Stone,
                BlockFrameX = 1,
                BlockFrameY = 1
            };
            worldService[0, 1] = new Tile {
                IsBlockActive = true,
                BlockType = BlockType.Stone,
                BlockFrameX = 2,
                BlockFrameY = 2
            };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeTrue();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentWall_ReturnsFalse() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { WallType = WallType.Stone };
            worldService[0, 1] = new Tile { WallType = WallType.NaturalDirt };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentLiquid_ReturnsFalse() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { LiquidAmount = 1 };
            worldService[0, 1] = new Tile { LiquidAmount = 2 };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentWallColor_ReturnsFalse() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { WallColor = PaintColor.Red };
            worldService[0, 1] = new Tile { WallColor = PaintColor.DeepRed };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentYellowWire_ReturnsFalse() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { HasYellowWire = true };
            worldService[0, 1] = new Tile();

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_isTheSameAs_DifferentBTileHeader_ReturnsFalse() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile {
                LiquidAmount = 1,
                _bTileHeader = 1
            };
            worldService[0, 1] = new Tile {
                LiquidAmount = 1,
                _bTileHeader = 2
            };

            Main.tile[0, 0].isTheSameAs(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_color() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { BlockColor = PaintColor.Red };

            Main.tile[0, 0].color().Should().Be((byte)PaintColor.Red);

            Main.tile[0, 0].color((byte)PaintColor.DeepRed);

            worldService[0, 0].BlockColor.Should().Be(PaintColor.DeepRed);
        }

        [Fact]
        public void Maintile_active() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { IsBlockActive = true };

            Main.tile[0, 0].active().Should().BeTrue();

            Main.tile[0, 0].active(false);

            worldService[0, 0].IsBlockActive.Should().BeFalse();
        }

        [Fact]
        public void Maintile_inActive() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { IsBlockActuated = true };

            Main.tile[0, 0].inActive().Should().BeTrue();

            Main.tile[0, 0].inActive(false);

            worldService[0, 0].IsBlockActuated.Should().BeFalse();
        }

        [Fact]
        public void Maintile_wire() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { HasRedWire = true };

            Main.tile[0, 0].wire().Should().BeTrue();

            Main.tile[0, 0].wire(false);

            worldService[0, 0].HasRedWire.Should().BeFalse();
        }

        [Fact]
        public void Maintile_wire2() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { HasBlueWire = true };

            Main.tile[0, 0].wire2().Should().BeTrue();

            Main.tile[0, 0].wire2(false);

            worldService[0, 0].HasBlueWire.Should().BeFalse();
        }

        [Fact]
        public void Maintile_wire3() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { HasGreenWire = true };

            Main.tile[0, 0].wire3().Should().BeTrue();

            Main.tile[0, 0].wire3(false);

            worldService[0, 0].HasGreenWire.Should().BeFalse();
        }

        [Fact]
        public void Maintile_halfBrick() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { IsBlockHalved = true };

            Main.tile[0, 0].halfBrick().Should().BeTrue();

            Main.tile[0, 0].halfBrick(false);

            worldService[0, 0].IsBlockHalved.Should().BeFalse();
        }

        [Fact]
        public void Maintile_actuator() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { HasActuator = true };

            Main.tile[0, 0].actuator().Should().BeTrue();

            Main.tile[0, 0].actuator(false);

            worldService[0, 0].HasActuator.Should().BeFalse();
        }

        [Fact]
        public void Maintile_slope() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { Slope = Slope.BottomRight };

            Main.tile[0, 0].slope().Should().Be((byte)Slope.BottomRight);

            Main.tile[0, 0].slope((byte)Slope.BottomLeft);

            worldService[0, 0].Slope.Should().Be(Slope.BottomLeft);
        }

        [Fact]
        public void Maintile_wallColor() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { WallColor = PaintColor.Red };

            Main.tile[0, 0].wallColor().Should().Be((byte)PaintColor.Red);

            Main.tile[0, 0].wallColor((byte)PaintColor.DeepRed);

            worldService[0, 0].WallColor.Should().Be(PaintColor.DeepRed);
        }

        [Fact]
        public void Maintile_lava() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { IsLava = true };

            Main.tile[0, 0].lava().Should().BeTrue();

            Main.tile[0, 0].lava(false);

            worldService[0, 0].IsLava.Should().BeFalse();
        }

        [Fact]
        public void Maintile_honey() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { IsHoney = true };

            Main.tile[0, 0].honey().Should().BeTrue();

            Main.tile[0, 0].honey(false);

            worldService[0, 0].IsHoney.Should().BeFalse();
        }

        [Fact]
        public void Maintile_liquidType() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { LiquidType = LiquidType.Lava };

            Main.tile[0, 0].liquidType().Should().Be((byte)LiquidType.Lava);

            Main.tile[0, 0].liquidType((int)LiquidType.Honey);

            worldService[0, 0].LiquidType.Should().Be(LiquidType.Honey);
        }

        [Fact]
        public void Maintile_wire4() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { HasYellowWire = true };

            Main.tile[0, 0].wire4().Should().BeTrue();

            Main.tile[0, 0].wire4(false);

            worldService[0, 0].HasYellowWire.Should().BeFalse();
        }

        [Fact]
        public void Maintile_checkingLiquid() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { IsCheckingLiquid = true };

            Main.tile[0, 0].checkingLiquid().Should().BeTrue();

            Main.tile[0, 0].checkingLiquid(false);

            worldService[0, 0].IsCheckingLiquid.Should().BeFalse();
        }

        [Fact]
        public void Maintile_skipLiquid() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { ShouldSkipLiquid = true };

            Main.tile[0, 0].skipLiquid().Should().BeTrue();

            Main.tile[0, 0].skipLiquid(false);

            worldService[0, 0].ShouldSkipLiquid.Should().BeFalse();
        }

        [Fact]
        public void Maintile_nactive() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { IsBlockActive = true };

            Main.tile[0, 0].nactive().Should().BeTrue();

            worldService[0, 0].IsBlockActuated = true;

            Main.tile[0, 0].nactive().Should().BeFalse();
        }

        [Theory]
        [InlineData(Slope.None, false)]
        [InlineData(Slope.TopLeft, true)]
        [InlineData(Slope.TopRight, true)]
        [InlineData(Slope.BottomLeft, false)]
        [InlineData(Slope.BottomRight, false)]
        public void Maintile_topSlope(Slope slope, bool value) {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { Slope = slope };

            Main.tile[0, 0].topSlope().Should().Be(value);
        }

        [Theory]
        [InlineData(Slope.None, false)]
        [InlineData(Slope.TopLeft, false)]
        [InlineData(Slope.TopRight, false)]
        [InlineData(Slope.BottomLeft, true)]
        [InlineData(Slope.BottomRight, true)]
        public void Maintile_bottomSlope(Slope slope, bool value) {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { Slope = slope };

            Main.tile[0, 0].bottomSlope().Should().Be(value);
        }

        [Theory]
        [InlineData(Slope.None, false)]
        [InlineData(Slope.TopLeft, true)]
        [InlineData(Slope.TopRight, false)]
        [InlineData(Slope.BottomLeft, true)]
        [InlineData(Slope.BottomRight, false)]
        public void Maintile_leftSlope(Slope slope, bool value) {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { Slope = slope };

            Main.tile[0, 0].leftSlope().Should().Be(value);
        }

        [Theory]
        [InlineData(Slope.None, false)]
        [InlineData(Slope.TopLeft, false)]
        [InlineData(Slope.TopRight, true)]
        [InlineData(Slope.BottomLeft, false)]
        [InlineData(Slope.BottomRight, true)]
        public void Maintile_rightSlope(Slope slope, bool value) {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { Slope = slope };

            Main.tile[0, 0].rightSlope().Should().Be(value);
        }

        [Fact]
        public void Maintile_HasSameSlope() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { Slope = Slope.BottomRight };
            worldService[0, 1] = new Tile { Slope = Slope.BottomRight };

            Main.tile[0, 0].HasSameSlope(Main.tile[0, 1]).Should().BeTrue();

            worldService[0, 1].Slope = Slope.BottomLeft;

            Main.tile[0, 0].HasSameSlope(Main.tile[0, 1]).Should().BeFalse();
        }

        [Fact]
        public void Maintile_blockType() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile();

            Main.tile[0, 0].blockType().Should().Be(0);
        }

        [Fact]
        public void Maintile_blockType_Halved() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { IsBlockHalved = true };

            Main.tile[0, 0].blockType().Should().Be(1);
        }

        [Fact]
        public void Maintile_blockType_Slope() {
            using var worldService = new OrionWorldService(Logger.None);
            worldService[0, 0] = new Tile { Slope = Slope.BottomRight };

            Main.tile[0, 0].blockType().Should().Be((int)Slope.BottomRight + 1);
        }
    }
}
