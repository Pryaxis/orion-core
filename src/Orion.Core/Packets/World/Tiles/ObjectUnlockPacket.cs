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

namespace Orion.Core.Packets.World.Tiles
{
    /// <summary>
    /// A packet sent to unlock an object.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 5)]
    public sealed class ObjectUnlockPacket : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the unlock type.
        /// </summary>
        /// <value>The unlock type.</value>
        [field: FieldOffset(0)] public UnlockType Type { get; set; }

        /// <summary>
        /// Gets or sets the object's X coordinate.
        /// </summary>
        /// <value>The object's X coordinate.</value>
        [field: FieldOffset(1)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the object's Y coordinate.
        /// </summary>
        /// <value>The object's Y coordinate.</value>
        [field: FieldOffset(3)] public short Y { get; set; }

        PacketId IPacket.Id => PacketId.ObjectUnlock;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 5);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 5);
    }
}
