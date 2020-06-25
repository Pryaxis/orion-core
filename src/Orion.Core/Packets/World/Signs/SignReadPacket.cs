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

namespace Orion.Core.Packets.World.Signs
{
    /// <summary>
    /// A packet sent from the client to the server to read a sign.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public sealed class SignReadPacket : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the sign's X coordinate.
        /// </summary>
        /// <value>The sign's X coordinate.</value>
        [field: FieldOffset(0)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the sign's Y coordinate.
        /// </summary>
        /// <value>The sign's Y coordinate.</value>
        [field: FieldOffset(2)] public short Y { get; set; }

        PacketId IPacket.Id => PacketId.SignRead;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 4);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 4);
    }
}
