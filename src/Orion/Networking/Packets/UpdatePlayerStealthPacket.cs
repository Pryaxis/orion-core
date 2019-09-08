using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update a player's stealth status.
    /// </summary>
    public sealed class UpdatePlayerStealthPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's stealth status.
        /// </summary>
        public float PlayerStealthStatus { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            PlayerStealthStatus = reader.ReadSingle();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerStealthStatus);
        }
    }
}
