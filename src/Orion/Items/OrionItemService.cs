// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Diagnostics;
using Orion.Collections;
using Orion.Events;
using Orion.Events.Items;
using Orion.Packets.DataStructures;
using Serilog;

namespace Orion.Items {
    [Service("orion-items")]
    internal sealed class OrionItemService : OrionService, IItemService {
        public IReadOnlyList<IItem> Items { get; }

        public OrionItemService(OrionKernel kernel, ILogger log) : base(kernel, log) {
            Debug.Assert(kernel != null);
            Debug.Assert(log != null);

            // Construct the `Items` array. Note that the last item should be ignored, as it is not a real item.
            Items = new WrappedReadOnlyList<OrionItem, Terraria.Item>(
                Terraria.Main.item.AsMemory(..^1),
                (itemIndex, terrariaItem) => new OrionItem(itemIndex, terrariaItem));

            OTAPI.Hooks.Item.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            OTAPI.Hooks.Item.PreUpdate = PreUpdateHandler;
        }

        public override void Dispose() {
            OTAPI.Hooks.Item.PreSetDefaultsById = null;
            OTAPI.Hooks.Item.PreUpdate = null;
        }

        public IItem? SpawnItem(ItemId id, Vector2f position, int stackSize = 1, ItemPrefix prefix = ItemPrefix.None) {
            // Not localized because this string is developer-facing.
            Log.Debug("Spawning {ItemId} x{ItemStackSize} at {Position}", id, stackSize, position);

            var itemIndex = Terraria.Item.NewItem(
                new Microsoft.Xna.Framework.Vector2(position.X, position.Y), Microsoft.Xna.Framework.Vector2.Zero,
                (int)id, stackSize, false, (int)prefix);
            return itemIndex >= 0 && itemIndex < Items.Count ? Items[itemIndex] : null;
        }

        private OTAPI.HookResult PreSetDefaultsByIdHandler(Terraria.Item terrariaItem, ref int itemId, ref bool _) {
            Debug.Assert(terrariaItem != null);

            var item = GetItem(terrariaItem);
            var evt = new ItemDefaultsEvent(item) { Id = (ItemId)itemId };
            Kernel.Raise(evt, Log);
            itemId = (int)evt.Id;
            return evt.IsCanceled() ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private OTAPI.HookResult PreUpdateHandler(Terraria.Item terrariaItem, ref int itemIndex) {
            Debug.Assert(terrariaItem != null);
            Debug.Assert(itemIndex >= 0 && itemIndex < Items.Count);

            // Set `whoAmI` since this is never done in the vanilla server.
            terrariaItem.whoAmI = itemIndex;

            var item = Items[itemIndex];
            var evt = new ItemTickEvent(item);
            Kernel.Raise(evt, Log);
            return evt.IsCanceled() ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        // Gets an `IItem` which corresponds to the given Terraria item. Retrieves the `IItem` from the `Items` array,
        // if possible.
        private IItem GetItem(Terraria.Item terrariaItem) {
            Debug.Assert(terrariaItem != null);

            var itemIndex = terrariaItem.whoAmI;
            Debug.Assert(itemIndex >= 0 && itemIndex < Items.Count);

            var isConcrete = terrariaItem == Terraria.Main.item[itemIndex];
            return isConcrete ? Items[itemIndex] : new OrionItem(terrariaItem);
        }
    }
}
