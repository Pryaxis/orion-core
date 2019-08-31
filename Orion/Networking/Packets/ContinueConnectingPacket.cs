namespace Orion.Networking.Packets {
    using System;
    using System.IO;

    /// <summary>
    /// Packet sent from the client to the server to request connection.
    /// </summary>
    public sealed class ContinueConnectingPacket : TerrariaPacket {
        private protected override int HeaderlessLength => 1;

        /// <inheritdoc />
        public override bool IsSentToClient => false;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <summary>
        /// Gets or sets the player ID.
        /// </summary>
        public byte PlayerId { get; set; }

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.ContinueConnecting;

        /// <summary>
        /// Reads a <see cref="ContinueConnectingPacket"/> from the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public static ContinueConnectingPacket FromReader(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            var playerId = reader.ReadByte();
            return new ContinueConnectingPacket {PlayerId = playerId};
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerId);
        }
    }
}
