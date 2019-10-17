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

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Orion.Utils {
    internal sealed class DirtiableList<T> : IList<T>, IDirtiable {
        private static readonly bool _containsDirtiableElements = typeof(IDirtiable).IsAssignableFrom(typeof(T));

        private readonly IList<T> _list = new List<T>();

        private bool _isDirty;

        public int Count => _list.Count;

        public bool IsDirty =>
            _isDirty || _containsDirtiableElements && this.Cast<IDirtiable>().Any(d => d?.IsDirty == true);

        public T this[int index] {
            get => _list[index];
            set {
                _list[index] = value;
                _isDirty = true;
            }
        }

        bool ICollection<T>.IsReadOnly => false;

        public DirtiableList() { }

        public DirtiableList(IList<T> list) {
            _list = list;
        }

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(T item) {
            _list.Add(item);
            _isDirty = true;
        }

        public void Clear() {
            _list.Clear();
            _isDirty = true;
        }

        [Pure]
        public bool Contains(T item) => _list.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public bool Remove(T item) {
            var result = _list.Remove(item);
            _isDirty = true;
            return result;
        }

        public int IndexOf(T item) => _list.IndexOf(item);

        public void Insert(int index, T item) {
            _list.Insert(index, item);
            _isDirty = true;
        }

        public void RemoveAt(int index) {
            _list.RemoveAt(index);
            _isDirty = true;
        }

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
