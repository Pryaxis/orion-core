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

namespace Orion.World {
    [Collection("TerrariaTestsCollection")]
    public class WallTypeTests {
        [Fact]
        public void FromId_IsCorrect() {
            for (byte i = 0; i < Terraria.Main.maxWallTypes; ++i) {
                WallType.FromId(i).Id.Should().Be(i);
            }

            WallType.FromId(Terraria.Main.maxWallTypes).Should().BeNull();
        }

        [Fact]
        public void FromId_ReturnsSameInstance() {
            var wallType = WallType.FromId(1);
            var wallType2 = WallType.FromId(1);

            wallType.Should().BeSameAs(wallType2);
        }

        [Fact]
        public void GetIsSafeForHouse_IsCorrect() {
            for (byte i = 0; i < Terraria.Main.maxWallTypes; ++i) {
                WallType.FromId(i).IsSafeForHouse.Should().Be(Terraria.Main.wallHouse[i]);
            }
        }
    }
}
