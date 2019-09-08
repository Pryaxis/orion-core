using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update a player's HP and maximum HP.
    /// </summary>
    public sealed class UpdatePlayerHpPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the HP.
        /// </summary>
        public short Hp { get; set; }

        /// <summary>
        /// Gets or sets the maximum HP.
        /// </summary>
        public short MaxHp { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            Hp = reader.ReadInt16();
            MaxHp = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(Hp);
            writer.Write(MaxHp);
        }
    }
}
