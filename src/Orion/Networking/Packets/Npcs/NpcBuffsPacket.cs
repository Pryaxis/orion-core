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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Entities;
using Orion.Utils;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set an NPC's buffs.
    /// </summary>
    public sealed class NpcBuffsPacket : Packet {
        private readonly Buff[] _npcBuffs = new Buff[Terraria.NPC.maxBuffs];
        private short _npcIndex;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex {
            get => _npcIndex;
            set {
                _npcIndex = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets the NPC's buffs.
        /// </summary>
        public IArray<Buff> NpcBuffs { get; }

        /// <inheritdoc />
        public override PacketType Type => PacketType.NpcBuffs;

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcBuffsPacket"/> class.
        /// </summary>
        public NpcBuffsPacket() {
            for (var i = 0; i < _npcBuffs.Length; ++i) {
                _npcBuffs[i] = new Buff(BuffType.None, TimeSpan.Zero);
            }

            NpcBuffs = new BuffArray(this);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            for (var i = 0; i < _npcBuffs.Length; ++i) {
                _npcBuffs[i] =
                    new Buff(BuffType.FromId(reader.ReadByte()) ?? throw new PacketException("Buff type is invalid."),
                             TimeSpan.FromSeconds(reader.ReadInt16() / 60.0));
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);
            foreach (var buff in NpcBuffs) {
                writer.Write(buff.BuffType.Id);

                var ticks = (int)(buff.Duration.TotalSeconds * 60.0);
                if (ticks >= short.MaxValue) {
                    writer.Write(short.MaxValue);
                } else {
                    writer.Write((short)ticks);
                }
            }
        }

        private class BuffArray : IArray<Buff> {
            private readonly NpcBuffsPacket _packet;

            public Buff this[int index] {
                get => _packet._npcBuffs[index];
                set {
                    _packet._npcBuffs[index] = value ?? throw new ArgumentNullException(nameof(value));
                    _packet.IsDirty = true;
                }
            }

            public int Count => _packet._npcBuffs.Length;

            public BuffArray(NpcBuffsPacket packet) {
                _packet = packet;
            }

            public IEnumerator<Buff> GetEnumerator() => ((IEnumerable<Buff>)_packet._npcBuffs).GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
