using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the client to the server to finish connecting. This is sent in response to a
    /// <see cref="ContinueConnectingPacket"/>
    /// </summary>
    public sealed class FinishConnectingPacket : Packet {
        private protected override PacketType Type => PacketType.FinishConnecting;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.FinishConnecting)}";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) { }
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) { }
    }
}
