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
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public struct PlayerDeathReason
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerDeathReason"/> structure with the specified properties.
        /// </summary>
        /// <param name="killerIndex">The killer index.</param>
        /// <param name="killingNpcIndex">The killing NPC index.</param>
        /// <param name="killingProjectileIndex">The killing projectile index.</param>
        /// <param name="causeOfDeath">The cause of death when killed by "Other".</param>
        /// <param name="projectileType">The projectile type.</param>
        /// <param name="itemType">The item type.</param>
        /// <param name="itemPrefix">The item prefix.</param>
        /// <param name="customDeathReason">The custom death reason.</param>
        public PlayerDeathReason(short killerIndex = -1, short killingNpcIndex = -1, short killingProjectileIndex = -1,
            CauseOfDeath causeOfDeath = CauseOfDeath.None, short projectileType = short.MinValue,
            short itemType = short.MinValue, byte itemPrefix = byte.MaxValue, string customDeathReason = "")
        {
            _flags = default;

            KillerIndex = killerIndex;
            KillingNpcIndex = killingNpcIndex;
            KillingProjectileIndex = killingProjectileIndex;
            CauseOfDeath = causeOfDeath;
            ProjectileType = projectileType;
            ItemType = itemType;
            ItemPrefix = itemPrefix;
            _customDeathReason = customDeathReason;
        }

        [FieldOffset(0)] private Flags8 _flags;
        [FieldOffset(16)] private string? _customDeathReason;

        /// <summary>
        /// Gets or sets the killer's index.
        /// </summary>
        [field: FieldOffset(1)] public short KillerIndex { get; set; }

        /// <summary>
        /// Gets or sets the killing NPC index.
        /// </summary>
        [field: FieldOffset(3)] public short KillingNpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the killing projectile index.
        /// </summary>
        [field: FieldOffset(5)] public short KillingProjectileIndex { get; set; }

        /// <summary>
        /// Gets or sets the cause of death when killed by "Other".
        /// </summary>
        [field: FieldOffset(7)] public CauseOfDeath CauseOfDeath { get; set; }

        /// <summary>
        /// Gets or sets the projectile type.
        /// </summary>
        [field: FieldOffset(8)] public short ProjectileType { get; set; }

        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        [field: FieldOffset(10)] public short ItemType { get; set; }

        /// <summary>
        /// Gets or sets the item prefix.
        /// </summary>
        [field: FieldOffset(12)] public byte ItemPrefix { get; set; }

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
            if (KillerIndex != -1)
            {
                Unsafe.CopyBlockUnaligned(ref span.At(length), ref Unsafe.Add(ref this.AsByte(), 1), 2);
                _flags[0] = true;
                length += 2;
            }

            if (KillingNpcIndex != -1)
            {
                Unsafe.CopyBlockUnaligned(ref span.At(length), ref Unsafe.Add(ref this.AsByte(), 3), 2);
                _flags[1] = true;
                length += 2;
            }

            if (KillingProjectileIndex != -1)
            {
                Unsafe.CopyBlockUnaligned(ref span.At(length), ref Unsafe.Add(ref this.AsByte(), 5), 2);
                _flags[2] = true;
                length += 2;
            }

            if (CauseOfDeath != CauseOfDeath.None)
            {
                Unsafe.CopyBlockUnaligned(ref span.At(length), ref Unsafe.Add(ref this.AsByte(), 7), 1);
                _flags[3] = true;
                length++;
            }

            if (ProjectileType > 0)
            {
                Unsafe.CopyBlockUnaligned(ref span.At(length), ref Unsafe.Add(ref this.AsByte(), 8), 2);
                _flags[4] = true;
                length += 2;
            }

            if (ItemType > 0)
            {
                Unsafe.CopyBlockUnaligned(ref span.At(length), ref Unsafe.Add(ref this.AsByte(), 10), 2);
                _flags[5] = true;
                length += 2;
            }

            if (ItemPrefix < 85)
            {
                Unsafe.CopyBlockUnaligned(ref span.At(length), ref Unsafe.Add(ref this.AsByte(), 12), 1);
                _flags[6] = true;
                length++;
            }

            if (!string.IsNullOrWhiteSpace(CustomDeathReason))
            {
                length += span[length..].Write(CustomDeathReason);
                _flags[7] = true;
            }

            Unsafe.CopyBlockUnaligned(ref span.At(0), ref this.AsByte(), 1);
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
            playerDeathReason = new PlayerDeathReason()
            { 
                KillerIndex = -1, KillingNpcIndex = -1, KillingProjectileIndex = -1, CauseOfDeath = CauseOfDeath.None,
                ProjectileType = -1, ItemType = -1, CustomDeathReason = string.Empty 
            };

            var length = 1;
            var flags = Unsafe.As<byte, Flags8>(ref span.At(0));
            if (flags[0])
            {
                // Killed by a player
                Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref playerDeathReason.AsByte(), 1), ref span.At(length), 2);
                length += 2;
            }

            if (flags[1])
            {
                // Killed by an NPC
                Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref playerDeathReason.AsByte(), 3), ref span.At(length), 2);
                length += 2;
            }

            if (flags[2])
            {
                // Killed by a projectile
                Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref playerDeathReason.AsByte(), 5), ref span.At(length), 2);
                length += 2;
            }

            if (flags[3])
            {
                // Killed via Other
                Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref playerDeathReason.AsByte(), 7), ref span.At(length), 1);
                length++;
            }

            if (flags[4])
            {
                // Projectile type
                Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref playerDeathReason.AsByte(), 8), ref span.At(length), 2);
                length += 2;
            }

            if (flags[5])
            {
                // Killed via PvP (Item type)
                Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref playerDeathReason.AsByte(), 10), ref span.At(length), 2);
                length += 2;
            }

            if (flags[6])
            {
                // Killed via PvP (Item Prefix)
                Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref playerDeathReason.AsByte(), 12), ref span.At(length), 1);
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
