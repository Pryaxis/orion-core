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
using Orion.Events;

namespace Orion.Packets {
    /// <summary>
    /// Represents a packet, the main form of communication between the server and its clients.
    /// </summary>
    public interface IPacket : IDirtiable {
        // Provide default implementations of the IDirtiable interface since very few packets actually require it.
        bool IDirtiable.IsDirty => false;
        void IDirtiable.Clean() { }

        /// <summary>
        /// Reads the packet from the given <paramref name="span"/> with the specified <paramref name="context"/>,
        /// mutating this instance.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <param name="context">The context.</param>
        /// <returns>The number of bytes read.</returns>
        void Read(ReadOnlySpan<byte> span, PacketContext context);

        /// <summary>
        /// Writes the packet to the given <paramref name="span"/> with the specified <paramref name="context"/>.
        /// Advances the span by the number of bytes written.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <param name="context">The context.</param>
        void Write(ref Span<byte> span, PacketContext context);
    }
}
