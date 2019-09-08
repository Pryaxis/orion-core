using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to remove the owner of an item.
    /// </summary>
    public sealed class RemoveItemOwnerPacket : Packet {
        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        public short ItemIndex { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) =>
            ItemIndex = reader.ReadInt16();

        private protected override void WriteToWriter(BinaryWriter writer) => writer.Write(ItemIndex);
    }
}
