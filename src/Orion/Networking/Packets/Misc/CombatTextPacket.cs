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
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;
using Terraria.Localization;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent from the server to the client to show combat text.
    /// </summary>
    public sealed class CombatTextPacket : Packet {
        private NetworkText _text = NetworkText.Empty;

        /// <summary>
        /// Gets or sets the text's position.
        /// </summary>
        public Vector2 TextPosition { get; set; }

        /// <summary>
        /// Gets or sets the text's color.
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public NetworkText Text {
            get => _text;
            set => _text = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override PacketType Type => PacketType.CombatText;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{Text} ({TextColor}) @ {TextPosition}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            TextPosition = reader.ReadVector2();
            TextColor = reader.ReadColor();
            Text = reader.ReadNetworkText();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(TextPosition);
            writer.Write(TextColor);
            writer.Write(Text);
        }
    }
}
