using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to set invasion information.
    /// </summary>
    public sealed class InvasionInfoPacket : Packet {
        /// <summary>
        /// Gets or sets the number of kills in the current wave.
        /// </summary>
        public int NumberOfKills { get; set; }

        /// <summary>
        /// Gets or sets the number of kills to progress the current wave.
        /// </summary>
        public int NumberOfKillsToProgress { get; set; }

        /// <summary>
        /// Gets or sets the invasion icon type.
        /// </summary>
        // TODO: implement enum for this.
        public int InvasionIconType { get; set; }

        /// <summary>
        /// Gets or sets the wave number.
        /// </summary>
        public int InvasionWaveNumber { get; set; }

        private protected override PacketType Type => PacketType.InvasionInfo;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[{NumberOfKills}/{NumberOfKillsToProgress}, " +
            $"T={InvasionIconType}, #={InvasionWaveNumber}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NumberOfKills = reader.ReadInt32();
            NumberOfKillsToProgress = reader.ReadInt32();
            InvasionIconType = reader.ReadInt32();
            InvasionWaveNumber = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NumberOfKills);
            writer.Write(NumberOfKillsToProgress);
            writer.Write(InvasionIconType);
            writer.Write(InvasionWaveNumber);
        }
    }
}
