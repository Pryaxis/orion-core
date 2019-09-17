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

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to set the kill count of an NPC type.
    /// </summary>
    public sealed class NpcKillCountPacket : Packet {
        private NpcType _npcType = NpcType.None;
        private int _npcTypeKillCount;

        /// <summary>
        /// Gets or sets the NPC type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public NpcType NpcType {
            get => _npcType;
            set {
                _npcType = value ?? throw new ArgumentNullException(nameof(value));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC kill count.
        /// </summary>
        public int NpcTypeKillCount {
            get => _npcTypeKillCount;
            set {
                _npcTypeKillCount = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override PacketType Type => PacketType.NpcKillCount;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{NpcType} x{NpcTypeKillCount}]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcType = NpcType.FromId(reader.ReadInt16()) ?? throw new PacketException("NPC type is invalid.");
            _npcTypeKillCount = reader.ReadInt32();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcType.Id);
            writer.Write(NpcTypeKillCount);
        }
    }
}
