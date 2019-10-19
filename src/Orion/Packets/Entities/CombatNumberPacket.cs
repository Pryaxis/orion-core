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
using Microsoft.Xna.Framework;
using Orion.Packets.Extensions;

namespace Orion.Packets.Entities {
    /// <summary>
    /// Packet sent from the server to the client to show a combat number.
    /// </summary>
    /// <remarks>
    /// Combat numbers are the numbers that show up when, e.g., a player is hurt or an NPC is damaged. They may be used
    /// to graphically show number information.
    /// </remarks>
    public sealed class CombatNumberPacket : Packet {
        private Vector2 _position;
        private Color _color;
        private int _number;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.CombatNumber;

        /// <summary>
        /// Gets or sets the number's position. The components are pixels.
        /// </summary>
        /// <value>The number's position.</value>
        public Vector2 Position {
            get => _position;
            set {
                _position = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the number's color. The alpha component is ignored.
        /// </summary>
        /// <value>The number's color.</value>
        public Color Color {
            get => _color;
            set {
                _color = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public int Number {
            get => _number;
            set {
                _number = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _position = reader.ReadVector2();
            _color = reader.ReadColor();
            _number = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(in _position);
            writer.Write(in _color);
            writer.Write(_number);
        }
    }
}
