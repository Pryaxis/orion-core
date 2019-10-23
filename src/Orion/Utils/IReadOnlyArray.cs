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

namespace Orion.Utils {
    /// <summary>
    /// Provides read-only access to a strongly-typed array of elements.
    /// </summary>
    /// <typeparam name="T">The type of element.</typeparam>
    [SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Type is an array")]
    public interface IReadOnlyArray<out T> : IEnumerable<T> {
        [ExcludeFromCodeCoverage]
        [SuppressMessage(
            "Design", "CA1033:Interface methods should be callable by child types",
            Justification = "Non-generic GetEnumerator")]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Gets the number of elements.
        /// </summary>
        /// <value>The number of elements.</value>
        int Count { get; }

        /// <summary>
        /// Gets the element at the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <value>The element at the given <paramref name="index"/>.</value>
        /// <exception cref="IndexOutOfRangeException"><paramref name="index"/> is out of range.</exception>
        T this[int index] { get; }
    }
}
