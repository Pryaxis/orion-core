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
using Orion.Packets.Extensions;
using TerrariaNetworkText = Terraria.Localization.NetworkText;

namespace Orion.Packets.Modules {
    /// <summary>
    /// Module sent for chat.
    /// </summary>
    public sealed class ChatModule : Module {
        private string _clientCommand = string.Empty;
        private string _clientText = string.Empty;
        private byte _serverChatterIndex;
        private TerrariaNetworkText _serverText = TerrariaNetworkText.Empty;
        private Color _serverColor;

        /// <inheritdoc/>
        public override ModuleType Type => ModuleType.Chat;

        /// <summary>
        /// Gets or sets the client's chat command.
        /// </summary>
        /// <value>The client's chat command.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string ClientCommand {
            get => _clientCommand;
            set {
                _clientCommand = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the client's chat text.
        /// </summary>
        /// <value>The client's chat text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string ClientText {
            get => _clientText;
            set {
                _clientText = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the server's chatter player index.
        /// </summary>
        /// <value>THe server's chatter player index.</value>
        public byte ServerChatterIndex {
            get => _serverChatterIndex;
            set {
                _serverChatterIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the server chat text.
        /// </summary>
        /// <value>The server's chat text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string ServerText {
            get => _serverText.ToString();
            set {
                _serverText =
                    TerrariaNetworkText.FromLiteral(value ?? throw new ArgumentNullException(nameof(value)));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the server chat's color. The alpha component is ignored.
        /// </summary>
        /// <value>The server chat's color.</value>
        public Color ServerColor {
            get => _serverColor;
            set {
                _serverColor = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            if (context == PacketContext.Server) {
                _clientCommand = reader.ReadString();
                _clientText = reader.ReadString();
            } else {
                _serverChatterIndex = reader.ReadByte();
                _serverText = reader.ReadNetworkText();
                _serverColor = reader.ReadColor();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            if (context == PacketContext.Server) {
                writer.Write(_serverChatterIndex);
                writer.Write(_serverText);
                writer.Write(in _serverColor);
            } else {
                writer.Write(_clientCommand);
                writer.Write(_clientText);
            }
        }
    }
}
