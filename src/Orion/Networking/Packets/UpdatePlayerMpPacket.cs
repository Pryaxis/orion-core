using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update a player's MP and maximum MP.
    /// </summary>
    public sealed class UpdatePlayerMpPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the MP.
        /// </summary>
        public short Mp { get; set; }

        /// <summary>
        /// Gets or sets the maximum MP.
        /// </summary>
        public short MaxMp { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            Mp = reader.ReadInt16();
            MaxMp = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(Mp);
            writer.Write(MaxMp);
        }
    }
}
