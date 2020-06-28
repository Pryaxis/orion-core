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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Orion.Core.Packets.Client;
using Orion.Core.Packets.Items;
using Orion.Core.Packets.Npcs;
using Orion.Core.Packets.Players;
using Orion.Core.Packets.Server;
using Orion.Core.Packets.World;
using Orion.Core.Packets.World.Chests;
using Orion.Core.Packets.World.Signs;
using Orion.Core.Packets.World.TileEntities;
using Orion.Core.Packets.World.Tiles;

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
        public PacketId Id { get; }

        /// <summary>
        /// Reads the packet's body from the given <paramref name="span"/> with the specified packet
        /// <paramref name="context"/>, mutating this instance. Returns the number of bytes read from the
        /// <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="context">The packet context to use when reading.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        /// <remarks>
        /// Implementations may not perform any bounds checking on <paramref name="span"/>.
        /// </remarks>
        public int ReadBody(Span<byte> span, PacketContext context);

        /// <summary>
        /// Writes the packet's body to the given <paramref name="span"/> with the specified packet
        /// <paramref name="context"/>. Returns the number of bytes written to the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        /// <remarks>
        /// Implementations may not perform any bounds checking on <paramref name="span"/>.
        /// </remarks>
        public int WriteBody(Span<byte> span, PacketContext context);
    }

    /// <summary>
    /// Provides extensions for the <see cref="IPacket"/> interface.
    /// </summary>
    public static class IPacketExtensions
    {
        /// <summary>
        /// Writes the packet to the given <paramref name="span"/> in the specified <paramref name="context"/>.
        /// Returns the number of bytes written to the <paramref name="span"/>.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="packet"/> is <see langword="null"/>.</exception>
        public static int Write(this IPacket packet, Span<byte> span, PacketContext context)
        {
            if (packet is null)
            {
                throw new ArgumentNullException(nameof(packet));
            }

            Debug.Assert(span.Length >= 3);

            ref var header = ref MemoryMarshal.GetReference(span);

            var packetLength = 3 + packet.WriteBody(span[3..], context);

            // Write the packet header with no bounds checking since we already performed bounds checking.
            Unsafe.WriteUnaligned(ref header, (ushort)packetLength);
            Unsafe.WriteUnaligned(ref Unsafe.Add(ref header, 2), packet.Id);

            return packetLength;
        }

        /// <summary>
        /// Writes the packet to the given <paramref name="span"/> in the specified <paramref name="context"/>.
        /// Returns the number of bytes written to the <paramref name="span"/>.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="packet">The packet.</param>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="packet"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This overload is provided for efficient struct codegen.
        /// </remarks>
        public static int Write<TPacket>(this TPacket packet, Span<byte> span, PacketContext context)
            where TPacket : struct, IPacket
        {
            Debug.Assert(span.Length >= 3);

            ref var header = ref MemoryMarshal.GetReference(span);

            var packetLength = 3 + packet.WriteBody(span[3..], context);

            // Write the packet header with no bounds checking since we already performed bounds checking.
            Unsafe.WriteUnaligned(ref header, (ushort)packetLength);
            Unsafe.WriteUnaligned(ref Unsafe.Add(ref header, 2), packet.Id);

            return packetLength;
        }
    }
}
