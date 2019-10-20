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
    /// Packet sent to damage a player.
    /// </summary>
    /// <remarks>This packet is sent when a player damages themselves or damages another player in PvP.</remarks>
    public sealed class PlayerDamagePacket : Packet {
        private byte _playerIndex;
        private TerrariaPlayerDeathReason _deathReason = TerrariaPlayerDeathReason.LegacyEmpty();
        private short _damage;
        private bool _hitDirection;
        private int _hitCooldown;
        private bool _isHitCritical;
        private bool _isHitFromPvp;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerDamage;

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
        /// Gets or sets the player's (potential) death reason.
        /// </summary>
        /// <value>The player's (potential) death reason.</value>
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
        /// Gets or sets the hit cooldown.
        /// </summary>
        /// <value>The hit cooldown.</value>
        public int HitCooldown {
            get => _hitCooldown;
            set {
                _hitCooldown = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is critical.
        /// </summary>
        /// <value><see langword="true"/> if the hit is critical; otherwise, <see langword="false"/>.</value>
        public bool IsHitCritical {
            get => _isHitCritical;
            set {
                _isHitCritical = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is from PvP.
        /// </summary>
        /// <value><see langword="true"/> if the hit is from PvP; otherwise, <see langword="false"/>.</value>
        /// <remarks>
        /// A critical hit doubles the damage dealt to the player after defense and other attributes are factored into
        /// the damage calculation.
        /// </remarks>
        public bool IsHitFromPvp {
            get => _isHitFromPvp;
            set {
                _isHitFromPvp = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _deathReason = reader.ReadPlayerDeathReason();
            _damage = reader.ReadInt16();
            _hitDirection = reader.ReadByte() == 2;
            Terraria.BitsByte flags = reader.ReadByte();
            _isHitCritical = flags[0];
            _isHitFromPvp = flags[1];
            _hitCooldown = reader.ReadSByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_deathReason);
            writer.Write(_damage);
            writer.Write((byte)(_hitDirection ? 2 : 0));
            Terraria.BitsByte flags = 0;
            flags[0] = _isHitCritical;
            flags[1] = _isHitFromPvp;
            writer.Write(flags);
            writer.Write((sbyte)_hitCooldown);
        }
    }
}
