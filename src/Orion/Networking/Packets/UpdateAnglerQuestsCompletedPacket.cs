using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update the number of angler quests completed.
    /// </summary>
    public sealed class UpdateAnglerQuestsCompletedPacket : Packet {
        /// <summary>
        /// Gets or sets the number of angler quests completed.
        /// </summary>
        public int NumberOfAnglerQuestsCompleted { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) =>
            NumberOfAnglerQuestsCompleted = reader.ReadInt32();

        private protected override void WriteToWriter(BinaryWriter writer) =>
            writer.Write(NumberOfAnglerQuestsCompleted);
    }
}
