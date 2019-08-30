namespace Orion.Networking.Packets {
    using System.IO;

    /// <summary>
    /// Used as a fail-safe for any packet that failed to be read.
    /// </summary>
    public sealed class UnknownPacket : TerrariaPacket {
        /// <summary>
        /// Gets the byte array.
        /// </summary>
        public byte[] Bytes { get; }

        /// <inheritdoc />
        private protected override short HeaderlessLength { get; }

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type { get; }

        internal UnknownPacket(BinaryReader reader, TerrariaPacketType type, short headerlessLength) {
            Type = type;
            HeaderlessLength = headerlessLength;
            Bytes = reader.ReadBytes(headerlessLength);
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Bytes);
        }
    }
}
