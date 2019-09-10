using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to set the angler quest.
    /// </summary>
    public sealed class AnglerQuestPacket : Packet {
        /// <summary>
        /// Gets or sets the angler quest.
        /// </summary>
        // TODO: implement enum for this.
        public byte CurrentAnglerQuest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the angler quest is finished.
        /// </summary>
        public bool IsAnglerQuestFinished { get; set; }

        private protected override PacketType Type => PacketType.AnglerQuest;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[{CurrentAnglerQuest}, F={IsAnglerQuestFinished}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            CurrentAnglerQuest = reader.ReadByte();
            IsAnglerQuestFinished = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(CurrentAnglerQuest);
            writer.Write(IsAnglerQuestFinished);
        }
    }
}
