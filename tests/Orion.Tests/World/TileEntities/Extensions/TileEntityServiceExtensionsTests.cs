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
using FluentAssertions;
using Moq;
using Xunit;

namespace Orion.World.TileEntities.Extensions {
    public class TileEntityServiceExtensionsTests {
        [Fact]
        public void AddChest_IsCorrect() {
            var chest = new Mock<IChest>().Object;
            var mockTileEntityService = new Mock<ITileEntityService>();
            mockTileEntityService.Setup(tes => tes.AddTileEntity(TileEntityType.Chest, 1, 2)).Returns(chest);

            mockTileEntityService.Object.AddChest(1, 2).Should().BeSameAs(chest);

            mockTileEntityService.Verify(tes => tes.AddTileEntity(TileEntityType.Chest, 1, 2));
            mockTileEntityService.VerifyNoOtherCalls();
        }

        [Fact]
        public void AddChest_NullTileEntityService_ThrowsArgumentNullException() {
            Func<IChest> func = () => TileEntityServiceExtensions.AddChest(null, 0, 0);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddSign_IsCorrect() {
            var sign = new Mock<ISign>().Object;
            var mockTileEntityService = new Mock<ITileEntityService>();
            mockTileEntityService.Setup(tes => tes.AddTileEntity(TileEntityType.Sign, 1, 2)).Returns(sign);

            mockTileEntityService.Object.AddSign(1, 2).Should().BeSameAs(sign);

            mockTileEntityService.Verify(tes => tes.AddTileEntity(TileEntityType.Sign, 1, 2));
            mockTileEntityService.VerifyNoOtherCalls();
        }

        [Fact]
        public void AddSign_NullTileEntityService_ThrowsArgumentNullException() {
            Func<ISign> func = () => TileEntityServiceExtensions.AddSign(null, 0, 0);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddTargetDummy_IsCorrect() {
            var targetDummy = new Mock<ITargetDummy>().Object;
            var mockTileEntityService = new Mock<ITileEntityService>();
            mockTileEntityService.Setup(tes => tes.AddTileEntity(TileEntityType.TargetDummy, 1, 2))
                                 .Returns(targetDummy);

            mockTileEntityService.Object.AddTargetDummy(1, 2).Should().BeSameAs(targetDummy);

            mockTileEntityService.Verify(tes => tes.AddTileEntity(TileEntityType.TargetDummy, 1, 2));
            mockTileEntityService.VerifyNoOtherCalls();
        }

        [Fact]
        public void AddTargetDummy_NullTileEntityService_ThrowsArgumentNullException() {
            Func<ITargetDummy> func = () => TileEntityServiceExtensions.AddTargetDummy(null, 0, 0);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddItemFrame_IsCorrect() {
            var itemFrame = new Mock<IItemFrame>().Object;
            var mockTileEntityService = new Mock<ITileEntityService>();
            mockTileEntityService.Setup(tes => tes.AddTileEntity(TileEntityType.ItemFrame, 1, 2)).Returns(itemFrame);

            mockTileEntityService.Object.AddItemFrame(1, 2).Should().BeSameAs(itemFrame);

            mockTileEntityService.Verify(tes => tes.AddTileEntity(TileEntityType.ItemFrame, 1, 2));
            mockTileEntityService.VerifyNoOtherCalls();
        }

        [Fact]
        public void AddItemFrame_NullTileEntityService_ThrowsArgumentNullException() {
            Func<IItemFrame> func = () => TileEntityServiceExtensions.AddItemFrame(null, 0, 0);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddLogicSensor_IsCorrect() {
            var logicSensor = new Mock<ILogicSensor>().Object;
            var mockTileEntityService = new Mock<ITileEntityService>();
            mockTileEntityService.Setup(tes => tes.AddTileEntity(TileEntityType.LogicSensor, 1, 2))
                                 .Returns(logicSensor);

            mockTileEntityService.Object.AddLogicSensor(1, 2).Should().BeSameAs(logicSensor);

            mockTileEntityService.Verify(tes => tes.AddTileEntity(TileEntityType.LogicSensor, 1, 2));
            mockTileEntityService.VerifyNoOtherCalls();
        }

        [Fact]
        public void AddLogicSensor_NullTileEntityService_ThrowsArgumentNullException() {
            Func<ILogicSensor> func = () => TileEntityServiceExtensions.AddLogicSensor(null, 0, 0);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
