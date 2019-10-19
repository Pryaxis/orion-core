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

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent to set an NPC's home.
    /// </summary>
    public sealed class NpcHomePacket : Packet {
        private short _npcIndex;
        private short _homeX;
        private short _homeY;
        private bool _isHomeless;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.NpcHome;

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
        /// Gets or sets the NPC's home's X coordinate.
        /// </summary>
        /// <value>The NPC's home's X coordinate.</value>
        public short NpcHomeX {
            get => _homeX;
            set {
                _homeX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's home's Y coordinate.
        /// </summary>
        /// <value>The NPC's home's Y coordinate.</value>
        public short NpcHomeY {
            get => _homeY;
            set {
                _homeY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the NPC is homeless.
        /// </summary>
        /// <value><see langword="true"/> if the NPC is homeless; otherwise, <see langword="false"/>.</value>
        public bool IsNpcHomeless {
            get => _isHomeless;
            set {
                _isHomeless = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            _homeX = reader.ReadInt16();
            _homeY = reader.ReadInt16();
            _isHomeless = reader.ReadByte() == 1;
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);
            writer.Write(_homeX);
            writer.Write(_homeY);
            writer.Write((byte)(_isHomeless ? 1 : 2));
        }
    }
}
