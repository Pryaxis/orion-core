using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to heal a player.
    /// </summary>
    public sealed class HealPlayerPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the heal amount.
        /// </summary>
        public short HealAmount { get; set; }

        private protected override PacketType Type => PacketType.HealPlayer;

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            HealAmount = reader.ReadInt16();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(HealAmount);
        }
    }
}
