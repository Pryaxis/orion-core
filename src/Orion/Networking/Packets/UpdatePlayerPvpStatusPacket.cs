using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet used to update a player's PvP status.
    /// </summary>
    public sealed class UpdatePlayerPvpStatusPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in PvP.
        /// </summary>
        public bool PlayerIsInPvp { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            PlayerIsInPvp = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerIsInPvp);
        }
    }
}
