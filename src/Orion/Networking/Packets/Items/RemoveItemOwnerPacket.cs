using System.IO;

namespace Orion.Networking.Packets.Items {
    /// <summary>
    /// Packet sent from the server to the client to remove the owner of an item.
    /// </summary>
    public sealed class RemoveItemOwnerPacket : Packet {
        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        public short ItemIndex { get; set; }

        private protected override PacketType Type => PacketType.RemoveItemOwner;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ItemIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ItemIndex);
        }
    }
}
