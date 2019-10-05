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
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Events.Extensions;
using Xunit;
using Main = Terraria.Main;
using TerrariaItem = Terraria.Item;

namespace Orion.Items {
    [Collection("TerrariaTestsCollection")]
    public class OrionItemServiceTests : IDisposable {
        private readonly IItemService _itemService;

        public OrionItemServiceTests() {
            for (var i = 0; i < Main.item.Length; ++i) {
                Main.item[i] = new TerrariaItem { whoAmI = i };
            }

            _itemService = new OrionItemService();
        }

        public void Dispose() => _itemService.Dispose();

        [Fact]
        public void Items_Item_Get() {
            var item = _itemService.Items[1];

            item.Index.Should().Be(1);
            ((OrionItem)item).Wrapped.Should().BeSameAs(Main.item[1]);
        }

        [Fact]
        public void Items_Item_GetMultipleTimes_ReturnsSameInstance() {
            var item = _itemService.Items[0];
            var item2 = _itemService.Items[0];

            item.Should().BeSameAs(item2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Items_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            Func<IItem> func = () => _itemService.Items[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Items_GetEnumerator() {
            var items = _itemService.Items.ToList();

            for (var i = 0; i < items.Count; ++i) {
                ((OrionItem)items[i]).Wrapped.Should().BeSameAs(Main.item[i]);
            }
        }

        [Fact]
        public void ItemSetDefaults() {
            var isRun = false;
            _itemService.ItemSetDefaults += (sender, args) => {
                isRun = true;
                ((OrionItem)args.Item).Wrapped.Should().BeSameAs(Main.item[0]);
                args.ItemType.Should().Be(ItemType.Sdmg);
            };

            Main.item[0].SetDefaults((int)ItemType.Sdmg);

            isRun.Should().BeTrue();
        }

        [Theory]
        [InlineData(ItemType.CopperPickaxe, ItemType.IronPickaxe)]
        [InlineData(ItemType.StoneBlock, ItemType.None)]
        public void ItemSetDefaults_ModifyType_IsCorrect(ItemType oldType, ItemType newType) {
            _itemService.ItemSetDefaults += (sender, args) => {
                args.ItemType = newType;
            };

            Main.item[0].SetDefaults((int)oldType);

            Main.item[0].type.Should().Be((int)newType);
        }

        [Fact]
        public void ItemSetDefaults_Canceled() {
            _itemService.ItemSetDefaults += (sender, args) => {
                args.Cancel();
            };

            Main.item[0].SetDefaults((int)ItemType.Sdmg);

            Main.item[0].type.Should().Be(0);
        }

        [Fact]
        public void ItemUpdate() {
            var isRun = false;
            _itemService.ItemUpdate += (sender, args) => {
                isRun = true;
                ((OrionItem)args.Item).Wrapped.Should().BeSameAs(Main.item[0]);
            };

            Main.item[0].UpdateItem(0);

            isRun.Should().BeTrue();
        }

        public static readonly IEnumerable<object[]> SpawnItemData = new List<object[]> {
            new object[] {ItemType.StoneBlock, 100, ItemPrefix.None},
            new object[] {ItemType.Sdmg, 1, ItemPrefix.Unreal},
            new object[] {ItemType.Meowmere, 1, ItemPrefix.Legendary}
        };

        [Theory]
        [MemberData(nameof(SpawnItemData))]
        public void SpawnItem_IsCorrect(ItemType type, int stackSize, ItemPrefix prefix) {
            var item = _itemService.SpawnItem(type, Vector2.Zero, stackSize, prefix);

            item.Should().NotBeNull();
            item.Type.Should().Be(type);
            item.StackSize.Should().Be(stackSize);
            item.Prefix.Should().Be(prefix);
        }

        [Theory]
        [MemberData(nameof(SpawnItemData))]
        public void SpawnItem_CachedItem_IsCorrect(ItemType type, int stackSize, ItemPrefix prefix) {
            TerrariaItem.itemCaches[(int)type] = 0;

            var item = _itemService.SpawnItem(type, Vector2.Zero, stackSize, prefix);

            item.Should().NotBeNull();
            item.Type.Should().Be(type);
            item.StackSize.Should().Be(stackSize);
            item.Prefix.Should().Be(prefix);
        }
    }
}
