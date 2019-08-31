namespace Orion.Networking.Packets {
    using System;
    using System.IO;

    /// <summary>
    /// Packet sent from the client to the server to finish the connection.
    /// </summary>
    public sealed class FinishConnectionPacket : TerrariaPacket {
        private protected override int HeaderlessLength => 0;

        /// <inheritdoc />
        public override bool IsSentToClient => false;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.FinishConnection;

        /// <summary>
        /// Reads a <see cref="FinishConnectionPacket"/> from the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public static FinishConnectionPacket FromReader(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            return new FinishConnectionPacket();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
        }
    }
}
