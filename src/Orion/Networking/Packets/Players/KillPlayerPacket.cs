// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Networking.Packets.Extensions;
using Terraria.DataStructures;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to kill a player.
    /// </summary>
    public sealed class KillPlayerPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the reason for the player's death.
        /// </summary>
        public PlayerDeathReason PlayerDeathReason { get; set; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public short Damage { get; set; }

        /// <summary>
        /// Gets or sets the hit direction.
        /// </summary>
        public int HitDirection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the death is from PvP.
        /// </summary>
        public bool IsDeathFromPvp { get; set; }

        public override PacketType Type => PacketType.KillPlayer;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, ...]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerDeathReason = reader.ReadPlayerDeathReason();
            Damage = reader.ReadInt16();
            HitDirection = reader.ReadByte() - 1;
            IsDeathFromPvp = reader.ReadBoolean();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerDeathReason);
            writer.Write(Damage);
            writer.Write((byte)(HitDirection + 1));
            writer.Write(IsDeathFromPvp);
        }
    }
}
