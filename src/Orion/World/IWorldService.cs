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
using Orion.Events;
using Orion.Events.World;
using Orion.World.Tiles;

namespace Orion.World {
    /// <summary>
    /// Represents a world service. Provides access to world-related events and methods, and in a thread-safe manner
    /// unless specified otherwise.
    /// </summary>
    public interface IWorldService {
        /// <summary>
        /// Gets the world's width.
        /// </summary>
        /// <value>The world's width.</value>
        int WorldWidth { get; }

        /// <summary>
        /// Gets the world's height.
        /// </summary>
        /// <value>The world's height.</value>
        int WorldHeight { get; }

        /// <summary>
        /// Gets a reference to the tile at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>A reference to the tile.</returns>
        /// <remarks>
        /// For optimization purposes, implementations are not required to perform range checking. A likely result of
        /// invalid coordinates would be an <see cref="AccessViolationException"/>.
        /// </remarks>
        ref Tile this[int x, int y] { get; }

        /// <summary>
        /// Gets the current invasion type.
        /// </summary>
        /// <value>The current invasion type.</value>
        InvasionType CurrentInvasionType { get; }

        /// <summary>
        /// Gets the event handlers that occur when the world is loading.
        /// </summary>
        /// <value>The event handlers that occur when the world is loading.</value>
        EventHandlerCollection<WorldLoadEventArgs> WorldLoad { get; }

        /// <summary>
        /// Gets the event handlers that occur when the world is saving. This event can be canceled.
        /// </summary>
        /// <value>The event handlers that occur when the world is saving.</value>
        EventHandlerCollection<WorldSaveEventArgs> WorldSave { get; }
    }
}
