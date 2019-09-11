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
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;
using Terraria.Localization;

namespace Orion.Networking.Packets.Modules {
    /// <summary>
    /// Module sent for chat.
    /// </summary>
    public sealed class ChatModule : Module {
        /// <summary>
        /// Gets or sets the client chat's command. Only applicable when received in the Server context.
        /// </summary>
        public string ClientChatCommand { get; set; }

        /// <summary>
        /// Gets or sets the client chat's text. Only applicable when received in the Server context.
        /// </summary>
        public string ClientChatText { get; set; }

        /// <summary>
        /// Gets or sets the server chatting player index. Only applicable when sent in the Server context.
        /// </summary>
        public byte ServerChattingPlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the server chat's text. Only applicable when sent in the Server context.
        /// </summary>
        public NetworkText ServerChatText { get; set; }

        /// <summary>
        /// Gets or sets the server chat's color. Only applicable when sent in the Server context.
        /// </summary>
        public Color ServerChatColor { get; set; }

        private protected override ModuleType Type => ModuleType.Chat;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() {
            var beginning = $"{nameof(ModuleType.Chat)}[";
            if (ClientChatCommand != null) {
                return beginning + $"{ClientChatCommand}, T={ClientChatText}]";
            }

            return beginning + $"{ServerChatText}, #={ServerChattingPlayerIndex}, C={ServerChatColor}]";
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            if (context == PacketContext.Server) {
                ClientChatCommand = reader.ReadString();
                ClientChatText = reader.ReadString();
            } else {
                ServerChattingPlayerIndex = reader.ReadByte();
                ServerChatText = reader.ReadNetworkText();
                ServerChatColor = reader.ReadColor();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            if (context == PacketContext.Server) {
                writer.Write(ServerChattingPlayerIndex);
                writer.Write(ServerChatText);
                writer.Write(ServerChatColor);
            } else {
                writer.Write(ClientChatCommand);
                writer.Write(ClientChatText);
            }
        }
    }
}
