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
using Orion.Events;

namespace Orion.Packets {
    /// <summary>
    /// Represents a packet, the main form of communication between the server and its clients.
    /// </summary>
    public interface IPacket : IDirtiable {
        /// <summary>
        /// The packet's header size.
        /// </summary>
        public const int HeaderSize = sizeof(ushort) + sizeof(PacketId);

        // Provide default implementations of the IDirtiable interface since very few packets actually require it.
        bool IDirtiable.IsDirty => false;
        void IDirtiable.Clean() { }

        /// <summary>
        /// Gets the packet's ID.
        /// </summary>
        PacketId Id { get; }

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

    /// <summary>
    /// Provides extensions for the <see cref="IPacket"/> interface.
    /// </summary>
    public static class PacketExtensions {
        /// <summary>
        /// Writes the <paramref name="packet"/> reference to the given <paramref name="span"/> with the specified
        /// <paramref name="context"/>, including the packet header. Advances the span by the number of bytes written.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="packet">The packet reference.</param>
        /// <param name="span">The span.</param>
        /// <param name="context">The context.</param>
        public static void WriteWithHeader<TPacket>(ref this TPacket packet, ref Span<byte> span, PacketContext context)
                where TPacket : struct, IPacket {
            // Write the payload portion of the packet.
            var tempSpan = span[IPacket.HeaderSize..];
            packet.Write(ref tempSpan, context);

            // Write the header portion of the packet.
            var packetLength = (ushort)(span.Length - tempSpan.Length);
            Unsafe.WriteUnaligned(ref span[0], packetLength);
            Unsafe.WriteUnaligned(ref span[2], packet.Id);

            // Advance the span by the packet length.
            span = span[packetLength..];
        }
    }
}
