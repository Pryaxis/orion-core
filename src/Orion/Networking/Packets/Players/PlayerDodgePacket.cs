using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to show a player dodge effect.
    /// </summary>
    public sealed class PlayerDodgePacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's dodge type.
        /// </summary>
        public PlayerDodgeType PlayerDodgeType {get; set; }

        private protected override PacketType Type => PacketType.PlayerDodge;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerDodgeType = (PlayerDodgeType)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write((byte)PlayerDodgeType);
        }
    }
}
