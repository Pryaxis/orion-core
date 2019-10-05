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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Packets.Extensions;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to damage a player.
    /// </summary>
    public sealed class PlayerDamagePacket : Packet {
        private byte _playerIndex;

        private Terraria.DataStructures.PlayerDeathReason _playerDeathReason =
            Terraria.DataStructures.PlayerDeathReason.LegacyEmpty();

        private short _damage;
        private sbyte _hitDirection;
        private int _hitCooldown;
        private bool _isHitCritical;
        private bool _isHitFromPvp;

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerDamage;

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
        /// Gets or sets the reason for the player's (potential) death.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public Terraria.DataStructures.PlayerDeathReason PlayerDeathReason {
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
        /// Gets or sets the hit cooldown.
        /// </summary>
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
        public bool IsHitFromPvp {
            get => _isHitFromPvp;
            set {
                _isHitFromPvp = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} for {Damage} hp, ...]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _playerDeathReason = reader.ReadPlayerDeathReason();
            _damage = reader.ReadInt16();
            _hitDirection = (sbyte)(reader.ReadByte() - 1);
            Terraria.BitsByte flags = reader.ReadByte();
            _isHitCritical = flags[0];
            _isHitFromPvp = flags[1];
            _hitCooldown = reader.ReadSByte();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_playerDeathReason);
            writer.Write(_damage);
            writer.Write((byte)(_hitDirection + 1));
            Terraria.BitsByte flags = 0;
            flags[0] = _isHitCritical;
            flags[1] = _isHitFromPvp;
            writer.Write(flags);
            writer.Write((sbyte)_hitCooldown);
        }
    }
}
