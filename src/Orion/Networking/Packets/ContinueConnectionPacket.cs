using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to continue the connection.
    /// </summary>
    public sealed class ContinueConnectionPacket : TerrariaPacket {
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

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerId = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerId);
        }
    }
}
