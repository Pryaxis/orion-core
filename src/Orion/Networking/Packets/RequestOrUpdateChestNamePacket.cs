using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to request or update a chest's name.
    /// </summary>
    public sealed class RequestOrUpdateChestNamePacket : Packet {
        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        public short ChestIndex { get; set; }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        public short ChestX { get; set; }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        public short ChestY { get; set; }

        /// <summary>
        /// Gets or sets the chest's name.
        /// </summary>
        public string ChestName { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ChestIndex = reader.ReadInt16();
            ChestX = reader.ReadInt16();
            ChestY = reader.ReadInt16();
            ChestName = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(ChestIndex);
            writer.Write(ChestX);
            writer.Write(ChestY);
            writer.Write(ChestName ?? "");
        }
    }
}
