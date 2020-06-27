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

using System.Diagnostics.CodeAnalysis;
using Orion.Core.World.TileEntities;
using Xunit;

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class TargetDummyTests
    {
        private readonly byte[] _bytes = { 0, 10, 0, 0, 0, 0, 1, 100, 0, 5, 0 };

        [Fact]
        public void Id_Get()
        {
            var targetDummy = new TargetDummy();

            Assert.Equal(TileEntityId.TargetDummy, targetDummy.Id);
        }

        [Fact]
        public void NpcIndex_Set_Get()
        {
            var targetDummy = new TargetDummy();

            targetDummy.NpcIndex = 5;

            Assert.Equal(5, targetDummy.NpcIndex);
        }

        [Fact]
        public void Read()
        {
            var targetDummy = TestUtils.ReadTileEntity<TargetDummy>(_bytes, true);

            Assert.Equal(10, targetDummy.Index);
            Assert.Equal(256, targetDummy.X);
            Assert.Equal(100, targetDummy.Y);

            Assert.Equal(5, targetDummy.NpcIndex);
        }
    }
}
