using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent from the server to the client to set a player's status.
    /// </summary>
    public sealed class PlayerStatusPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is active.
        /// </summary>
        public bool IsPlayerActive { get; set; }

        private protected override PacketType Type => PacketType.PlayerStatus;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            IsPlayerActive = reader.ReadByte() == 1;
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(IsPlayerActive);
        }
    }
}
