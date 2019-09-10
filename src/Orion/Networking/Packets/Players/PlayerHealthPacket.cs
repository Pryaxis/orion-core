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
        /// Gets or sets the player's maximum health. The minimum that this can actually become is 100, and this is
        /// enforced client-side and server-side.
        /// </summary>
        public short PlayerMaxHealth { get; set; }

        private protected override PacketType Type => PacketType.PlayerHealth;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerHealth = reader.ReadInt16();
            PlayerMaxHealth = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerHealth);
            writer.Write(PlayerMaxHealth);
        }
    }
}
