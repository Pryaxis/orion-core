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
    /// Represents an object that wraps another type of object.
    /// </summary>
    /// <typeparam name="TWrapped">The wrapped type.</typeparam>
    public interface IWrapping<out TWrapped> {
        /// <summary>
        /// Gets the wrapped object. This is not guaranteed to succeed.
        /// </summary>
        /// <value>The wrapped object.</value>
        [Obsolete("Avoid this property outside of Orion if possible.")]
        TWrapped Wrapped { get; }
    }
}
