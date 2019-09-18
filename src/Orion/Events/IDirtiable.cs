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

namespace Orion.Events {
    /// <summary>
    /// Represents something that can be dirtied and cleaned.
    /// </summary>
    public interface IDirtiable {
        /// <summary>
        /// Gets a value indicating whether the object is dirty: i.e., whether it has been modified since it was last
        /// cleaned.
        /// </summary>
        bool IsDirty { get; }

        /// <summary>
        /// Cleans the object and marks it as not dirty.
        /// </summary>
        void Clean();
    }
}
