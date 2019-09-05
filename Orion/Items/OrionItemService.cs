using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Orion.Framework;
using Orion.Items.Events;
using OTAPI;

namespace Orion.Items {
    /// <summary>
    /// Orion's implementation of <see cref="IItemService"/>.
    /// </summary>
    internal sealed class OrionItemService : OrionService, IItemService {
        private readonly IList<Terraria.Item> _terrariaItems;
        private readonly IList<OrionItem> _items;

        public override string Author => "Pryaxis";
        public override string Name => "Orion Item Service";
        
        /*
         * We need to subtract 1 from the count. This is because Terraria actually has an extra slot which is reserved
         * as a failure index.
         */
        public int Count => _items.Count - 1;

        public IItem this[int index] {
            get {
                if (index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                if (_items[index]?.Wrapped != _terrariaItems[index]) {
                    _items[index] = new OrionItem(_terrariaItems[index]);
                }

                var item = _items[index];
                Debug.Assert(item != null, $"{nameof(item)} should not be null.");
                Debug.Assert(item.Wrapped != null, $"{nameof(item.Wrapped)} should not be null.");
                return item;
            }
        }

        public event EventHandler<SettingItemDefaultsEventArgs> SettingItemDefaults;
        public event EventHandler<SetItemDefaultsEventArgs> SetItemDefaults;
        public event EventHandler<UpdatingItemEventArgs> UpdatingItem;
        public event EventHandler<UpdatedItemEventArgs> UpdatedItem;

        public OrionItemService() {
            _terrariaItems = Terraria.Main.item;
            _items = new OrionItem[_terrariaItems.Count];

            Hooks.Item.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            Hooks.Item.PostSetDefaultsById = PostSetDefaultsByIdHandler;
            Hooks.Item.PreUpdate = PreUpdateHandler;
            Hooks.Item.PostUpdate = PostUpdateHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) {
                return;
            }

            Hooks.Item.PreSetDefaultsById = null;
            Hooks.Item.PostSetDefaultsById = null;
            Hooks.Item.PreUpdate = null;
            Hooks.Item.PostUpdate = null;
        }

        public IEnumerator<IItem> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IItem SpawnItem(ItemType type, Vector2 position, int stackSize = 1,
                                    ItemPrefix prefix = ItemPrefix.None) {
            /*
             * Terraria has this mechanism of item caching which allows, for instance, The Plan to drop all wires at
             * once. We need to disable that temporarily so that our item *definitely* spawns.
             */
            var oldItemCache = Terraria.Item.itemCaches[(int)type];
            Terraria.Item.itemCaches[(int)type] = -1;

            var itemIndex = Terraria.Item.NewItem(position, Vector2.Zero, (int)type,
                                                  stackSize, prefixGiven: (int)prefix);

            Debug.Assert(itemIndex >= 0 && itemIndex < Count, $"{nameof(itemIndex)} should be a valid index.");

            Terraria.Item.itemCaches[(int)type] = oldItemCache;
            return this[itemIndex];
        }


        private HookResult PreSetDefaultsByIdHandler(Terraria.Item terrariaItem, ref int type,
                                                     ref bool noMaterialCheck) {
            var item = new OrionItem(terrariaItem);
            var args = new SettingItemDefaultsEventArgs(item, (ItemType)type);
            SettingItemDefaults?.Invoke(this, args);

            type = (int)args.Type;
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostSetDefaultsByIdHandler(Terraria.Item terrariaItem, ref int type, ref bool noMaterialCheck) {
            var item = new OrionItem(terrariaItem);
            var args = new SetItemDefaultsEventArgs(item);
            SetItemDefaults?.Invoke(this, args);
        }

        private HookResult PreUpdateHandler(Terraria.Item terrariaItem, ref int i) {
            var item = new OrionItem(terrariaItem);
            var args = new UpdatingItemEventArgs(item);
            UpdatingItem?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostUpdateHandler(Terraria.Item terrariaItem, int i) {
            var item = new OrionItem(terrariaItem);
            var args = new UpdatedItemEventArgs(item);
            UpdatedItem?.Invoke(this, args);
        }
    }
}
