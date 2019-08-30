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
        /// Gets the player ID.
        /// </summary>
        public byte PlayerId { get; }

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.ContinueConnecting;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionRequestPacket"/> with the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public ContinueConnectingPacket(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            PlayerId = reader.ReadByte();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionRequestPacket"/> with the given player ID.
        /// </summary>
        /// <param name="playerId">The player ID.</param>
        public ContinueConnectingPacket(byte playerId) {
            PlayerId = playerId;
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerId);
        }
    }
}
