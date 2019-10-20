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

using System.IO;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's stealth.
    /// </summary>
    /// <remarks>This packet is used to synchronize player state.</remarks>
    public sealed class PlayerStealthPacket : Packet {
        private byte _playerIndex;
        private float _stealthStatus;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerStealth;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        /// <value>The player index.</value>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's stealth status.
        /// </summary>
        /// <value>The player's stealth status.</value>
        /// <remarks>
        /// This value can range from <c>0.0</c> to <c>1.0</c>. A value of <c>0.0</c> represents full stealth.
        /// </remarks>
        public float StealthStatus {
            get => _stealthStatus;
            set {
                _stealthStatus = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _stealthStatus = reader.ReadSingle();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_stealthStatus);
        }
    }
}
