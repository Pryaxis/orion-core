namespace Orion.Networking.Packets {
    using System;
    using System.IO;
    using System.Text;
    using Orion.Networking.Packets.Extensions;

    /// <summary>
    /// Packet sent from the server to disconnect the client.
    /// </summary>
    public sealed class DisconnectPacket : TerrariaPacket {
        /// <inheritdoc />
        private protected override int HeaderlessLength => Reason.GetBinaryLength(Encoding.UTF8);

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => false;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.Disconnect;

        /// <summary>
        /// Gets the disconnect reason.
        /// </summary>
        public string Reason { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DisconnectPacket"/> with the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public DisconnectPacket(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            Reason = reader.ReadString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisconnectPacket"/> with the given reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reason"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="reason"/> is too long.</exception>
        public DisconnectPacket(string reason) {
            Reason = reason ?? throw new ArgumentNullException(nameof(reason));

            if (HeaderLength + HeaderlessLength > short.MaxValue) {
                throw new ArgumentOutOfRangeException(nameof(reason), "Reason string is too long.");
            }
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Reason);
        }
    }
}
