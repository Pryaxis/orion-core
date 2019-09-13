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
using Terraria;
using Terraria.DataStructures;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to damage a player.
    /// </summary>
    public sealed class DamagePlayerPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the reason for the player's (potential) death.
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
        /// Gets or sets the hit cooldown.
        /// </summary>
        public int HitCooldown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is critical.
        /// </summary>
        public bool IsHitCritical { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is from PvP.
        /// </summary>
        public bool IsHitFromPvp { get; set; }

        internal override PacketType Type => PacketType.DamagePlayer;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} by {Damage}, ...]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerDeathReason = reader.ReadPlayerDeathReason();
            Damage = reader.ReadInt16();
            HitDirection = reader.ReadByte() - 1;
            BitsByte flags = reader.ReadByte();
            IsHitCritical = flags[0];
            IsHitFromPvp = flags[1];
            HitCooldown = reader.ReadSByte();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerDeathReason);
            writer.Write(Damage);
            writer.Write((byte)(HitDirection + 1));
            BitsByte flags = 0;
            flags[0] = IsHitCritical;
            flags[1] = IsHitFromPvp;
            writer.Write(flags);
            writer.Write((sbyte)HitCooldown);
        }
    }
}
