namespace Orion.Networking.Packets {
    using System;
    using System.IO;
    using System.Text;
    using Orion.Networking.Packets.Extensions;

    /// <summary>
    /// Packet sent from the server to disconnect the client.
    /// </summary>
    public sealed class DisconnectPacket : TerrariaPacket {
        private string _reason = "";

        /// <inheritdoc />
        private protected override int HeaderlessLength => Reason.GetBinaryLength(Encoding.UTF8);

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => false;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.Disconnect;

        /// <summary>
        /// Gets or sets the disconnect reason.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string Reason {
            get => _reason;
            set => _reason = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Reads a <see cref="DisconnectPacket"/> from the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public static DisconnectPacket FromReader(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            return new DisconnectPacket {_reason = reader.ReadString()};
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Reason);
        }
    }
}
