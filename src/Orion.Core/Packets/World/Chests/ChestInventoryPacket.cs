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
using Orion.Core.Items;

namespace Orion.Core.Packets.World.Chests
{
    /// <summary>
    /// A packet sent to set a chest's inventory.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ChestInventoryPacket : IPacket
    {
        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        /// <value>The chest index.</value>
        [field: FieldOffset(0)] public short ChestIndex { get; set; }

        /// <summary>
        /// Gets or sets the inventory slot.
        /// </summary>
        /// <value>The inventory slot.</value>
        [field: FieldOffset(2)] public byte Slot { get; set; }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        /// <value>The item's stack size.</value>
        [field: FieldOffset(3)] public short StackSize { get; set; }

        /// <summary>
        /// Gets or sets the item's prefix.
        /// </summary>
        /// <value>The item's prefix.</value>
        [field: FieldOffset(5)] public ItemPrefix Prefix { get; set; }

        /// <summary>
        /// Gets or sets the item's ID.
        /// </summary>
        /// <value>The item's ID.</value>
        [field: FieldOffset(6)] public ItemId Id { get; set; }

        PacketId IPacket.Id => PacketId.ChestInventory;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context) => span.Read(ref this.AsRefByte(0), 8);

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) => span.Write(ref this.AsRefByte(0), 8);
    }
}
