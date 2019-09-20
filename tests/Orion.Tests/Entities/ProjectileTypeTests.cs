// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

using FluentAssertions;
using Xunit;

namespace Orion.Entities {
    [Collection("TerrariaTestsCollection")]
    public class ProjectileTypeTests {
        [Fact]
        public void IsHostile_IsCorrect() {
            for (short i = 0; i < Terraria.ID.ProjectileID.Count; ++i) {
                ProjectileType.FromId(i)?.IsHostile.Should().Be(Terraria.Main.projHostile[i]);
            }
        }

        [Fact]
        public void FromId_IsCorrect() {
            for (short i = 0; i < Terraria.ID.ProjectileID.Count; ++i) {
                ProjectileType.FromId(i)?.Id.Should().Be(i);
            }

            ProjectileType.FromId(-1).Should().BeNull();
            ProjectileType.FromId(Terraria.ID.ProjectileID.Count).Should().BeNull();
        }

        [Fact]
        public void FromId_ReturnsSameInstance() {
            var itemType = ProjectileType.FromId(1);
            var itemType2 = ProjectileType.FromId(1);

            itemType.Should().BeSameAs(itemType2);
        }
    }
}
