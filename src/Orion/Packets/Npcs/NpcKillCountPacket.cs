// Copyright (c) 2019 Pryaxis & Orion Contributors
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

using System.IO;
using Orion.Npcs;

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set the kill count of an NPC type.
    /// </summary>
    public sealed class NpcKillCountPacket : Packet {
        private NpcType _npcType;
        private int _killCount;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.NpcKillCount;

        /// <summary>
        /// Gets or sets the NPC type.
        /// </summary>
        /// <value>The NPC type.</value>
        /// <remarks>
        /// There appears to be a bug where Terraria rejects this packet if the NPC type is larger than <c>267</c>.
        /// </remarks>
        public NpcType NpcType {
            get => _npcType;
            set {
                _npcType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC type's kill count.
        /// </summary>
        /// <value>The NPC type's kill count.</value>
        public int KillCount {
            get => _killCount;
            set {
                _killCount = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcType = (NpcType)reader.ReadInt16();
            _killCount = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((short)_npcType);
            writer.Write(_killCount);
        }
    }
}
