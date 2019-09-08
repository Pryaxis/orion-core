using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to update an NPC shop slot.
    /// </summary>
    public sealed class UpdateNpcShopSlotPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC shop slot.
        /// </summary>
        public byte NpcShopSlot { get; set; }

        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        /// Gets or sets the item stack size.
        /// </summary>
        public short ItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the item prefix.
        /// </summary>
        public ItemPrefix ItemPrefix { get; set; }

        /// <summary>
        /// Gets or sets the item value.
        /// </summary>
        public int ItemValue { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcShopSlot = reader.ReadByte();
            ItemType = (ItemType)reader.ReadInt16();
            ItemStackSize = reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ItemValue = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcShopSlot);
            writer.Write((short)ItemType);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix);
            writer.Write(ItemValue);
        }
    }
}
