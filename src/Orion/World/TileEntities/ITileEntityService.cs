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
using Orion.Utils;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Represents a tile entity service. Provides tile entity-related hooks and methods.
    /// </summary>
    public interface ITileEntityService : IService {
        /// <summary>
        /// Gets the chests.
        /// </summary>
        IReadOnlyArray<IChest?> Chests { get; }

        /// <summary>
        /// Gets the signs.
        /// </summary>
        IReadOnlyArray<ISign?> Signs { get; }

        /// <summary>
        /// Adds and returns a tile entity with the given type at the specified coordinates.
        /// </summary>
        /// <param name="tileEntityType">The tile entity type.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting tile entity, or <see langword="null" /> if none was found.</returns>
        ITileEntity? AddTileEntity(TileEntityType tileEntityType, int x, int y);

        /// <summary>
        /// Returns the tile entity at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The tile entity, or <see langword="null" /> if none was found.</returns>
        ITileEntity? GetTileEntity(int x, int y);

        /// <summary>
        /// Removes the given tile entity and returns a value indicating success.
        /// </summary>
        /// <param name="tileEntity">The tile entity.</param>
        /// <returns>A value indicating whether the tile entity was successfully removed.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="tileEntity"/> is <see langword="null" />.
        /// </exception>
        bool RemoveTileEntity(ITileEntity tileEntity);
    }
}
