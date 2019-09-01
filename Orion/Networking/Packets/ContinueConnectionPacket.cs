using System;
using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to continue the connection.
    /// </summary>
    public sealed class ContinueConnectionPacket : TerrariaPacket {
        private protected override int HeaderlessLength => 1;

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => false;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.ContinueConnection;

        /// <summary>
        /// Gets or sets the player ID.
        /// </summary>
        public byte PlayerId { get; set; }

        /// <summary>
        /// Reads a <see cref="ContinueConnectionPacket"/> from the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public static ContinueConnectionPacket FromReader(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            return new ContinueConnectionPacket {PlayerId = reader.ReadByte()};
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerId);
        }
    }
}
