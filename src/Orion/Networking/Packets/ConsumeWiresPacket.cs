using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to consume a player's items.
    /// </summary>
    public sealed class ConsumeWiresPacket : Packet {
        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        /// Gets or sets the item stack size.
        /// </summary>
        public short ItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ItemType = (ItemType)reader.ReadInt16();
            ItemStackSize = reader.ReadInt16();
            PlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write((short)ItemType);
            writer.Write(ItemStackSize);
            writer.Write(PlayerIndex);
        }
    }
}
