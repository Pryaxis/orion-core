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

using System;
using System.IO;

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to set sign information. This is sent in response to a <see cref="SignReadPacket"/>.
    /// </summary>
    public sealed class SignInfoPacket : Packet {
        private short _signIndex;
        private short _x;
        private short _y;
        private string _text = string.Empty;
        private byte _modifierIndex;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.SignInfo;

        /// <summary>
        /// Gets or sets the sign's index.
        /// </summary>
        /// <value>The sign's index.</value>
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
        /// <value>The sign's X coordinate.</value>
        public short X {
            get => _x;
            set {
                _x = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the sign's Y coordinate.
        /// </summary>
        /// <value>The sign's Y coordinate.</value>
        public short Y {
            get => _y;
            set {
                _y = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the sign's text.
        /// </summary>
        /// <value>The sign's text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Text {
            get => _text;
            set {
                _text = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the modifier's player index.
        /// </summary>
        /// <value>Gets the modifier's player index.</value>
        public byte ModifierIndex {
            get => _modifierIndex;
            set {
                _modifierIndex = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _signIndex = reader.ReadInt16();
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            _text = reader.ReadString();
            _modifierIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_signIndex);
            writer.Write(_x);
            writer.Write(_y);
            writer.Write(_text);
            writer.Write(_modifierIndex);
        }
    }
}
