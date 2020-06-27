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
using System.Diagnostics.CodeAnalysis;
using Orion.Core.World.TileEntities;
using Xunit;

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class UnknownTileEntityTests
    {
        private readonly byte[] _bytes = { 255, 10, 0, 0, 0, 0, 1, 100, 0, 0, 1, 2, 3, 4, 5, 6, 7 };
        private readonly byte[] _emptyBytes = { 255, 10, 0, 0, 0, 0, 1, 100, 0 };

        [Fact]
        public void Ctor_NegativeLength_ThrowsArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new UnknownTileEntity(-1, (TileEntityId)255));
        }

        [Fact]
        public void Id_Get()
        {
            var tileEntity = new UnknownTileEntity(8, (TileEntityId)255);

            Assert.Equal((TileEntityId)255, tileEntity.Id);
        }

        [Fact]
        public void Data_Get()
        {
            var tileEntity = new UnknownTileEntity(8, (TileEntityId)255);

            Assert.Equal(8, tileEntity.Data.Length);
        }

        [Fact]
        public void Read()
        {
            var tileEntity = TestUtils.ReadTileEntity<UnknownTileEntity>(_bytes, true);

            Assert.Equal(10, tileEntity.Index);
            Assert.Equal(256, tileEntity.X);
            Assert.Equal(100, tileEntity.Y);

            Assert.Equal(8, tileEntity.Data.Length);
            for (var i = 0; i < 8; ++i)
            {
                Assert.Equal(i, tileEntity.Data[i]);
            }
        }

        [Fact]
        public void Read_Empty()
        {
            var tileEntity = TestUtils.ReadTileEntity<UnknownTileEntity>(_emptyBytes, true);

            Assert.Equal(10, tileEntity.Index);
            Assert.Equal(256, tileEntity.X);
            Assert.Equal(100, tileEntity.Y);

            Assert.Equal(0, tileEntity.Data.Length);
        }
    }
}
