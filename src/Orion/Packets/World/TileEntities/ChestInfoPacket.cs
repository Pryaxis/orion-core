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

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to set chest information.
    /// </summary>
    [PublicAPI]
    public sealed class ChestInfoPacket : Packet {
        private short _chestIndex;
        private short _chestX;
        private short _chestY;
        [CanBeNull] private string _chestName;

        /// <inheritdoc />
        public override PacketType Type => PacketType.ChestInfo;

        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        public short ChestIndex {
            get => _chestIndex;
            set {
                _chestIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        public short ChestX {
            get => _chestX;
            set {
                _chestX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        public short ChestY {
            get => _chestY;
            set {
                _chestY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest's name.
        /// </summary>
        [CanBeNull]
        public string ChestName {
            get => _chestName;
            set {
                _chestName = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={ChestIndex} @ ({ChestX}, {ChestY}) is {ChestName ?? "un-named"}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _chestIndex = reader.ReadInt16();
            _chestX = reader.ReadInt16();
            _chestY = reader.ReadInt16();
            var nameLength = reader.ReadByte();

            if (nameLength > 0 && nameLength <= Terraria.Chest.MaxNameLength) {
                _chestName = reader.ReadString();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_chestIndex);
            writer.Write(_chestX);
            writer.Write(_chestY);

            // This packet's logic is actually insane... why is it set up this way?
            if (_chestName is null) {
                writer.Write((byte)0);
            } else if (_chestName.Length == 0 || _chestName.Length > Terraria.Chest.MaxNameLength) {
                writer.Write(byte.MaxValue);
            } else {
                writer.Write((byte)_chestName.Length);
                writer.Write(_chestName);
            }
        }
    }
}
