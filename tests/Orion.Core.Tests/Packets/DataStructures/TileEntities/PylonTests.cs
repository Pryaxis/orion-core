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

using Orion.Core.World.TileEntities;
using Xunit;

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    public class PylonTests
    {
        private readonly byte[] _bytes = { 7, 10, 0, 0, 0, 0, 1, 100, 0 };

        [Fact]
        public void Id_Get()
        {
            var pylon = new Pylon();

            Assert.Equal(TileEntityId.Pylon, pylon.Id);
        }

        [Fact]
        public void Read()
        {
            var pylon = TestUtils.ReadTileEntity<Pylon>(_bytes, includeIndex: true);

            Assert.Equal(10, pylon.Index);
            Assert.Equal(256, pylon.X);
            Assert.Equal(100, pylon.Y);
        }
    }
}
