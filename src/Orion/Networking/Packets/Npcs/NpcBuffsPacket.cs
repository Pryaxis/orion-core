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
using Orion.Events;
using Orion.Utils;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set an NPC's buffs.
    /// </summary>
    public sealed class NpcBuffsPacket : Packet {
        private short _npcIndex;

        /// <inheritdoc />
        public override bool IsDirty => base.IsDirty || NpcBuffs.IsDirty;

        /// <inheritdoc />
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
        /// Gets the NPC's buffs.
        /// </summary>
        public Buffs NpcBuffs { get; } = new Buffs();

        /// <inheritdoc />
        public override void Clean() {
            base.Clean();
            NpcBuffs.Clean();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcIndex = reader.ReadInt16();
            for (var i = 0; i < NpcBuffs.Count; ++i) {
                NpcBuffs[i] =
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

        /// <summary>
        /// Represents the buffs in an <see cref="NpcBuffsPacket"/>.
        /// </summary>
        public sealed class Buffs : IArray<Buff>, IDirtiable {
            private readonly Buff[] _buffs = new Buff[Terraria.NPC.maxBuffs];

            /// <inheritdoc cref="IArray{T}.this" />
            public Buff this[int index] {
                get => _buffs[index];
                set {
                    _buffs[index] = value ?? throw new ArgumentNullException(nameof(value));
                    IsDirty = true;
                }
            }

            /// <inheritdoc />
            public int Count => _buffs.Length;

            /// <inheritdoc />
            public bool IsDirty { get; private set; }

            internal Buffs() {
                for (var i = 0; i < _buffs.Length; ++i) {
                    _buffs[i] = new Buff(BuffType.None, TimeSpan.Zero);
                }
            }

            /// <inheritdoc />
            public IEnumerator<Buff> GetEnumerator() => ((IEnumerable<Buff>)_buffs).GetEnumerator();

            [ExcludeFromCodeCoverage]
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            /// <inheritdoc />
            public void Clean() {
                IsDirty = false;
            }
        }
    }
}
