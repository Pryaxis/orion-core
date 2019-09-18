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
using Microsoft.Xna.Framework;
using Orion.Entities;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to spread Nebula Armor buffs to nearby players.
    /// </summary>
    public sealed class NebulaBuffPlayersPacket : Packet {
        private byte _playerIndex;
        private BuffType _buffType = BuffType.None;
        private Vector2 _buffPosition;

        /// <inheritdoc />
        public override PacketType Type => PacketType.NebulaBuffPlayers;

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
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public BuffType BuffType {
            get => _buffType;
            set {
                _buffType = value ?? throw new ArgumentNullException(nameof(value));
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
            PlayerIndex = reader.ReadByte();
            BuffType = BuffType.FromId(reader.ReadByte()) ?? throw new PacketException("Buff type is invalid.");
            BuffPosition = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(BuffType.Id);
            writer.Write(BuffPosition);
        }
    }
}
