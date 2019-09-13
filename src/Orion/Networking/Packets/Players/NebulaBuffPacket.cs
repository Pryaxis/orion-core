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
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;
using Orion.Players;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to spread Nebula Armor buffs to nearby players.
    /// </summary>
    public sealed class NebulaBuffPacket : Packet {
        /// <summary>
        /// Gets or set the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the buff's <see cref="Orion.Players.BuffType"/>.
        /// </summary>
        public BuffType BuffType { get; set; }

        /// <summary>
        /// Gets or sets the buff's position.
        /// </summary>
        public Vector2 BuffPosition { get; set; }

        internal override PacketType Type => PacketType.NebulaBuff;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, {BuffType} at ({BuffPosition})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            BuffType = (BuffType)reader.ReadByte();
            BuffPosition = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write((byte)BuffType);
            writer.Write(BuffPosition);
        }
    }
}
