using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Orion.Items {
    /// <summary>
    /// Orion's implementation of <see cref="IItemList"/>.
    /// </summary>
    internal sealed class OrionItemList : IItemList {
        private readonly IList<Terraria.Item> _terrariaItems;
        private readonly IList<OrionItem> _items;

        public int Count => _items.Count;

        public IItem this[int index] {
            get {
                if (index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException(
                        "Index was out of range. Must be non-negative and less than the length of the collection.");
                }

                if (_items[index]?.WrappedItem != _terrariaItems[index]) {
                    _items[index] = new OrionItem(_terrariaItems[index]);
                }

                return _items[index];
            }
        }

        public OrionItemList(IList<Terraria.Item> terrariaItems) {
            Debug.Assert(terrariaItems != null, $"{nameof(terrariaItems)} should not be null.");

            _terrariaItems = terrariaItems;
            _items = new OrionItem[_terrariaItems.Count];
        }

        public IEnumerator<IItem> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
