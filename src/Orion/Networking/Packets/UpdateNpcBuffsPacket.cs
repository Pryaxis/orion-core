using System.IO;
using Orion.Players;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to update an NPC's buffs.
    /// </summary>
    public sealed class UpdateNpcBuffsPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets the buff types.
        /// </summary>
        public BuffType[] NpcBuffs { get; } = new BuffType[Terraria.NPC.maxBuffs];

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
