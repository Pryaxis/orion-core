using System.IO;
using Orion.Npcs;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the update the number of times an NPC type has been killed.
    /// </summary>
    public sealed class UpdateNpcKillsPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC type.
        /// </summary>
        public NpcType NpcType { get; set; }

        /// <summary>
        /// Gets or sets the NPC kill count.
        /// </summary>
        public int NpcTypeKillCount { get; set; }

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcType = (NpcType)reader.ReadInt16();
            NpcTypeKillCount = reader.ReadInt32();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write((short)NpcType);
            writer.Write(NpcTypeKillCount);
        }
    }
}
