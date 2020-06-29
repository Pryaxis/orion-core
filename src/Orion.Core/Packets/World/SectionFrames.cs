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
using System.Runtime.InteropServices;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent from the server to the client to frame sections of the world.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct SectionFrames : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.

        /// <summary>
        /// Gets or sets the starting section's X index.
        /// </summary>
        /// <value>The starting section's X index.</value>
        [field: FieldOffset(0)] public short StartX { get; set; }

        /// <summary>
        /// Gets or sets the starting section's Y index.
        /// </summary>
        /// <value>The starting section's Y index.</value>
        [field: FieldOffset(2)] public short StartY { get; set; }

        /// <summary>
        /// Gets or sets the ending section's X index.
        /// </summary>
        /// <value>The ending section's X index.</value>
        [field: FieldOffset(4)] public short EndX { get; set; }

        /// <summary>
        /// Gets or sets the ending section's Y index.
        /// </summary>
        /// <value>The ending section's Y index.</value>
        [field: FieldOffset(6)] public short EndY { get; set; }

        PacketId IPacket.Id => PacketId.SectionFrames;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 8);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 8);
    }
}
