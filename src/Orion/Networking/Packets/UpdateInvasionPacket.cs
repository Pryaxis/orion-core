using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update an invasion.
    /// </summary>
    public sealed class UpdateInvasionPacket : Packet {
        /// <summary>
        /// Gets or sets the number of kills.
        /// </summary>
        public int NumberOfKills { get; set; }

        /// <summary>
        /// Gets or sets the number of kills to progress the wave.
        /// </summary>
        public int NumberOfKillsToProgress { get; set; }

        /// <summary>
        /// Gets or sets the invasion icon type.
        /// </summary>
        public int InvasionIconType { get; set; }

        /// <summary>
        /// Gets or sets the wave number.
        /// </summary>
        public int WaveNumber { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NumberOfKills = reader.ReadInt32();
            NumberOfKillsToProgress = reader.ReadInt32();
            InvasionIconType = reader.ReadInt32();
            WaveNumber = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NumberOfKills);
            writer.Write(NumberOfKillsToProgress);
            writer.Write(InvasionIconType);
            writer.Write(WaveNumber);
        }
    }
}
