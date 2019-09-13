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
    /// Packet sent from the server to the client to show chat.
    /// </summary>
    public sealed class ChatPacket : Packet {
        private NetworkText _chatText;

        /// <summary>
        /// Gets or sets the chat's color.
        /// </summary>
        public Color ChatColor { get; set; }

        /// <summary>
        /// Gets or sets the chat's text.
        /// </summary>
        public NetworkText ChatText {
            get => _chatText;
            set => _chatText = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the chat's line width.
        /// </summary>
        public short ChatLineWidth { get; set; }

        internal override PacketType Type => PacketType.Chat;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ChatText}, C={ChatColor}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ChatColor = reader.ReadColor();
            ChatText = reader.ReadNetworkText();
            ChatLineWidth = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ChatColor);
            writer.Write(ChatText);
            writer.Write(ChatLineWidth);
        }
    }
}
