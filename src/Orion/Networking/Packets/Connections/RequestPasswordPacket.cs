using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the server to the client to request a password. This may be sent in response to a
    /// <see cref="StartConnectingPacket"/>.
    /// </summary>
    public sealed class RequestPasswordPacket : Packet {
        private protected override PacketType Type => PacketType.RequestPassword;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) { }
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) { }
    }
}
