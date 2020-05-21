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
using System.Runtime.InteropServices;

namespace Orion.Packets {
    /// <summary>
    /// Represents an unknown packet.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct UnknownPacket : IPacket {
        /// <summary>
        /// The packet length.
        /// </summary>
        [FieldOffset(0)]
        public ushort Length;

        [FieldOffset(2)]
        private PacketId _id;

        /// <summary>
        /// The packet data.
        /// </summary>
        [FieldOffset(3)]
        public unsafe fixed byte Data[ushort.MaxValue - IPacket.HeaderSize];

        /// <summary>
        /// Gets or sets the packet's ID.
        /// </summary>
        /// <value>The packet's ID.</value>
        public PacketId Id {
            get => _id;
            set => _id = value;
        }

        /// <inheritdoc/>
        public unsafe void Read(ReadOnlySpan<byte> span, PacketContext context) {
            Length = (ushort)span.Length;
            if (Length == 0) {
                return;
            }

            Unsafe.CopyBlockUnaligned(ref Data[0], ref Unsafe.AsRef(in span[0]), Length);
        }

        /// <inheritdoc/>
        public unsafe void Write(ref Span<byte> span, PacketContext context) {
            if (Length == 0) {
                return;
            }

            Unsafe.CopyBlockUnaligned(ref span[0], ref Data[0], Length);
            span = span[Length..];
        }
    }
}
