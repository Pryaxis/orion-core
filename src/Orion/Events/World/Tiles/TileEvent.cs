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

using Destructurama.Attributed;
using Orion.Players;

namespace Orion.Events.World.Tiles {
    /// <summary>
    /// Represents a tile-related event.
    /// </summary>
    public abstract class TileEvent : Event, ICancelable {
        /// <summary>
        /// Gets the player involved in the event, or <see langword="null"/> if there is no player.
        /// </summary>
        /// <value>The player involved in the event.</value>
        public IPlayer? Player { get; }

        /// <summary>
        /// Gets the X coordinate.
        /// </summary>
        /// <value>The X coordinate.</value>
        public int X { get; }

        /// <summary>
        /// Gets the Y coordinate.
        /// </summary>
        /// <value>The Y coordinate.</value>
        public int Y { get; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileEvent"/> class with the specified <paramref name="player"/>
        /// and coordinates.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        protected TileEvent(IPlayer? player, int x, int y) {
            Player = player;
            X = x;
            Y = y;
        }
    }
}
