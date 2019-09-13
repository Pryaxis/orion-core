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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent from the client to the server to set an item frame.
    /// </summary>
    public sealed class ItemFramePacket : Packet {
        /// <summary>
        /// Gets or sets the item frame's X coordinate.
        /// </summary>
        public short ItemFrameX { get; set; }

        /// <summary>
        /// Gets or sets the item frame's Y coordinate.
        /// </summary>
        public short ItemFrameY { get; set; }

        /// <summary>
        /// Gets or sets the item's <see cref="Orion.Items.ItemType"/>.
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        /// Gets or sets the item's <see cref="Orion.Items.ItemPrefix"/>.
        /// </summary>
        public ItemPrefix ItemPrefix { get; set; }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        public short ItemStackSize { get; set; }

        internal override PacketType Type => PacketType.ItemFrame;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[({ItemFrameX}, {ItemFrameY}) is " +
            $"{(ItemPrefix != ItemPrefix.None ? ItemPrefix + " " : "")}{ItemType} x{ItemStackSize}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ItemFrameX = reader.ReadInt16();
            ItemFrameY = reader.ReadInt16();
            ItemType = (ItemType)reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ItemStackSize = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ItemFrameX);
            writer.Write(ItemFrameY);
            writer.Write((short)ItemType);
            writer.Write((byte)ItemPrefix);
            writer.Write(ItemStackSize);
        }
    }
}
