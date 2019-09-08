using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to unlock a tile.
    /// </summary>
    public sealed class UnlockTilePacket : Packet {
        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        public Type ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        public short TileX { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        public short TileY { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ObjectType = (Type)reader.ReadByte();
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write((byte)ObjectType);
            writer.Write(TileX);
            writer.Write(TileY);
        }

        /// <summary>
        /// Specifies the unlock type.
        /// </summary>
        public enum Type : byte {
#pragma warning disable 1591
            None = 0,
            UnlockChest = 1,
            UnlockDoor = 2,
#pragma warning restore 1591
        }
    }
}
