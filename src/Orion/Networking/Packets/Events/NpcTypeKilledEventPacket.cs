using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Npcs;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to notify that an NPC type was killed.
    /// </summary>
    public sealed class NpcTypeKilledEventPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC type that was killed.
        /// </summary>
        public NpcType NpcTypeKilled { get; set; }

        private protected override PacketType Type => PacketType.NpcTypeKilledEvent;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{NpcTypeKilled}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcTypeKilled = (NpcType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((short)NpcTypeKilled);
        }
    }
}
