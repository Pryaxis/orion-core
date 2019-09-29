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
using TerrariaTargetDummy = Terraria.GameContent.Tile_Entities.TETrainingDummy;

namespace Orion.World.TileEntities {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionTargetDummyTests {
        [Fact]
        public void GetNpcIndex_IsCorrect() {
            var terrariaTargetDummy = new TerrariaTargetDummy {npc = 100};
            ITargetDummy targetDummy = new OrionTargetDummy(terrariaTargetDummy);

            targetDummy.NpcIndex.Should().Be(100);
        }

        [Fact]
        public void SetNpcIndex_IsCorrect() {
            var terrariaTargetDummy = new TerrariaTargetDummy();
            ITargetDummy targetDummy = new OrionTargetDummy(terrariaTargetDummy);

            targetDummy.NpcIndex = 100;

            terrariaTargetDummy.npc.Should().Be(100);
        }
    }
}
