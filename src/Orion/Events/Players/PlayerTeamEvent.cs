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
    /// An event that occurs when a player sends their team. This event can be canceled.
    /// </summary>
    [Event("player-team")]
    public sealed class PlayerTeamEvent : PlayerEvent, ICancelable {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerTeamEvent"/> class with the specified
        /// <paramref name="player"/> and <paramref name="team"/>.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="team">The player's team.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is <see langword="null"/>.</exception>
        public PlayerTeamEvent(IPlayer player, PlayerTeam team) : base(player) {
            Team = team;
        }

        /// <summary>
        /// Gets the player's team.
        /// </summary>
        /// <value>The player's team.</value>
        public PlayerTeam Team { get; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }
    }
}
