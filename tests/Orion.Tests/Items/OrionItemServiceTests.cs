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
using Orion.Events;
using Orion.Events.Items;
using Orion.Packets.DataStructures;
using Serilog.Core;
using Xunit;

namespace Orion.Items {
    // These tests depend on Terraria state.
    [Collection("TerrariaTestsCollection")]
    public class OrionItemServiceTests {
        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Items_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            using var kernel = new OrionKernel(Logger.None);
            using var itemService = new OrionItemService(kernel, Logger.None);

            Assert.Throws<IndexOutOfRangeException>(() => itemService.Items[index]);
        }

        [Fact]
        public void Items_Item_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var itemService = new OrionItemService(kernel, Logger.None);
            var item = itemService.Items[1];

            Assert.Equal(1, item.Index);
            Assert.Same(Terraria.Main.item[1], ((OrionItem)item).Wrapped);
        }

        [Fact]
        public void Items_Item_GetMultipleTimes_ReturnsSameInstance() {
            using var kernel = new OrionKernel(Logger.None);
            using var itemService = new OrionItemService(kernel, Logger.None);

            var item = itemService.Items[0];
            var item2 = itemService.Items[0];

            Assert.Same(item, item2);
        }

        [Fact]
        public void Items_GetEnumerator() {
            using var kernel = new OrionKernel(Logger.None);
            using var itemService = new OrionItemService(kernel, Logger.None);

            var items = itemService.Items.ToList();

            for (var i = 0; i < items.Count; ++i) {
                Assert.Same(Terraria.Main.item[i], ((OrionItem)items[i]).Wrapped);
            }
        }

        [Fact]
        public void ItemDefaults_EventTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var itemService = new OrionItemService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<ItemDefaultsEvent>(evt => {
                Assert.Same(Terraria.Main.item[0], ((OrionItem)evt.Item).Wrapped);
                Assert.Equal(ItemId.Sdmg, evt.Id);
                isRun = true;
            }, Logger.None);

            Terraria.Main.item[0].SetDefaults((int)ItemId.Sdmg);

            Assert.True(isRun);
            Assert.Equal(ItemId.Sdmg, (ItemId)Terraria.Main.item[0].type);
        }

        [Fact]
        public void ItemDefaults_EventModified() {
            using var kernel = new OrionKernel(Logger.None);
            using var itemService = new OrionItemService(kernel, Logger.None);
            kernel.RegisterHandler<ItemDefaultsEvent>(evt => evt.Id = ItemId.DirtBlock, Logger.None);

            Terraria.Main.item[0].SetDefaults((int)ItemId.Sdmg);

            Assert.Equal(ItemId.DirtBlock, (ItemId)Terraria.Main.item[0].type);
        }

        [Fact]
        public void ItemDefaults_EventCanceled() {
            Terraria.Main.item[0] = new Terraria.Item { whoAmI = 0 }; 

            using var kernel = new OrionKernel(Logger.None);
            using var itemService = new OrionItemService(kernel, Logger.None);
            kernel.RegisterHandler<ItemDefaultsEvent>(evt => evt.Cancel(), Logger.None);

            Terraria.Main.item[0].SetDefaults((int)ItemId.Sdmg);

            Assert.Equal(ItemId.None, (ItemId)Terraria.Main.item[0].type);
        }

        [Fact]
        public void ItemTick_EventTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var itemService = new OrionItemService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<ItemTickEvent>(evt => {
                Assert.Same(Terraria.Main.item[0], ((OrionItem)evt.Item).Wrapped);
                isRun = true;
            }, Logger.None);

            Terraria.Main.item[0].UpdateItem(0);

            Assert.True(isRun);
        }

        [Theory]
        [InlineData(ItemId.StoneBlock, 100, ItemPrefix.None)]
        [InlineData(ItemId.Sdmg, 1, ItemPrefix.Unreal)]
        [InlineData(ItemId.Meowmere, 1, ItemPrefix.Legendary)]
        public void SpawnItem(ItemId id, int stackSize, ItemPrefix prefix) {
            using var kernel = new OrionKernel(Logger.None);
            using var itemService = new OrionItemService(kernel, Logger.None);
            var item = itemService.SpawnItem(id, Vector2f.Zero, stackSize, prefix);

            Assert.NotNull(item);
            Assert.Equal(id, item!.Id);
            Assert.Equal(stackSize, item!.StackSize);
            Assert.Equal(prefix, item!.Prefix);
        }
    }
}
