using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to update NPC information.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct NpcInfo : IPacket
    {
        private const int MaxAi = 4;

        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        [FieldOffset(22)] private Flags8 _flags;
        [FieldOffset(23)] private Flags8 _flags2;
        [FieldOffset(24)] private float[] _ai;
        [FieldOffset(45)] private byte _lifeOccupiedBytes;
        [FieldOffset(50)] private byte? _releaseOwnerIndex;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        [field: FieldOffset(0)] public int NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the X position.
        /// </summary>
        [field: FieldOffset(4)] public float X { get; set; }

        /// <summary>
        /// Gets or sets the Y position.
        /// </summary>
        [field: FieldOffset(8)] public float Y { get; set; }

        /// <summary>
        /// Gets or sets the X velocity.
        /// </summary>
        [field: FieldOffset(12)] public float VelocityX { get; set; }

        /// <summary>
        /// Gets or sets the Y velocity.
        /// </summary>
        [field: FieldOffset(16)] public float VelocityY { get; set; }

        /// <summary>
        /// Gets or sets the targeted player.
        /// </summary>
        [field: FieldOffset(20)] public ushort TargetIndex { get; set; }

        /// <summary>
        /// Gets additional information.
        /// </summary>
        public float[] AdditionalInformation => _ai ??= new float[MaxAi];

        /// <summary>
        /// Gets or sets the NPC's net ID.
        /// </summary>
        [field: FieldOffset(38)] public short NetId { get; set; }

        /// <summary>
        /// Gets or sets a value that defines how the NPC's difficulty scales with respect to player count changes.
        /// </summary>
        [field: FieldOffset(40)] public byte DifficultyScalingOverride { get; set; }

        /// <summary>
        /// Gets or sets the strength multiplier override.
        /// </summary>
        [field: FieldOffset(41)] public float StrengthMultiplierOverride { get; set; }

        /// <summary>
        /// Gets or sets the NPC's health.
        /// </summary>
        [field: FieldOffset(46)] public int Health { get; set; }

        /// <summary>
        /// Gets or sets a catchable NPC's owner index.
        /// </summary>
        public byte ReleaseOwnerIndex
        {
            get => _releaseOwnerIndex.HasValue ? _releaseOwnerIndex.Value : (byte) 255;
            set => _releaseOwnerIndex = value;
        }

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
            var length = span.Read(ref _bytes, 24);
            for (var i = 0; i < AdditionalInformation.Length; ++i)
            {
                if (!_flags[i + 2])
                {
                    continue;
                }

                length += span[length..].Read(ref Unsafe.Add(ref Unsafe.As<float, byte>(ref AdditionalInformation[0]), i * 4), 4);
            }

            length += span[length..].Read(ref Unsafe.Add(ref _bytes, 38), 2);

            if (_flags2[0])
            {
                length += span[length..].Read(ref Unsafe.Add(ref _bytes, 40), 1);
            }

            if (_flags2[2])
            {
                length += span[length..].Read(ref Unsafe.Add(ref _bytes, 41), 4);
            }

            if (!_flags[7])
            {
                length += span[length..].Read(ref _lifeOccupiedBytes, 1);
                length += span[length..].Read(ref Unsafe.Add(ref _lifeOccupiedBytes, 1), _lifeOccupiedBytes);
            }

            if (length < span.Length) // ReleaseOwnerIndex
            {
                length += span[length..].Read(ref Unsafe.Add(ref _bytes, 50), 1);
            }

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 24);
            for (var i = 0; i < AdditionalInformation.Length; ++i)
            {
                if (AdditionalInformation[i] == 0)
                {
                    continue;
                }

                length += span[length..].Write(ref Unsafe.Add(ref Unsafe.As<float, byte>(ref AdditionalInformation[0]), i * 4), 4);
                _flags[i + 2] = true;
            }

            length += span[length..].Write(ref Unsafe.Add(ref _bytes, 38), 2);
            if (DifficultyScalingOverride > 1)
            {
                length += span[length..].Write(ref Unsafe.Add(ref _bytes, 40), 1);
                _flags2[0] = true;
            } 

            if (StrengthMultiplierOverride != 0)
            {
                length += span[length..].Write(ref Unsafe.Add(ref _bytes, 41), 4);
                _flags2[2] = true;
            }

            _flags[7] = true;
            if (Health > 0)
            {
                _lifeOccupiedBytes = Health switch
                {
                    var n when n > sbyte.MaxValue => sizeof(short),
                    var n when n > short.MaxValue => sizeof(int),
                    _ => sizeof(sbyte)
                };

                // Write the size as well as the actual health value
                length += span[length..].Write(ref _lifeOccupiedBytes, 1 + _lifeOccupiedBytes);
                _flags[7] = false; // Indicates whether Health == NPC.maxLife
            }

            if (ReleaseOwnerIndex != 255)
            {
                length += span[length..].Write(ref Unsafe.Add(ref _bytes, 50), 1);
            }

            span[22..].Write(ref Unsafe.Add(ref _bytes, 22), 2); // Overwrite the flags with accurate information
            return length;
        }
    }
}
