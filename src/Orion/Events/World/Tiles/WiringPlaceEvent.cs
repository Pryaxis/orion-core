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

using Orion.Players;
using Orion.World.Tiles;

namespace Orion.Events.World.Tiles {
    /// <summary>
    /// An event that occurs when wiring is being placed. This event can be canceled.
    /// </summary>
    [Event("wiring-place")]
    public sealed class WiringPlaceEvent : TileEvent {
        /// <summary>
        /// Gets the wiring type being placed.
        /// </summary>
        /// <value>The wiring type being placed.</value>
        public Wiring Wiring { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WiringPlaceEvent"/> class with the specified
        /// <paramref name="player"/>, coordinates, and <paramref name="wiring"/>.
        /// </summary>
        /// <param name="player">The player, or <see langword="null"/> for none.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="wiring">The wiring type.</param>
        public WiringPlaceEvent(IPlayer? player, int x, int y, Wiring wiring) : base(player, x, y) {
            Wiring = wiring;
        }
    }
}
