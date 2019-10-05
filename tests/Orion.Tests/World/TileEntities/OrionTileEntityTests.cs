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

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;
using TerrariaTileEntity = Terraria.DataStructures.TileEntity;

namespace Orion.World.TileEntities {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionTileEntityTests {
        [Fact]
        public void Type_Get() {
            var terrariaTileEntity = new TestTerrariaTileEntity { type = (byte)TileEntityType.ItemFrame };
            ITileEntity tileEntity = new TestOrionTileEntity(terrariaTileEntity);

            tileEntity.Type.Should().Be(TileEntityType.ItemFrame);
        }

        [Fact]
        public void Index_Get() {
            var terrariaTileEntity = new TestTerrariaTileEntity { ID = 100 };
            ITileEntity tileEntity = new TestOrionTileEntity(terrariaTileEntity);

            tileEntity.Index.Should().Be(100);
        }

        [Fact]
        public void X_Get() {
            var terrariaTileEntity =
                new TestTerrariaTileEntity { Position = new Terraria.DataStructures.Point16(100, 0) };
            ITileEntity tileEntity = new TestOrionTileEntity(terrariaTileEntity);

            tileEntity.X.Should().Be(100);
        }

        [Fact]
        public void X_Set() {
            var terrariaTileEntity = new TestTerrariaTileEntity();
            ITileEntity tileEntity = new TestOrionTileEntity(terrariaTileEntity);

            tileEntity.X = 100;

            terrariaTileEntity.Position.X.Should().Be(100);
        }

        [Fact]
        public void Y_Get() {
            var terrariaTileEntity =
                new TestTerrariaTileEntity { Position = new Terraria.DataStructures.Point16(0, 100) };
            ITileEntity tileEntity = new TestOrionTileEntity(terrariaTileEntity);

            tileEntity.Y.Should().Be(100);
        }

        [Fact]
        public void Y_Set() {
            var terrariaTileEntity = new TestTerrariaTileEntity();
            ITileEntity tileEntity = new TestOrionTileEntity(terrariaTileEntity);

            tileEntity.Y = 100;

            terrariaTileEntity.Position.Y.Should().Be(100);
        }

        private class TestOrionTileEntity : OrionTileEntity<TestTerrariaTileEntity> {
            public TestOrionTileEntity(TestTerrariaTileEntity terrariaTileEntity) : base(terrariaTileEntity) { }
        }

        private class TestTerrariaTileEntity : TerrariaTileEntity { }
    }
}
