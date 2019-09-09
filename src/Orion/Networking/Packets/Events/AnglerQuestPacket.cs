using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent from the server to the client to set the angler quest.
    /// </summary>
    public sealed class AnglerQuestPacket : Packet {
        /// <summary>
        /// Gets or sets the angler quest.
        /// </summary>
        public byte AnglerQuest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the angler quest is finished.
        /// </summary>
        public bool IsAnglerQuestFinished { get; set; }

        private protected override PacketType Type => PacketType.AnglerQuest;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            AnglerQuest = reader.ReadByte();
            IsAnglerQuestFinished = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(AnglerQuest);
            writer.Write(IsAnglerQuestFinished);
        }
    }
}
