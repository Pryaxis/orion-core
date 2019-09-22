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
using Orion.Entities;

namespace Orion.Networking.Packets.Entities {
    /// <summary>
    /// Packet sent to add a buff to an NPC.
    /// </summary>
    public sealed class BuffNpcPacket : Packet {
        private short _npcIndex;
        private Buff _npcBuff;

        /// <inheritdoc />
        public override PacketType Type => PacketType.BuffNpc;

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
        /// Gets or sets the NPC's buff. The buff duration is limited to approximately 546.1 seconds.
        /// </summary>
        public Buff NpcBuff {
            get => _npcBuff;
            set {
                _npcBuff = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex}, {NpcBuff}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcIndex = reader.ReadInt16();
            NpcBuff = new Buff((BuffType)reader.ReadByte(), TimeSpan.FromSeconds(reader.ReadInt16() / 60.0));
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);
            writer.Write((byte)NpcBuff.BuffType);

            var ticks = (int)(NpcBuff.Duration.TotalSeconds * 60.0);
            if (ticks >= short.MaxValue) {
                writer.Write(short.MaxValue);
            } else {
                writer.Write((short)ticks);
            }
        }
    }
}
