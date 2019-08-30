namespace Orion.Networking.Packets {
    using System.IO;
    using System.Text;
    using Orion.Networking.Packets.Extensions;

    /// <summary>
    /// Packet sent from the server to disconnect the client.
    /// </summary>
    public sealed class DisconnectPacket : TerrariaPacket {
        /// <inheritdoc />
        private protected override short HeaderlessLength => (short)Reason.GetBinaryLength(Encoding.UTF8);

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => false;

        /// <summary>
        /// Gets the disconnect reason.
        /// </summary>
        public string Reason { get; }

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.Disconnect;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DisconnectPacket"/> with the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal DisconnectPacket(BinaryReader reader) {
            Reason = reader.ReadString();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Reason);
        }
    }
}
