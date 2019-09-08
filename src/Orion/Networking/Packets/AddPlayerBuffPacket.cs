using System.IO;
using Orion.Players;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to add a buff to a player.
    /// </summary>
    public sealed class AddPlayerBuffPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the buff type.
        /// </summary>
        public BuffType BuffType { get; set; }

        /// <summary>
        /// Gets or sets the buff time.
        /// </summary>
        public short BuffTime { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            BuffType = (BuffType)reader.ReadByte();
            BuffTime = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write((byte)BuffType);
            writer.Write(BuffTime);
        }
    }
}
