using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to damage an NPC with a player's selected item.
    /// </summary>
    public sealed class DamageNpcWithSelectedItemPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcIndex = reader.ReadInt16();
            PlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcIndex);
            writer.Write(PlayerIndex);
        }
    }
}
