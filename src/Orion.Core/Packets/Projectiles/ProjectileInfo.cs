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
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Projectiles
{
    /// <summary>
    /// A packet sent to update projectile information.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 42)]
    public struct ProjectileInfo : IPacket
    {
        private const int MaxAi = 2;

        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        [FieldOffset(21)] private Flags8 _flags;
        [FieldOffset(24)] private float[]? _ai;

        /// <summary>
        /// Gets or sets the identity, i.e, the projectile index.
        /// </summary>
        [field: FieldOffset(0)] public short Identity { get; set; }

        /// <summary>
        /// Gets or sets the X position.
        /// </summary>
        [field: FieldOffset(2)] public float X { get; set; }

        /// <summary>
        /// Gets or sets the Y position.
        /// </summary>
        [field: FieldOffset(6)] public float Y { get; set; }

        /// <summary>
        /// Gets or sets the velocity X.
        /// </summary>
        [field: FieldOffset(10)] public float VelocityX { get; set; }

        /// <summary>
        /// Gets or sets the velocity Y.
        /// </summary>
        [field: FieldOffset(14)] public float VelocityY { get; set; }

        /// <summary>
        /// Gets or sets the owner index.
        /// </summary>
        [field: FieldOffset(18)] public byte OwnerIndex { get; set; }

        /// <summary>
        /// Gets or sets the projectile type.
        /// </summary>
        [field: FieldOffset(19)] public short Type { get; set; }

        /// <summary>
        /// Gets additional information.
        /// </summary>
        public float[] AdditionalInformation => _ai ??= new float[MaxAi];

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        [field: FieldOffset(32)] public short Damage { get; set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        [field: FieldOffset(34)] public float Knockback { get; set; }

        /// <summary>
        /// Gets or sets the original damage.
        /// </summary>
        [field: FieldOffset(38)] public short OriginalDamage { get; set; }

        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [field: FieldOffset(40)] public short Uuid { get; set; }

        PacketId IPacket.Id => PacketId.ProjectileInfo;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 22);
            for (var i = 0; i < AdditionalInformation.Length; ++i)
            {
                if (!_flags[i])
                {
                    continue;
                }

                length += span[length..].Read(ref Unsafe.Add(ref Unsafe.As<float, byte>(ref AdditionalInformation[0]), i * 4), 4);
            }

            if (_flags[4])
            {
                length += span[length..].Read(ref Unsafe.Add(ref _bytes, 32), 2);
            }

            if (_flags[5])
            {
                length += span[length..].Read(ref Unsafe.Add(ref _bytes, 34), 4);
            }

            if (_flags[6])
            {
                length += span[length..].Read(ref Unsafe.Add(ref _bytes, 38), 2);
            }

            if (_flags[7])
            {
                length += span[length..].Read(ref Unsafe.Add(ref _bytes, 40), 2);
            }

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 22);
            for (var i = 0; i < AdditionalInformation.Length; ++i)
            {
                if (AdditionalInformation[i] == 0)
                {
                    continue;
                }

                length += span[length..].Write(ref Unsafe.Add(ref Unsafe.As<float, byte>(ref AdditionalInformation[0]), i * 4), 4);
            }

            if (Damage > 0)
            {
                length += span[length..].Write(ref Unsafe.Add(ref _bytes, 32), 2);
                _flags[4] = true;
            }

            if (Knockback > 0)
            {
                length += span[length..].Write(ref Unsafe.Add(ref _bytes, 34), 4);
                _flags[5] = true;
            }

            if (OriginalDamage > 0)
            {
                length += span[length..].Write(ref Unsafe.Add(ref _bytes, 38), 2);
                _flags[6] = true;
            }

            if (Uuid > 0)
            {
                length += span[length..].Write(ref Unsafe.Add(ref _bytes, 40), 2);
                _flags[7] = true;
            }

            span[21..].Write(ref Unsafe.Add(ref _bytes, 21), 1);
            return length;
        }
    }
}
