using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to finish the connection.
    /// </summary>
    public sealed class FinishConnectionPacket : Packet {
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) { }
        private protected override void WriteToWriter(BinaryWriter writer) { }
    }
}
