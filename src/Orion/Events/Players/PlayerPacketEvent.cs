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
using System.Runtime.CompilerServices;
using Destructurama.Attributed;
using Orion.Packets;
using Orion.Players;

namespace Orion.Events.Players {
    /// <summary>
    /// Represents a player-related packet event. This event can be canceled.
    /// </summary>
    /// <typeparam name="TPacket">The type of packet.</typeparam>
    public abstract unsafe class PlayerPacketEvent<TPacket> : PlayerEvent, ICancelable where TPacket : struct, IPacket {
        // Store a pointer to the packet. This is quite unsafe and requires callers to ensure that the `TPacket` is
        // stored on the stack. However, this lets us save on a struct copy.
        private readonly void* _packetPtr;

        /// <summary>
        /// Gets a reference to the packet.
        /// </summary>
        /// <value>A reference to the packet.</value>
        protected ref TPacket Packet => ref Unsafe.AsRef<TPacket>(_packetPtr);

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerPacketEvent{TPacket}"/> class with the specified
        /// <paramref name="player"/> and <paramref name="packet"/> reference.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet reference. <b>This must be on the stack!</b></param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is <see langword="null"/>.</exception>
        protected PlayerPacketEvent(IPlayer player, ref TPacket packet) : base(player) {
            _packetPtr = Unsafe.AsPointer(ref packet);
        }
    }
}
