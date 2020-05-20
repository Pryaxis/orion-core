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

namespace Orion.Entities {
    /// <summary>
    /// Provides methods with which to get, set, and remove annotations.
    /// </summary>
    public interface IAnnotatable {
        /// <summary>
        /// Gets the annotation of type <typeparamref name="T"/> with the given <paramref name="key"/>, using the given
        /// <paramref name="defaultValueProvider"/> if the key does not exist.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="defaultValueProvider">
        /// The default value provider. If <see langword="null"/>, then the provider will return a default instance of
        /// <typeparamref name="T"/>.
        /// </param>
        /// <param name="createIfNotExists">
        /// <see langword="true"/> to create the annotation if it does not exist; otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>
        /// The annotation, or a default instance provided by <paramref name="defaultValueProvider"/> if the key does
        /// not exist.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        T GetAnnotationOrDefault<T>(string key, Func<T>? defaultValueProvider = null, bool createIfNotExists = false);

        /// <summary>
        /// Sets the annotation of type <typeparamref name="T"/> with the given <paramref name="key"/> to the specified
        /// <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        void SetAnnotation<T>(string key, T value);

        /// <summary>
        /// Removes the annotation with the given <paramref name="key"/>. Returns a value indicating success.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// <see langword="true"/> if the annotation was removed; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        bool RemoveAnnotation(string key);
    }
}
