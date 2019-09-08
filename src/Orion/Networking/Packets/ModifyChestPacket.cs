using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to modify a chest.
    /// </summary>
    public sealed class ModifyChestPacket : Packet {
        /// <summary>
        /// Gets or sets the modification type.
        /// </summary>
        public Type ModificationType { get; set; }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        public short ChestX { get; set; }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        public short ChestY { get; set; }

        /// <summary>
        /// Gets or sets the chest style.
        /// </summary>
        public short ChestStyle { get; set; }

        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        public short ChestIndex { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ModificationType = (Type)reader.ReadByte();
            ChestX = reader.ReadInt16();
            ChestY = reader.ReadInt16();
            ChestStyle = reader.ReadInt16();
            ChestIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write((byte)ModificationType);
            writer.Write(ChestX);
            writer.Write(ChestY);
            writer.Write(ChestStyle);
            writer.Write(ChestIndex);
        }

        /// <summary>
        /// Specifies the type of modification.
        /// </summary>
        public enum Type {
#pragma warning disable 1591
            PlaceContainers,
            BreakContainers,
            PlaceDressers,
            BreakDressers,
            PlaceContainers2,
            BreakContainers2,
#pragma warning restore 1591
        }
    }
}
