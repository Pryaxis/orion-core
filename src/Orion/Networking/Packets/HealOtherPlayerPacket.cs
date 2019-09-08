using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to heal another player.
    /// </summary>
    public sealed class HealOtherPlayerPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the heal amount.
        /// </summary>
        public short HealAmount { get; set; }

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
