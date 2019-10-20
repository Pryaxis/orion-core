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
using TerrariaNetworkText = Terraria.Localization.NetworkText;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent from the server to the client to set the player's status.
    /// </summary>
    /// <remarks>This packet is used to update a player's connection status.</remarks>
    public sealed class PlayerStatusPacket : Packet {
        private TerrariaNetworkText _statusText = TerrariaNetworkText.Empty;
        private int _statusIncrease;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerStatus;

        /// <summary>
        /// Gets or sets the player's status increase.
        /// </summary>
        /// <value>The player's status increase.</value>
        public int StatusIncrease {
            get => _statusIncrease;
            set {
                _statusIncrease = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's status text.
        /// </summary>
        /// <value>The player's status text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string StatusText {
            get => _statusText.ToString();
            set {
                _statusText =
                    TerrariaNetworkText.FromLiteral(value ?? throw new ArgumentNullException(nameof(value)));
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _statusIncrease = reader.ReadInt32();
            _statusText = reader.ReadNetworkText();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_statusIncrease);
            writer.Write(_statusText);
        }
    }
}
