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
using Orion.Core.Items;

namespace Orion.Core.Packets.World.TileEntities
{
    /// <summary>
    /// A packet sent from the client to the server to set an item frame.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ItemFrameInfoPacket : IPacket
    {
        /// <summary>
        /// Gets or sets the item frame's X coordinate.
        /// </summary>
        /// <value>The item frame's X coordinate.</value>
        [field: FieldOffset(0)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the item frame's Y coordinate.
        /// </summary>
        /// <value>The item frame's Y coordinate.</value>
        [field: FieldOffset(2)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the item frame item's ID.
        /// </summary>
        /// <value>The item frame item's ID.</value>
        [field: FieldOffset(4)] public ItemId Id { get; set; }

        /// <summary>
        /// Gets or sets the item frame item's prefix.
        /// </summary>
        /// <value>The item frame item's prefix.</value>
        [field: FieldOffset(6)] public ItemPrefix Prefix { get; set; }

        /// <summary>
        /// Gets or sets the item frame item's stack size.
        /// </summary>
        /// <value>The item frame item's stack size.</value>
        [field: FieldOffset(7)] public short StackSize { get; set; }

        PacketId IPacket.Id => PacketId.ItemFrameInfo;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context) => span.Read(ref this.AsRefByte(0), 9);

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) => span.Write(ref this.AsRefByte(0), 9);
    }
}
