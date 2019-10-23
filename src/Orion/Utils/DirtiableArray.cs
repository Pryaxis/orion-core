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

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Destructurama.Attributed;

namespace Orion.Utils {
    internal sealed class DirtiableArray<T> : IArray<T>, IDirtiable {
        private static readonly bool _containsDirtiableElements = typeof(IDirtiable).IsAssignableFrom(typeof(T));

        private readonly T[] _array;

        private bool _isDirty;

        public int Count => _array.Length;

        public T this[int index] {
            get => _array[index];
            set {
                _array[index] = value;
                _isDirty = true;
            }
        }

        [NotLogged]
        public bool IsDirty =>
            _isDirty || _containsDirtiableElements && this.Cast<IDirtiable>().Any(d => d?.IsDirty == true);

        public DirtiableArray(int count) {
            Debug.Assert(count > 0, "count should be positive");

            _array = new T[count];
        }

        public DirtiableArray(T[] array) {
            Debug.Assert(array != null, "array should not be null");

            _array = array;
        }

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_array).GetEnumerator();

        public void Clean() {
            _isDirty = false;
            if (!_containsDirtiableElements) {
                return;
            }

            foreach (var dirtiable in this.Cast<IDirtiable>()) {
                dirtiable?.Clean();
            }
        }
    }
}
