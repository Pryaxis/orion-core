using System.IO;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to damage an NPC with a player's selected item.
    /// </summary>
    public sealed class DamageNpcWithItemPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the damager's player index.
        /// </summary>
        public byte DamagerPlayerIndex { get; set; }

        private protected override PacketType Type => PacketType.DamageNpcWithItem;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcIndex = reader.ReadInt16();
            DamagerPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcIndex);
            writer.Write(DamagerPlayerIndex);
        }
    }
}
