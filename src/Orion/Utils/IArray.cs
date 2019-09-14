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

namespace Orion.Utils {
    /// <summary>
    /// Provides access to a strongly-typed array of elements.
    /// </summary>
    /// <typeparam name="T">The type of element.</typeparam>
    public interface IArray<T> : IReadOnlyArray<T> {
        /// <summary>
        /// Gets or sets the element at the given index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <exception cref="IndexOutOfRangeException"><paramref name="index"/> is out of range.</exception>
        new T this[int index] { get; set; }
    }
}
