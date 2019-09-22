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

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to show a tree growing effect.
    /// </summary>
    [PublicAPI]
    public sealed class TreeGrowingEffectPacket : Packet {
        private byte _data;
        private short _treeX;
        private short _treeY;
        private byte _treeHeight;
        private short _treeType;

        /// <inheritdoc />
        public override PacketType Type => PacketType.TreeGrowingEffect;

        /// <summary>
        /// Gets or sets the tree's X coordinate.
        /// </summary>
        public short TreeX {
            get => _treeX;
            set {
                _treeX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tree's Y coordinate.
        /// </summary>
        public short TreeY {
            get => _treeY;
            set {
                _treeY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tree height.
        /// </summary>
        public byte TreeHeight {
            get => _treeHeight;
            set {
                _treeHeight = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tree type.
        /// </summary>
        public short TreeType {
            get => _treeType;
            set {
                _treeType = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[({TreeX}, {TreeY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            // This byte is basically useless, but we'll keep track of it anyways.
            _data = reader.ReadByte();
            _treeX = reader.ReadInt16();
            _treeY = reader.ReadInt16();
            _treeHeight = reader.ReadByte();
            _treeType = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_data);
            writer.Write(_treeX);
            writer.Write(_treeY);
            writer.Write(_treeHeight);
            writer.Write(_treeType);
        }
    }
}
