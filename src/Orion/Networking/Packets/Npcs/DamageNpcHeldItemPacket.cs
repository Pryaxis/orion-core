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

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to damage an NPC with a player's held item.
    /// </summary>
    public sealed class DamageNpcHeldItemPacket : Packet {
        private short _npcIndex;
        private byte _damagerPlayerIndex;

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
        /// Gets or sets the damager's player index.
        /// </summary>
        public byte DamagerPlayerIndex {
            get => _damagerPlayerIndex;
            set {
                _damagerPlayerIndex = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override PacketType Type => PacketType.DamageNpcHeldItem;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex} by P={DamagerPlayerIndex}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            _damagerPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);
            writer.Write(DamagerPlayerIndex);
        }
    }
}
