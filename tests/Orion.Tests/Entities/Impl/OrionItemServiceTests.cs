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
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Xunit;

namespace Orion.Entities.Impl {
    [Collection("TerrariaTestsCollection")]
    public class OrionItemServiceTests : IDisposable {
        private readonly IItemService _itemService;

        public OrionItemServiceTests() {
            for (var i = 0; i < Terraria.Main.item.Length; ++i) {
                Terraria.Main.item[i] = new Terraria.Item {whoAmI = i};
            }

            _itemService = new OrionItemService();
        }

        public void Dispose() {
            _itemService.Dispose();
        }

        [Fact]
        public void GetItem_IsCorrect() {
            var item = (OrionItem)_itemService[0];

            item.Wrapped.Should().BeSameAs(Terraria.Main.item[0]);
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
        public void ItemSetDefaults_IsCorrect() {
            OrionItem argsItem = null;
            _itemService.ItemSetDefaults += (sender, args) => {
                argsItem = (OrionItem)args.Item;
                args.ItemType.Should().Be(ItemType.Sdmg);
            };

            Terraria.Main.item[0].SetDefaults((int)ItemType.Sdmg);

            argsItem.Should().NotBeNull();
            argsItem.Wrapped.Should().BeSameAs(Terraria.Main.item[0]);
        }

        [Theory]
        [InlineData(ItemType.CopperPickaxe, ItemType.IronPickaxe)]
        [InlineData(ItemType.StoneBlock, ItemType.None)]
        public void ItemSetDefaults_ModifyType_IsCorrect(ItemType oldType, ItemType newType) {
            _itemService.ItemSetDefaults += (sender, args) => {
                args.ItemType = newType;
            };

            Terraria.Main.item[0].SetDefaults((int)oldType);

            Terraria.Main.item[0].type.Should().Be((int)newType);
        }

        [Fact]
        public void ItemSetDefaults_Canceled_IsCorrect() {
            _itemService.ItemSetDefaults += (sender, args) => {
                args.IsCanceled = true;
            };

            Terraria.Main.item[0].SetDefaults((int)ItemType.Sdmg);

            Terraria.Main.item[0].type.Should().Be(0);
        }

        [Fact]
        public void ItemUpdate_IsCorrect() {
            OrionItem argsItem = null;
            _itemService.ItemUpdate += (sender, args) => {
                argsItem = (OrionItem)args.Item;
            };

            Terraria.Main.item[0].UpdateItem(0);

            argsItem.Should().NotBeNull();
            argsItem.Wrapped.Should().BeSameAs(Terraria.Main.item[0]);
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var items = _itemService.ToList();

            for (var i = 0; i < items.Count; ++i) {
                ((OrionItem)items[i]).Wrapped.Should().BeSameAs(Terraria.Main.item[i]);
            }
        }

        public static readonly IEnumerable<object[]> SpawnItemData = new List<object[]> {
            new object[] {ItemType.StoneBlock, 100, ItemPrefix.None},
            new object[] {ItemType.Sdmg, 1, ItemPrefix.Unreal},
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
    }
}
