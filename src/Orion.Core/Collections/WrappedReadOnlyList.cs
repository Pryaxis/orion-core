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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Orion.Core.Entities;

namespace Orion.Core.Collections
{
    // Wraps an array of type `TWrapped` to act as a read-only array of type `T`. This is extremely useful for wrapping
    // Terraria arrays as Orion interface arrays.
    internal sealed class WrappedReadOnlyList<T, TWrapped> : IReadOnlyList<T> where T : class, IWrapping<TWrapped>
    {
        private readonly ReadOnlyMemory<TWrapped> _wrappedItems;
        private readonly Func<int, TWrapped, T> _converter;
        private readonly T?[] _items;

        public WrappedReadOnlyList(ReadOnlyMemory<TWrapped> wrappedItems, Func<int, TWrapped, T> converter)
        {
            Debug.Assert(converter != null);

            _wrappedItems = wrappedItems;
            _converter = converter;
            _items = new T?[wrappedItems.Length];
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    // Not localized because this string is developer-facing.
                    throw new IndexOutOfRangeException($"Index out of range (expected: 0 to {Count - 1})");
                }

                var wrappedItem = _wrappedItems.Span[index];
                ref var item = ref _items[index];
                if (item is null || !ReferenceEquals(item.Wrapped, wrappedItem))
                {
                    item = _converter(index, wrappedItem);
                }

                return item;
            }
        }

        public int Count => _items.Length;

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; ++i)
            {
                yield return this[i];
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
