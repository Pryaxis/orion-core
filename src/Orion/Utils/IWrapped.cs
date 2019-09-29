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

namespace Orion.Utils {
    /// <summary>
    /// Represents an object that wraps another type of object.
    /// </summary>
    /// <typeparam name="TWrapped">The wrapped type.</typeparam>
    public interface IWrapped<out TWrapped> where TWrapped : class {
        /// <summary>
        /// Gets the wrapped object. This is not required to succeed, so use should be avoided where possible!
        /// </summary>
        TWrapped Wrapped { get; }
    }
}
