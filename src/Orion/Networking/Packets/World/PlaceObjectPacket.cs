using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to place an object.
    /// </summary>
    public sealed class PlaceObjectPacket : Packet {
        /// <summary>
        /// Gets or sets the object's X coordinate.
        /// </summary>
        public short ObjectX { get; set; }

        /// <summary>
        /// Gets or sets the object's Y coordinate.
        /// </summary>
        public short ObjectY { get; set; }

        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        public BlockType ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the object style.
        /// </summary>
        public short ObjectStyle { get; set; }

        /// <summary>
        /// Gets or sets the object's random state.
        /// </summary>
        public sbyte ObjectRandomState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the object direction.
        /// </summary>
        public bool ObjectDirection { get; set; }

        private protected override PacketType Type => PacketType.PlaceObject;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ObjectType}_{ObjectStyle} @ ({ObjectX}, {ObjectY}), ...]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ObjectX = reader.ReadInt16();
            ObjectY = reader.ReadInt16();
            ObjectType = (BlockType)reader.ReadInt16();
            ObjectStyle = reader.ReadInt16();
            reader.ReadByte();
            ObjectRandomState = reader.ReadSByte();
            ObjectDirection = reader.ReadBoolean();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ObjectX);
            writer.Write(ObjectY);
            writer.Write((short)ObjectType);
            writer.Write(ObjectStyle);
            writer.Write((byte)0);
            writer.Write(ObjectRandomState);
            writer.Write(ObjectDirection);
        }
    }
}
