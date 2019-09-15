// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using System.IO;
using System.Text;
using Orion.Networking.Packets.Players;

namespace Orion.Networking {
    /// <summary>
    /// Represents a packet.
    /// </summary>
    public abstract class Packet {
        private static readonly IDictionary<PacketType, Func<Packet>> PacketConstructors =
            new Dictionary<PacketType, Func<Packet>> {[PacketType.PlayerConnect] = () => new PlayerConnectPacket()};

        /// <summary>
        /// Gets the packet's type.
        /// </summary>
        public abstract PacketType Type { get; }

        /// <summary>
        /// Gets a value indicating whether the packet is dirty.
        /// </summary>
        public bool IsPacketDirty { get; private protected set; }

        /// <summary>
        /// Reads and returns a packet from the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">The context with which to read the packet.</param>
        /// <returns>The resulting packet.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        public static Packet ReadFromStream(Stream stream, PacketContext context) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var reader = new BinaryReader(stream, Encoding.UTF8, true);
#if DEBUG
            var oldPosition = stream.Position;
            var packetLength = reader.ReadUInt16();
#else
            reader.ReadUInt16();
#endif
            var packetType = new PacketType(reader.ReadByte());
            var packet = PacketConstructors[packetType]();
            packet.ReadFromReader(reader, context);

#if DEBUG
            Debug.Assert(stream.Position - oldPosition == packetLength, "Packet should have been consumed.");
#endif
            return packet;
        }

        /// <summary>
        /// Writes the packet to the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">The context with which to read the packet.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        public void WriteToStream(Stream stream, PacketContext context) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var writer = new BinaryWriter(stream, Encoding.UTF8, true);
            var oldPosition = stream.Position;
            writer.Write((short)0);
            writer.Write(Type.Id);
            WriteToWriter(writer, context);

            var position = stream.Position;
            var packetLength = position - oldPosition;
            if (packetLength > ushort.MaxValue) {
                throw new InvalidOperationException("Packet is too long.");
            }

            stream.Position = oldPosition;
            writer.Write((ushort)packetLength);
            stream.Position = position;
        }

        private protected abstract void ReadFromReader(BinaryReader reader, PacketContext context);
        private protected abstract void WriteToWriter(BinaryWriter writer, PacketContext context);
    }
}
