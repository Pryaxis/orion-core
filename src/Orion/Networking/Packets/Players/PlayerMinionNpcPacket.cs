using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's minion's target NPC.
    /// </summary>
    public sealed class PlayerMinionNpcPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's minions' target NPC index.
        /// </summary>
        public short PlayerMinionTargetNpcIndex { get; set; }

        private protected override PacketType Type => PacketType.PlayerMinionNpc;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerMinionTargetNpcIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerMinionTargetNpcIndex);
        }
    }
}
