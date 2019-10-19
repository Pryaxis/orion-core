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

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to perform a chest modification. See <see cref="ChestModification"/> for a list of chest
    /// modifications.
    /// </summary>
    public sealed class ChestModificationPacket : Packet {
        private ChestModification _modification;
        private short _x;
        private short _y;
        private short _style;
        private short _chestIndex;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ChestModification;

        /// <summary>
        /// Gets or sets the chest modification.
        /// </summary>
        /// <value>The chest modification.</value>
        public ChestModification Modification {
            get => _modification;
            set {
                _modification = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        /// <value>The chest's X coordinate.</value>
        public short X {
            get => _x;
            set {
                _x = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        /// <value>The chest's Y coordinate.</value>
        public short Y {
            get => _y;
            set {
                _y = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest's style.
        /// </summary>
        /// <value>The chest's style.</value>
        /// <remarks>
        /// This property is only applicable if the modification is related to placing some sort of container.
        /// </remarks>
        public short Style {
            get => _style;
            set {
                _style = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        /// <value>The chest index.</value>
        public short ChestIndex {
            get => _chestIndex;
            set {
                _chestIndex = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _modification = (ChestModification)reader.ReadByte();
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            _style = reader.ReadInt16();
            _chestIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)_modification);
            writer.Write(_x);
            writer.Write(_y);
            writer.Write(_style);
            writer.Write(_chestIndex);
        }
    }
}
