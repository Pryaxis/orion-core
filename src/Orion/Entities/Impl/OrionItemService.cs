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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Orion.Events;
using Orion.Events.Entities;
using OTAPI;

namespace Orion.Entities.Impl {
    internal sealed class OrionItemService : OrionService, IItemService {
        [NotNull, ItemNotNull] private readonly IList<Terraria.Item> _terrariaItems;
        [NotNull, ItemCanBeNull] private readonly IList<OrionItem> _items;

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";

        // Subtract 1 from the count. This is because Terraria has an extra slot.
        public int Count => _items.Count - 1;

        [NotNull]
        public IItem this[int index] {
            get {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

                if (_items[index]?.Wrapped != _terrariaItems[index]) {
                    _items[index] = new OrionItem(_terrariaItems[index]);
                }

                Debug.Assert(_items[index] != null, "_items[index] != null");
                return _items[index];
            }
        }

        public EventHandlerCollection<ItemSetDefaultsEventArgs> ItemSetDefaults { get; set; }
        public EventHandlerCollection<ItemUpdateEventArgs> ItemUpdate { get; set; }

        public OrionItemService() {
            Debug.Assert(Terraria.Main.item != null, "Terraria.Main.item != null");
            Debug.Assert(Terraria.Main.item.All(i => i != null), "Terraria.Main.item.All(i => i != null)");

            _terrariaItems = Terraria.Main.item;
            _items = new OrionItem[_terrariaItems.Count];

            Hooks.Item.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            Hooks.Item.PreUpdate = PreUpdateHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) return;

            Hooks.Item.PreSetDefaultsById = null;
            Hooks.Item.PreUpdate = null;
        }

        public IEnumerator<IItem> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public IItem SpawnItem(ItemType itemType, Vector2 position, int stackSize = 1,
                               ItemPrefix prefix = ItemPrefix.None) {
            // Terraria has a mechanism of item caching which allows, for instance, The Plan to drop all wires at once.
            // We need to disable that temporarily so that our item *definitely* spawns.
            var oldItemCache = Terraria.Item.itemCaches[(int)itemType];
            Terraria.Item.itemCaches[(int)itemType] = -1;

            var itemIndex =
                Terraria.Item.NewItem(position, Vector2.Zero, (int)itemType, stackSize, prefixGiven: (int)prefix);
            Terraria.Item.itemCaches[(int)itemType] = oldItemCache;
            return itemIndex >= 0 && itemIndex < Count ? this[itemIndex] : null;
        }

        private HookResult PreSetDefaultsByIdHandler([NotNull] Terraria.Item terrariaItem, ref int type,
                                                     ref bool noMaterialCheck) {
            var item = new OrionItem(terrariaItem);
            var args = new ItemSetDefaultsEventArgs(item, (ItemType)type);
            ItemSetDefaults?.Invoke(this, args);
            type = (int)args.ItemType;
            return args.IsCanceled ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreUpdateHandler([NotNull] Terraria.Item terrariaItem, ref int i) {
            var item = new OrionItem(terrariaItem);
            var args = new ItemUpdateEventArgs(item);
            ItemUpdate?.Invoke(this, args);
            return args.IsCanceled ? HookResult.Cancel : HookResult.Continue;
        }
    }
}
