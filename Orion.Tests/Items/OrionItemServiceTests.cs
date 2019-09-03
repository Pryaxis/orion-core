using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Items;
using Xunit;

namespace Orion.Tests.Items {
    public class OrionItemServiceTests : IDisposable {
        private readonly IItemService _itemService;

        public OrionItemServiceTests() {
            for (var i = 0; i < Terraria.Main.maxItems + 1; ++i) {
                Terraria.Main.item[i] = new Terraria.Item {whoAmI = i};
            }
            
            _itemService = new OrionItemService();
        }

        public void Dispose() {
            _itemService.Dispose();
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

        public static readonly IEnumerable<object[]> SpawnItemData = new List<object[]> {
            new object[] {ItemType.StoneBlock, 100, ItemPrefix.None},
            new object[] {ItemType.SDMG, 1, ItemPrefix.Unreal},
            new object[] {ItemType.Meowmere, 1, ItemPrefix.Legendary},
        };

        [Theory]
        [MemberData(nameof(SpawnItemData))]
        public void SpawnItem_IsCorrect(ItemType type, int stackSize, ItemPrefix prefix) {
            var item = _itemService.SpawnItem(type, Vector2.Zero, stackSize, prefix);

            item.Type.Should().Be(type);
            item.StackSize.Should().Be(stackSize);
            item.Prefix.Should().Be(prefix);
        }
        
        [Theory]
        [MemberData(nameof(SpawnItemData))]
        public void SpawnItem_CachedItem_IsCorrect(ItemType type, int stackSize, ItemPrefix prefix) {
            Terraria.Item.itemCaches[(int)type] = 0;

            var item = _itemService.SpawnItem(type, Vector2.Zero, stackSize, prefix);

            item.Type.Should().Be(type);
            item.StackSize.Should().Be(stackSize);
            item.Prefix.Should().Be(prefix);
        }
        
        [Fact]
        public void ItemSettingDefaults_IsCorrect() {
            IItem argsItem = null;
            _itemService.ItemSettingDefaults += (sender, args) => {
                argsItem = args.Item;
            };
            
            var item = _itemService.SpawnItem(ItemType.SDMG, Vector2.Zero, 1, ItemPrefix.Unreal);

            argsItem.Should().NotBeNull();
            argsItem.WrappedItem.Should().BeSameAs(item.WrappedItem);
        }
        
        [Theory]
        [InlineData(ItemType.CopperPickaxe, ItemType.IronPickaxe)]
        [InlineData(ItemType.StoneBlock, ItemType.None)]
        public void ItemSettingDefaults_ModifyType_IsCorrect(ItemType oldType, ItemType newType) {
            _itemService.ItemSettingDefaults += (sender, args) => {
                args.Type = newType;
            };

            var item = _itemService.SpawnItem(oldType, Vector2.Zero);

            item.Type.Should().Be(newType);
        }

        [Fact]
        public void ItemSettingDefaults_Handled_IsCorrect() {
            _itemService.ItemSettingDefaults += (sender, args) => {
                args.Handled = true;
            };

            var item = _itemService.SpawnItem(ItemType.SDMG, Vector2.Zero);

            item.Type.Should().Be(ItemType.None);
        }

        [Fact]
        public void ItemSetDefaults_IsCorrect() {
            IItem argsItem = null;
            _itemService.ItemSetDefaults += (sender, args) => {
                argsItem = args.Item;
                args.Item.Type.Should().Be(ItemType.SDMG);
                args.Item.StackSize.Should().Be(1);
            };

            var item = _itemService.SpawnItem(ItemType.SDMG, Vector2.Zero);
            argsItem.Should().NotBeNull();
            argsItem.WrappedItem.Should().BeSameAs(item.WrappedItem);
        }

        [Fact]
        public void ItemUpdating_IsCorrect() {
            IItem argsItem = null;
            _itemService.ItemUpdating += (sender, args) => {
                argsItem = args.Item;
            };
            var item = _itemService.SpawnItem(ItemType.SDMG, Vector2.Zero);

            item.WrappedItem.UpdateItem(item.Index);
            
            argsItem.Should().NotBeNull();
            argsItem.WrappedItem.Should().BeSameAs(item.WrappedItem);
        }

        [Fact]
        public void ItemUpdated_IsCorrect() {
            IItem argsItem = null;
            _itemService.ItemUpdated += (sender, args) => {
                argsItem = args.Item;
            };
            var item = _itemService.SpawnItem(ItemType.SDMG, Vector2.Zero);

            item.WrappedItem.UpdateItem(item.Index);
            
            argsItem.Should().NotBeNull();
            argsItem.WrappedItem.Should().BeSameAs(item.WrappedItem);
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
