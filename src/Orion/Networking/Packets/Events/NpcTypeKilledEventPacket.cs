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

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to notify that an NPC type was killed.
    /// </summary>
    public sealed class NpcTypeKilledEventPacket : Packet {
        /// <summary>
        /// Gets or sets the <see cref="NpcType"/> that was killed.
        /// </summary>
        public NpcType NpcTypeKilled { get; set; }

        internal override PacketType Type => PacketType.NpcTypeKilledEvent;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{NpcTypeKilled}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcTypeKilled = (NpcType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((short)NpcTypeKilled);
        }
    }
}
