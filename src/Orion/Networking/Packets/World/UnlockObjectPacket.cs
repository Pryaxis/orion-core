using System.Diagnostics.CodeAnalysis;
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
        public short ObjectX { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        public short ObjectY { get; set; }

        private protected override PacketType Type => PacketType.UnlockObject;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ObjectType} @ ({ObjectX}, {ObjectY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ObjectType = (UnlockObjectType)reader.ReadByte();
            ObjectX = reader.ReadInt16();
            ObjectY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)ObjectType);
            writer.Write(ObjectX);
            writer.Write(ObjectY);
        }
    }
}
