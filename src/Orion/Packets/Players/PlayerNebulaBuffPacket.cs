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
        private Vector2 _position;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerNebulaBuff;

        /// <summary>
        /// Gets or set the player index.
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
        /// Gets or sets the buff's type.
        /// </summary>
        /// <value>The buff's type.</value>
        public BuffType BuffType {
            get => _buffType;
            set {
                _buffType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the position. The components are pixels.
        /// </summary>
        /// <value>The position.</value>
        public Vector2 Position {
            get => _position;
            set {
                _position = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _buffType = (BuffType)reader.ReadByte();
            _position = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write((byte)_buffType);
            writer.Write(in _position);
        }
    }
}
