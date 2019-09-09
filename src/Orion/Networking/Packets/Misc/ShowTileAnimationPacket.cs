using System.IO;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent to create a temporary animation.
    /// </summary>
    public sealed class ShowTileAnimationPacket : Packet {
        /// <summary>
        /// Gets or sets the animation type.
        /// </summary>
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

        private protected override PacketType Type => PacketType.ShowTileAnimation;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            AnimationType = reader.ReadInt16();
            BlockType = (BlockType)reader.ReadUInt16();
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(AnimationType);
            writer.Write((ushort)BlockType);
            writer.Write(TileX);
            writer.Write(TileY);
        }
    }
}
