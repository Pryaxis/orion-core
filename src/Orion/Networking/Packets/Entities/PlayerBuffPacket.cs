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

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;
using Orion.Entities;

namespace Orion.Networking.Packets.Entities {
    /// <summary>
    /// Packet sent to add a buff to a player.
    /// </summary>
    [PublicAPI]
    public sealed class PlayerBuffPacket : Packet {
        private byte _playerIndex;
        private Buff _playerBuff;

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerBuff;

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
        /// Gets or sets the player's buff.
        /// </summary>
        public Buff PlayerBuff {
            get => _playerBuff;
            set {
                _playerBuff = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, {PlayerBuff}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _playerBuff = new Buff((BuffType)reader.ReadByte(), TimeSpan.FromSeconds(reader.ReadInt32() / 60.0));
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write((byte)_playerBuff.BuffType);
            writer.Write((int)(_playerBuff.Duration.TotalSeconds * 60.0));
        }
    }
}
