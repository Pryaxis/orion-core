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
using Orion.Core.Packets;

namespace Orion.Core.Events.Packets
{
    /// <summary>
    /// Provides the base class for a packet-related event.
    /// </summary>
    public abstract class PacketEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PacketEvent"/> class with the specified
        /// <paramref name="packet"/>.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException"><paramref name="packet"/> is <see langword="null"/>.</exception>
        public PacketEvent(IPacket packet)
        {
            Packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }

        /// <summary>
        /// Gets the packet involved in the event.
        /// </summary>
        /// <value>The packet involved in the event.</value>
        public IPacket Packet { get; }
    }
}
