using System.IO;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to set an NPC's home.
    /// </summary>
    public sealed class NpcHomePacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC home's X coordinate.
        /// </summary>
        public short NpcHomeX { get; set; }
        
        /// <summary>
        /// Gets or sets the NPC home's Y coordinate.
        /// </summary>
        public short NpcHomeY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the NPC is homeless.
        /// </summary>
        public bool IsNpcHomeless { get; set; }

        private protected override PacketType Type => PacketType.NpcHome;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcIndex = reader.ReadInt16();
            NpcHomeX = reader.ReadInt16();
            NpcHomeY = reader.ReadInt16();
            IsNpcHomeless = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcIndex);
            writer.Write(NpcHomeX);
            writer.Write(NpcHomeY);
            writer.Write(IsNpcHomeless);
        }
    }
}
