using System;
using System.IO;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to update an NPC's name.
    /// </summary>
    public sealed class NpcNamePacket : Packet {
        private string _npcName;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC name.
        /// </summary>
        public string NpcName {
            get => _npcName;
            set => _npcName = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override PacketType Type => PacketType.NpcName;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcIndex = reader.ReadInt16();
            NpcName = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcIndex);
            writer.Write(NpcName);
        }
    }
}
