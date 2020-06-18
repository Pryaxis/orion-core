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

namespace Orion.Core.Packets
{
    /// <summary>
    /// Represents a packet, the main form of communication between the server and its clients.
    /// </summary>
    public interface IPacket
    {
        /// <summary>
        /// The packet header size.
        /// </summary>
        public const int HeaderSize = sizeof(ushort) + sizeof(PacketId);

        /// <summary>
        /// Gets the packet's ID.
        /// </summary>
        /// <value>The packet's ID.</value>
        PacketId Id { get; }

        /// <summary>
        /// Reads the packet from the given <paramref name="span"/> with the specified packet
        /// <paramref name="context"/>, mutating this instance. Returns the number of bytes read from the
        /// <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="context">The packet context to use when reading.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        int Read(Span<byte> span, PacketContext context);

        /// <summary>
        /// Writes the packet to the given <paramref name="span"/> with the specified packet <paramref name="context"/>.
        /// Returns the number of bytes written to the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        int Write(Span<byte> span, PacketContext context);
    }

    /// <summary>
    /// Provides extensions for the <see cref="IPacket"/> interface.
    /// </summary>
    public static class IPacketExtensions
    {
        /// <summary>
        /// Writes the <paramref name="packet"/> reference to the given <paramref name="span"/> with the specified
        /// packet <paramref name="context"/>, including the packet header. Returns the number of bytes written to the
        /// <paramref name="span"/>.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="packet">The packet reference.</param>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        public static int WriteWithHeader<TPacket>(ref this TPacket packet, Span<byte> span, PacketContext context)
            where TPacket : struct, IPacket
        {
            var packetLength = IPacket.HeaderSize + packet.Write(span[IPacket.HeaderSize..], context);
            Unsafe.WriteUnaligned(ref span[0], (ushort)packetLength);
            span[2] = (byte)packet.Id;
            return packetLength;
        }
    }
}
