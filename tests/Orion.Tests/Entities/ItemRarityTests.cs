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
    public class ItemRarityTests {
        [Fact]
        public void FromId_IsCorrect() {
            ItemRarity.FromLevel(-12).Level.Should().Be(-12);
            ItemRarity.FromLevel(-11).Level.Should().Be(-11);
            for (int i = -1; i < 12; ++i) {
                ItemRarity.FromLevel(i).Level.Should().Be(i);
            }

            ItemRarity.FromLevel(12).Should().BeNull();
        }

        [Fact]
        public void FromId_ReturnsSameInstance() {
            var itemRarity = ItemRarity.FromLevel(1);
            var itemRarity2 = ItemRarity.FromLevel(1);

            itemRarity.Should().BeSameAs(itemRarity2);
        }
    }
}
