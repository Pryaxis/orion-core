using System.IO;
using Orion.World.Tiles;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to place an object.
    /// </summary>
    public sealed class PlaceObjectPacket : Packet {
        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        public short TileX { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        public short TileY { get; set; }

        /// <summary>
        /// Gets or sets the block type.
        /// </summary>
        public BlockType ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the object style.
        /// </summary>
        public short ObjectStyle { get; set; }

        /// <summary>
        /// Gets or sets the alternate. (NOT USED?)
        /// </summary>
        public byte Alternate { get; set; }

        /// <summary>
        /// Gets or sets the random state.
        /// </summary>
        public sbyte RandomState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the direction.
        /// </summary>
        public bool Direction { get; set; }

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
            ObjectType = (BlockType)reader.ReadInt16();
            ObjectStyle = reader.ReadInt16();
            Alternate = reader.ReadByte();
            RandomState = reader.ReadSByte();
            Direction = reader.ReadBoolean();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(TileX);
            writer.Write(TileY);
            writer.Write((short)ObjectType);
            writer.Write(ObjectStyle);
            writer.Write(Alternate);
            writer.Write(RandomState);
            writer.Write(Direction);
        }
    }
}
