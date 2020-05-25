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
using System.Diagnostics;
using Orion.Collections;
using Orion.Entities;
using Orion.Events;
using Orion.Events.Items;
using Serilog;

namespace Orion.Items {
    [Service("orion-items")]
    internal sealed class OrionItemService : OrionService, IItemService {
        public IReadOnlyArray<IItem> Items { get; }

        public OrionItemService(OrionKernel kernel, ILogger log) : base(kernel, log) {
            Debug.Assert(kernel != null);
            Debug.Assert(log != null);

            // Construct the `Items` array. Note that the last item should be ignored, as it is not a real item.
            Items = new WrappedReadOnlyArray<OrionItem, Terraria.Item>(
                Terraria.Main.item.AsMemory(..^1),
                (itemIndex, terrariaItem) => new OrionItem(itemIndex, terrariaItem));

            OTAPI.Hooks.Item.PreSetDefaultsById = PreSetDefaultsByIdHandler;
        }

        public override void Dispose() {
            OTAPI.Hooks.Item.PreSetDefaultsById = null;
        }

        private OTAPI.HookResult PreSetDefaultsByIdHandler(Terraria.Item terrariaItem, ref int itemId, ref bool _) {
            Debug.Assert(terrariaItem != null);

            var item = GetItem(terrariaItem);
            var evt = new ItemDefaultsEvent(item, (ItemId)itemId);
            Kernel.Raise(evt, Log);
            itemId = (int)evt.Id;
            return evt.IsCanceled() ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        // Gets an `IItem` which corresponds to the given Terraria item. Retrieves the `IItem` from the `Items` array,
        // if possible.
        private IItem GetItem(Terraria.Item terrariaItem) {
            var itemIndex = terrariaItem.whoAmI;
            Debug.Assert(itemIndex >= 0 && itemIndex < Items.Count);

            var isConcrete = terrariaItem == Terraria.Main.item[itemIndex];
            return isConcrete ? Items[itemIndex] : new OrionItem(terrariaItem);
        }
    }
}
