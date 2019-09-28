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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Packets.Extensions;

namespace Orion.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to show chat.
    /// </summary>
    public sealed class ChatPacket : Packet {
        private Color _chatColor;
        private Terraria.Localization.NetworkText _chatText = Terraria.Localization.NetworkText.Empty;
        private short _chatLineWidth;

        /// <inheritdoc />
        public override PacketType Type => PacketType.Chat;

        /// <summary>
        /// Gets or sets the chat's color.
        /// </summary>
        public Color ChatColor {
            get => _chatColor;
            set {
                _chatColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chat's text.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string ChatText {
            get => _chatText.ToString();
            set {
                _chatText = Terraria.Localization.NetworkText.FromLiteral(
                    value ?? throw new ArgumentNullException(nameof(value)));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chat's line width.
        /// </summary>
        public short ChatLineWidth {
            get => _chatLineWidth;
            set {
                _chatLineWidth = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ChatText}, C={ChatColor}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _chatColor = reader.ReadColor();
            _chatText = reader.ReadNetworkText();
            _chatLineWidth = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_chatColor);
            writer.Write(_chatText);
            writer.Write(_chatLineWidth);
        }
    }
}
