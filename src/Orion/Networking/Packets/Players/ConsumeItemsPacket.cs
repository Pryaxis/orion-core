using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent from the server to the client to consume a player's items. This is sent in response to a
    /// <see cref="RequestMassWireOperationPacket"/>.
    /// </summary>
    public sealed class ConsumeItemsPacket : Packet {
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

        private protected override PacketType Type => PacketType.ConsumeItems;

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
