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
using System.Collections.Generic;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Represents a service that manages chests. Provides chest-related methods.
    /// </summary>
    public interface IChestService : IReadOnlyList<IChest>, IService {
        /// <summary>
        /// Attempts to add a chest at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting chest, or <c>null</c> if none was added.</returns>
        IChest AddChest(int x, int y);

        /// <summary>
        /// Gets the chest at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The chest, or <c>null</c> if there is none.</returns>
        IChest GetChest(int x, int y);

        /// <summary>
        /// Removes the given chest.
        /// </summary>
        /// <param name="chest">The chest.</param>
        /// <exception cref="ArgumentNullException"><paramref name="chest"/> is <c>null</c>.</exception>
        /// <returns>A value indicating whether or not the chest was successfully removed.</returns>
        bool RemoveChest(IChest chest);
    }
}
