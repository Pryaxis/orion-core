using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to set a tile entity's information.
    /// </summary>
    public sealed class TileEntityInfoPacket : Packet {
        /// <summary>
        /// Gets or sets the tile entity index.
        /// </summary>
        public int TileEntityIndex { get; set; }

        /// <summary>
        /// Gets or sets the tile entity. A value of <c>null</c> indicates a deletion.
        /// </summary>
        public NetworkTileEntity TileEntity { get; set; }

        private protected override PacketType Type => PacketType.TileEntityInfo;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={TileEntityIndex}, {TileEntity}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            TileEntityIndex = reader.ReadInt32();
            if (!reader.ReadBoolean()) {
                return;
            }

            TileEntity = NetworkTileEntity.FromReader(reader, false);
            TileEntity.Index = TileEntityIndex;
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(TileEntityIndex);
            writer.Write(TileEntity != null);
            TileEntity?.WriteToWriter(writer, false);
        }
    }
}
