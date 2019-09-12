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
using Moq;
using Orion.World;
using Orion.World.TileEntities;
using Terraria;
using Terraria.ID;
using Xunit;
using TDS = Terraria.DataStructures;
using TGCTE = Terraria.GameContent.Tile_Entities;

namespace Orion.Tests.World {
    [Collection("TerrariaTestsCollection")]
    public class OrionWorldServiceTests : IDisposable {
        private readonly Mock<IChestService> _mockChestService;
        private readonly Mock<ISignService> _mockSignService;
        private readonly OrionWorldService _worldService;

        public OrionWorldServiceTests() {
            _mockChestService = new Mock<IChestService>();
            _mockSignService = new Mock<ISignService>();
            _worldService = new OrionWorldService(_mockChestService.Object, _mockSignService.Object);
        }

        public void Dispose() {
            _worldService.Dispose();
        }

        [Theory]
        [InlineData("test")]
        public void GetWorldName_IsCorrect(string worldName) {
            Main.worldName = worldName;

            _worldService.WorldName.Should().Be(worldName);
        }

        [Theory]
        [InlineData("test")]
        public void SetWorldName_IsCorrect(string worldName) {
            _worldService.WorldName = worldName;

            Main.worldName.Should().Be(worldName);
        }

        [Fact]
        public void SetWorldName_NullValue_ThrowsArgumentNullException() {
            Action action = () => _worldService.WorldName = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(1000)]
        public void GetWorldWidth_IsCorrect(int worldWidth) {
            Main.maxTilesX = worldWidth;

            _worldService.WorldWidth.Should().Be(worldWidth);
        }

        [Theory]
        [InlineData(1000)]
        public void GetWorldHeight_IsCorrect(int worldHeight) {
            Main.maxTilesY = worldHeight;

            _worldService.WorldHeight.Should().Be(worldHeight);
        }

        [Theory]
        [InlineData(100.0)]
        public void GetTime_IsCorrect(double time) {
            Main.time = time;

            _worldService.Time.Should().Be(time);
        }

        [Theory]
        [InlineData(100.0)]
        public void SetTime_IsCorrect(double time) {
            _worldService.Time = time;

            Main.time.Should().Be(time);
        }

        [Theory]
        [InlineData(true)]
        public void GetIsDaytime_IsCorrect(bool isDaytime) {
            Main.dayTime = isDaytime;

            _worldService.IsDaytime.Should().Be(isDaytime);
        }

        [Theory]
        [InlineData(true)]
        public void SetIsDaytime_IsCorrect(bool isDaytime) {
            _worldService.IsDaytime = isDaytime;

            Main.dayTime.Should().Be(isDaytime);
        }

        [Theory]
        [InlineData(true)]
        public void GetIsHardmode_IsCorrect(bool isHardmode) {
            Main.hardMode = isHardmode;

            _worldService.IsHardmode.Should().Be(isHardmode);
        }

        [Theory]
        [InlineData(true)]
        public void SetIsHardmode_IsCorrect(bool isHardmode) {
            _worldService.IsHardmode = isHardmode;

            Main.hardMode.Should().Be(isHardmode);
        }

        [Theory]
        [InlineData(true)]
        public void GetIsExpertMode_IsCorrect(bool isExpertMode) {
            Main.expertMode = isExpertMode;

            _worldService.IsExpertMode.Should().Be(isExpertMode);
        }

        [Theory]
        [InlineData(true)]
        public void SetIsExpertMode_IsCorrect(bool isExpertMode) {
            _worldService.IsExpertMode = isExpertMode;

            Main.expertMode.Should().Be(isExpertMode);
        }

        [Theory]
        [InlineData(InvasionType.FrostLegion)]
        public void GetCurrentInvasion_IsCorrect(InvasionType invasionType) {
            Main.invasionType = (int)invasionType;

            _worldService.CurrentInvasionType.Should().Be(invasionType);
        }

        [Fact]
        public void CheckingHalloween_IsCorrect() {
            var isRun = false;
            _worldService.CheckingHalloween += (sender, args) => {
                isRun = true;
            };

            Main.checkHalloween();

            isRun.Should().BeTrue();
        }

        // This test should basically never flake, since it's pretty hard for both runs to pass.
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CheckingHalloween_Handled_IsCorrect(bool halloween) {
            Main.halloween = halloween;
            _worldService.CheckingHalloween += (sender, args) => args.Handled = true;

            Main.checkHalloween();

            Main.halloween.Should().Be(halloween);
        }

