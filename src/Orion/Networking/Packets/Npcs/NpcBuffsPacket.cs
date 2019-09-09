using System.IO;
using Orion.Players;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set an NPC's buffs. Each buff will be set for one second.
    /// </summary>
    public sealed class NpcBuffsPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets the buff types.
        /// </summary>
        public BuffType[] NpcBuffs { get; } = new BuffType[Terraria.NPC.maxBuffs];

        private protected override PacketType Type => PacketType.NpcBuffs;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcIndex = reader.ReadInt16();
            for (var i = 0; i < NpcBuffs.Length; ++i) {
                NpcBuffs[i] = (BuffType)reader.ReadByte();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcIndex);
            foreach (var buffType in NpcBuffs) {
                writer.Write((byte)buffType);
            }
        }
    }
}
