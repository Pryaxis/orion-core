using System;
using System.IO;
using Orion.Players;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to add a buff to a npc.
    /// </summary>
    public sealed class AddBuffToNpcPacket : Packet {
        /// <summary>
        /// Gets or sets the npc index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the buff. The buff duration is limited to approximately 546.1 seconds.
        /// </summary>
        public Buff Buff { get; set; }

        private protected override PacketType Type => PacketType.AddBuffToNpc;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcIndex = reader.ReadInt16();
            Buff = new Buff((BuffType)reader.ReadByte(), TimeSpan.FromSeconds(reader.ReadInt16() / 60.0));
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);
            writer.Write((byte)Buff.BuffType);

            var ticks = (int)(Buff.Duration.TotalSeconds * 60.0);
            if (ticks >= short.MaxValue) {
                writer.Write(short.MaxValue);
            } else {
                writer.Write((short)ticks);
            }
        }
    }
}
