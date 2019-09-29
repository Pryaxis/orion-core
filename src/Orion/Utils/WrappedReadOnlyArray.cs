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

namespace Orion.Utils {
    internal sealed class WrappedReadOnlyArray<T, TWrapped> : IReadOnlyArray<T>
        where T : class, IWrapped<TWrapped>
        where TWrapped : class {
        private readonly Memory<TWrapped> _wrappedItems;
        private readonly Func<int, TWrapped, T> _converter;
        private readonly T?[] _items;

        public int Count => _items.Length;

        public T this[int index] {
            get {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

                ref var wrappedItem = ref _wrappedItems.Span[index];
                ref var item = ref _items[index];
#pragma warning disable 618
                if (item?.Wrapped != wrappedItem) {
#pragma warning restore 618
                    return item = _converter(index, wrappedItem);
                }

                Debug.Assert(item != null, "_items[index] != null");
                return item;
            }
        }

        public WrappedReadOnlyArray(Memory<TWrapped> wrappedItems, Func<int, TWrapped, T> converter) {
            foreach (var wrappedItem in wrappedItems.Span) {
                Debug.Assert(wrappedItem != null, "wrappedItem != null");
            }

            Debug.Assert(converter != null, "converter != null");

            _wrappedItems = wrappedItems;
            _converter = converter;
            _items = new T?[_wrappedItems.Length];
        }

        public IEnumerator<T> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
