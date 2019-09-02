using System;
using System.Linq;
using FluentAssertions;
using Orion.Items;
using Xunit;

namespace Orion.Tests.Items {
    public class OrionItemServiceTests : IDisposable {
        private readonly IItemService _itemService;

        public OrionItemServiceTests() {
            for (var i = 0; i < Terraria.Main.maxItems; ++i) {
                Terraria.Main.item[i] = new Terraria.Item {whoAmI = i};
            }
            
            _itemService = new OrionItemService();
        }

        public void Dispose() {
            _itemService.Dispose();
        }

        [Fact]
        public void GetCount_IsCorrect() {
            _itemService.Count.Should().Be(Terraria.Main.maxItems);
        }

        [Fact]
        public void GetItem_IsCorrect() {
            var item = _itemService[0];

            item.WrappedItem.Should().BeSameAs(Terraria.Main.item[0]);
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
            var item = _itemService[0];
            var item2 = _itemService[0];

            item.Should().BeSameAs(item2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void GetItem_InvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            Func<IItem> func = () => _itemService[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var items = _itemService.ToList();

            for (var i = 0; i < items.Count; ++i) {
                items[i].WrappedItem.Should().BeSameAs(Terraria.Main.item[i]);
            }
        }
    }
}
