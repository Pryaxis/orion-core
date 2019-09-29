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

namespace Orion.World.TileEntities.Extensions {
    /// <summary>
    /// Provides extensions for the <see cref="ITileEntityService"/> interface.
    /// </summary>
    public static class TileEntityServiceExtensions {
        /// <summary>
        /// Adds a chest with the given coordinates.
        /// </summary>
        /// <param name="tileEntityService">The tile entity service.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting chest, or <c>null</c> if none was added.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tileEntityService"/> is <c>null</c>.</exception>
        public static IChest? AddChest(this ITileEntityService tileEntityService, int x, int y) {
            if (tileEntityService is null) throw new ArgumentNullException(nameof(tileEntityService));

            return tileEntityService.AddTileEntity(TileEntityType.Chest, x, y) as IChest;
        }

        /// <summary>
        /// Adds a sign with the given coordinates.
        /// </summary>
        /// <param name="tileEntityService">The tile entity service.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting sign, or <c>null</c> if none was added.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tileEntityService"/> is <c>null</c>.</exception>
        public static ISign? AddSign(this ITileEntityService tileEntityService, int x, int y) {
            if (tileEntityService is null) throw new ArgumentNullException(nameof(tileEntityService));

            return tileEntityService.AddTileEntity(TileEntityType.Sign, x, y) as ISign;
        }

        /// <summary>
        /// Adds a target dummy with the given coordinates.
        /// </summary>
        /// <param name="tileEntityService">The tile entity service.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting target dummy, or <c>null</c> if none was added.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tileEntityService"/> is <c>null</c>.</exception>
        public static ITargetDummy? AddTargetDummy(this ITileEntityService tileEntityService, int x, int y) {
            if (tileEntityService is null) throw new ArgumentNullException(nameof(tileEntityService));

            return tileEntityService.AddTileEntity(TileEntityType.TargetDummy, x, y) as ITargetDummy;
        }

        /// <summary>
        /// Adds an item frame with the given coordinates.
        /// </summary>
        /// <param name="tileEntityService">The tile entity service.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting item frame, or <c>null</c> if none was added.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tileEntityService"/> is <c>null</c>.</exception>
        public static IItemFrame? AddItemFrame(this ITileEntityService tileEntityService, int x, int y) {
            if (tileEntityService is null) throw new ArgumentNullException(nameof(tileEntityService));

            return tileEntityService.AddTileEntity(TileEntityType.ItemFrame, x, y) as IItemFrame;
        }

        /// <summary>
        /// Adds a logic sensor with the given coordinates.
        /// </summary>
        /// <param name="tileEntityService">The tile entity service.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting logic sensor, or <c>null</c> if none was added.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tileEntityService"/> is <c>null</c>.</exception>
        public static ILogicSensor? AddLogicSensor(this ITileEntityService tileEntityService, int x, int y) {
            if (tileEntityService is null) throw new ArgumentNullException(nameof(tileEntityService));

            return tileEntityService.AddTileEntity(TileEntityType.LogicSensor, x, y) as ILogicSensor;
        }
    }
}
