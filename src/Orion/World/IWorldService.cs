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
        int WorldWidth { get; }

        /// <summary>
        /// Gets the world's height.
        /// </summary>
        int WorldHeight { get; }

        /// <summary>
        /// Gets a reference to the tile at the given coordinates.
        /// 
        /// <para>
        /// For optimization purposes, there will be no range checking. A likely result of invalid coordinates is an
        /// <see cref="AccessViolationException"/>.
        /// </para>
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>A reference to the tile.</returns>
        ref Tile this[int x, int y] { get; }

        /// <summary>
        /// Gets the current invasion.
        /// </summary>
        InvasionType CurrentInvasionType { get; }

        /// <summary>
        /// Gets the event handlers that occur when the world is loading.
        /// </summary>
        EventHandlerCollection<WorldLoadEventArgs> WorldLoad { get; }

        /// <summary>
        /// Gets the event handlers that occur when the world is saving. This event can be canceled.
        /// </summary>
        EventHandlerCollection<WorldSaveEventArgs> WorldSave { get; }
    }
}
