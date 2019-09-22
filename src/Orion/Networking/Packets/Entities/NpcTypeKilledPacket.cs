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
using JetBrains.Annotations;
using Orion.Entities;

namespace Orion.Networking.Packets.Entities {
    /// <summary>
    /// Packet sent from the server to the client to notify that an NPC type was killed. This is used for achievements.
    /// </summary>
    [PublicAPI]
    public sealed class NpcTypeKilledPacket : Packet {
        private NpcType _npcTypeKilled;

        /// <inheritdoc />
        public override PacketType Type => PacketType.NpcTypeKilled;

        /// <summary>
        /// Gets or sets the NPC type that was killed.
        /// </summary>
        public NpcType NpcTypeKilled {
            get => _npcTypeKilled;
            set {
                _npcTypeKilled = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{NpcTypeKilled}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcTypeKilled = (NpcType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((short)_npcTypeKilled);
        }
    }
}
