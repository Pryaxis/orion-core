using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to paint a wall.
    /// </summary>
    public sealed class PaintWallPacket : Packet {
        /// <summary>
        /// Gets or sets the wall's X coordinate.
        /// </summary>
        public short WallX { get; set; }

        /// <summary>
        /// Gets or sets the wall's Y coordinate.
        /// </summary>
        public short WallY { get; set; }

        /// <summary>
        /// Gets or sets the wall color.
        /// </summary>
        public PaintColor WallColor { get; set; }

        private protected override PacketType Type => PacketType.PaintWall;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{WallColor} @ ({WallX}, {WallY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            WallX = reader.ReadInt16();
            WallY = reader.ReadInt16();
            WallColor = (PaintColor)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(WallX);
            writer.Write(WallY);
            writer.Write((byte)WallColor);
        }
    }
}
