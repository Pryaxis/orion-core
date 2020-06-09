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

namespace Orion.Core.Events.Players {
    /// <summary>
    /// An event that occurs when a player is sending their mana information. This event can be canceled.
    /// </summary>
    [Event("player-mp")]
    public sealed class PlayerManaEvent : PlayerEvent, ICancelable {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerManaEvent"/> class with the specified
        /// <paramref name="player"/>, <paramref name="mana"/>, and <paramref name="maxMana"/>.
        /// </summary>
        /// <param name="player">The player sending their mana information.</param>
        /// <param name="mana">The player's mana.</param>
        /// <param name="maxMana">The player's maximum mana.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is <see langword="null"/>.</exception>
        public PlayerManaEvent(IPlayer player, int mana, int maxMana) : base(player) {
            Mana = mana;
            MaxMana = maxMana;
        }

        /// <summary>
        /// Gets the player's mana.
        /// </summary>
        /// <value>The player's mana.</value>
        public int Mana { get; }

        /// <summary>
        /// Gets the player's maximum mana.
        /// </summary>
        /// <value>The player's maximum mana.</value>
        public int MaxMana { get; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }
    }
}
