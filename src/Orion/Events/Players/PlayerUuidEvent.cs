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
    /// An event that occurs when a player sends their UUID. This event can be canceled and modified.
    /// </summary>
    [EventArgs("player-uuid")]
    public sealed class PlayerUuidEvent : PlayerPacketEvent<PlayerUuidPacket> {
        /// <summary>
        /// Gets or sets the player's UUID.
        /// </summary>
        /// <value>The player's UUID.</value>
        /// <remarks>
        /// The player's UUID is (most likely) unique to a given player, but it is easily changed by a client. As such,
        /// it can be used to verify a player's identity positively but not negatively.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Uuid {
            get => _packet.Uuid;
            set => _packet.Uuid = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerUuidEvent"/> class with the specified player and packet.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="packet"/> are <see langword="null"/>.
        /// </exception>
        public PlayerUuidEvent(IPlayer player, PlayerUuidPacket packet) : base(player, packet) { }
    }
}
