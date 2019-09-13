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
using Orion.Npcs;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to set the kill count of an NPC type.
    /// </summary>
    public sealed class NpcKillCountPacket : Packet {
        /// <summary>
        /// Gets or sets the <see cref="Orion.Npcs.NpcType"/>.
        /// </summary>
        public NpcType NpcType { get; set; }

        /// <summary>
        /// Gets or sets the NPC kill count.
        /// </summary>
        public int NpcTypeKillCount { get; set; }

        internal override PacketType Type => PacketType.NpcKillCount;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{NpcType} x{NpcTypeKillCount}]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcType = (NpcType)reader.ReadInt16();
            NpcTypeKillCount = reader.ReadInt32();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((short)NpcType);
            writer.Write(NpcTypeKillCount);
        }
    }
}
