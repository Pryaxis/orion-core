namespace Orion.Networking.Packets {
    using System;
    using System.IO;
    using System.Text;
    using Orion.Networking.Packets.Extensions;

    /// <summary>
    /// Packet sent from the client to the server to request connection.
    /// </summary>
    public sealed class ConnectionRequestPacket : TerrariaPacket {
        private protected override int HeaderlessLength => Version.GetBinaryLength(Encoding.UTF8);

        /// <inheritdoc />
        public override bool IsSentToClient => false;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.ConnectionRequest;

        /// <summary>
        /// Gets the version, which is of the form $"Terraria{Main.curRelease}".
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionRequestPacket"/> with the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public ConnectionRequestPacket(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            Version = reader.ReadString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionRequestPacket"/> with the given version.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <exception cref="ArgumentNullException"><paramref name="version"/> is <c>null</c>.</exception>
        public ConnectionRequestPacket(string version) {
            Version = version ?? throw new ArgumentNullException(nameof(version));

            if (HeaderLength + HeaderlessLength > short.MaxValue) {
                throw new ArgumentOutOfRangeException(nameof(version), "Version string is too long.");
            }
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Version);
        }
    }
}
