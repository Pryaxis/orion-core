using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Players;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set an NPC's buffs.
    /// </summary>
    public sealed class NpcBuffsPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets the buffs.
        /// </summary>
        public Buff[] NpcBuffs { get; } = new Buff[Terraria.NPC.maxBuffs];

        private protected override PacketType Type => PacketType.NpcBuffs;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcIndex = reader.ReadInt16();
            for (var i = 0; i < NpcBuffs.Length; ++i) {
                NpcBuffs[i] = new Buff((BuffType)reader.ReadByte(), TimeSpan.FromSeconds(reader.ReadInt16() / 60.0));
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);
            foreach (var buff in NpcBuffs) {
                writer.Write((byte)buff.BuffType);

                var ticks = (int)(buff.Duration.TotalSeconds * 60.0);
                if (ticks >= short.MaxValue) {
                    writer.Write(short.MaxValue);
                } else {
                    writer.Write((short)ticks);
                }
            }
        }
    }
}
