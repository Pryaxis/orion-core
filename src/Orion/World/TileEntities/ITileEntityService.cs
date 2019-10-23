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
    /// Represents a tile entity service. Provides access to tile entity-related properties and methods in a thread-safe
    /// manner unless specified otherwise.
    /// </summary>
    public interface ITileEntityService {
        /// <summary>
        /// Gets the chests. All chests are returned, regardless of whether or not they are actually active.
        /// </summary>
        /// <value>The chests.</value>
        IReadOnlyArray<IChest?> Chests { get; }

        /// <summary>
        /// Gets the signs. All signs are returned, regardless of whether or not they are actually active.
        /// </summary>
        /// <value>The signs.</value>
        IReadOnlyArray<ISign?> Signs { get; }

        /// <summary>
        /// Adds and returns a tile entity with the given <paramref name="tileEntityType"/> at the specified
        /// coordinates.
        /// </summary>
        /// <param name="tileEntityType">The tile entity type.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting tile entity, or <see langword="null"/> if none was added.</returns>
        /// <remarks>Implementations of this method are not required to be thread-safe.</remarks>
        ITileEntity? AddTileEntity(TileEntityType tileEntityType, int x, int y);

        /// <summary>
        /// Returns the tile entity at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The tile entity, or <see langword="null"/> if none was found.</returns>
        ITileEntity? GetTileEntity(int x, int y);

        /// <summary>
        /// Removes the given <paramref name="tileEntity"/> and returns a value indicating success.
        /// </summary>
        /// <param name="tileEntity">The tile entity.</param>
        /// <returns>
        /// <see langword="true"/> if the tile entity was removed; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>Implementations of this method are not required to be thread-safe.</remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="tileEntity"/> is <see langword="null"/>.
        /// </exception>
        bool RemoveTileEntity(ITileEntity tileEntity);
    }

    /// <summary>
    /// Provides extensions for the <see cref="ITileEntityService"/> interface.
    /// </summary>
    public static class TileEntityServiceExtensions {
        /// <summary>
        /// Adds and returns a chest with the given coordinates.
        /// </summary>
        /// <param name="tileEntityService">The tile entity service.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting chest, or <see langword="null"/> if none was added.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="tileEntityService"/> is <see langword="null"/>.
        /// </exception>
        public static IChest? AddChest(this ITileEntityService tileEntityService, int x, int y) {
            if (tileEntityService is null) {
                throw new ArgumentNullException(nameof(tileEntityService));
            }

            return tileEntityService.AddTileEntity(TileEntityType.Chest, x, y) as IChest;
        }

        /// <summary>
        /// Adds and returns a sign with the given coordinates.
        /// </summary>
        /// <param name="tileEntityService">The tile entity service.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting sign, or <see langword="null"/> if none was added.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="tileEntityService"/> is <see langword="null"/>.
        /// </exception>
        public static ISign? AddSign(this ITileEntityService tileEntityService, int x, int y) {
            if (tileEntityService is null) {
                throw new ArgumentNullException(nameof(tileEntityService));
            }

            return tileEntityService.AddTileEntity(TileEntityType.Sign, x, y) as ISign;
        }

        /// <summary>
        /// Adds and returns a target dummy with the given coordinates.
        /// </summary>
        /// <param name="tileEntityService">The tile entity service.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting target dummy, or <see langword="null"/> if none was added.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="tileEntityService"/> is <see langword="null"/>.
        /// </exception>
        public static ITargetDummy? AddTargetDummy(this ITileEntityService tileEntityService, int x, int y) {
            if (tileEntityService is null) {
                throw new ArgumentNullException(nameof(tileEntityService));
            }

            return tileEntityService.AddTileEntity(TileEntityType.TargetDummy, x, y) as ITargetDummy;
        }

        /// <summary>
        /// Adds and returns an item frame with the given coordinates.
        /// </summary>
        /// <param name="tileEntityService">The tile entity service.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting item frame, or <see langword="null"/> if none was added.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="tileEntityService"/> is <see langword="null"/>.
        /// </exception>
        public static IItemFrame? AddItemFrame(this ITileEntityService tileEntityService, int x, int y) {
            if (tileEntityService is null) {
                throw new ArgumentNullException(nameof(tileEntityService));
            }

            return tileEntityService.AddTileEntity(TileEntityType.ItemFrame, x, y) as IItemFrame;
        }

        /// <summary>
        /// Adds and returns a logic sensor with the given coordinates.
        /// </summary>
        /// <param name="tileEntityService">The tile entity service.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting logic sensor, or <see langword="null"/> if none was added.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="tileEntityService"/> is <see langword="null"/>.
        /// </exception>
        public static ILogicSensor? AddLogicSensor(this ITileEntityService tileEntityService, int x, int y) {
            if (tileEntityService is null) {
                throw new ArgumentNullException(nameof(tileEntityService));
            }

            return tileEntityService.AddTileEntity(TileEntityType.LogicSensor, x, y) as ILogicSensor;
        }
    }
}
