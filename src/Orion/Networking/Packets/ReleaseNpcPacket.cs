using System.IO;
using Orion.Npcs;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to release an NPC.
    /// </summary>
    public sealed class ReleaseNpcPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC's X position.
        /// </summary>
        public int NpcX { get; set; }

        /// <summary>
        /// Gets or sets the NPC's Y position.
        /// </summary>
        public int NpcY { get; set; }

        /// <summary>
        /// Gets or sets the NPC type.
        /// </summary>
        public NpcType NpcType { get; set; }

        /// <summary>
        /// Gets or sets the NPC style.
        /// </summary>
        public byte NpcStyle { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcX = reader.ReadInt32();
            NpcY = reader.ReadInt32();
            NpcType = (NpcType)reader.ReadInt16();
            NpcStyle = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcX);
            writer.Write(NpcY);
            writer.Write((short)NpcType);
            writer.Write(NpcStyle);
        }
    }
}
