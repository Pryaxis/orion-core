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
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Orion.Events;
using Orion.Events.Items;
using Orion.Properties;
using Orion.Utils;
using OTAPI;
using Serilog;
using Main = Terraria.Main;
using TerrariaItem = Terraria.Item;

namespace Orion.Items {
    [Service("orion-items")]
    internal sealed class OrionItemService : OrionService, IItemService {
        public IReadOnlyArray<IItem> Items { get; }
        public EventHandlerCollection<ItemSetDefaultsEventArgs> ItemSetDefaults { get; }
        public EventHandlerCollection<ItemUpdateEventArgs> ItemUpdate { get; }

        public OrionItemService(ILogger log) : base(log) {
            Debug.Assert(log != null, "log should not be null");
            Debug.Assert(Main.item != null, "Terraria items should not be null");

            // Ignore the last item since it is used as a failure slot.
            Items = new WrappedReadOnlyArray<OrionItem, TerrariaItem>(
                Main.item.AsMemory(..^1), (itemIndex, terrariaItem) => new OrionItem(itemIndex, terrariaItem));

            ItemSetDefaults = new EventHandlerCollection<ItemSetDefaultsEventArgs>(log);
            ItemUpdate = new EventHandlerCollection<ItemUpdateEventArgs>(log);

            Hooks.Item.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            Hooks.Item.PreUpdate = PreUpdateHandler;
        }

        public override void Dispose() {
            Hooks.Item.PreSetDefaultsById = null;
            Hooks.Item.PreUpdate = null;
        }

        public IItem? SpawnItem(ItemType type, Vector2 position,
                int stackSize = 1, ItemPrefix prefix = ItemPrefix.None) {
            Log.Debug(Resources.ItemService_SpawnItem, type, position, stackSize);

            // Terraria has a mechanism of item caching which allows, for instance, the Grand Design to drop all wires
            // at once. We need to disable that temporarily so that our item *definitely* spawns.
            var oldItemCache = TerrariaItem.itemCaches[(int)type];
            TerrariaItem.itemCaches[(int)type] = -1;

            var itemIndex =
                TerrariaItem.NewItem(position, Vector2.Zero, (int)type, stackSize, prefixGiven: (int)prefix);
            TerrariaItem.itemCaches[(int)type] = oldItemCache;
            return itemIndex >= 0 && itemIndex < Items.Count ? Items[itemIndex] : null;
        }

        private IItem GetItem(TerrariaItem terrariaItem) {
            Debug.Assert(terrariaItem.whoAmI >= 0 && terrariaItem.whoAmI < Items.Count,
                "Terraria item index should be valid");

            // We want to retrieve the world item if this item is real. Otherwise, return a "fake" item.
            return terrariaItem == Main.item[terrariaItem.whoAmI]
                ? Items[terrariaItem.whoAmI]
                : new OrionItem(terrariaItem);
        }

        private HookResult PreSetDefaultsByIdHandler(TerrariaItem terrariaItem, ref int itemType, ref bool _) {
            Debug.Assert(terrariaItem != null, "Terraria item should not be null");

            var item = GetItem(terrariaItem);
            var args = new ItemSetDefaultsEventArgs(item, (ItemType)itemType);
            ItemSetDefaults.Invoke(this, args);
            if (args.IsCanceled()) {
                return HookResult.Cancel;
            }

            itemType = (int)args.ItemType;
            return HookResult.Continue;
        }

        private HookResult PreUpdateHandler(TerrariaItem terrariaItem, ref int itemIndex) {
            Debug.Assert(terrariaItem != null, "Terraria item should not be null");
            Debug.Assert(itemIndex >= 0 && itemIndex < Items.Count, "item index should be valid");

            // Set terrariaItem.whoAmI since this is never done in vanilla.
            terrariaItem.whoAmI = itemIndex;

            var args = new ItemUpdateEventArgs(Items[itemIndex]);
            ItemUpdate.Invoke(this, args);
            return args.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }
    }
}
