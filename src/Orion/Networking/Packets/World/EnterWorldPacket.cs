using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to enter the world. This is sent in response to a
    /// <see cref="RequestSectionPacket"/>.
    /// </summary>
    public sealed class EnterWorldPacket : Packet {
        private protected override PacketType Type => PacketType.EnterWorld;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.EnterWorld)}";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) { }
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) { }
    }
}