        [Fact]
        public void CheckingChristmas_IsCorrect() {
            var isRun = false;
            _worldService.CheckingChristmas += (sender, args) => {
                isRun = true;
            };

            Main.checkXMas();

            isRun.Should().BeTrue();
        }

        // This test should basically never flake, since it's pretty hard for both runs to pass.
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CheckingChristmas_Handled_IsCorrect(bool christmas) {
            Main.xMas = christmas;
            _worldService.CheckingChristmas += (sender, args) => args.Handled = true;

            Main.checkXMas();

            Main.xMas.Should().Be(christmas);
        }

        [Theory]
        [InlineData(InvasionType.FrostLegion)]
        public void StartInvasion_NoCurrentInvasion_ReturnsTrue(InvasionType invasionType) {
            // Fake an active player with enough HP.
            Main.player[0].active = true;
            Main.player[0].statLifeMax = 200;
            Main.invasionType = 0;

            var result = _worldService.StartInvasion(invasionType);

            result.Should().BeTrue();
            Main.invasionType.Should().Be((int)invasionType);
        }

        [Theory]
        [InlineData(InvasionType.FrostLegion)]
        public void StartInvasion_CurrentInvasion_ReturnsFalse(InvasionType invasionType) {
            // Fake an active player with enough HP.
            Main.player[0].active = true;
            Main.player[0].statLifeMax = 200;
            Main.invasionType = 1;
            Main.invasionSize = 100;

            var result = _worldService.StartInvasion(invasionType);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(InvasionType.FrostLegion)]
        public void StartInvasion_NoPlayers_ReturnsFalse(InvasionType invasionType) {
            Main.player[0].active = false;
            Main.invasionType = 0;

            var result = _worldService.StartInvasion(invasionType);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(100, 100)]
        public void AddTargetDummy_IsCorrect(int x, int y) {
            var position = new TDS.Point16(x, y);
            TDS.TileEntity.ByPosition.Remove(position);

            var targetDummy = _worldService.AddTargetDummy(x, y);

            targetDummy.Should().NotBeNull();
            targetDummy.X.Should().Be(x);
            targetDummy.Y.Should().Be(y);
            var terrariaTileEntity = TDS.TileEntity.ByPosition[position];
            terrariaTileEntity.type.Should().Be(TileEntityID.TrainingDummy);
            terrariaTileEntity.Position.X.Should().Be((short)x);
            terrariaTileEntity.Position.Y.Should().Be((short)y);
        }

        [Theory]
        [InlineData(100, 100)]
        public void AddItemFrame_IsCorrect(int x, int y) {
            var position = new TDS.Point16(x, y);
            TDS.TileEntity.ByPosition.Remove(position);

            var itemFrame = _worldService.AddItemFrame(x, y);

            itemFrame.Should().NotBeNull();
            itemFrame.X.Should().Be(x);
            itemFrame.Y.Should().Be(y);
            var terrariaTileEntity = TDS.TileEntity.ByPosition[position];
            terrariaTileEntity.type.Should().Be(TileEntityID.ItemFrame);
            terrariaTileEntity.Position.X.Should().Be((short)x);
            terrariaTileEntity.Position.Y.Should().Be((short)y);
        }

        [Theory]
        [InlineData(100, 100)]
        public void AddLogicSensor_IsCorrect(int x, int y) {
            var position = new TDS.Point16(x, y);
            TDS.TileEntity.ByPosition.Remove(position);

            var logicSensor = _worldService.AddLogicSensor(x, y);

            logicSensor.Should().NotBeNull();
            logicSensor.X.Should().Be(x);
            logicSensor.Y.Should().Be(y);
            var terrariaTileEntity = TDS.TileEntity.ByPosition[position];
            terrariaTileEntity.type.Should().Be(TileEntityID.LogicSensor);
            terrariaTileEntity.Position.X.Should().Be((short)x);
            terrariaTileEntity.Position.Y.Should().Be((short)y);
        }

        [Theory]
        [InlineData(100, 100)]
        public void GetTileEntity_WithChest_IsCorrect(int x, int y) {
            var chest = new Mock<IChest>();
            _mockChestService.Setup(c => c.GetChest(x, y)).Returns(chest.Object);
            TDS.TileEntity.ByPosition.Remove(new TDS.Point16(x, y));

            var tileEntity = _worldService.GetTileEntity(x, y);

            tileEntity.Should().NotBeNull();
            tileEntity.Should().BeAssignableTo<IChest>();
            tileEntity.Should().Be(chest.Object);
        }

