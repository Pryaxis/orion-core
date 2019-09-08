using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to request a section of the world.
    /// </summary>
    public sealed class RequestWorldSectionPacket : Packet {
        /// <summary>
        /// Gets or sets the section's X coordinate.
        /// </summary>
        public int SectionX { get; set; }

        /// <summary>
        /// Gets or sets the section's Y coordinate.
        /// </summary>
        public int SectionY { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            SectionX = reader.ReadInt32();
            SectionY = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(SectionX);
            writer.Write(SectionY);
        }
    }
}
