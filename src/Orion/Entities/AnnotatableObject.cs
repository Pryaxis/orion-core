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
using System.Collections.Generic;

namespace Orion.Entities {
    /// <summary>
    /// Represents an annotatable object. Provides the base class for implementations of interfaces derived from
    /// <see cref="IAnnotatable"/>.
    /// </summary>
    public class AnnotatableObject : IAnnotatable {
        private readonly IDictionary<string, object> _annotations = new Dictionary<string, object>();

        /// <inheritdoc/>
        public ref T GetAnnotation<T>(string key, Func<T>? initializer = null) {
            if (key is null) {
                throw new ArgumentNullException(nameof(key));
            }

            if (_annotations.TryGetValue(key, out var boxObj)) {
                if (!(boxObj is Box<T> box)) {
                    // Not localized because this string is developer-facing.
                    var expectedType = boxObj.GetType().GetGenericArguments()[0];
                    throw new ArgumentException(
                        $"Invalid annotation type (expected: `{expectedType}`, actual: `{typeof(T)}`)");
                }

                return ref box.Value;
            } else {
                var box = new Box<T>();
                if (initializer != null) {
                    box.Value = initializer();
                }

                _annotations.Add(key, box);
                return ref box.Value;
            }
        }

        /// <inheritdoc/>
        public bool RemoveAnnotation(string key) {
            if (key is null) {
                throw new ArgumentNullException(nameof(key));
            }

            return _annotations.Remove(key);
        }

        // Utility class for returning a `ref` to `T`.
        private class Box<T> {
            public T Value = default!;
        }
    }
}
