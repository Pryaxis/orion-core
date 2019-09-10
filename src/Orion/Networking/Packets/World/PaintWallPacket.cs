using System.IO;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to paint a wall.
    /// </summary>
    public sealed class PaintWallPacket : Packet {
        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        public short TileX { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        public short TileY { get; set; }

        /// <summary>
        /// Gets or sets the wall color.
        /// </summary>
        public PaintColor WallColor { get; set; }

        private protected override PacketType Type => PacketType.PaintWall;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
            WallColor = (PaintColor)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(TileX);
            writer.Write(TileY);
            writer.Write((byte)WallColor);
        }
    }
}
