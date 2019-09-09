using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to modify a tile.
    /// </summary>
    public sealed class ModifyTilePacket : Packet {
        /// <summary>
        /// Gets or sets the modification type.
        /// </summary>
        public TileModificationType ModificationType { get; set; }

        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        public short TileX { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public short TileY { get; set; }

        /// <summary>
        /// Gets or sets the modification data.
        /// </summary>
        public short ModificationData { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        public byte ModificationStyle { get; set; }

        private protected override PacketType Type => PacketType.ModifyTile;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ModificationType = (TileModificationType)reader.ReadByte();
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
            ModificationData = reader.ReadInt16();
            ModificationStyle = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write((byte)ModificationType);
            writer.Write(TileX);
            writer.Write(TileY);
            writer.Write(ModificationData);
            writer.Write(ModificationStyle);
        }
    }
}
