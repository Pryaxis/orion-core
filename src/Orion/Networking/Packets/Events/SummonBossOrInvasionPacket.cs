using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent to summon a boss or an invasion.
    /// </summary>
    public sealed class SummonBossOrInvasionPacket : Packet {
        /// <summary>
        /// Gets or sets the summoner's player index.
        /// </summary>
        public short SummonerPlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the boss or invasion type.
        /// </summary>
        public short BossOrInvasionType { get; set; }

        private protected override PacketType Type => PacketType.SummonBossOrInvasion;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            SummonerPlayerIndex = reader.ReadInt16();
            BossOrInvasionType = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(SummonerPlayerIndex);
            writer.Write(BossOrInvasionType);
        }
    }
}
