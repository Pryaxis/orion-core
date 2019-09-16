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
using Orion.Networking.Packets.Extensions;
using Terraria;
using Terraria.DataStructures;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to damage a player.
    /// </summary>
    public sealed class DamagePlayerPacket : Packet {
        private byte _playerIndex;
        private PlayerDeathReason _playerDeathReason;
        private short _damage;
        private int _hitDirection;
        private int _hitCooldown;
        private bool _isHitCritical;
        private bool _isHitFromPvp;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the reason for the player's (potential) death.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public PlayerDeathReason PlayerDeathReason {
            get => _playerDeathReason;
            set {
                _playerDeathReason = value ?? throw new ArgumentNullException(nameof(value));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public short Damage {
            get => _damage;
            set {
                _damage = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the hit direction.
        /// </summary>
        public int HitDirection {
            get => _hitDirection;
            set {
                _hitDirection = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the hit cooldown.
        /// </summary>
        public int HitCooldown {
            get => _hitCooldown;
            set {
                _hitCooldown = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is critical.
        /// </summary>
        public bool IsHitCritical {
            get => _isHitCritical;
            set {
                _isHitCritical = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is from PvP.
        /// </summary>
        public bool IsHitFromPvp {
            get => _isHitFromPvp;
            set {
                _isHitFromPvp = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override PacketType Type => PacketType.DamagePlayer;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} by {Damage}, ...]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _playerDeathReason = reader.ReadPlayerDeathReason();
            _damage = reader.ReadInt16();
            _hitDirection = reader.ReadByte() - 1;
            BitsByte flags = reader.ReadByte();
            _isHitCritical = flags[0];
            _isHitFromPvp = flags[1];
            _hitCooldown = reader.ReadSByte();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerDeathReason);
            writer.Write(Damage);
            writer.Write((byte)(HitDirection + 1));
            BitsByte flags = 0;
            flags[0] = IsHitCritical;
            flags[1] = IsHitFromPvp;
            writer.Write(flags);
            writer.Write((sbyte)HitCooldown);
        }
    }
}
