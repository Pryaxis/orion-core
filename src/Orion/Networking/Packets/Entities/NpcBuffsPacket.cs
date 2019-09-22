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
using JetBrains.Annotations;
using Orion.Entities;
using Orion.Utils;

namespace Orion.Networking.Packets.Entities {
    /// <summary>
    /// Packet sent from the server to the client to set an NPC's buffs.
    /// </summary>
    [PublicAPI]
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
        [NotNull]
        public DirtiableArray<Buff> NpcBuffs { get; } = new DirtiableArray<Buff>(Terraria.NPC.maxBuffs);

        /// <inheritdoc />
        public override void Clean() {
            base.Clean();
            NpcBuffs.Clean();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            for (var i = 0; i < NpcBuffs.Count; ++i) {
                NpcBuffs._array[i] = new Buff((BuffType)reader.ReadByte(),
                                              TimeSpan.FromSeconds(reader.ReadInt16() / 60.0));
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);
            foreach (var buff in NpcBuffs) {
                writer.Write((byte)buff.BuffType);

                var ticks = (int)(buff.Duration.TotalSeconds * 60.0);
                if (ticks >= short.MaxValue) {
                    writer.Write(short.MaxValue);
                } else {
                    writer.Write((short)ticks);
                }
            }
        }
    }
}
