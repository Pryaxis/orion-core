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

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent from the client to the server to inform the server about the player's UUID. This is sent in response
    /// to a <see cref="PlayerContinueConnectingPacket"/>.
    /// </summary>
    public sealed class PlayerUuidPacket : Packet {
        private string _playerUuid = "";

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerUuid;

        /// <summary>
        /// Gets or sets the player's UUID.
        /// </summary>
        public string PlayerUuid {
            get => _playerUuid;
            set {
                _playerUuid = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{PlayerUuid}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _playerUuid = reader.ReadString();

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            writer.Write(_playerUuid);
    }
}
