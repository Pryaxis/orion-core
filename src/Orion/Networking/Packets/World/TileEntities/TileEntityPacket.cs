using System.IO;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to modify a tile entity.
    /// </summary>
    public sealed class TileEntityPacket : Packet {
        /// <summary>
        /// Gets or sets the tile entity index.
        /// </summary>
        public int TileEntityIndex { get; set; }

        /// <summary>
        /// Gets or sets the tile entity. A value of <c>null</c> indicates a deletion.
        /// </summary>
        public NetworkTileEntity TileEntity { get; set; }

        private protected override PacketType Type => PacketType.TileEntity;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            TileEntityIndex = reader.ReadInt32();
            if (!reader.ReadBoolean()) {
                return;
            }

            TileEntity = NetworkTileEntity.FromReader(reader, false);
            TileEntity.Index = TileEntityIndex;
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(TileEntityIndex);
            writer.Write(TileEntity != null);
            TileEntity?.WriteToWriter(writer, false);
        }
    }
}
