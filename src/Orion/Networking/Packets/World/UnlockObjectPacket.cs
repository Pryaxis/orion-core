using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to unlock an object (chest, door, etc.).
    /// </summary>
    public sealed class UnlockObjectPacket : Packet {
        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        public UnlockObjectType ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        public short TileX { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        public short TileY { get; set; }

        private protected override PacketType Type => PacketType.UnlockObject;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ObjectType = (UnlockObjectType)reader.ReadByte();
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)ObjectType);
            writer.Write(TileX);
            writer.Write(TileY);
        }
    }
}
