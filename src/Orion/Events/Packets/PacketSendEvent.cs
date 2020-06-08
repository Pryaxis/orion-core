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
using Orion.Packets;
using Orion.Players;
using Serilog.Events;

namespace Orion.Events.Packets {
    /// <summary>
    /// An event that occurs when a packet is being sent. This event can be canceled.
    /// </summary>
    /// <typeparam name="TPacket">The type of packet.</typeparam>
    [Event("packet-send", LoggingLevel = LogEventLevel.Verbose)]
    public sealed class PacketSendEvent<TPacket> : PacketEvent<TPacket>, ICancelable where TPacket : struct, IPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="PacketSendEvent{TPacket}"/> class with the specified
        /// <paramref name="packet"/> reference and packet <paramref name="receiver"/>.
        /// </summary>
        /// <param name="packet">The packet reference being sent. <b>This must be on the stack!</b></param>
        /// <param name="receiver">The packet's receiver.</param>
        /// <exception cref="ArgumentNullException"><paramref name="receiver"/> is <see langword="null"/>.</exception>
        public PacketSendEvent(ref TPacket packet, IPlayer receiver) : base(ref packet) {
            Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
        }

        /// <summary>
        /// Gets the packet's receiver.
        /// </summary>
        /// <value>The packet's receiver.</value>
        public IPlayer Receiver { get; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }
    }
}
