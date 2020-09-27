using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Orion.Core.Utils
{
    /// <summary>
    ///     Represents a packed two float component vector.
    /// </summary>
    /// <remarks>
    ///     The difference between <see cref="PackedVector2f" /> and <see cref="Vector2f" /> is that a
    ///     <see cref="PackedVector2f" /> is read/sent as a packed value comprised of both components, whereas with a
    ///     <see cref="Vector2f" /> both components are written to memory.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct PackedVector2f : IEquatable<PackedVector2f>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PackedVector2f" /> structure with the specified components.
        /// </summary>
        /// <param name="x">The X component.</param>
        /// <param name="y">The Y component.</param>
        public PackedVector2f(float x, float y)
        {
            PackedValue = 0;
            Vector2 = new Vector2f(x, y);
        }

        /// <summary>
        ///     Gets or sets the underlying <see cref="Vector2f" />.
        /// </summary>
        public Vector2f Vector2
        {
            get => new Vector2f(Unpack((ushort)PackedValue), Unpack((ushort)(PackedValue >> 0x10)));
            set
            {
                uint packedX = Pack(BitConverter.ToInt32(BitConverter.GetBytes(value.X)));
                var packedY = (uint)(Pack(BitConverter.ToInt32(BitConverter.GetBytes(value.Y))) << 0x10);
                PackedValue = packedX | packedY;
            }
        }

        /// <summary>
        ///     Gets the packed value.
        /// </summary>
        [field: FieldOffset(0)] public uint PackedValue { get; private set; }

        /// <inheritdoc />
        public bool Equals(PackedVector2f other)
        {
            return PackedValue == other.PackedValue;
        }

        // See https://github.com/MonoGame/MonoGame/blob/6f34eb393aa0ac005888d74c5c4c6ab5615fdc8c/MonoGame.Framework/Graphics/PackedVector/HalfTypeHelper.cs
        [StructLayout(LayoutKind.Explicit)]
        private struct Uif
        {
            [FieldOffset(0)] public readonly float F;
            [FieldOffset(0)] public readonly int I;
            [FieldOffset(0)] public uint U;
        }

        [ExcludeFromCodeCoverage]
        private static ushort Pack(int i)
        {
            var s = (i >> 16) & 0x00008000;
            var e = ((i >> 23) & 0x000000ff) - (127 - 15);
            var m = i & 0x007fffff;

            if (e <= 0)
            {
                if (e < -10)
                {
                    return (ushort)s;
                }

                m = m | 0x00800000;

                var t = 14 - e;
                var a = (1 << (t - 1)) - 1;
                var b = (m >> t) & 1;

                m = (m + a + b) >> t;

                return (ushort)(s | m);
            }

            if (e == 0xff - (127 - 15))
            {
                if (m == 0)
                {
                    return (ushort)(s | 0x7c00);
                }

                m >>= 13;
                return (ushort)(s | 0x7c00 | m | (m == 0 ? 1 : 0));
            }

            m = m + 0x00000fff + ((m >> 13) & 1);

            if ((m & 0x00800000) != 0)
            {
                m = 0;
                e += 1;
            }

            if (e > 30)
            {
                return (ushort)(s | 0x7c00);
            }

            return (ushort)(s | (e << 10) | (m >> 13));
        }

        [ExcludeFromCodeCoverage]
        private static float Unpack(ushort value)
        {
            uint rst;
            var mantissa = (uint)(value & 1023);
            var exp = 0xfffffff2;

            if ((value & -33792) == 0)
            {
                if (mantissa != 0)
                {
                    while ((mantissa & 1024) == 0)
                    {
                        exp--;
                        mantissa = mantissa << 1;
                    }

                    mantissa &= 0xfffffbff;
                    rst = (((uint)value & 0x8000) << 16) | ((exp + 127) << 23) | (mantissa << 13);
                }
                else
                {
                    rst = (uint)((value & 0x8000) << 16);
                }
            }
            else
            {
                rst = (((uint)value & 0x8000) << 16) | ((((((uint)value >> 10) & 0x1f) - 15) + 127) << 23) |
                      (mantissa << 13);
            }

            var uif = new Uif {U = rst};
            return uif.F;
        }
    }
}
