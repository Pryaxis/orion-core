namespace Orion.Networking.Packets {
    using System.IO;
    using System.Text;
    using Orion.Networking.Packets.Extensions;

    /// <summary>
    /// Packet sent from the client to the server to request connection.
    /// </summary>
    public sealed class ConnectionRequestPacket : TerrariaPacket {
        private protected override short HeaderlessLength => (short)Version.GetBinaryLength(Encoding.UTF8);

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
        internal ConnectionRequestPacket(BinaryReader reader) {
            Version = reader.ReadString();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Version);
        }
    }
}
