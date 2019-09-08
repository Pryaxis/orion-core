using System.IO;
using Orion.Players;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to add a buff to an NPC.
    /// </summary>
    public sealed class AddNpcBuffPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the buff type.
        /// </summary>
        public BuffType BuffType { get; set; }

        /// <summary>
        /// Gets or sets the buff time.
        /// </summary>
        public short BuffTime { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcIndex = reader.ReadInt16();
            BuffType = (BuffType)reader.ReadByte();
            BuffTime = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcIndex);
            writer.Write((byte)BuffType);
            writer.Write(BuffTime);
        }
    }
}
