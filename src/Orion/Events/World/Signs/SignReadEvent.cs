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
using System.Collections.Generic;
using System.Text;
using Destructurama.Attributed;
using Orion.Players;
using Orion.World;

namespace Orion.Events.World.Signs {
    /// <summary>
    /// An event that occurs when a player reads a sign. This event can be canceled.
    /// </summary>
    [Event("sign-read")]
    public sealed class SignReadEvent : WorldEvent, ICancelable {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignReadEvent"/> class with the specified
        /// <paramref name="world"/>, <paramref name="player"/>, and sign coordinates.
        /// </summary>
        /// <param name="world">The world involved in the event.</param>
        /// <param name="player">The player reading the sign.</param>
        /// <param name="x">The sign's X coordinate.</param>
        /// <param name="y">The sign's Y coordinate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is <see langword="null"/>.</exception>
        public SignReadEvent(IWorld world, IPlayer player, int x, int y) : base(world) {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets the player reading the sign.
        /// </summary>
        /// <value>The player reading the sign.</value>
        public IPlayer Player { get; }

        /// <summary>
        /// Gets the sign's X coordinate.
        /// </summary>
        /// <value>The sign's X coordinate.</value>
        public int X { get; }

        /// <summary>
        /// Gets the sign's Y coordinate.
        /// </summary>
        /// <value>The sign's Y coordinate.</value>
        public int Y { get; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }
    }
}
