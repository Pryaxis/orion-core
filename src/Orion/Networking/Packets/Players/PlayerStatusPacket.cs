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
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent from the server to the client to set the player's status.
    /// </summary>
    public sealed class PlayerStatusPacket : Packet {
        private Terraria.Localization.NetworkText _playerStatusText = Terraria.Localization.NetworkText.Empty;
        private int _playerStatusIncrease;

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerStatus;

        /// <summary>
        /// Gets or sets the player's status increase.
        /// </summary>
        public int PlayerStatusIncrease {
            get => _playerStatusIncrease;
            set {
                _playerStatusIncrease = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's status text.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public Terraria.Localization.NetworkText PlayerStatusText {
            get => _playerStatusText;
            set {
                _playerStatusText = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{PlayerStatusText}, I={PlayerStatusIncrease}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerStatusIncrease = reader.ReadInt32();
            PlayerStatusText = reader.ReadNetworkText();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerStatusIncrease);
            writer.Write(PlayerStatusText);
        }
    }
}
