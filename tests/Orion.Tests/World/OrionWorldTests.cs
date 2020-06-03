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

using Orion.World.Tiles;
using Xunit;

namespace Orion.World {
    public class OrionWorldTests {
        [Fact]
        public void Width_Get() {
            using var world = new OrionWorld(123, 456);

            Assert.Equal(123, world.Width);
        }

        [Fact]
        public void Height_Get() {
            using var world = new OrionWorld(123, 456);

            Assert.Equal(456, world.Height);
        }

        [Fact]
        public void Item_Get_Mutate() {
            using var world = new OrionWorld(123, 456);

            world[0, 0].BlockId = BlockId.Stone;

            Assert.Equal(BlockId.Stone, world[0, 0].BlockId);
        }
    }
}
