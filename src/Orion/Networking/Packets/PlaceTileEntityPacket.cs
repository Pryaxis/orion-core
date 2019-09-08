using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to place a tile entity.
    /// </summary>
    public sealed class PlaceTileEntityPacket : Packet {
        /// <summary>
        /// Gets or sets the tile entity's X coordinate.
        /// </summary>
        public short TileEntityX { get; set; }

        /// <summary>
        /// Gets or sets the tile entity's Y coordinate.
        /// </summary>
        public short TileEntityY { get; set; }

        /// <summary>
        /// Gets or sets the tile entity's type.
        /// </summary>
        public byte TileEntityType { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            TileEntityX = reader.ReadInt16();
            TileEntityY = reader.ReadInt16();
            TileEntityType = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(TileEntityX);
            writer.Write(TileEntityY);
            writer.Write(TileEntityType);
        }
    }
}
