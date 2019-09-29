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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Orion.Utils;

namespace Orion.Items {
    internal sealed class OrionItemArray : IReadOnlyArray<IItem> {
        private readonly IList<Terraria.Item> _terrariaItems;
        private readonly IList<OrionItem> _items;

        public int Count => _items.Count;

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

        public OrionItemArray(IList<Terraria.Item> terrariaItems) {
            Debug.Assert(terrariaItems != null, "terrariaItems != null");

            _terrariaItems = terrariaItems;
            _items = new OrionItem[_terrariaItems.Count];
        }

        public IEnumerator<IItem> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
