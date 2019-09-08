using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update one slot of a chest's contents.
    /// </summary>
    public sealed class UpdateChestContentsSlotPacket : Packet {
        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        public short ChestIndex { get; set; }

        /// <summary>
        /// Gets or sets the chest content slot.
        /// </summary>
        public byte ChestContentsSlot { get; set; }

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

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ChestIndex = reader.ReadInt16();
            ChestContentsSlot = reader.ReadByte();
            ItemStackSize = reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ItemType = (ItemType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(ChestIndex);
            writer.Write(ChestContentsSlot);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix);
            writer.Write((short)ItemType);
        }
    }
}
