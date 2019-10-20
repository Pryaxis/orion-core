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
using Microsoft.Xna.Framework;
using TerrariaNetworkText = Terraria.Localization.NetworkText;

namespace Orion.Packets.Entities {
    /// <summary>
    /// Packet sent from the server to the client to show combat text.
    /// </summary>
    /// <remarks>This packet is not normally sent.</remarks>
    /// <seealso cref="CombatNumberPacket"/>
    public sealed class CombatTextPacket : Packet {
        private Vector2 _position;
        private Color _color;
        private TerrariaNetworkText _text = TerrariaNetworkText.Empty;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.CombatText;

        /// <summary>
        /// Gets or sets the text's position. The components are pixels.
        /// </summary>
        /// <value>The text's position.</value>
        public Vector2 Position {
            get => _position;
            set {
                _position = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the text's color. The alpha component is ignored.
        /// </summary>
        /// <value>The text's color.</value>
        public Color Color {
            get => _color;
            set {
                _color = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Text {
            get => _text.ToString();
            set {
                _text = TerrariaNetworkText.FromLiteral(value ?? throw new ArgumentNullException(nameof(value)));
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _position = reader.ReadVector2();
            _color = reader.ReadColor();
            _text = reader.ReadNetworkText();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(in _position);
            writer.Write(in _color);
            writer.Write(_text);
        }
    }
}
