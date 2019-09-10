using System.IO;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to catch an NPC.
    /// </summary>
    public sealed class CatchNpcPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC catcher's player index.
        /// </summary>
        public byte NpcCatcherPlayerIndex { get; set; }

        private protected override PacketType Type => PacketType.CatchNpc;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcIndex = reader.ReadInt16();
            NpcCatcherPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);
            writer.Write(NpcCatcherPlayerIndex);
        }
    }
}
