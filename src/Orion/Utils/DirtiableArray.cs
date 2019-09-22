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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Orion.Events;

namespace Orion.Utils {
    /// <summary>
    /// Represents a dirtiable array of objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DirtiableArray<T> : IArray<T>, IDirtiable {
        internal readonly T[] _array;
        private bool _isDirty;

        private static bool ContainsDirtiableElements => typeof(IDirtiable).IsAssignableFrom(typeof(T));

        /// <inheritdoc />
        public int Count => _array.Length;

        /// <inheritdoc cref="IArray{T}.this" />
        /// <exception cref="ArgumentNullException">
        /// The array does not allow <c>null</c> elements and <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The array does not allow <c>null</c> elements and the element is <c>null</c>.
        /// </exception>
        public T this[int index] {
            get {
                var value = _array[index];
                if (!AllowsNull && value == null) throw new InvalidOperationException("Element is null.");

                return value;
            }
            set {
                if (!AllowsNull && value == null) throw new ArgumentNullException(nameof(value));

                _array[index] = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        public bool IsDirty =>
            _isDirty || ContainsDirtiableElements && this.Cast<IDirtiable>().Any(d => d?.IsDirty == true);

        /// <summary>
        /// Gets a value indicating whether the array allows <c>null</c> elements.
        /// </summary>
        public bool AllowsNull { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirtiableArray{T}"/> class with the specified count and option
        /// to allow <c>null</c> elements.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="allowsNull">A value indicating whether to allow <c>null</c> elements.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        public DirtiableArray(int count, bool allowsNull = false) {
            if (count <= 0) {
                throw new ArgumentOutOfRangeException(nameof(count), "Count is negative.");
            }

            _array = new T[count];
            AllowsNull = allowsNull;
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
