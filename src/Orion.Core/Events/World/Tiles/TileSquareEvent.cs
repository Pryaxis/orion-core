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
using Destructurama.Attributed;
using Orion.Core.Players;
using Orion.Core.World;
using Orion.Core.World.Tiles;

namespace Orion.Core.Events.World.Tiles
{
    /// <summary>
    /// An event that occurs when a player is sending a square of tiles. This event can be canceled.
    /// </summary>
    [Event("tile-square")]
    public sealed class TileSquareEvent : WorldEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TileSquareEvent"/> class with the specified
        /// <paramref name="world"/>, <paramref name="player"/>, top-left tile coordinates, and
        /// <paramref name="tiles"/>.
        /// </summary>
        /// <param name="world">The world involved in the event.</param>
        /// <param name="player">The player sending the square of tiles.</param>
        /// <param name="x">The top-left tile's X coordinate.</param>
        /// <param name="y">The top-left tile's Y coordinate.</param>
        /// <param name="tiles">The tiles.</param>
        /// <exception cref="ArgumentException"><paramref name="tiles"/> is not a square.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="world"/>, <paramref name="player"/>, or <paramref name="tiles"/> are <see langword="null"/>.
        /// </exception>
        public TileSquareEvent(IWorld world, IPlayer player, int x, int y, ITileSlice tiles) : base(world)
        {
            if (tiles is null)
            {
                throw new ArgumentNullException(nameof(tiles));
            }

            if (tiles.Width != tiles.Height)
            {
                // Not localized because this string is developer-facing.
                throw new ArgumentException("Tiles is not a square", nameof(tiles));
            }

            Player = player ?? throw new ArgumentNullException(nameof(player));
            X = x;
            Y = y;
            Tiles = tiles;
        }

        /// <summary>
        /// Gets the player that sent the square of tiles.
        /// </summary>
        /// <value>The player that sent the square of tiles.</value>
        public IPlayer Player { get; }

        /// <summary>
        /// Gets the top-left tile's X coordinate.
        /// </summary>
        /// <value>The top-left tile's X coordinate.</value>
        public int X { get; }

        /// <summary>
        /// Gets the top-left tile's Y coordinate.
        /// </summary>
        /// <value>The top-left tile's Y coordinate.</value>
        public int Y { get; }

        /// <summary>
        /// Gets the square of tiles.
        /// </summary>
        /// <value>The square of tiles.</value>
        [NotLogged] public ITileSlice Tiles { get; }
    }
}
