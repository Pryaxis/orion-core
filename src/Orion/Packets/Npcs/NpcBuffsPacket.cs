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

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using Orion.Entities;
using Orion.Utils;
using TerrariaNpc = Terraria.NPC;

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set an NPC's buffs.
    /// </summary>
    public sealed class NpcBuffsPacket : Packet {
        private short _npcIndex;
        private readonly DirtiableArray<Buff> _npcBuffs = new DirtiableArray<Buff>(TerrariaNpc.maxBuffs);

        /// <inheritdoc/>
        public override bool IsDirty => base.IsDirty || _npcBuffs.IsDirty;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.NpcBuffs;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex {
            get => _npcIndex;
            set {
                _npcIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets the NPC's buffs. The buff durations are limited to approximately 546.1 seconds.
        /// </summary>
        public IArray<Buff> NpcBuffs => _npcBuffs;

        /// <inheritdoc/>
        public override void Clean() {
            base.Clean();
            _npcBuffs.Clean();
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            for (var i = 0; i < _npcBuffs.Count; ++i) {
                _npcBuffs._array[i] = Buff.ReadFromReader(reader, 2);
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);
            foreach (var buff in _npcBuffs) {
                buff.WriteToWriter(writer, 2);
            }
        }
    }
}
