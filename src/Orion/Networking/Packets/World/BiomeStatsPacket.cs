using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to set the biome stats.
    /// </summary>
    public sealed class BiomeStatsPacket : Packet {
        /// <summary>
        /// Gets or sets the hallowed biome amount.
        /// </summary>
        public byte HallowedAmount { get; set; }

        /// <summary>
        /// Gets or sets the corruption biome amount.
        /// </summary>
        public byte CorruptionAmount { get; set; }

        /// <summary>
        /// Gets or sets the crimson biome amount.
        /// </summary>
        public byte CrimsonAmount { get; set; }

        private protected override PacketType Type => PacketType.BiomeStats;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[H={HallowedAmount} vs. (C={CorruptionAmount} or C'={CrimsonAmount})]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            HallowedAmount = reader.ReadByte();
            CorruptionAmount = reader.ReadByte();
            CrimsonAmount = reader.ReadByte();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(HallowedAmount);
            writer.Write(CorruptionAmount);
            writer.Write(CrimsonAmount);
        }
    }
}
