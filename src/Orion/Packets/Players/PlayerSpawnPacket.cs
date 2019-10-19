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
    /// Packet sent to spawn a player.
    /// </summary>
    public sealed class PlayerSpawnPacket : Packet {
        private byte _playerIndex;
        private short _spawnX;
        private short _spawnY;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerSpawn;

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
        /// Gets the player's spawn's X coordinate. If negative, then the world's spawn will be used.
        /// </summary>
        /// <value>The player's spawn's X coordinate.</value>
        public short SpawnX {
            get => _spawnX;
            set {
                _spawnX = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets the player's spawn's Y coordinate. If negative, then the world's spawn will be used.
        /// </summary>
        /// <value>The player's spawn's Y coordinate.</value>
        public short SpawnY {
            get => _spawnY;
            set {
                _spawnY = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _spawnX = reader.ReadInt16();
            _spawnY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_spawnX);
            writer.Write(_spawnY);
        }
    }
}
