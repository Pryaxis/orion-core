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

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to set sign information. This is sent in response to a <see cref="SignReadPacket"/>.
    /// </summary>
    public sealed class SignInfoPacket : Packet {
        private short _signIndex;
        private short _signX;
        private short _signY;
        private string _signText = "";
        private byte _modifierPlayerIndex;

        /// <inheritdoc />
        public override PacketType Type => PacketType.SignInfo;

        /// <summary>
        /// Gets or sets the sign's index.
        /// </summary>
        public short SignIndex {
            get => _signIndex;
            set {
                _signIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the sign's X coordinate.
        /// </summary>
        public short SignX {
            get => _signX;
            set {
                _signX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the sign's Y coordinate.
        /// </summary>
        public short SignY {
            get => _signY;
            set {
                _signY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the sign's text.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string SignText {
            get => _signText;
            set {
                _signText = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the modifier's player index.
        /// </summary>
        public byte ModifierPlayerIndex {
            get => _modifierPlayerIndex;
            set {
                _modifierPlayerIndex = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={SignIndex} @ ({SignX}, {SignY}): \"{SignText}\"]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _signIndex = reader.ReadInt16();
            _signX = reader.ReadInt16();
            _signY = reader.ReadInt16();
            _signText = reader.ReadString();
            _modifierPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_signIndex);
            writer.Write(_signX);
            writer.Write(_signY);
            writer.Write(_signText);
            writer.Write(_modifierPlayerIndex);
        }
    }
}
