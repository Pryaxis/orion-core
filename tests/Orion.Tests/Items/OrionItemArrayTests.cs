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

using System;
using System.Linq;
using FluentAssertions;
using Orion.Items;
using Terraria;
using Xunit;

namespace Orion.Tests.Items {
    public class OrionItemArrayTests {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void GetCount_IsCorrect(int count) {
            var terrariaItems = new Item[count];
            var items = new OrionItemArray(terrariaItems);

            items.Count.Should().Be(count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void GetItem_IsCorrect(int index) {
            var terrariaItems = new Item[10];
            terrariaItems[index] = new Item();
            var items = new OrionItemArray(terrariaItems);

            ((OrionItem)items[index]).Wrapped.Should().BeSameAs(terrariaItems[index]);
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
            var terrariaItems = new Item[10];
            terrariaItems[0] = new Item();
            var items = new OrionItemArray(terrariaItems);

            var item = items[0];
            var item2 = items[0];

            item.Should().BeSameAs(item2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000)]
        public void GetItem_IndexOutOfRange_ThrowsArgumentOutOfRangeException(int index) {
            var terrariaItems = new Item[10];
            var items = new OrionItemArray(terrariaItems);

            Func<IItem> func = () => items[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var terrariaItems = new Item[10];
            for (var i = 0; i < 10; ++i) {
                terrariaItems[i] = new Item();
            }

            var items = new OrionItemArray(terrariaItems).ToList();
            for (var i = 0; i < 10; ++i) {
                ((OrionItem)items[i]).Wrapped.Should().BeSameAs(terrariaItems[i]);
            }
        }
    }
}
