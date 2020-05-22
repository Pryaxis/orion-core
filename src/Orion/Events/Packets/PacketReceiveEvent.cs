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
using Orion.Packets;
using Orion.Players;
using Serilog.Events;

namespace Orion.Events.Packets {
    /// <summary>
    /// An event that occurs when a packet is being received. This event can be canceled.
    /// </summary>
    /// <typeparam name="TPacket">The type of packet.</typeparam>
    [Event("packet-recv", LoggingLevel = LogEventLevel.Verbose)]
    public sealed class PacketReceiveEvent<TPacket> : PacketEvent<TPacket> where TPacket : struct, IPacket {
        /// <summary>
        /// Gets the sender.
        /// </summary>
        /// <value>The sender.</value>
        public IPlayer Sender { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketReceiveEvent{TPacket}"/> class with the specified
        /// <paramref name="packet"/> reference and <paramref name="sender"/>.
        /// </summary>
        /// <param name="packet">The packet reference. <b>This must be on the stack!</b></param>
        /// <param name="sender">The sender.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sender"/> is <see langword="null"/>.</exception>
        public PacketReceiveEvent(ref TPacket packet, IPlayer sender) : base(ref packet) {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }
    }
}
