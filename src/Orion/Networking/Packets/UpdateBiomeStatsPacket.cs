using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to update the biome stats.
    /// </summary>
    public sealed class UpdateBiomeStatsPacket : Packet {
        /// <summary>
        /// Gets or sets the "good" biome amount.
        /// </summary>
        public byte GoodAmount { get; set; }

        /// <summary>
        /// Gets or sets the corruption amount.
        /// </summary>
        public byte CorruptionAmount { get; set; }

        /// <summary>
        /// Gets or sets the crimson amount.
        /// </summary>
        public byte CrimsonAmount { get; set; }

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            GoodAmount = reader.ReadByte();
            CorruptionAmount = reader.ReadByte();
            CrimsonAmount = reader.ReadByte();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(GoodAmount);
            writer.Write(CorruptionAmount);
            writer.Write(CrimsonAmount);
        }
    }
}
