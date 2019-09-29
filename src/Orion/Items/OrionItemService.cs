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
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Orion.Events;
using Orion.Events.Extensions;
using Orion.Events.Items;
using Orion.Utils;
using OTAPI;
using Main = Terraria.Main;
using TerrariaItem = Terraria.Item;

namespace Orion.Items {
    internal sealed class OrionItemService : OrionService, IItemService {
        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";

        public IReadOnlyArray<IItem> Items { get; }
        public EventHandlerCollection<ItemSetDefaultsEventArgs>? ItemSetDefaults { get; set; }
        public EventHandlerCollection<ItemUpdateEventArgs>? ItemUpdate { get; set; }

        public OrionItemService() {
            // Ignore the last item since it is used as a failure slot.
            Items = new WrappedReadOnlyArray<OrionItem, TerrariaItem>(
                Main.item.AsMemory(..^1),
                (itemIndex, terrariaItem) => new OrionItem(itemIndex, terrariaItem));

            Hooks.Item.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            Hooks.Item.PreUpdate = PreUpdateHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) return;

            Hooks.Item.PreSetDefaultsById = null;
            Hooks.Item.PreUpdate = null;
        }

        public IItem? SpawnItem(ItemType itemType, Vector2 position, int stackSize = 1,
                                ItemPrefix prefix = ItemPrefix.None) {
            // Terraria has a mechanism of item caching which allows, for instance, the Grand Design to drop all wires
            // at once. We need to disable that temporarily so that our item *definitely* spawns.
            var oldItemCache = TerrariaItem.itemCaches[(int)itemType];
            TerrariaItem.itemCaches[(int)itemType] = -1;

            var itemIndex =
                TerrariaItem.NewItem(position, Vector2.Zero, (int)itemType, stackSize, prefixGiven: (int)prefix);
            TerrariaItem.itemCaches[(int)itemType] = oldItemCache;
            return itemIndex >= 0 && itemIndex < Items.Count ? Items[itemIndex] : null;
        }

        private IItem GetItem(TerrariaItem terrariaItem) {
            Debug.Assert(terrariaItem.whoAmI >= 0 && terrariaItem.whoAmI < Items.Count,
                         "terrariaItem.whoAmI >= 0 && terrariaItem.whoAmI < Items.Count");

            return terrariaItem == Main.item[terrariaItem.whoAmI]
                ? Items[terrariaItem.whoAmI]
                : new OrionItem(terrariaItem);
        }

        private HookResult PreSetDefaultsByIdHandler(TerrariaItem terrariaItem, ref int itemType, ref bool _) {
            Debug.Assert(terrariaItem != null, "terrariaItem != null");

            var item = GetItem(terrariaItem);
            var args = new ItemSetDefaultsEventArgs(item, (ItemType)itemType);
            ItemSetDefaults?.Invoke(this, args);
            if (args.IsCanceled()) return HookResult.Cancel;

            itemType = (int)args.ItemType;
            return HookResult.Continue;
        }

        private HookResult PreUpdateHandler(TerrariaItem terrariaItem, ref int itemIndex) {
            Debug.Assert(terrariaItem != null, "terrariaItem != null");
            Debug.Assert(itemIndex >= 0 && itemIndex < Items.Count, "itemIndex >= 0 && itemIndex < Items.Count");

            // Set terrariaItem.whoAmI since this is never done in vanilla.
            terrariaItem.whoAmI = itemIndex;

            var args = new ItemUpdateEventArgs(Items[itemIndex]);
            ItemUpdate?.Invoke(this, args);
            return args.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }
    }
}
