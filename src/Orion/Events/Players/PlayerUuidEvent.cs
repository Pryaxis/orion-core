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
using Orion.Packets.Client;
using Orion.Players;

namespace Orion.Events.Players {
    /// <summary>
    /// An event that occurs when a player sends their UUID.
    /// </summary>
    [Event("player-uuid")]
    public sealed class PlayerUuidEvent : PlayerPacketEvent<ClientUuidPacket> {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        /// <value>The UUID.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Uuid {
            get => Packet.Uuid;
            set => Packet.Uuid = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerUuidEvent"/> class with the specified
        /// <paramref name="player"/> and <paramref name="packet"/> reference.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet reference. <b>This must be on the stack!</b></param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is <see langword="null"/>.</exception>
        public PlayerUuidEvent(IPlayer player, ref ClientUuidPacket packet) : base(player, ref packet) { }
    }
}
