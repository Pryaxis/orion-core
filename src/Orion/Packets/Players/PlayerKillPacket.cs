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
using Orion.Packets.Extensions;
using TDS = Terraria.DataStructures;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to kill a player.
    /// </summary>
    public sealed class PlayerKillPacket : Packet {
        private byte _playerIndex;
        private TDS.PlayerDeathReason _playerDeathReason = TDS.PlayerDeathReason.LegacyEmpty();
        private short _damage;
        private sbyte _hitDirection;
        private bool _isDeathFromPvp;

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerKill;

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
        /// Gets or sets the reason for the player's death.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public TDS.PlayerDeathReason PlayerDeathReason {
            get => _playerDeathReason;
            set {
                _playerDeathReason = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public short Damage {
            get => _damage;
            set {
                _damage = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the hit direction. Values are -1 or 1.
        /// </summary>
        public sbyte HitDirection {
            get => _hitDirection;
            set {
                _hitDirection = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the death is from PvP.
        /// </summary>
        public bool IsDeathFromPvp {
            get => _isDeathFromPvp;
            set {
                _isDeathFromPvp = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, ...]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _playerDeathReason = reader.ReadPlayerDeathReason();
            _damage = reader.ReadInt16();
            _hitDirection = (sbyte)(reader.ReadByte() - 1);
            _isDeathFromPvp = reader.ReadBoolean();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_playerDeathReason);
            writer.Write(_damage);
            writer.Write((byte)(_hitDirection + 1));
            writer.Write(_isDeathFromPvp);
        }
    }
}
