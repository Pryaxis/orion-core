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
    public sealed class NpcInfo : IPacket
    {
        private const int MaxAi = 4;
        private Flags8 _flags;
        private Flags8 _flags2;
        private float[] _ai = new float[MaxAi];

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

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
        public float[] AdditionalInformation => _ai;

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
        public int Health { get; set; } = -1;

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
            var length = 0;
            NpcIndex = span.Read<short>();
            length += 2;

            Position = span.Read<Vector2f>();
            length += 8;

            Velocity = span.Read<Vector2f>();
            length += 8;

            TargetIndex = span.Read<ushort>();
            length += 2;

            _flags = span.Read<Flags8>();
            ++length;

            _flags2 = span.Read<Flags8>();
            ++length;

            for (var i = 0; i < MaxAi; ++i)
            {
                if (_flags[i + 2])
                {
                    AdditionalInformation[i] = span.Read<float>();
                    length += 4;
                }
                else
                {
                    AdditionalInformation[i] = 0F;
                }
            }

            NetId = span.Read<short>();
            length += 2;

            if (_flags2[0])
            {
                DifficultyScalingOverride = span.Read<byte>();
                ++length;
            }
            else
            {
                DifficultyScalingOverride = 1;
            }

            if (_flags2[2])
            {
                StrengthMultiplierOverride = span.Read<float>();
                length += 4;
            }
            else
            {
                StrengthMultiplierOverride = 1F;
            }

            Health = -1;
            if (!_flags[7])
            {
                var lifeBytes = span.Read<byte>();
                ++length;

                Health = lifeBytes switch
                {
                    2 => span.Read<short>(),
                    4 => span.Read<int>(),
                    _ => span.Read<sbyte>()
                };

                length += lifeBytes;
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

            for (var i = 0; i < MaxAi; i++)
            {
                _flags[i + 2] = AdditionalInformation[i] != 0F;
            }

            _flags[7] = Health < 0;
            length += span[length..].Write(_flags);

            _flags2[0] = DifficultyScalingOverride > 1;
            _flags2[1] = SpawnedFromStatue;
            _flags2[2] = StrengthMultiplierOverride != 1F;

            length += span[length..].Write(_flags2);

            for (var i = 0; i < MaxAi; ++i)
            {
                if (_flags[i + 2])
                {
                    length += span[length..].Write(AdditionalInformation[i]);
                }
            }

            length += span[length..].Write(NetId);

            if (_flags2[0])
            {
                length += span[length..].Write(DifficultyScalingOverride);
            }

            if (_flags2[2])
            {
                length += span[length..].Write(StrengthMultiplierOverride);
            }

            if (!_flags[7])
            {
                switch (Health)
                {
                case { } n when n >= short.MaxValue:
                    length += span[length..].Write((byte)sizeof(int));
                    length += span[length..].Write(Health);
                    break;
                case { } n when n >= sbyte.MaxValue:
                    length += span[length..].Write((byte)sizeof(short));
                    length += span[length..].Write((short)Health);
                    break;
                default:
                    length += span[length..].Write((byte)sizeof(sbyte));
                    length += span[length..].Write((sbyte)Health);
                    break;
                }
            }

            if (ReleaseOwnerIndex.HasValue)
            {
                length += span[length..].Write(ReleaseOwnerIndex.Value);
            }

            return length;
        }
    }
}
