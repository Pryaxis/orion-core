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

namespace Orion.Core.Events.Players
{
    /// <summary>
    /// An event that occurs when a player is sending their password. This event can be canceled.
    /// </summary>
    [Event("player-password")]
    public sealed class PlayerPasswordEvent : PlayerEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerPasswordEvent"/> class with the specified
        /// <paramref name="player"/> and <paramref name="password"/>.
        /// </summary>
        /// <param name="player">The player sending their password.</param>
        /// <param name="password">The password.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="password"/> are <see langword="null"/>.
        /// </exception>
        public PlayerPasswordEvent(IPlayer player, string password) : base(player)
        {
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        /// <summary>
        /// Gets the player's password.
        /// </summary>
        /// <value>The player's password.</value>
        [LogMasked] public string Password { get; }
    }
}
