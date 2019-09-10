﻿using System.IO;
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
        /// Gets or sets the item type.
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        /// Gets or sets the item prefix.
        /// </summary>
        public ItemPrefix ItemPrefix { get; set; }

        /// <summary>
        /// Gets or sets the item stack size.
        /// </summary>
        public short ItemStackSize { get; set; }

        private protected override PacketType Type => PacketType.ItemFrame;

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
