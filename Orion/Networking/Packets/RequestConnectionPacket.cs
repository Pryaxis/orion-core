namespace Orion.Networking.Packets {
    using System;
    using System.IO;
    using System.Text;
    using Orion.Networking.Packets.Extensions;

    /// <summary>
    /// Packet sent from the client to the server to request connection.
    /// </summary>
    public sealed class RequestConnectionPacket : TerrariaPacket {
        private string _version = "";

        private protected override int HeaderlessLength => Version.GetBinaryLength(Encoding.UTF8);

        /// <inheritdoc />
        public override bool IsSentToClient => false;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.RequestConnection;

        /// <summary>
        /// Gets or sets the version, which is of the form $"Terraria{Main.curRelease}".
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string Version {
            get => _version;
            set => _version = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Reads a <see cref="RequestConnectionPacket"/> from the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public static RequestConnectionPacket FromReader(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            return new RequestConnectionPacket {_version = reader.ReadString()};
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Version);
        }
    }
}
