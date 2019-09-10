using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent to show a tile animation.
    /// </summary>
    public sealed class TileAnimationPacket : Packet {
        /// <summary>
        /// Gets or sets the animation type.
        /// </summary>
        // TODO: implement enum for this.
        public short AnimationType { get; set; }

        /// <summary>
        /// Gets or sets the block type.
        /// </summary>
        public BlockType BlockType {get; set;}

        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        public short TileX { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        public short TileY { get; set; }

        private protected override PacketType Type => PacketType.TileAnimation;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{AnimationType} @ ({TileX}, {TileY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            AnimationType = reader.ReadInt16();
            BlockType = (BlockType)reader.ReadUInt16();
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(AnimationType);
            writer.Write((ushort)BlockType);
            writer.Write(TileX);
            writer.Write(TileY);
        }
    }
}
