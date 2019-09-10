using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to paint a block.
    /// </summary>
    public sealed class PaintBlockPacket : Packet {
        /// <summary>
        /// Gets or sets the block's X coordinate.
        /// </summary>
        public short BlockX { get; set; }

        /// <summary>
        /// Gets or sets the block's Y coordinate.
        /// </summary>
        public short BlockY { get; set; }

        /// <summary>
        /// Gets or sets the block color.
        /// </summary>
        public PaintColor BlockColor { get; set; }

        private protected override PacketType Type => PacketType.PaintBlock;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{BlockColor} @ ({BlockX}, {BlockY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            BlockX = reader.ReadInt16();
            BlockY = reader.ReadInt16();
            BlockColor = (PaintColor)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(BlockX);
            writer.Write(BlockY);
            writer.Write((byte)BlockColor);
        }
    }
}
