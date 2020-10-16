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
using System.Xml.Schema;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Projectiles
{
    /// <summary>
    /// A packet sent to update projectile information.
    /// </summary>
    public struct ProjectileInfo : IPacket
    {
        private const int MaxAi = 2;

        private Flags8 _flags;
        private float[]? _ai;

        /// <summary>
        /// Gets or sets the identity, i.e, the projectile index.
        /// </summary>
        public short Identity { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2f Position { get; set; }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector2f Velocity { get; set; }

        /// <summary>
        /// Gets or sets the owner index.
        /// </summary>
        public byte OwnerIndex { get; set; }

        /// <summary>
        /// Gets or sets the projectile type.
        /// </summary>
        public short Type { get; set; }

        /// <summary>
        /// Gets additional information.
        /// </summary>
        public float[] AdditionalInformation => _ai ??= new float[MaxAi];

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public short Damage { get; set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        public float Knockback { get; set; }

        /// <summary>
        /// Gets or sets the original damage.
        /// </summary>
        public short OriginalDamage { get; set; }

        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        public short Uuid { get; set; }

        PacketId IPacket.Id => PacketId.ProjectileInfo;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = 22;
            Identity = span.Read<short>();
            Position = span.Read<Vector2f>();
            Velocity = span.Read<Vector2f>();
            OwnerIndex = span.Read<byte>();
            Type = span.Read<short>();
            _flags = span.Read<Flags8>();
            for (var i = 0; i < AdditionalInformation.Length; ++i)
            {
                if (!_flags[i])
                {
                    continue;
                }

                AdditionalInformation[i] = span.Read<float>();
                length += 4;
            }

            if (_flags[4])
            {
                Damage = span.Read<short>();
                length += 2;
            }

            if (_flags[5])
            {
                Knockback = span.Read<float>();
                length += 4;
            }

            if (_flags[6])
            {
                OriginalDamage = span.Read<short>();
                length += 2;
            }

            if (_flags[7])
            {
                Uuid = span.Read<short>();
                length += 2;
            }

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(Identity);
            length += span[length..].Write(Position);
            length += span[length..].Write(Velocity);
            length += span[length..].Write(OwnerIndex);
            length += span[length..].Write(Type);

            var flagsOffset = length++;
            for (var i = 0; i < AdditionalInformation.Length; ++i)
            {
                if (AdditionalInformation[i] <= 0)
                {
                    continue;
                }

                _flags[i] = true;
                length += span[length..].Write(AdditionalInformation[i]);
            }

            if (Damage > 0)
            {
                _flags[4] = true;
                length += span[length..].Write(Damage);
            }

            if (Knockback > 0)
            {
                _flags[5] = true;
                length += span[length..].Write(Knockback);
            }

            if (OriginalDamage > 0)
            {
                _flags[6] = true;
                length += span[length..].Write(OriginalDamage);
            }

            if (Uuid > 0)
            {
                _flags[7] = true;
                length += span[length..].Write(Uuid);
            }

            span[flagsOffset] = Unsafe.As<Flags8, byte>(ref _flags);
            return length;
        }
    }
}
