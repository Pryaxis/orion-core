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
using Orion.Players;
using Orion.World;

namespace Orion.Events.World.Tiles {
    /// <summary>
    /// Provides the base class for a tile-related event.
    /// </summary>
    public abstract class TileEvent : WorldEvent {
        /// <summary>
        /// Initializes a new instance of the <see cref="TileEvent"/> class with the specified <paramref name="world"/>,
        /// <paramref name="player"/>, and coordinates.
        /// </summary>
        /// <param name="world">The world involved in the event.</param>
        /// <param name="player">The player involved in the event, or <see langword="null"/> for none.</param>
        /// <param name="x">The tile's X coordinate.</param>
        /// <param name="y">The tile's Y coordinate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="world"/> is <see langword="null"/>.</exception>
        protected TileEvent(IWorld world, IPlayer? player, int x, int y) : base(world) {
            Player = player;
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets the player involved in the event, or <see langword="null"/> if there is none.
        /// </summary>
        /// <value>The player involved in the event.</value>
        public IPlayer? Player { get; }

        /// <summary>
        /// Gets the tile's X coordinate.
        /// </summary>
        /// <value>The tile's X coordinate.</value>
        public int X { get; }

        /// <summary>
        /// Gets the tile's Y coordinate.
        /// </summary>
        /// <value>The tile's Y coordinate.</value>
        public int Y { get; }
    }
}
