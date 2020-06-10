// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using Moq;
using Xunit;

namespace Orion.Core.World.TileEntities {
    public class TileEntityTests {
        [Fact]
        public void IsConcrete_NullTileEntity_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => TileEntityExtensions.IsConcrete(null!));
        }

        [Fact]
        public void IsConcrete_ReturnsTrue() {
            var tileEntity = Mock.Of<ITileEntity>(te => te.Index == 1);

            Assert.True(tileEntity.IsConcrete());
        }

        [Fact]
        public void IsConcrete_ReturnsFalse() {
            var tileEntity = Mock.Of<ITileEntity>(te => te.Index == -1);

            Assert.False(tileEntity.IsConcrete());
        }
    }
}
