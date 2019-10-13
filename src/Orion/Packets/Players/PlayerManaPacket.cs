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

using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's mana and maximum mana.
    /// </summary>
    public sealed class PlayerManaPacket : Packet {
        private byte _playerIndex;
        private short _playerMana;
        private short _playerMaxMana;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerMana;

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
        /// Gets or sets the player's mana.
        /// </summary>
        public short PlayerMana {
            get => _playerMana;
            set {
                _playerMana = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's maximum mana.
        /// </summary>
        public short PlayerMaxMana {
            get => _playerMaxMana;
            set {
                _playerMaxMana = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} has {PlayerMana}/{PlayerMaxMana} mp]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _playerMana = reader.ReadInt16();
            _playerMaxMana = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_playerMana);
            writer.Write(_playerMaxMana);
        }
    }
}
