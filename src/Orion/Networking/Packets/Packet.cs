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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Orion.Events;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Represents a packet.
    /// </summary>
    public abstract class Packet : IDirtiable {
        private protected bool _isDirty;

        /// <inheritdoc />
        public virtual bool IsDirty => _isDirty;

        /// <summary>
        /// Gets the packet's type.
        /// </summary>
        public abstract PacketType Type { get; }

        // Prevent outside inheritance.
        private protected Packet() { }

        /// <summary>
        /// Reads and returns a packet from the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">The context with which to read the packet.</param>
        /// <returns>The resulting packet.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="PacketException">The packet could not be parsed correctly.</exception>
        public static Packet ReadFromStream(Stream stream, PacketContext context) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            try {
                var reader = new BinaryReader(stream, Encoding.UTF8, true);
#if DEBUG
                var oldPosition = stream.Position;
                var packetLength = reader.ReadUInt16();
#else
                reader.ReadUInt16();
#endif
                var packetType = PacketType.FromId(reader.ReadByte()) ??
                                 throw new PacketException("Packet type is invalid.");
                var packet = packetType.Constructor();

                packet.ReadFromReader(reader, context);
                packet.Clean();
#if DEBUG
                Debug.Assert(stream.Position - oldPosition == packetLength, "Packet should have been consumed.");
                Debug.Assert(!packet.IsDirty, "Packet should not be dirty.");
#endif
                return packet;
            } catch (Exception ex) when (!(ex is PacketException)) {
                throw new PacketException("An exception occurred when parsing the packet.", ex);
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}";

        /// <inheritdoc />
        public virtual void Clean() {
            _isDirty = false;
        }

        /// <summary>
        /// Writes the packet to the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">The context with which to read the packet.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="PacketException">The packet could not be written correctly.</exception>
        public void WriteToStream(Stream stream, PacketContext context) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            try {
                var writer = new BinaryWriter(stream, Encoding.UTF8, true);
                var oldPosition = stream.Position;
                stream.Position += 2;
                writer.Write(Type.Id);
                WriteToWriter(writer, context);

                var position = stream.Position;
                var packetLength = position - oldPosition;
                if (packetLength > ushort.MaxValue) {
                    throw new PacketException("Packet is too long.");
                }

                stream.Position = oldPosition;
                writer.Write((ushort)packetLength);
                stream.Position = position;
            } catch (Exception ex) when (!(ex is PacketException)) {
                throw new PacketException("An exception occurred when writing the packet.", ex);
            }
        }

        private protected abstract void ReadFromReader(BinaryReader reader, PacketContext context);
        private protected abstract void WriteToWriter(BinaryWriter writer, PacketContext context);
    }
}
