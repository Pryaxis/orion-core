using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's health and maximum health.
    /// </summary>
    public sealed class PlayerHealthPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's health.
        /// </summary>
        public short PlayerHealth { get; set; }

        /// <summary>
        /// Gets or sets the player's maximum health.
        /// </summary>
        public short PlayerMaxHealth { get; set; }

        private protected override PacketType Type => PacketType.PlayerHealth;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            PlayerHealth = reader.ReadInt16();
            PlayerMaxHealth = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerHealth);
            writer.Write(PlayerMaxHealth);
        }
    }
}
