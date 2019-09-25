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
using Orion.Entities;
using Orion.Packets.Extensions;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to spread Nebula Armor buffs to nearby players.
    /// </summary>
    public sealed class PlayerNebulaBuffPacket : Packet {
        private byte _playerIndex;
        private BuffType _buffType;
        private Vector2 _buffPosition;

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerNebulaBuff;

        /// <summary>
        /// Gets or set the player index.
        /// </summary>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the buff's type.
        /// </summary>
        public BuffType BuffType {
            get => _buffType;
            set {
                _buffType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the buff's position.
        /// </summary>
        public Vector2 BuffPosition {
            get => _buffPosition;
            set {
                _buffPosition = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, {BuffType} at ({BuffPosition})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _buffType = (BuffType)reader.ReadByte();
            _buffPosition = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write((byte)_buffType);
            writer.Write(_buffPosition);
        }
    }
}
