using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to request a section of the world.
    /// </summary>
    public sealed class RequestWorldSectionPacket : TerrariaPacket {
        /// <inheritdoc />
        public override bool IsSentToClient => false;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.RequestWorldSection;

        /// <summary>
        /// Gets or sets the section's X position.
        /// </summary>
        public int SectionX { get; set; }

        /// <summary>
        /// Gets or sets the section's Y position.
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
