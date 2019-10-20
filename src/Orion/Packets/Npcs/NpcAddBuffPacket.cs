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
using Orion.Entities;

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent to add a buff to an NPC.
    /// </summary>
    /// <remarks>This packet is sent when a player debuffs an NPC.</remarks>
    public sealed class NpcAddBuffPacket : Packet {
        private short _npcIndex;
        private Buff _buff;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.NpcAddBuff;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        /// <value>The NPC index.</value>
        public short NpcIndex {
            get => _npcIndex;
            set {
                _npcIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's buff. The buff duration is limited to approximately 546.1 seconds.
        /// </summary>
        /// <value>The NPC's buff.</value>
        public Buff Buff {
            get => _buff;
            set {
                _buff = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            _buff = Buff.ReadFromReader(reader, 2);
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);
            _buff.WriteToWriter(writer, 2);
        }
    }
}
