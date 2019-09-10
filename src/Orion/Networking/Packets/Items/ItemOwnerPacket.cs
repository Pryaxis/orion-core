using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Items {
    /// <summary>
    /// Packet sent to set an item's owner.
    /// </summary>
    public sealed class ItemOwnerPacket : Packet {
        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        public short ItemIndex { get; set; }

        /// <summary>
        /// Gets or sets the owner's player index.
        /// </summary>
        public byte OwnerPlayerIndex { get; set; }

        private protected override PacketType Type => PacketType.ItemOwner;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={ItemIndex}, O={OwnerPlayerIndex}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ItemIndex = reader.ReadInt16();
            OwnerPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ItemIndex);
            writer.Write(OwnerPlayerIndex);
        }
    }
}
