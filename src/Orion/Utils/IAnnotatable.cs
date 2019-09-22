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
using JetBrains.Annotations;

namespace Orion.Utils {
    /// <summary>
    /// Provides methods with which to get, set, and remove annotations.
    /// </summary>
    [PublicAPI]
    public interface IAnnotatable {
        /// <summary>
        /// Gets the annotation of a type with the given key, returning a default value if the key does not exist.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The metadata, or a default value if the key does not exist.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        [CanBeNull]
        T GetAnnotation<T>([NotNull] string key, [CanBeNull] T defaultValue = default);

        /// <summary>
        /// Sets the annotation of a type with the given key to the specified value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        void SetAnnotation<T>([NotNull] string key, [CanBeNull] T value);

        /// <summary>
        /// Removes the annotation with the given key and returns a success value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A value indicating whether the key was successfully removed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        bool RemoveAnnotation([NotNull] string key);
    }
}
