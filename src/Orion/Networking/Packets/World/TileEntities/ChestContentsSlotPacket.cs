using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to set a chest contents' slot. This is sent in response to a <see cref="RequestChestPacket"/>.
    /// </summary>
    public sealed class ChestContentsSlotPacket : Packet {
        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        public short ChestIndex { get; set; }

        /// <summary>
        /// Gets or sets the chest contents' slot index.
        /// </summary>
        public byte ChestContentsSlotIndex { get; set; }

        /// <summary>
        /// Gets or sets the item stack size.
        /// </summary>
        public short ItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the item prefix.
        /// </summary>
        public ItemPrefix ItemPrefix { get; set; }

        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        public ItemType ItemType { get; set; }

        private protected override PacketType Type => PacketType.ChestContentsSlot;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ChestIndex = reader.ReadInt16();
            ChestContentsSlotIndex = reader.ReadByte();
            ItemStackSize = reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ItemType = (ItemType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ChestIndex);
            writer.Write(ChestContentsSlotIndex);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix);
            writer.Write((short)ItemType);
        }
    }
}
