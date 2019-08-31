namespace Orion.Networking.Packets {
    using System;
    using System.IO;

    /// <summary>
    /// Packet sent from the server to the client to continue the connection.
    /// </summary>
    public sealed class ContinueConnectingPacket : TerrariaPacket {
        private protected override int HeaderlessLength => 1;

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => false;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.ContinueConnecting;

        /// <summary>
        /// Gets or sets the player ID.
        /// </summary>
        public byte PlayerId { get; set; }

        /// <summary>
        /// Reads a <see cref="ContinueConnectingPacket"/> from the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public static ContinueConnectingPacket FromReader(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            return new ContinueConnectingPacket {PlayerId = reader.ReadByte()};
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerId);
        }
    }
}
