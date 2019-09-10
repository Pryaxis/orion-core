using System;
using FluentAssertions;
using Moq;
using Orion.World;
using Orion.World.TileEntities;
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
            Terraria.Main.worldName = worldName;

            _worldService.WorldName.Should().Be(worldName);
        }

        [Theory]
        [InlineData("test")]
        public void SetWorldName_IsCorrect(string worldName) {
            _worldService.WorldName = worldName;

            Terraria.Main.worldName.Should().Be(worldName);
        }

        [Fact]
        public void SetWorldName_NullValue_ThrowsArgumentNullException() {
            Action action = () => _worldService.WorldName = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(1000)]
        public void GetWorldWidth_IsCorrect(int worldWidth) {
            Terraria.Main.maxTilesX = worldWidth;

            _worldService.WorldWidth.Should().Be(worldWidth);
        }

        [Theory]
        [InlineData(1000)]
        public void GetWorldHeight_IsCorrect(int worldHeight) {
            Terraria.Main.maxTilesY = worldHeight;

            _worldService.WorldHeight.Should().Be(worldHeight);
        }

        [Theory]
        [InlineData(100.0)]
        public void GetTime_IsCorrect(double time) {
            Terraria.Main.time = time;

            _worldService.Time.Should().Be(time);
        }

        [Theory]
        [InlineData(100.0)]
        public void SetTime_IsCorrect(double time) {
            _worldService.Time = time;

            Terraria.Main.time.Should().Be(time);
        }

        [Theory]
        [InlineData(true)]
        public void GetIsDaytime_IsCorrect(bool isDaytime) {
            Terraria.Main.dayTime = isDaytime;

            _worldService.IsDaytime.Should().Be(isDaytime);
        }

        [Theory]
        [InlineData(true)]
        public void SetIsDaytime_IsCorrect(bool isDaytime) {
            _worldService.IsDaytime = isDaytime;

            Terraria.Main.dayTime.Should().Be(isDaytime);
        }

        [Theory]
        [InlineData(true)]
        public void GetIsHardmode_IsCorrect(bool isHardmode) {
            Terraria.Main.hardMode = isHardmode;

            _worldService.IsHardmode.Should().Be(isHardmode);
        }

        [Theory]
        [InlineData(true)]
        public void SetIsHardmode_IsCorrect(bool isHardmode) {
            _worldService.IsHardmode = isHardmode;

            Terraria.Main.hardMode.Should().Be(isHardmode);
        }

        [Theory]
        [InlineData(true)]
        public void GetIsExpertMode_IsCorrect(bool isExpertMode) {
            Terraria.Main.expertMode = isExpertMode;

            _worldService.IsExpertMode.Should().Be(isExpertMode);
        }

        [Theory]
        [InlineData(true)]
        public void SetIsExpertMode_IsCorrect(bool isExpertMode) {
            _worldService.IsExpertMode = isExpertMode;

            Terraria.Main.expertMode.Should().Be(isExpertMode);
        }

        [Theory]
        [InlineData(InvasionType.FrostLegion)]
        public void GetCurrentInvasion_IsCorrect(InvasionType invasionType) {
            Terraria.Main.invasionType = (int)invasionType;

            _worldService.CurrentInvasionType.Should().Be(invasionType);
        }

        [Fact]
        public void CheckingHalloween_IsCorrect() {
            var isRun = false;
            _worldService.CheckingHalloween += (sender, args) => {
                isRun = true;
            };

            Terraria.Main.checkHalloween();

            isRun.Should().BeTrue();
        }

        // This test should basically never flake, since it's pretty hard for both runs to pass.
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CheckingHalloween_Handled_IsCorrect(bool halloween) {
            Terraria.Main.halloween = halloween;
            _worldService.CheckingHalloween += (sender, args) => args.Handled = true;

            Terraria.Main.checkHalloween();

            Terraria.Main.halloween.Should().Be(halloween);
        }

        [Fact]
        public void CheckingChristmas_IsCorrect() {
            var isRun = false;
            _worldService.CheckingChristmas += (sender, args) => {
                isRun = true;
            };

            Terraria.Main.checkXMas();

            isRun.Should().BeTrue();
        }

        // This test should basically never flake, since it's pretty hard for both runs to pass.
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CheckingChristmas_Handled_IsCorrect(bool christmas) {
            Terraria.Main.xMas = christmas;
            _worldService.CheckingChristmas += (sender, args) => args.Handled = true;

            Terraria.Main.checkXMas();

            Terraria.Main.xMas.Should().Be(christmas);
        }

        [Theory]
        [InlineData(InvasionType.FrostLegion)]
        public void StartInvasion_NoCurrentInvasion_ReturnsTrue(InvasionType invasionType) {
            // Fake an active player with enough HP.
            Terraria.Main.player[0].active = true;
            Terraria.Main.player[0].statLifeMax = 200;
            Terraria.Main.invasionType = 0;

            var result = _worldService.StartInvasion(invasionType);

            result.Should().BeTrue();
            Terraria.Main.invasionType.Should().Be((int)invasionType);
        }

        [Theory]
        [InlineData(InvasionType.FrostLegion)]
        public void StartInvasion_CurrentInvasion_ReturnsFalse(InvasionType invasionType) {
            // Fake an active player with enough HP.
            Terraria.Main.player[0].active = true;
            Terraria.Main.player[0].statLifeMax = 200;
            Terraria.Main.invasionType = 1;
            Terraria.Main.invasionSize = 100;

            var result = _worldService.StartInvasion(invasionType);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(InvasionType.FrostLegion)]
        public void StartInvasion_NoPlayers_ReturnsFalse(InvasionType invasionType) {
            Terraria.Main.player[0].active = false;
            Terraria.Main.invasionType = 0;

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
            terrariaTileEntity.type.Should().Be(Terraria.ID.TileEntityID.TrainingDummy);
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
            terrariaTileEntity.type.Should().Be(Terraria.ID.TileEntityID.ItemFrame);
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
            terrariaTileEntity.type.Should().Be(Terraria.ID.TileEntityID.LogicSensor);
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
