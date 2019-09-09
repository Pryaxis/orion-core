using System.IO;
using Orion.Npcs;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to notify an NPC kill event.
    /// </summary>
    public sealed class NpcKilledEventPacket : Packet {
        /// <summary>
        /// Gets or sets the killed NPC type.
        /// </summary>
        public NpcType KilledNpcType { get; set; }

        private protected override PacketType Type => PacketType.NpcKilledEvent;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            KilledNpcType = (NpcType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write((short)KilledNpcType);
        }
    }
}
