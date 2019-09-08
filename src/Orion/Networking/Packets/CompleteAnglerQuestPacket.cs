using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to complete the angler quest.
    /// </summary>
    public sealed class CompleteAnglerQuestPacket : Packet {
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) { }
        private protected override void WriteToWriter(BinaryWriter writer) { }
    }
}
