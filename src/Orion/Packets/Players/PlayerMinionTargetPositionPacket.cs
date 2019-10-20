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

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's minion target position.
    /// </summary>
    /// <remarks>
    /// This packet is used to synchronize player state. <para/>
    /// 
    /// Only sentry-style minions are affected by this packet.
    /// </remarks>
    public sealed class PlayerMinionTargetPositionPacket : Packet {
        private byte _playerIndex;
        private Vector2 _position;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerMinionTargetPosition;

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
        /// Gets or sets the player's minion target position. The components are pixels.
        /// </summary>
        /// <value>The player's minion target position.</value>
        public Vector2 Position {
            get => _position;
            set {
                _position = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _position = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(in _position);
        }
    }
}
