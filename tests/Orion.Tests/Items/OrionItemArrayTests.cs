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

using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Orion.Items {
    public class OrionItemArrayTests {
        [Fact]
        public void GetCount_IsCorrect() {
            var terrariaItems = new Terraria.Item[10];
            var items = new OrionItemArray(terrariaItems);

            items.Count.Should().Be(10);
        }

        [Fact]
        public void GetItem_IsCorrect() {
            var terrariaItems = new Terraria.Item[10];
            terrariaItems[1] = new Terraria.Item();
            var items = new OrionItemArray(terrariaItems);

            ((OrionItem)items[1]).Wrapped.Should().BeSameAs(terrariaItems[1]);
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
            var terrariaItems = new Terraria.Item[10];
            terrariaItems[0] = new Terraria.Item();
            var items = new OrionItemArray(terrariaItems);

            var item = items[0];
            var item2 = items[0];

            item.Should().BeSameAs(item2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000)]
        public void GetItem_IndexOutOfRange_ThrowsArgumentOutOfRangeException(int index) {
            var terrariaItems = new Terraria.Item[10];
            var items = new OrionItemArray(terrariaItems);

            Func<IItem> func = () => items[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var terrariaItems = new Terraria.Item[10];
            for (var i = 0; i < 10; ++i) {
                terrariaItems[i] = new Terraria.Item();
            }

            var items = new OrionItemArray(terrariaItems).ToList();
            for (var i = 0; i < 10; ++i) {
                ((OrionItem)items[i]).Wrapped.Should().BeSameAs(terrariaItems[i]);
            }
        }
    }
}
