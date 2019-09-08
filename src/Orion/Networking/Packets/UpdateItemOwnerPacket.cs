using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update an item's owner.
    /// </summary>
    public sealed class UpdateItemOwnerPacket : Packet {
        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        public short ItemIndex { get; set; }

        /// <summary>
        /// Gets or sets the owner's player index.
        /// </summary>
        public byte OwnerPlayerIndex { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ItemIndex = reader.ReadInt16();
            OwnerPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(ItemIndex);
            writer.Write(OwnerPlayerIndex);
        }
    }
}
