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

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Players;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to add a buff to a player.
    /// </summary>
    public sealed class BuffPlayer : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's buff.
        /// </summary>
        public Buff PlayerBuff { get; set; }

        internal override PacketType Type => PacketType.BuffPlayer;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, {PlayerBuff}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerBuff = new Buff((BuffType)reader.ReadByte(), TimeSpan.FromSeconds(reader.ReadInt32() / 60.0));
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write((byte)PlayerBuff.BuffType);
            writer.Write((int)(PlayerBuff.Duration.TotalSeconds * 60.0));
        }
    }
}
