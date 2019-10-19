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
using Orion.Packets.Extensions;
using TerrariaNetworkText = Terraria.Localization.NetworkText;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent from the server to the client to disconnect it. This is sent in response to an invalid
    /// <see cref="PlayerPasswordResponsePacket"/> or for various other reasons.
    /// </summary>
    public sealed class PlayerDisconnectPacket : Packet {
        private TerrariaNetworkText _disconnectReason = TerrariaNetworkText.Empty;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerDisconnect;

        /// <summary>
        /// Gets or sets the player's disconnect reason.
        /// </summary>
        /// <value>The player's disconnect reason.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string DisconnectReason {
            get => _disconnectReason.ToString();
            set {
                _disconnectReason =
                    TerrariaNetworkText.FromLiteral(value ?? throw new ArgumentNullException(nameof(value)));
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _disconnectReason = reader.ReadNetworkText();

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            writer.Write(_disconnectReason);
    }
}
