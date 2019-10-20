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

using System;
using System.IO;
using TerrariaPlayerDeathReason = Terraria.DataStructures.PlayerDeathReason;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to kill a player.
    /// </summary>
    /// <remarks>This packet is sent when a player is killed or when a player kills another player in PvP.</remarks>
    public sealed class PlayerKillPacket : Packet {
        private byte _playerIndex;
        private TerrariaPlayerDeathReason _deathReason = TerrariaPlayerDeathReason.LegacyEmpty();
        private short _damage;
        private bool _hitDirection;
        private bool _isDeathFromPvp;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerKill;

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
        /// Gets or sets the player's death reason.
        /// </summary>
        /// <value>The player's death reason.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public TerrariaPlayerDeathReason DeathReason {
            get => _deathReason;
            set {
                _deathReason = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        /// <value>The damage.</value>
        public short Damage {
            get => _damage;
            set {
                _damage = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the hit direction.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the hit is to the right; <see langword="false"/> if the hit is to the left.
        /// </value>
        public bool HitDirection {
            get => _hitDirection;
            set {
                _hitDirection = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the kill is from PvP.
        /// </summary>
        /// <value><see langword="true"/> if the kill is from PvP; otherwise, <see langword="false"/>.</value>
        public bool IsFromPvp {
            get => _isDeathFromPvp;
            set {
                _isDeathFromPvp = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _deathReason = reader.ReadPlayerDeathReason();
            _damage = reader.ReadInt16();
            _hitDirection = reader.ReadByte() == 2;
            _isDeathFromPvp = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_deathReason);
            writer.Write(_damage);
            writer.Write((byte)(_hitDirection ? 2 : 0));
            writer.Write(_isDeathFromPvp);
        }
    }
}
