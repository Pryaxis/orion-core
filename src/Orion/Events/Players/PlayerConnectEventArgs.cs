// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using Orion.Entities;

namespace Orion.Events.Players {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PlayerConnect"/> event.
    /// </summary>
    public sealed class PlayerConnectEventArgs : PlayerEventArgs {
        private string _playerVersionString;

        /// <summary>
        /// Gets the player's version string.
        /// </summary>
        public string PlayerVersionString {
            get => _playerVersionString;
            set => _playerVersionString = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerConnectEventArgs"/> class with the specified player and
        /// version string.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="playerVersionString">The version string.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="playerVersionString"/> are <c>null</c>.
        /// </exception>
        public PlayerConnectEventArgs(IPlayer player, string playerVersionString) : base(player) {
            PlayerVersionString = playerVersionString;
        }
    }
}
