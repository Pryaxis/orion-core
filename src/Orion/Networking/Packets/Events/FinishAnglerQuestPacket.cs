using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the client to the server to finish the angler quest.
    /// </summary>
    public sealed class FinishAnglerQuestPacket : Packet {
        private protected override PacketType Type => PacketType.FinishAnglerQuest;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.FinishAnglerQuest)}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) { }
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) { }
    }
}
