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

namespace Orion.Core.Entities {
    /// <summary>
    /// Provides annotation support. This is primarily used to attach customizable state to instances.
    /// </summary>
    public interface IAnnotatable {
        /// <summary>
        /// Gets a reference to the annotation of type <typeparamref name="T"/> with the given <paramref name="key"/>,
        /// using the specified <paramref name="initializer"/> to initialize the annotation.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="initializer">
        /// The initializer. If <see langword="null"/>, then a default initializer is used.
        /// </param>
        /// <returns>A reference to the annotation.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="key"/> does not refer to an annotation of type <typeparamref name="T"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        ref T GetAnnotation<T>(string key, Func<T>? initializer = null);

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
