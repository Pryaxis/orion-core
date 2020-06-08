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
    /// An event that occurs when a player is sending their health information. This event can be canceled.
    /// </summary>
    [Event("player-hp")]
    public sealed class PlayerHealthEvent : PlayerEvent, ICancelable {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHealthEvent"/> class with the specified
        /// <paramref name="player"/>, <paramref name="health"/>, and <paramref name="maxHealth"/>.
        /// </summary>
        /// <param name="player">The player sending their health information.</param>
        /// <param name="health">The player's health.</param>
        /// <param name="maxHealth">The player's maximum health.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is <see langword="null"/>.</exception>
        public PlayerHealthEvent(IPlayer player, int health, int maxHealth) : base(player) {
            Health = health;
            MaxHealth = maxHealth;
        }

        /// <summary>
        /// Gets the player's health.
        /// </summary>
        /// <value>The player's health.</value>
        public int Health { get; }

        /// <summary>
        /// Gets the player's maximum health.
        /// </summary>
        /// <value>The player's maximum health.</value>
        public int MaxHealth { get; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }
    }
}
