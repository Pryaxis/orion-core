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
using Destructurama.Attributed;
using Orion.Packets;
using Orion.Utils;

namespace Orion.Events.Players {
    /// <summary>
    /// Represents a packet-related event.
    /// </summary>
    public abstract class PacketEvent : Event, IDirtiable {
        /// <inheritdoc/>
        [NotLogged]
        public bool IsDirty => Packet.IsDirty;

        /// <summary>
        /// Gets the packet involved in the event.
        /// </summary>
        /// <value>The packet involved in the event.</value>
        public Packet Packet { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketEvent"/> class with the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException"><paramref name="packet"/> is <see langword="null"/>.</exception>
        protected PacketEvent(Packet packet) {
            Packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }

        /// <inheritdoc/>
        public void Clean() => Packet.Clean();
    }
}
