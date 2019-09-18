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

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to spawn a player.
    /// </summary>
    public sealed class SpawnPlayerPacket : Packet {
        private byte _playerIndex;
        private short _playerSpawnX;
        private short _playerSpawnY;

        /// <inheritdoc />
        public override PacketType Type => PacketType.SpawnPlayer;

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
        /// Gets or sets the player spawn's X coordinate. A negative value results in the world spawn.
        /// </summary>
        public short PlayerSpawnX {
            get => _playerSpawnX;
            set {
                _playerSpawnX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player spawn's Y coordinate. A negative value results in the world spawn.
        /// </summary>
        public short PlayerSpawnY {
            get => _playerSpawnY;
            set {
                _playerSpawnY = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} @ ({PlayerSpawnX}, {PlayerSpawnY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerSpawnX = reader.ReadInt16();
            PlayerSpawnY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerSpawnX);
            writer.Write(PlayerSpawnY);
        }
    }
}
