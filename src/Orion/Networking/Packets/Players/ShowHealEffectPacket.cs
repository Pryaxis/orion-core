using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to show a heal effect on a player.
    /// </summary>
    public sealed class ShowHealEffectPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the heal amount.
        /// </summary>
        public short HealAmount { get; set; }

        private protected override PacketType Type => PacketType.ShowHealEffect;

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            HealAmount = reader.ReadInt16();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(HealAmount);
        }
    }
}
