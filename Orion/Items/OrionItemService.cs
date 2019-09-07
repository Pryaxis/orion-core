using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Orion.Hooks;
using Orion.Items.Events;

namespace Orion.Items {
    internal sealed class OrionItemService : OrionService, IItemService {
        private readonly IList<Terraria.Item> _terrariaItems;
        private readonly IList<OrionItem> _items;
        
        [ExcludeFromCodeCoverage]
        public override string Author => "Pryaxis";

        [ExcludeFromCodeCoverage]
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

        public HookHandlerCollection<SettingItemDefaultsEventArgs> SettingItemDefaults { get; set; }
        public HookHandlerCollection<SetItemDefaultsEventArgs> SetItemDefaults { get; set; }
        public HookHandlerCollection<UpdatingItemEventArgs> UpdatingItem { get; set; }
        public HookHandlerCollection<UpdatedItemEventArgs> UpdatedItem { get; set; }

        public OrionItemService() {
            _terrariaItems = Terraria.Main.item;
            _items = new OrionItem[_terrariaItems.Count];

            OTAPI.Hooks.Item.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            OTAPI.Hooks.Item.PostSetDefaultsById = PostSetDefaultsByIdHandler;
            OTAPI.Hooks.Item.PreUpdate = PreUpdateHandler;
            OTAPI.Hooks.Item.PostUpdate = PostUpdateHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) {
                return;
            }

            OTAPI.Hooks.Item.PreSetDefaultsById = null;
            OTAPI.Hooks.Item.PostSetDefaultsById = null;
            OTAPI.Hooks.Item.PreUpdate = null;
            OTAPI.Hooks.Item.PostUpdate = null;
        }

        public IEnumerator<IItem> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }
        
        [ExcludeFromCodeCoverage]
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


        private OTAPI.HookResult PreSetDefaultsByIdHandler(Terraria.Item terrariaItem, ref int type,
                                                     ref bool noMaterialCheck) {
            var item = new OrionItem(terrariaItem);
            var args = new SettingItemDefaultsEventArgs(item, (ItemType)type);
            SettingItemDefaults?.Invoke(this, args);

            type = (int)args.Type;
            return args.Handled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private void PostSetDefaultsByIdHandler(Terraria.Item terrariaItem, ref int type, ref bool noMaterialCheck) {
            var item = new OrionItem(terrariaItem);
            var args = new SetItemDefaultsEventArgs(item);
            SetItemDefaults?.Invoke(this, args);
        }

        private OTAPI.HookResult PreUpdateHandler(Terraria.Item terrariaItem, ref int i) {
            var item = new OrionItem(terrariaItem);
            var args = new UpdatingItemEventArgs(item);
            UpdatingItem?.Invoke(this, args);

            return args.Handled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private void PostUpdateHandler(Terraria.Item terrariaItem, int i) {
            var item = new OrionItem(terrariaItem);
            var args = new UpdatedItemEventArgs(item);
            UpdatedItem?.Invoke(this, args);
        }
    }
}
