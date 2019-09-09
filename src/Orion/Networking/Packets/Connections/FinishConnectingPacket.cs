using System.IO;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the client to the server to finish connecting. This is sent in response to a
    /// <see cref="ContinueConnectingPacket"/>
    /// </summary>
    public sealed class FinishConnectingPacket : Packet {
        private protected override PacketType Type => PacketType.FinishConnecting;
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) { }
        private protected override void WriteToWriter(BinaryWriter writer) { }
    }
}
