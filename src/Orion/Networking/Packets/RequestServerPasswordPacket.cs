using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to request a password.
    /// </summary>
    public sealed class RequestServerPasswordPacket : Packet {
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) { }
        private protected override void WriteToWriter(BinaryWriter writer) { }
    }
}
