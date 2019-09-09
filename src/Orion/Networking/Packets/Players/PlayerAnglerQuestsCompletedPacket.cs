using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set the number of angler quests a player has completed.
    /// </summary>
    public sealed class PlayerAnglerQuestsCompletedPacket : Packet {
        /// <summary>
        /// Gets or sets the number of angler quests the player has completed.
        /// </summary>
        public int PlayerNumberOfAnglerQuestsCompleted { get; set; }

        private protected override PacketType Type => PacketType.PlayerAnglerQuestsCompleted;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerNumberOfAnglerQuestsCompleted = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerNumberOfAnglerQuestsCompleted);
        }
    }
}
