using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set an NPC's shop slot.
    /// </summary>
    public sealed class NpcShopSlotPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC shop slot index.
        /// </summary>
        public byte NpcShopSlotIndex { get; set; }

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

        private protected override PacketType Type => PacketType.NpcShopSlot;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcShopSlotIndex = reader.ReadByte();
            ItemType = (ItemType)reader.ReadInt16();
            ItemStackSize = reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ItemValue = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcShopSlotIndex);
            writer.Write((short)ItemType);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix);
            writer.Write(ItemValue);
        }
    }
}