        [Theory]
        [InlineData(100, 100)]
        public void GetTileEntity_WithSign_IsCorrect(int x, int y) {
            var sign = new Mock<ISign>();
            _mockSignService.Setup(c => c.GetSign(x, y)).Returns(sign.Object);
            TDS.TileEntity.ByPosition.Remove(new TDS.Point16(x, y));

            var tileEntity = _worldService.GetTileEntity(x, y);

            tileEntity.Should().NotBeNull();
            tileEntity.Should().BeAssignableTo<ISign>();
            tileEntity.Should().Be(sign.Object);
        }

        [Theory]
        [InlineData(100, 100)]
        public void GetTileEntity_WithTargetDummy_IsCorrect(int x, int y) {
            TDS.TileEntity.ByPosition.Remove(new TDS.Point16(x, y));
            TGCTE.TETrainingDummy.Place(x, y);

            var tileEntity = _worldService.GetTileEntity(x, y);

            tileEntity.Should().NotBeNull();
            tileEntity.Should().BeAssignableTo<ITargetDummy>();
            tileEntity.X.Should().Be(x);
            tileEntity.Y.Should().Be(x);
        }

        [Theory]
        [InlineData(100, 100)]
        public void GetTileEntity_WithItemFrame_IsCorrect(int x, int y) {
            TDS.TileEntity.ByPosition.Remove(new TDS.Point16(x, y));
            TGCTE.TEItemFrame.Place(x, y);

            var tileEntity = _worldService.GetTileEntity(x, y);

            tileEntity.Should().NotBeNull();
            tileEntity.Should().BeAssignableTo<IItemFrame>();
            tileEntity.X.Should().Be(x);
            tileEntity.Y.Should().Be(x);
        }

        [Theory]
        [InlineData(100, 100)]
        public void GetTileEntity_WithLogicSensor_IsCorrect(int x, int y) {
            TDS.TileEntity.ByPosition.Remove(new TDS.Point16(x, y));
            TGCTE.TELogicSensor.Place(x, y);

            var tileEntity = _worldService.GetTileEntity(x, y);

            tileEntity.Should().NotBeNull();
            tileEntity.Should().BeAssignableTo<ILogicSensor>();
            tileEntity.X.Should().Be(x);
            tileEntity.Y.Should().Be(x);
        }

        [Theory]
        [InlineData(100, 100)]
        public void GetTileEntity_Nothing_ReturnsNull(int x, int y) {
            TDS.TileEntity.ByPosition.Remove(new TDS.Point16(x, y));

            var tileEntity = _worldService.GetTileEntity(x, y);

            tileEntity.Should().BeNull();
        }

        [Theory]
        [InlineData(100, 100, true)]
        public void RemoveTileEntity_WithChest_IsCorrect(int x, int y, bool removeResult) {
            var chest = new Mock<IChest>();
            _mockChestService.Setup(c => c.GetChest(x, y)).Returns(chest.Object);
            _mockChestService.Setup(c => c.RemoveChest(chest.Object)).Returns(removeResult);
            TDS.TileEntity.ByPosition.Remove(new TDS.Point16(x, y));
            var tileEntity = _worldService.GetTileEntity(x, y);

            var result = _worldService.RemoveTileEntity(tileEntity);

            result.Should().Be(removeResult);
        }

        [Theory]
        [InlineData(100, 100, true)]
        public void RemoveTileEntity_WithSign_IsCorrect(int x, int y, bool removeResult) {
            var sign = new Mock<ISign>();
            _mockSignService.Setup(c => c.GetSign(x, y)).Returns(sign.Object);
            _mockSignService.Setup(c => c.RemoveSign(sign.Object)).Returns(removeResult);
            TDS.TileEntity.ByPosition.Remove(new TDS.Point16(x, y));
            var tileEntity = _worldService.GetTileEntity(x, y);

            var result = _worldService.RemoveTileEntity(tileEntity);

            result.Should().Be(removeResult);
        }

        [Theory]
        [InlineData(100, 100)]
        public void RemoveTileEntity_NotChestOrSign_IsCorrect(int x, int y) {
            TDS.TileEntity.ByPosition.Remove(new TDS.Point16(x, y));
            var index = TGCTE.TELogicSensor.Place(x, y);
            var tileEntity = new Mock<ITileEntity>();
            tileEntity.Setup(t => t.X).Returns(x);
            tileEntity.Setup(t => t.Y).Returns(y);
            tileEntity.Setup(t => t.Index).Returns(index);

            var result = _worldService.RemoveTileEntity(tileEntity.Object);

            result.Should().BeTrue();
        }
    }
}
