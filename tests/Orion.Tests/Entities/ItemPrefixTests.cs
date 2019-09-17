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
    public class ItemPrefixTests {
        [Fact]
        public void FromId_IsCorrect() {
            for (int i = -2; i < Terraria.ID.PrefixID.Count; ++i) {
                ItemPrefix.FromId(i).Id.Should().Be(i);
            }

            ItemPrefix.FromId(Terraria.ID.PrefixID.Count).Should().BeNull();
        }

        [Fact]
        public void FromId_ReturnsSameInstance() {
            var itemPrefix = ItemPrefix.FromId(1);
            var itemPrefix2 = ItemPrefix.FromId(1);

            itemPrefix.Should().BeSameAs(itemPrefix2);
        }
    }
}
