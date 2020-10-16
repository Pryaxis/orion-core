// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Orion.Core.Utils;

namespace Orion.Core.Packets.DataStructures
{
    /// <summary>
    /// Provides context surrounding a player death.
    /// </summary>
    public struct PlayerDeathReason
    {
        private Flags8 _flags;
        private string? _customDeathReason;

        /// <summary>
        /// Gets or sets the killer's index.
        /// </summary>
        public short? KillerIndex { get; set; }

        /// <summary>
        /// Gets or sets the killing NPC index.
        /// </summary>
        public short? KillingNpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the killing projectile index.
        /// </summary>
        public short? KillingProjectileIndex { get; set; }

        /// <summary>
        /// Gets or sets the cause of death when killed by "Other".
        /// </summary>
        public CauseOfDeath? CauseOfDeath { get; set; }

        /// <summary>
        /// Gets or sets the projectile type.
        /// </summary>
        public short? ProjectileType { get; set; }

        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        public short? ItemType { get; set; }

        /// <summary>
        /// Gets or sets the item prefix.
        /// </summary>
        public byte? ItemPrefix { get; set; }

        /// <summary>
        /// Gets or sets the custom death reason.
        /// </summary>
        public string CustomDeathReason
        {
            get => _customDeathReason ??= string.Empty;
            set => _customDeathReason = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Writes the current <see cref="PlayerDeathReason"/> to the specified span and returns the number of bytes written.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <returns>The number of bytes written.</returns>
        public int Write(Span<byte> span)
        {
            var length = 1;
            if (KillerIndex.HasValue)
            {
                length += span[length..].Write(KillerIndex.Value);
                _flags[0] = true;
            }
            if (KillingNpcIndex.HasValue)
            {
                length += span[length..].Write(KillingNpcIndex.Value);
                _flags[1] = true;
            }
            if (KillingProjectileIndex.HasValue)
            {
                length += span[length..].Write(KillingProjectileIndex.Value);
                _flags[2] = true;
            }
            if (CauseOfDeath.HasValue)
            {
                length += span[length..].Write(CauseOfDeath.Value);
                _flags[3] = true;
            }
            if (ProjectileType.HasValue)
            {
                length += span[length..].Write(ProjectileType.Value);
                _flags[4] = true;
            }
            if (ItemType.HasValue)
            {
                length += span[length..].Write(ItemType.Value);
                _flags[5] = true;
            }
            if (ItemPrefix.HasValue)
            {
                length += span[length..].Write(ItemPrefix.Value);
                _flags[6] = true;
            }
            if (!string.IsNullOrWhiteSpace(CustomDeathReason))
            {
                length += span[length..].Write(CustomDeathReason);
                _flags[7] = true;
            }

            span[0] = Unsafe.As<Flags8, byte>(ref _flags);
            return length;
        }

        /// <summary>
        /// Reads a <see cref="PlayerDeathReason"/> from the specified span and returns the number of bytes read.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="playerDeathReason">The resulting <see cref="PlayerDeathReason"/>.</param>
        /// <returns>The number of bytes read.</returns>
        public static int Read(Span<byte> span, out PlayerDeathReason playerDeathReason)
        {
            playerDeathReason = new PlayerDeathReason();
            var length = 1;
            var flags = MemoryMarshal.Read<Flags8>(span);
            if (flags[0])
            {
                // Killed by a player
                playerDeathReason.KillerIndex = MemoryMarshal.Read<short>(span[length..]);
                length += 2;
            }

            if (flags[1])
            {
                // Killed by an NPC
                playerDeathReason.KillingNpcIndex = MemoryMarshal.Read<short>(span[length..]);
                length += 2;
            }

            if (flags[2])
            {
                // Killed by a projectile
                playerDeathReason.KillingProjectileIndex = MemoryMarshal.Read<short>(span[length..]);
                length += 2;
            }

            if (flags[3])
            {
                // Killed via Other
                playerDeathReason.CauseOfDeath = (CauseOfDeath) MemoryMarshal.Read<byte>(span[length..]);
                length++;
            }

            if (flags[4])
            {
                // Projectile type
                playerDeathReason.ProjectileType = MemoryMarshal.Read<short>(span[length..]);
                length += 2;
            }

            if (flags[5])
            {
                // Killed via PvP (Item type)
                playerDeathReason.ItemType = MemoryMarshal.Read<short>(span[length..]);
                length += 2;
            }

            if (flags[6])
            {
                // Killed via PvP (Item Prefix)
                playerDeathReason.ItemPrefix = MemoryMarshal.Read<byte>(span[length..]);
                length++;
            }

            if (flags[7])
            {
                // Custom reason
                length += span[length..].Read(out playerDeathReason._customDeathReason);
            }

            return length;
        }
    }

    /// <summary>
    /// Specifies a cause of death when killed by "Other".
    /// </summary>
    public enum CauseOfDeath : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        FallDamage,
        Drowning,
        LavaDamage,
        FallDamage2,
        DemonAltar,
        CompanionCube,
        Suffocation,
        Burning,
        Poison,
        Electrified,
        TriedToEscapeWoF,
        WoFLicked,
        ChaosState,
        MaleChaosStateV2,
        FemaleChaosStateV2,
        None
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
