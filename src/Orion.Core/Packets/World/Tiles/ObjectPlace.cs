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
using Orion.Core.Utils;

namespace Orion.Core.Packets.World.Tiles
{
    /// <summary>
    /// A packet used to send object placements.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 11)]
    public struct ObjectPlace : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        [field: FieldOffset(0)] public short X { get; set; }

        /// <summary>
        /// Gest or sets the Y coordinate.
        /// </summary>
        [field: FieldOffset(2)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the type of tile.
        /// </summary>
        [field: FieldOffset(4)] public short TileType { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        [field: FieldOffset(6)] public short Style { get; set; }

        /// <summary>
        /// Gets or sets the alternate tile style.
        /// </summary>
        [field: FieldOffset(8)] public byte AlternateStyle { get; set; }

        /// <summary>
        /// Gets or sets a value used when calculating a random placement style.
        /// </summary>
        [field: FieldOffset(9)] public sbyte RandomStyleModifier { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the tile is placed facing the right direction.
        /// </summary>
        [field: FieldOffset(10)] public bool IsRightDirection { get; set; }

        PacketId IPacket.Id => PacketId.ObjectPlace;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 11);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 11);
    }
}
