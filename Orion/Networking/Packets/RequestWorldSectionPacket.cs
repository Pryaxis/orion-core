namespace Orion.Networking.Packets {
    using System;
    using System.IO;

    /// <summary>
    /// Packet sent from the client to the server to request a world section.
    /// </summary>
    public sealed class RequestWorldSectionPacket : TerrariaPacket {
        private protected override int HeaderlessLength => 8;

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

        /// <summary>
        /// Reads a <see cref="RequestWorldSectionPacket"/> from the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public static RequestWorldSectionPacket FromReader(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            return new RequestWorldSectionPacket {SectionX = reader.ReadInt32(), SectionY = reader.ReadInt32()};
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(SectionX);
            writer.Write(SectionY);
        }
    }
}
