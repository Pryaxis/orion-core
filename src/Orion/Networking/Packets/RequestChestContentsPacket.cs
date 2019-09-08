using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to request chest contents.
    /// </summary>
    public sealed class RequestChestContentsPacket : Packet {
        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        public short ChestX { get; set; }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        public short ChestY { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ChestX = reader.ReadInt16();
            ChestY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(ChestX);
            writer.Write(ChestY);
        }
    }
}
