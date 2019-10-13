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

using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent from the server to the client to set a player's activity. This is used to synchronize joins and
    /// leaves.
    /// </summary>
    public sealed class PlayerActivityPacket : Packet {
        private byte _playerIndex;
        private bool _isPlayerActive;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerActivity;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is active.
        /// </summary>
        public bool IsPlayerActive {
            get => _isPlayerActive;
            set {
                _isPlayerActive = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, {IsPlayerActive}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _isPlayerActive = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_isPlayerActive);
        }
    }
}
