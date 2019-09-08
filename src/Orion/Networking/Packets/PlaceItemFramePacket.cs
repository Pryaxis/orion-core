using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to place an item into an item frame.
    /// </summary>
    public sealed class PlaceItemFramePacket : Packet {
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

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ItemFrameX = reader.ReadInt16();
            ItemFrameY = reader.ReadInt16();
            ItemType = (ItemType)reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ItemStackSize = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(ItemFrameX);
            writer.Write(ItemFrameY);
            writer.Write((short)ItemType);
            writer.Write((byte)ItemPrefix);
            writer.Write(ItemStackSize);
        }
    }
}
