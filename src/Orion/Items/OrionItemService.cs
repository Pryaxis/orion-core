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

namespace Orion.Items {
    internal sealed class OrionItemService : OrionService, IItemService {
        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";

        public IReadOnlyArray<IItem> Items { get; }
        public EventHandlerCollection<ItemSetDefaultsEventArgs>? ItemSetDefaults { get; set; }
        public EventHandlerCollection<ItemUpdateEventArgs>? ItemUpdate { get; set; }

        public OrionItemService() {
            // Ignore the last item since it is used as a failure slot.
            Items = new WrappedReadOnlyArray<OrionItem, Terraria.Item>(
                Terraria.Main.item.AsMemory(..^1),
                (_, terrariaItem) => new OrionItem(terrariaItem));

            Hooks.Item.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            Hooks.Item.PreUpdate = PreUpdateHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) return;

            Hooks.Item.PreSetDefaultsById = null;
            Hooks.Item.PreUpdate = null;
        }

        /// <inheritdoc />
        public IItem? SpawnItem(ItemType itemType, Vector2 position, int stackSize = 1,
                                ItemPrefix prefix = ItemPrefix.None) {
            // Terraria has a mechanism of item caching which allows, for instance, The Plan to drop all wires at once.
            // We need to disable that temporarily so that our item *definitely* spawns.
            var oldItemCache = Terraria.Item.itemCaches[(int)itemType];
            Terraria.Item.itemCaches[(int)itemType] = -1;

            var itemIndex =
                Terraria.Item.NewItem(position, Vector2.Zero, (int)itemType, stackSize, prefixGiven: (int)prefix);
            Terraria.Item.itemCaches[(int)itemType] = oldItemCache;
            return itemIndex >= 0 && itemIndex < Items.Count ? Items[itemIndex] : null;
        }

        private HookResult PreSetDefaultsByIdHandler(Terraria.Item terrariaItem, ref int itemType,
                                                     ref bool noMaterialCheck) {
            Debug.Assert(terrariaItem != null, "terrariaItem != null");

            var item = new OrionItem(terrariaItem);
            var args = new ItemSetDefaultsEventArgs(item, (ItemType)itemType);
            ItemSetDefaults?.Invoke(this, args);
            if (args.IsCanceled()) return HookResult.Cancel;

            itemType = (int)args.ItemType;
            return HookResult.Continue;
        }

        private HookResult PreUpdateHandler(Terraria.Item terrariaItem, ref int itemIndex) {
            Debug.Assert(terrariaItem != null, "terrariaItem != null");

            var item = new OrionItem(terrariaItem);
            var args = new ItemUpdateEventArgs(item);
            ItemUpdate?.Invoke(this, args);
            return args.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }
    }
}
