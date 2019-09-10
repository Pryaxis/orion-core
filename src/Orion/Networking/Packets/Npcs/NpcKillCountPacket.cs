using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Npcs;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to set the kill count of an NPC type.
    /// </summary>
    public sealed class NpcKillCountPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC type.
        /// </summary>
        public NpcType NpcType { get; set; }

        /// <summary>
        /// Gets or sets the NPC kill count.
        /// </summary>
        public int NpcTypeKillCount { get; set; }

        private protected override PacketType Type => PacketType.NpcKillCount;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{NpcType} x{NpcTypeKillCount}]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcType = (NpcType)reader.ReadInt16();
            NpcTypeKillCount = reader.ReadInt32();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((short)NpcType);
            writer.Write(NpcTypeKillCount);
        }
    }
}
