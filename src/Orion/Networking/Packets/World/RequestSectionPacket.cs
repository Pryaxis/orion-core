using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the client to the server to request a section of the world.
    /// </summary>
    public sealed class RequestSectionPacket : Packet {
        /// <summary>
        /// Gets or sets the section's X position. An invalid value results in only the spawn section being sent.
        /// </summary>
        public int SectionX { get; set; }

        /// <summary>
        /// Gets or sets the section's Y position. An invalid value results in only the spawn section being sent.
        /// </summary>
        public int SectionY { get; set; }

        private protected override PacketType Type => PacketType.RequestSection;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            SectionX = reader.ReadInt32();
            SectionY = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(SectionX);
            writer.Write(SectionY);
        }
    }
}
