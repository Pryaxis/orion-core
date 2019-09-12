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
using Orion.Hooks;
using Orion.World.Events;
using Orion.World.TileEntities;
using Orion.World.Tiles;

namespace Orion.World {
    /// <summary>
    /// Represents a service that manages the world. Provides world-related hooks and methods.
    /// </summary>
    public interface IWorldService : IService {
        /// <summary>
        /// Gets or sets the world's name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        string WorldName { get; set; }

        /// <summary>
        /// Gets the world's width.
        /// </summary>
        int WorldWidth { get; }

        /// <summary>
        /// Gets the world's height.
        /// </summary>
        int WorldHeight { get; }

        /// <summary>
        /// Gets or sets the tile at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The tile.</returns>
        Tile this[int x, int y] { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        double Time { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world is in daytime.
        /// </summary>
        bool IsDaytime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world is in hardmode.
        /// </summary>
        bool IsHardmode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world is in expert mode.
        /// </summary>
        bool IsExpertMode { get; set; }

        /// <summary>
        /// Gets the current invasion type.
        /// </summary>
        InvasionType CurrentInvasionType { get; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when the world is checking for whether it's halloween. This hook
        /// can be handled.
        /// </summary>
        HookHandlerCollection<CheckingHalloweenEventArgs> CheckingHalloween { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when the world is checking for whether it's Christmas. This hook
        /// can be handled.
        /// </summary>
        HookHandlerCollection<CheckingChristmasEventArgs> CheckingChristmas { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when the world is loading. This hook can be handled.
        /// </summary>
        HookHandlerCollection<LoadingWorldEventArgs> LoadingWorld { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when the world was loaded.
        /// </summary>
        HookHandlerCollection<LoadedWorldEventArgs> LoadedWorld { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when the world is saving. This hook can be handled.
        /// </summary>
        HookHandlerCollection<SavingWorldEventArgs> SavingWorld { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when the world was saved.
        /// </summary>
        HookHandlerCollection<SavedWorldEventArgs> SavedWorld { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when the world is starting hardmode. This hook can be handled.
        /// </summary>
        HookHandlerCollection<StartingHardmodeEventArgs> StartingHardmode { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when the world has started hardmode.
        /// </summary>
        HookHandlerCollection<StartedHardmodeEventArgs> StartedHardmode { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when the world is updating a tile in hardmode. This hook can be
        /// handled.
        /// </summary>
        HookHandlerCollection<UpdatingHardmodeBlockEventArgs> UpdatingHardmodeBlock { get; set; }

        /// <summary>
        /// Starts an invasion with the given type.
        /// </summary>
        /// <param name="invasionType">The invasion type.</param>
        /// <returns><c>true</c> if the invasion was successfully started; <c>false</c> otherwise.</returns>
        bool StartInvasion(InvasionType invasionType);

        /// <summary>
        /// Adds a target dummy at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting training dummy, or <c>null</c> if none was placed.</returns>
        ITargetDummy AddTargetDummy(int x, int y);

        /// <summary>
        /// Adds an item frame at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting item frame, or <c>null</c> if none was placed.</returns>
        IItemFrame AddItemFrame(int x, int y);

        /// <summary>
        /// Adds a logic sensor at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting logic sensor, or <c>null</c> if none was placed.</returns>
        ILogicSensor AddLogicSensor(int x, int y);

        /// <summary>
        /// Gets the tile entity at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The tile entity, or <c>null</c> if there is none.</returns>
        ITileEntity GetTileEntity(int x, int y);

        /// <summary>
        /// Removes the given tile entity from the world and returns a value indicating success.
        /// </summary>
        /// <param name="tileEntity">The tile entity.</param>
        /// <returns>A value indicating whether the tile entity was successfully removed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tileEntity"/> is <c>null</c>.</exception>
        bool RemoveTileEntity(ITileEntity tileEntity);

        /// <summary>
        /// Saves the world.
        /// </summary>
        void SaveWorld();
    }
}
