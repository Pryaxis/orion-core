using System;
using System.Linq;
using FluentAssertions;
using Orion.Items;
using Xunit;

namespace Orion.Tests.Items {
    public class OrionItemListTests {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void GetCount_IsCorrect(int count) {
            var terrariaItems = new Terraria.Item[count];
            var items = new OrionItemList(terrariaItems);

            items.Count.Should().Be(count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void GetItem_IsCorrect(int index) {
            var terrariaItems = new Terraria.Item[10];
            terrariaItems[index] = new Terraria.Item();
            var items = new OrionItemList(terrariaItems);

            ((OrionItem)items[index]).Wrapped.Should().BeSameAs(terrariaItems[index]);
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
            var terrariaItems = new Terraria.Item[10];
            terrariaItems[0] = new Terraria.Item();
            var items = new OrionItemList(terrariaItems);

            var item = items[0];
            var item2 = items[0];

            item.Should().BeSameAs(item2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000)]
        public void GetItem_IndexOutOfRange_ThrowsArgumentOutOfRangeException(int index) {
            var terrariaItems = new Terraria.Item[10];
            var items = new OrionItemList(terrariaItems);

            Func<IItem> func = () => items[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var terrariaItems = new Terraria.Item[10];
            for (var i = 0; i < 10; ++i) {
                terrariaItems[i] = new Terraria.Item();
            }

            var items = new OrionItemList(terrariaItems).ToList();
            for (var i = 0; i < 10; ++i) {
                ((OrionItem)items[i]).Wrapped.Should().BeSameAs(terrariaItems[i]);
            }
        }
    }
}
