using System.IO;
using Orion.Npcs;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to notify a client of NPC kills.
    /// </summary>
    public sealed class NotifyNpcKillPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC type.
        /// </summary>
        public NpcType NpcType { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) =>
            NpcType = (NpcType)reader.ReadInt16();

        private protected override void WriteToWriter(BinaryWriter writer) => writer.Write((short)NpcType);
    }
}
