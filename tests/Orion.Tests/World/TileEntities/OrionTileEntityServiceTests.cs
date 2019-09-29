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
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Moq;
using Xunit;
using Main = Terraria.Main;
using TerrariaChest = Terraria.Chest;
using TerrariaTargetDummy = Terraria.GameContent.Tile_Entities.TETrainingDummy;
using TerrariaItem = Terraria.Item;
using TerrariaItemFrame = Terraria.GameContent.Tile_Entities.TEItemFrame;
using TerrariaLogicSensor = Terraria.GameContent.Tile_Entities.TELogicSensor;
using TerrariaSign = Terraria.Sign;
using TerrariaTileEntity = Terraria.DataStructures.TileEntity;

namespace Orion.World.TileEntities {
    [Collection("TerrariaTestsCollection")]
    public class OrionTileEntityServiceTests : IDisposable {
        private readonly ITileEntityService _tileEntityService;

        public OrionTileEntityServiceTests() {
            _tileEntityService = new OrionTileEntityService();

            for (var i = 0; i < Main.chest.Length; ++i) {
                Main.chest[i] = null;
            }

            for (var i = 0; i < Main.sign.Length; ++i) {
                Main.sign[i] = null;
            }

            TerrariaTileEntity.Clear();
        }

        public void Dispose() {
            _tileEntityService.Dispose();
        }

        [Fact]
        public void Chests_GetItem_IsCorrect() {
            Main.chest[0] = new TerrariaChest();
            for (var i = 0; i < TerrariaChest.maxItems; ++i) {
                Main.chest[0].item[i] = new TerrariaItem();
            }

            var chest = _tileEntityService.Chests[0];

            chest.Should().NotBeNull();
            ((OrionChest)chest).Wrapped.Should().BeSameAs(Main.chest[0]);
        }

