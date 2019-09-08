using System.IO;
using Orion.Players;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update a player's team.
    /// </summary>
    public sealed class UpdatePlayerTeamPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's team.
        /// </summary>
        public PlayerTeam PlayerTeam { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            PlayerTeam = (PlayerTeam)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write((byte)PlayerTeam);
        }
    }
}
