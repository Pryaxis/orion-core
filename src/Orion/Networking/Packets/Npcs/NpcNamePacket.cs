using System.IO;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to either request or set an NPC's name.
    /// </summary>
    public sealed class NpcNamePacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC name. A value of <c>null</c> indicates a request for the name.
        /// </summary>
        public string NpcName { get; set; }

        private protected override PacketType Type => PacketType.NpcName;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcIndex = reader.ReadInt16();

            // This packet is written differently depending on who sent the packet. So we need to manually check the
            // packet length here.
            if (packetLength > HeaderLength + sizeof(short)) {
                NpcName = reader.ReadString();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcIndex);
            if (NpcName != null) {
                writer.Write(NpcName);
            }
        }
    }
}
