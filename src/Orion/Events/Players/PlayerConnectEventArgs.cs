// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Orion.Packets.Players;
using Orion.Players;

namespace Orion.Events.Players {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PlayerConnect"/> event.
    /// </summary>
    public sealed class PlayerConnectEventArgs : PlayerEventArgs, ICancelable {
        private readonly PlayerConnectPacket _packet;

        /// <inheritdoc/>
        public string? CancellationReason { get; set; }

        /// <inheritdoc cref="PlayerConnectPacket.PlayerVersionString"/>
        public string PlayerVersionString {
            get => _packet.PlayerVersionString;
            set => _packet.PlayerVersionString = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerConnectEventArgs"/> class with the specified player and
        /// packet.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="packet"/> are <see langword="null"/>.
        /// </exception>
        public PlayerConnectEventArgs(IPlayer player, PlayerConnectPacket packet) : base(player) {
            _packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }
    }
}
