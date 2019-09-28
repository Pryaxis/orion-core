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
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Orion.Utils {
    /// <summary>
    /// Represents a dirtiable array of objects.
    /// </summary>
    /// <typeparam name="T">The type of element.</typeparam>
    public sealed class DirtiableArray<T> : IArray<T>, IDirtiable {
        internal readonly T[] _array;
        private bool _isDirty;

        private static bool ContainsDirtiableElements => typeof(IDirtiable).IsAssignableFrom(typeof(T));

        /// <inheritdoc />
        public int Count => _array.Length;

        /// <inheritdoc cref="IArray{T}.this" />
        public T this[int index] {
            get => _array[index];
            set {
                _array[index] = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        public bool IsDirty =>
            _isDirty || ContainsDirtiableElements && this.Cast<IDirtiable>().Any(d => d?.IsDirty == true);

        /// <summary>
        /// Initializes a new instance of the <see cref="DirtiableArray{T}"/> class with the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        public DirtiableArray(int count) {
            if (count <= 0) {
                throw new ArgumentOutOfRangeException(nameof(count), "Count is negative.");
            }

            _array = new T[count];
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_array).GetEnumerator();

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public void Clean() {
            _isDirty = false;
            if (!ContainsDirtiableElements) return;

            foreach (var dirtiable in this.Cast<IDirtiable>()) {
                dirtiable?.Clean();
            }
        }
    }
}
