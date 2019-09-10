﻿using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet used to set a player's PvP status.
    /// </summary>
    public sealed class PlayerPvpPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in PvP.
        /// </summary>
        public bool PlayerIsInPvp { get; set; }

        private protected override PacketType Type => PacketType.PlayerPvp;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerIsInPvp = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerIsInPvp);
        }
    }
}
