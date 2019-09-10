using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's mana and maximum mana.
    /// </summary>
    public sealed class PlayerManaPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's mana.
        /// </summary>
        public short PlayerMana { get; set; }

        /// <summary>
        /// Gets or sets the player's maximum mana.
        /// </summary>
        public short PlayerMaxMana { get; set; }

        private protected override PacketType Type => PacketType.PlayerMana;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerMana = reader.ReadInt16();
            PlayerMaxMana = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerMana);
            writer.Write(PlayerMaxMana);
        }
    }
}
