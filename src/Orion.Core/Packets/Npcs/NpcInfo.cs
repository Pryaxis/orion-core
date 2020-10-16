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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to update NPC information.
    /// </summary>
    public struct NpcInfo : IPacket
    {
        private const int MaxAi = 4;
        private Flags8 _flags;
        private Flags8 _flags2;
        private float[] _ai;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public int NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2f Position { get; set; }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector2f Velocity { get; set; }

        /// <summary>
        /// Gets or sets the targeted player.
        /// </summary>
        public ushort TargetIndex { get; set; }

        /// <summary>
        /// Gets additional information.
        /// </summary>
        public float[] AdditionalInformation => _ai ??= new float[MaxAi];

        /// <summary>
        /// Gets or sets the NPC's net ID.
        /// </summary>
        public short NetId { get; set; }

        /// <summary>
        /// Gets or sets a value that defines how the NPC's difficulty scales with respect to player count changes.
        /// </summary>
        public byte DifficultyScalingOverride { get; set; }

        /// <summary>
        /// Gets or sets the strength multiplier override.
        /// </summary>
        public float StrengthMultiplierOverride { get; set; }

        /// <summary>
        /// Gets or sets the NPC's health.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or sets a catchable NPC's owner index.
        /// </summary>
        public byte? ReleaseOwnerIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sprite is facing right.
        /// </summary>
        public bool IsSpriteFacingRight
        {
            get => _flags[6];
            set => _flags[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the NPC is moving towards the right.
        /// </summary>
        public bool IsMovingRight
        {
            get => _flags[0];
            set => _flags[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the NPC is moving downwards.
        /// </summary>
        public bool IsMovingDown
        {
            get => _flags[1];
            set => _flags[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the NPC has spawned from a statue.
        /// </summary>
        public bool SpawnedFromStatue
        {
            get => _flags2[1];
            set => _flags2[1] = value;
        }

        PacketId IPacket.Id => PacketId.NpcInfo;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = 24;
            NpcIndex = span.Read<int>();
            Position = span.Read<Vector2f>();
            Velocity = span.Read<Vector2f>();
            TargetIndex = span.Read<ushort>();
            _flags = span.Read<Flags8>();
            _flags2 = span.Read<Flags8>();
            for (var i = 0; i < AdditionalInformation.Length; ++i)
            {
                if (!_flags[i + 2])
                {
                    continue;
                }

                AdditionalInformation[i] = span.Read<float>();
                length += 4;
            }

            NetId = span.Read<short>();
            length += 2;

            if(_flags2[0])
            {
                DifficultyScalingOverride = span.Read<byte>();
                ++length;
            }
            if (_flags2[2])
            {
                StrengthMultiplierOverride = span.Read<float>();
                length += 4;
            }
            if (!_flags[7])
            {
                var lifeBytes = span.Read<byte>();
                Health = lifeBytes switch
                {
                    1 => span.Read<sbyte>(),
                    2 => span.Read<short>(),
                    4 => span.Read<int>(),
                    _ => throw new ArgumentOutOfRangeException(nameof(lifeBytes))
                };

                length += lifeBytes + 1;
            }

            if (span.Length > 0)
            {
                ReleaseOwnerIndex = span.Read<byte>();
                ++length;
            }

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(NpcIndex);
            length += span[length..].Write(Position);
            length += span[length..].Write(Velocity);
            length += span[length..].Write(TargetIndex);

            span.Write(default(byte));
            var flagsOffset = length++;

            span.Write(default(byte));
            var flags2Offset = length++;

            for (var i = 0; i < AdditionalInformation.Length; ++i)
            {
                if (AdditionalInformation[i] <= 0)
                {
                    continue;
                }

                _flags[i + 2] = true;
                length += span[length..].Write(AdditionalInformation[i]);
            }

            length += span[length..].Write(NetId);
            if (DifficultyScalingOverride > 1)
            {
                length += span[length..].Write(DifficultyScalingOverride);
            }
            if (StrengthMultiplierOverride != 0)
            {
                length += span[length..].Write(StrengthMultiplierOverride);
            }

            _flags[7] = true;
            if (Health > 0)
            {
                _flags[7] = false;
                var lifeBytes = Health switch
                {
                    var n when n > sbyte.MaxValue => sizeof(short),
                    var n when n > short.MaxValue => sizeof(int),
                    _ => sizeof(sbyte)
                };

                span[(length++)..].Write((byte) lifeBytes);
                span[length..].Write(Health);
                length += lifeBytes;
            }

            if (ReleaseOwnerIndex.HasValue)
            {
                length += span[length..].Write(ReleaseOwnerIndex.Value);
            }

            span[flagsOffset] = Unsafe.As<Flags8, byte>(ref _flags);
            span[flags2Offset] = Unsafe.As<Flags8, byte>(ref _flags2);
            return length;
        }
    }
}
