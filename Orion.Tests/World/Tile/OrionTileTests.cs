using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Moq;
using Orion.World;
using Orion.World.TileEntities;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Tests.World.Tile {
    [Collection("TerrariaTestsCollection")]
    public class OrionTileTests : IDisposable {
        private readonly OrionWorldService _worldService;

        public OrionTileTests() {
            var mockChestService = new Mock<IChestService>();
            var mockSignService = new Mock<ISignService>();
            _worldService = new OrionWorldService(mockChestService.Object, mockSignService.Object);
        }

        public void Dispose() {
            _worldService.Dispose();
        }

        [Theory]
        [InlineData(BlockType.Stone)]
        public void GetBlockType_IsCorrect(BlockType blockType) {
            Terraria.Main.tile[100, 100].type = (ushort)blockType;

            var tile = _worldService[100, 100];

            tile.BlockType.Should().Be(blockType);
        }

        [Theory]
        [InlineData(BlockType.Stone)]
        public void SetBlockType_IsCorrect(BlockType blockType) {
            var tile = _worldService[100, 100];

            tile.BlockType = blockType;

            Terraria.Main.tile[100, 100].type.Should().Be((ushort)blockType);
        }

        [Theory]
        [InlineData(WallType.Stone)]
        public void GetWallType_IsCorrect(WallType wallType) {
            Terraria.Main.tile[100, 100].wall = (byte)wallType;

            var tile = _worldService[100, 100];

            tile.WallType.Should().Be(wallType);
        }

        [Theory]
        [InlineData(WallType.Stone)]
        public void SetWallType_IsCorrect(WallType wallType) {
            var tile = _worldService[100, 100];

            tile.WallType = wallType;

            Terraria.Main.tile[100, 100].wall.Should().Be((byte)wallType);
        }

        [Theory]
        [InlineData(100)]
        public void GetLiquidAmount_IsCorrect(byte liquidAmount) {
            Terraria.Main.tile[100, 100].liquid = liquidAmount;

            var tile = _worldService[100, 100];

            tile.LiquidAmount.Should().Be(liquidAmount);
        }

        [Theory]
        [InlineData(100)]
        public void SetLiquidAmount_IsCorrect(byte liquidAmount) {
            var tile = _worldService[100, 100];

            tile.LiquidAmount = liquidAmount;

            Terraria.Main.tile[100, 100].liquid.Should().Be(liquidAmount);
        }

        [Theory]
        [InlineData(100)]
        public void GetBlockFrameX_IsCorrect(byte blockFrameX) {
            Terraria.Main.tile[100, 100].frameX = blockFrameX;

            var tile = _worldService[100, 100];

            tile.BlockFrameX.Should().Be(blockFrameX);
        }

        [Theory]
        [InlineData(100)]
        public void SetBlockFrameX_IsCorrect(byte blockFrameX) {
            var tile = _worldService[100, 100];

            tile.BlockFrameX = blockFrameX;

            Terraria.Main.tile[100, 100].frameX.Should().Be(blockFrameX);
        }

        [Theory]
        [InlineData(100)]
        public void GetBlockFrameY_IsCorrect(byte blockFrameY) {
            Terraria.Main.tile[100, 100].frameY = blockFrameY;

            var tile = _worldService[100, 100];

            tile.BlockFrameY.Should().Be(blockFrameY);
        }

        [Theory]
        [InlineData(100)]
        public void SetBlockFrameY_IsCorrect(byte blockFrameY) {
            var tile = _worldService[100, 100];

            tile.BlockFrameY = blockFrameY;

            Terraria.Main.tile[100, 100].frameY.Should().Be(blockFrameY);
        }

        [Theory]
        [InlineData(PaintColor.Red)]
        public void GetBlockColor_IsCorrect(PaintColor blockColor) {
            Terraria.Main.tile[100, 100].color((byte)blockColor);

            var tile = _worldService[100, 100];

            tile.BlockColor.Should().Be(blockColor);
        }

        [Theory]
        [InlineData(PaintColor.Red)]
        public void SetBlockColor_IsCorrect(PaintColor blockColor) {
            var tile = _worldService[100, 100];

            tile.BlockColor = blockColor;

            Terraria.Main.tile[100, 100].color().Should().Be((byte)blockColor);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsBlockActive_IsCorrect(bool isBlockActive) {
            Terraria.Main.tile[100, 100].active(isBlockActive);

            var tile = _worldService[100, 100];

            tile.IsBlockActive.Should().Be(isBlockActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsBlockActive_IsCorrect(bool isBlockActive) {
            var tile = _worldService[100, 100];

            tile.IsBlockActive = isBlockActive;

            Terraria.Main.tile[100, 100].active().Should().Be(isBlockActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsBlockActuated_IsCorrect(bool isBlockActuated) {
            Terraria.Main.tile[100, 100].inActive(isBlockActuated);

            var tile = _worldService[100, 100];

            tile.IsBlockActuated.Should().Be(isBlockActuated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsBlockActuated_IsCorrect(bool isBlockActuated) {
            var tile = _worldService[100, 100];

            tile.IsBlockActuated = isBlockActuated;

            Terraria.Main.tile[100, 100].inActive().Should().Be(isBlockActuated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetHasRedWire_IsCorrect(bool hasRedWire) {
            Terraria.Main.tile[100, 100].wire(hasRedWire);

            var tile = _worldService[100, 100];

            tile.HasRedWire.Should().Be(hasRedWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetHasRedWire_IsCorrect(bool hasRedWire) {
            var tile = _worldService[100, 100];

            tile.HasRedWire = hasRedWire;

            Terraria.Main.tile[100, 100].wire().Should().Be(hasRedWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetHasBlueWire_IsCorrect(bool hasBlueWire) {
            Terraria.Main.tile[100, 100].wire2(hasBlueWire);

            var tile = _worldService[100, 100];

            tile.HasBlueWire.Should().Be(hasBlueWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetHasBlueWire_IsCorrect(bool hasBlueWire) {
            var tile = _worldService[100, 100];

            tile.HasBlueWire = hasBlueWire;

            Terraria.Main.tile[100, 100].wire2().Should().Be(hasBlueWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetHasGreenWire_IsCorrect(bool hasGreenWire) {
            Terraria.Main.tile[100, 100].wire3(hasGreenWire);

            var tile = _worldService[100, 100];

            tile.HasGreenWire.Should().Be(hasGreenWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetHasGreenWire_IsCorrect(bool hasGreenWire) {
            var tile = _worldService[100, 100];

            tile.HasGreenWire = hasGreenWire;

            Terraria.Main.tile[100, 100].wire3().Should().Be(hasGreenWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsBlockHalved_IsCorrect(bool isBlockHalved) {
            Terraria.Main.tile[100, 100].halfBrick(isBlockHalved);

            var tile = _worldService[100, 100];

            tile.IsBlockHalved.Should().Be(isBlockHalved);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsBlockHalved_IsCorrect(bool isBlockHalved) {
            var tile = _worldService[100, 100];

            tile.IsBlockHalved = isBlockHalved;

            Terraria.Main.tile[100, 100].halfBrick().Should().Be(isBlockHalved);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetHasActuator_IsCorrect(bool hasActuator) {
            Terraria.Main.tile[100, 100].actuator(hasActuator);

            var tile = _worldService[100, 100];

            tile.HasActuator.Should().Be(hasActuator);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetHasActuator_IsCorrect(bool hasActuator) {
            var tile = _worldService[100, 100];

            tile.HasActuator = hasActuator;

            Terraria.Main.tile[100, 100].actuator().Should().Be(hasActuator);
        }

        [Theory]
        [InlineData(SlopeType.TopLeft)]
        public void GetSlopeType_IsCorrect(SlopeType slopeType) {
            Terraria.Main.tile[100, 100].slope((byte)slopeType);

            var tile = _worldService[100, 100];

            tile.SlopeType.Should().Be(slopeType);
        }

        [Theory]
        [InlineData(SlopeType.TopLeft)]
        public void SetSlopeType_IsCorrect(SlopeType slopeType) {
            var tile = _worldService[100, 100];

            tile.SlopeType = slopeType;

            Terraria.Main.tile[100, 100].slope().Should().Be((byte)slopeType);
        }

        [Theory]
        [InlineData(PaintColor.Red)]
        public void GetWallColor_IsCorrect(PaintColor wallColor) {
            Terraria.Main.tile[100, 100].wallColor((byte)wallColor);

            var tile = _worldService[100, 100];

            tile.WallColor.Should().Be(wallColor);
        }

        [Theory]
        [InlineData(PaintColor.Red)]
        public void SetWallColor_IsCorrect(PaintColor wallColor) {
            var tile = _worldService[100, 100];

            tile.WallColor = wallColor;

            Terraria.Main.tile[100, 100].wallColor().Should().Be((byte)wallColor);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsLava_IsCorrect(bool isLava) {
            Terraria.Main.tile[100, 100].lava(isLava);

            var tile = _worldService[100, 100];

            tile.IsLava.Should().Be(isLava);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsLava_IsCorrect(bool isLava) {
            var tile = _worldService[100, 100];

            tile.IsLava = isLava;

            Terraria.Main.tile[100, 100].lava().Should().Be(isLava);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsHoney_IsCorrect(bool isHoney) {
            Terraria.Main.tile[100, 100].honey(isHoney);

            var tile = _worldService[100, 100];

            tile.IsHoney.Should().Be(isHoney);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsHoney_IsCorrect(bool isHoney) {
            var tile = _worldService[100, 100];

            tile.IsHoney = isHoney;

            Terraria.Main.tile[100, 100].honey().Should().Be(isHoney);
        }

        [Theory]
        [InlineData(LiquidType.Water)]
        [InlineData(LiquidType.Lava)]
        [InlineData(LiquidType.Honey)]
        public void GetLiquidType_IsCorrect(LiquidType liquidType) {
            Terraria.Main.tile[100, 100].liquidType((int)liquidType);

            var tile = _worldService[100, 100];

            tile.LiquidType.Should().Be(liquidType);
        }

        [Theory]
        [InlineData(LiquidType.Water)]
        [InlineData(LiquidType.Lava)]
        [InlineData(LiquidType.Honey)]
        public void SetLiquidType_IsCorrect(LiquidType liquidType) {
            var tile = _worldService[100, 100];

            tile.LiquidType = liquidType;

            Terraria.Main.tile[100, 100].liquidType().Should().Be((byte)liquidType);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetHasYellowWire_IsCorrect(bool hasYellowWire) {
            Terraria.Main.tile[100, 100].wire4(hasYellowWire);

            var tile = _worldService[100, 100];

            tile.HasYellowWire.Should().Be(hasYellowWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetHasYellowWire_IsCorrect(bool hasYellowWire) {
            var tile = _worldService[100, 100];

            tile.HasYellowWire = hasYellowWire;

            Terraria.Main.tile[100, 100].wire4().Should().Be(hasYellowWire);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsCheckingLiquid_IsCorrect(bool isCheckingLiquid) {
            Terraria.Main.tile[100, 100].checkingLiquid(isCheckingLiquid);

            var tile = _worldService[100, 100];

            tile.IsCheckingLiquid.Should().Be(isCheckingLiquid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsCheckingLiquid_IsCorrect(bool isCheckingLiquid) {
            var tile = _worldService[100, 100];

            tile.IsCheckingLiquid = isCheckingLiquid;

            Terraria.Main.tile[100, 100].checkingLiquid().Should().Be(isCheckingLiquid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetShouldSkipLiquid_IsCorrect(bool shouldSkipLiquid) {
            Terraria.Main.tile[100, 100].skipLiquid(shouldSkipLiquid);

            var tile = _worldService[100, 100];

            tile.ShouldSkipLiquid.Should().Be(shouldSkipLiquid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetShouldSkipLiquid_IsCorrect(bool shouldSkipLiquid) {
            var tile = _worldService[100, 100];

            tile.ShouldSkipLiquid = shouldSkipLiquid;

            Terraria.Main.tile[100, 100].skipLiquid().Should().Be(shouldSkipLiquid);
        }
    }
}