        [Fact]
        public void Chests_GetItem_MultipleTimes_ReturnsSameInstance() {
            Main.chest[0] = new TerrariaChest();
            for (var i = 0; i < TerrariaChest.maxItems; ++i) {
                Main.chest[0].item[i] = new TerrariaItem();
            }

            var chest = _tileEntityService.Chests[0];
            var chest2 = _tileEntityService.Chests[0];

            chest.Should().BeSameAs(chest2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Chests_GetItem_InvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            Func<IChest> func = () => _tileEntityService.Chests[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Chests_GetEnumerator_IsCorrect() {
            for (var i = 0; i < Main.chest.Length; ++i) {
                Main.chest[i] = new TerrariaChest();
                for (var j = 0; j < TerrariaChest.maxItems; ++j) {
                    Main.chest[i].item[j] = new TerrariaItem();
                }
            }

            var chests = _tileEntityService.Chests.ToList();

            for (var i = 0; i < chests.Count; ++i) {
                chests[i].Should().NotBeNull();
                ((OrionChest)chests[i]).Wrapped.Should().BeSameAs(Main.chest[i]);
            }
        }

        [Fact]
        public void Signs_GetItem_IsCorrect() {
            Main.sign[0] = new TerrariaSign();
            var sign = _tileEntityService.Signs[0];
            
            sign.Should().NotBeNull();
            ((OrionSign)sign).Wrapped.Should().BeSameAs(Main.sign[0]);
        }

        [Fact]
        public void Signs_GetItem_MultipleTimes_ReturnsSameInstance() {
            Main.sign[0] = new TerrariaSign();
            var sign = _tileEntityService.Signs[0];
            var sign2 = _tileEntityService.Signs[0];

            sign.Should().BeSameAs(sign2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Signs_GetItem_InvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            Func<ISign> func = () => _tileEntityService.Signs[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Signs_GetEnumerator_IsCorrect() {
            for (var i = 0; i < Main.sign.Length; ++i) {
                Main.sign[i] = new TerrariaSign();
            }

            var signs = _tileEntityService.Signs.ToList();

            for (var i = 0; i < signs.Count; ++i) {
                signs[i].Should().NotBeNull();
                ((OrionSign)signs[i]).Wrapped.Should().BeSameAs(Main.sign[i]);
            }
        }

        [Fact]
        public void AddTileEntity_Chest_IsCorrect() {
            var chest = (IChest)_tileEntityService.AddTileEntity(TileEntityType.Chest, 1, 2);
            
            chest.Should().NotBeNull();
            chest.Index.Should().Be(0);
            chest.X.Should().Be(1);
            chest.Y.Should().Be(2);
            ((OrionChest)chest).Wrapped.Should().BeSameAs(Main.chest[0]);
        }

        [Fact]
        public void AddTileEntity_Chest_NoRoom_ReturnsNull() {
            for (var i = 0; i < Main.chest.Length; ++i) {
                Main.chest[i] = new TerrariaChest();
            }

            var chest = (IChest)_tileEntityService.AddTileEntity(TileEntityType.Chest, 1, 2);

            chest.Should().BeNull();
        }

        [Fact]
        public void AddTileEntity_Sign_IsCorrect() {
            var sign = (ISign)_tileEntityService.AddTileEntity(TileEntityType.Sign, 1, 2);
            
            sign.Should().NotBeNull();
            sign.Index.Should().Be(0);
            sign.X.Should().Be(1);
            sign.Y.Should().Be(2);
            ((OrionSign)sign).Wrapped.Should().BeSameAs(Main.sign[0]);
        }

        [Fact]
        public void AddTileEntity_Sign_NoRoom_ReturnsNull() {
            for (var i = 0; i < Main.sign.Length; ++i) {
                Main.sign[i] = new TerrariaSign();
            }

            var sign = (ISign)_tileEntityService.AddTileEntity(TileEntityType.Sign, 1, 2);

            sign.Should().BeNull();
        }

        [Fact]
        public void AddTileEntity_TargetDummy_IsCorrect() {
            var targetDummy = (ITargetDummy)_tileEntityService.AddTileEntity(TileEntityType.TargetDummy, 1, 2);
            
            targetDummy.Should().NotBeNull();
            targetDummy.Index.Should().Be(0);
            targetDummy.X.Should().Be(1);
            targetDummy.Y.Should().Be(2);
            ((OrionTargetDummy)targetDummy).Wrapped.Should().BeSameAs(TerrariaTileEntity.ByID[0]);
        }

        [Fact]
        public void AddTileEntity_ItemFrame_IsCorrect() {
            var itemFrame = (IItemFrame)_tileEntityService.AddTileEntity(TileEntityType.ItemFrame, 1, 2);
            
            itemFrame.Should().NotBeNull();
            itemFrame.Index.Should().Be(0);
            itemFrame.X.Should().Be(1);
            itemFrame.Y.Should().Be(2);
            ((OrionItemFrame)itemFrame).Wrapped.Should().BeSameAs(TerrariaTileEntity.ByID[0]);
        }

        [Fact]
        public void AddTileEntity_LogicSensor_IsCorrect() {
            var logicSensor = (ILogicSensor)_tileEntityService.AddTileEntity(TileEntityType.LogicSensor, 1, 2);
            
            logicSensor.Should().NotBeNull();
            logicSensor.Index.Should().Be(0);
            logicSensor.X.Should().Be(1);
            logicSensor.Y.Should().Be(2);
            ((OrionLogicSensor)logicSensor).Wrapped.Should().BeSameAs(TerrariaTileEntity.ByID[0]);
        }

        [Fact]
        public void AddTileEntity_ExistsAlready_ReturnsNull() {
            _tileEntityService.AddTileEntity(TileEntityType.Chest, 1, 2);

            _tileEntityService.AddTileEntity(TileEntityType.Chest, 1, 2).Should().BeNull();
        }

        [Fact]
        public void TileEntity_Get_Chest_IsCorrect() {
            Main.chest[1] = new TerrariaChest {
                x = 1,
                y = 2
            };
            for (var i = 0; i < TerrariaChest.maxItems; ++i) {
                Main.chest[1].item[i] = new TerrariaItem();
            }

            var chest = (IChest)_tileEntityService.GetTileEntity(1, 2);
            
            chest.Should().NotBeNull();
            chest.Index.Should().Be(1);
            chest.X.Should().Be(1);
            chest.Y.Should().Be(2);
        }

        [Fact]
        public void TileEntity_Get_Sign_IsCorrect() {
            Main.sign[1] = new TerrariaSign {
                x = 1,
                y = 2,
                text = "test"
            };

            var sign = (ISign)_tileEntityService.GetTileEntity(1, 2);
            
            sign.Should().NotBeNull();
            sign.Index.Should().Be(1);
            sign.X.Should().Be(1);
            sign.Y.Should().Be(2);
            sign.Text.Should().Be("test");
        }

        [Fact]
        public void TileEntity_Get_TargetDummy_IsCorrect() {
            var targetDummyIndex = TerrariaTargetDummy.Place(1, 2);

            var targetDummy = (ITargetDummy)_tileEntityService.GetTileEntity(1, 2);
            
            targetDummy.Should().NotBeNull();
            targetDummy.Index.Should().Be(targetDummyIndex);
            targetDummy.X.Should().Be(1);
            targetDummy.Y.Should().Be(2);
        }

        [Fact]
        public void TileEntity_Get_ItemFrame_IsCorrect() {
            var itemFrameIndex = TerrariaItemFrame.Place(1, 2);

            var itemFrame = (IItemFrame)_tileEntityService.GetTileEntity(1, 2);
            
            itemFrame.Should().NotBeNull();
            itemFrame.Index.Should().Be(itemFrameIndex);
            itemFrame.X.Should().Be(1);
            itemFrame.Y.Should().Be(2);
        }

        [Fact]
        public void TileEntity_Get_LogicSensor_IsCorrect() {
            var logicSensorIndex = TerrariaLogicSensor.Place(1, 2);

            var logicSensor = (ILogicSensor)_tileEntityService.GetTileEntity(1, 2);
            
            logicSensor.Should().NotBeNull();
            logicSensor.Index.Should().Be(logicSensorIndex);
            logicSensor.X.Should().Be(1);
            logicSensor.Y.Should().Be(2);
        }

        [Fact]
        public void TileEntity_Get_NoTileEntity_ReturnsNull() {
            _tileEntityService.GetTileEntity(1, 2).Should().BeNull();
        }

        [Fact]
        public void RemoveTileEntity_Chest_IsCorrect() {
            Main.chest[1] = new TerrariaChest {
                x = 1,
                y = 2
            };
            for (var i = 0; i < TerrariaChest.maxItems; ++i) {
                Main.chest[1].item[i] = new TerrariaItem();
            }

            var mockChest = new Mock<IChest>();
            mockChest.SetupGet(c => c.Type).Returns(TileEntityType.Chest);
            mockChest.SetupGet(c => c.Index).Returns(1);
            mockChest.SetupGet(c => c.X).Returns(1);
            mockChest.SetupGet(c => c.Y).Returns(2);

            _tileEntityService.RemoveTileEntity(mockChest.Object).Should().BeTrue();

            Main.chest[1].Should().BeNull();
        }

        [Fact]
        public void RemoveTileEntity_Sign_IsCorrect() {
            Main.sign[1] = new TerrariaSign {
                x = 1,
                y = 2,
                text = "test"
            };
            var mockSign = new Mock<ISign>();
            mockSign.SetupGet(s => s.Type).Returns(TileEntityType.Sign);
            mockSign.SetupGet(s => s.Index).Returns(1);
            mockSign.SetupGet(s => s.X).Returns(1);
            mockSign.SetupGet(s => s.Y).Returns(2);

            _tileEntityService.RemoveTileEntity(mockSign.Object).Should().BeTrue();

            Main.sign[1].Should().BeNull();
        }

        [Fact]
        public void RemoveTileEntity_TerrariaTileEntity_IsCorrect() {
            var targetDummyIndex = TerrariaTargetDummy.Place(1, 2);

            var mockTargetDummy = new Mock<ITargetDummy>();
            mockTargetDummy.SetupGet(td => td.Type).Returns(TileEntityType.TargetDummy);
            mockTargetDummy.SetupGet(td => td.Index).Returns(targetDummyIndex);
            mockTargetDummy.SetupGet(td => td.X).Returns(1);
            mockTargetDummy.SetupGet(td => td.Y).Returns(2);

            _tileEntityService.RemoveTileEntity(mockTargetDummy.Object).Should().BeTrue();

            TerrariaTileEntity.ByID.ContainsKey(targetDummyIndex).Should().BeFalse();
            TerrariaTileEntity.ByPosition.ContainsKey(new Terraria.DataStructures.Point16(1, 2)).Should().BeFalse();
        }

        [Fact]
        public void RemoveTileEntity_NoTileEntity_ReturnsFalse() {
            var mockTileEntity = new Mock<ITileEntity>();
            mockTileEntity.SetupGet(te => te.Type).Returns(TileEntityType.TargetDummy);
            mockTileEntity.SetupGet(te => te.Index).Returns(0);
            mockTileEntity.SetupGet(te => te.X).Returns(1);
            mockTileEntity.SetupGet(te => te.Y).Returns(2);

            _tileEntityService.RemoveTileEntity(mockTileEntity.Object).Should().BeFalse();
        }
    }
}
