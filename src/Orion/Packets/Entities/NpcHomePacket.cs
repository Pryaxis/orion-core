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

namespace Orion.Packets.Entities {
    /// <summary>
    /// Packet sent to set an NPC's home.
    /// </summary>
    [PublicAPI]
    public sealed class NpcHomePacket : Packet {
        private short _npcIndex;
        private short _npcHomeX;
        private short _npcHomeY;
        private bool _isNpcHomeless;

        /// <inheritdoc />
        public override PacketType Type => PacketType.NpcHome;

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
        /// Gets or sets the NPC home's X coordinate.
        /// </summary>
        public short NpcHomeX {
            get => _npcHomeX;
            set {
                _npcHomeX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC home's Y coordinate.
        /// </summary>
        public short NpcHomeY {
            get => _npcHomeY;
            set {
                _npcHomeY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the NPC is homeless.
        /// </summary>
        public bool IsNpcHomeless {
            get => _isNpcHomeless;
            set {
                _isNpcHomeless = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex} @ ({NpcHomeX}, {NpcHomeY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            _npcHomeX = reader.ReadInt16();
            _npcHomeY = reader.ReadInt16();
            _isNpcHomeless = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);
            writer.Write(_npcHomeX);
            writer.Write(_npcHomeY);
            writer.Write(_isNpcHomeless);
        }
    }
}
