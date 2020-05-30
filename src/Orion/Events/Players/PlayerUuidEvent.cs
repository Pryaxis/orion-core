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
using Orion.Players;

namespace Orion.Events.Players {
    /// <summary>
    /// An event that occurs when a player sends their UUID. This event can be canceled.
    /// </summary>
    [Event("player-uuid")]
    public sealed class PlayerUuidEvent : PlayerEvent, ICancelable {
        /// <summary>
        /// Gets the UUID of the player.
        /// </summary>
        /// <value>The UUID of the player.</value>
        public string Uuid { get; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerUuidEvent"/> class with the specified
        /// <paramref name="player"/> and <paramref name="uuid"/>.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="uuid">The UUID.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="uuid"/> are <see langword="null"/>.
        /// </exception>
        public PlayerUuidEvent(IPlayer player, string uuid) : base(player) {
            Uuid = uuid ?? throw new ArgumentNullException(nameof(uuid));
        }
    }
}
