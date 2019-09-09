using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set the NPC that a player is talking to.
    /// </summary>
    public sealed class PlayerTalkingToNpcPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        private protected override PacketType Type => PacketType.PlayerTalkingToNpc;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            NpcIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(NpcIndex);
        }
    }
}
