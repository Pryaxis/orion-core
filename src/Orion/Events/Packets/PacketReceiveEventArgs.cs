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
using Orion.Packets;
using Orion.Players;

namespace Orion.Events.Packets {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PacketReceive"/> event.
    /// </summary>
    [EventArgs("packet-recv")]
    public sealed class PacketReceiveEventArgs : PacketEventArgs {
        /// <summary>
        /// Gets the sender.
        /// </summary>
        public IPlayer Sender { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketReceiveEventArgs"/> class with the given sender and
        /// packet.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sender"/> or <paramref name="packet"/> are <see langword="null"/>.
        /// </exception>
        public PacketReceiveEventArgs(IPlayer sender, Packet packet) : base(packet) {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }
    }
}
