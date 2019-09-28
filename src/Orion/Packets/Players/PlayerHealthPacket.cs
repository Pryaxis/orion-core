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
    /// Packet sent to set a player's health and maximum health.
    /// </summary>
    public sealed class PlayerHealthPacket : Packet {
        private byte _playerIndex;
        private short _playerHealth;
        private short _playerMaxHealth;

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerHealth;

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
        /// Gets or sets the player's health.
        /// </summary>
        public short PlayerHealth {
            get => _playerHealth;
            set {
                _playerHealth = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's maximum health.
        /// </summary>
        public short PlayerMaxHealth {
            get => _playerMaxHealth;
            set {
                _playerMaxHealth = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} has {PlayerHealth}/{PlayerMaxHealth} hp]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _playerHealth = reader.ReadInt16();
            _playerMaxHealth = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_playerHealth);
            writer.Write(_playerMaxHealth);
        }
    }
}
