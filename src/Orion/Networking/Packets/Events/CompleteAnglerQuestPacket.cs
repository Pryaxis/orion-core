using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the client to the server to complete the angler quest.
    /// </summary>
    public sealed class CompleteAnglerQuestPacket : Packet {
        private protected override PacketType Type => PacketType.CompleteAnglerQuest;
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) { }
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) { }
    }
}
