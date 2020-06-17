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
using Orion.Core.World.Tiles;
using Xunit;

namespace Orion.Core.World
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionWorldTests
    {
        [Fact]
        public void Item_Get_Mutate()
        {
            using var world = new OrionWorld(123, 456, "test");

            world[0, 0].BlockId = BlockId.Stone;

            Assert.Equal(BlockId.Stone, world[0, 0].BlockId);
        }

        [Fact]
        public void Width_Get()
        {
            using var world = new OrionWorld(123, 456, "test");

            Assert.Equal(123, world.Width);
        }

        [Fact]
        public void Height_Get()
        {
            using var world = new OrionWorld(123, 456, "test");

            Assert.Equal(456, world.Height);
        }

        [Fact]
        public void Name_Get()
        {
            using var world = new OrionWorld(123, 456, "test");

            Assert.Equal("test", world.Name);
        }

        [Fact]
        public void Difficulty_Get()
        {
            Terraria.Main.GameMode = (int)WorldDifficulty.Master;

            using var world = new OrionWorld(123, 456, "test");

            Assert.Equal(WorldDifficulty.Master, world.Difficulty);
        }

        [Fact]
        public void Difficulty_Set()
        {
            using var world = new OrionWorld(123, 456, "test");

            world.Difficulty = WorldDifficulty.Master;

            Assert.Equal(WorldDifficulty.Master, (WorldDifficulty)Terraria.Main.GameMode);
        }
    }
}
