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
using Orion.Core.Players;
using Orion.Core.World;
using Orion.Core.World.Tiles;

namespace Orion.Core.Events.World.Tiles {
    /// <summary>
    /// An event that occurs when a wall is being placed. This event can be canceled.
    /// </summary>
    [Event("wall-place")]
    public sealed class WallPlaceEvent : TileEvent {
        /// <summary>
        /// Initializes a new instance of the <see cref="WallPlaceEvent"/> class with the specified
        /// <paramref name="world"/>, <paramref name="player"/>, coordinates, wall <paramref name="id"/>, and
        /// replacement flag.
        /// </summary>
        /// <param name="world">The world involved in the event.</param>
        /// <param name="player">The player placing the wall, or <see langword="null"/> for none.</param>
        /// <param name="x">The wall's X coordinate.</param>
        /// <param name="y">The wall's Y coordinate.</param>
        /// <param name="id">The wall ID being placed.</param>
        /// <param name="isReplacement">Whether the wall placing is a replacement.</param>
        /// <exception cref="ArgumentNullException"><paramref name="world"/> is <see langword="null"/>.</exception>
        public WallPlaceEvent(IWorld world, IPlayer? player, int x, int y, WallId id, bool isReplacement)
                : base(world, player, x, y) {
            Id = id;
            IsReplacement = isReplacement;
        }

        /// <summary>
        /// Gets the wall ID.
        /// </summary>
        /// <value>The wall ID.</value>
        public WallId Id { get; }

        /// <summary>
        /// Gets a value indicating whether the wall placing is a replacement.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the wall placing is a replacement; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsReplacement { get; }
    }
}
