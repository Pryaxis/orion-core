namespace Orion.Networking.Packets {
    using System.IO;

    /// <summary>
    /// Packet sent from the client to the server to request connection.
    /// </summary>
    public sealed class ContinueConnectingPacket : TerrariaPacket {
        private protected override short HeaderlessLength => 1;

        /// <inheritdoc />
        public override bool IsSentToClient => false;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <summary>
        /// Gets the player ID.
        /// </summary>
        public byte PlayerId { get; }

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.ContinueConnecting;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionRequestPacket"/> with the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal ContinueConnectingPacket(BinaryReader reader) {
            PlayerId = reader.ReadByte();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerId);
        }
    }
}
