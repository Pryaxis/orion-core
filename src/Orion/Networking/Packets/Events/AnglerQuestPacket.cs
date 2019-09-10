using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to set the angler quest. This is sent when the client first connects
    /// and every dawn.
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

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            AnglerQuest = reader.ReadByte();
            IsAnglerQuestFinished = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(AnglerQuest);
            writer.Write(IsAnglerQuestFinished);
        }
    }
}
