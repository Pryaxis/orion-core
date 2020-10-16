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
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Schema;

namespace Orion.Core.Utils
{
    /// <summary>
    /// Provides extensions for the <see cref="Span{T}"/> structure.
    /// </summary>
    internal static class SpanExtensions
    {
        /// <summary>
        /// Returns a reference to the element at the given <paramref name="index"/>. <i>Performs no bounds
        /// checking!</i>
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="span">The span.</param>
        /// <param name="index">The index.</param>
        /// <returns>A reference to the element at the given <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T At<T>(this Span<T> span, int index)
        {
            Debug.Assert(index >= 0 && index < span.Length);

            return ref Unsafe.Add(ref MemoryMarshal.GetReference(span), index);
        }

        /// <summary>
        /// Reads <paramref name="length"/> bytes from the <paramref name="span"/> into the given
        /// <paramref name="destination"/>. Returns the number of bytes read. <i>Performs no bounds checking!</i>
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="destination">The destination to write to.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Read(this Span<byte> span, ref byte destination, int length)
        {
            Debug.Assert(length > 0);

            Unsafe.CopyBlockUnaligned(ref destination, ref span.At(0), (uint)length);
            return length;
        }

        /// <summary>
        /// Reads a value of type <typeparamref name="T"/> from the specified span. Advances the span position by number of bytes read.
        /// </summary>
        /// <typeparam name="T">The type of value to read.</typeparam>
        /// <param name="span">The span to read from.</param>
        /// <returns>The read value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Read<T>(this ref Span<byte> span) where T : unmanaged
        {
            var value = MemoryMarshal.Read<T>(span);
            var bytesRead = Unsafe.SizeOf<T>();
            span = span[bytesRead..];
            return value;
        }

        /// <summary>
        /// Reads a UTF8-encoded string from the <paramref name="span"/>. Returns the number of bytes read.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="value">The resulting string.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        public static int Read(this Span<byte> span, out string value)
        {
            var index = Read7BitEncodedInt(span, out var length);
            value = Encoding.UTF8.GetString(span[index..(index + length)]);
            return index + length;
        }

        /// <summary>
        /// Writes <paramref name="length"/> bytes into the <paramref name="span"/> from the given
        /// <paramref name="source"/>. Returns the number of bytes written. <i>Performs no bounds checking!</i>
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <param name="source">The source to read from.</param>
        /// <param name="length">The number of bytes to write.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Write(this Span<byte> span, ref byte source, int length)
        {
            Debug.Assert(length > 0);

            Unsafe.CopyBlockUnaligned(ref span.At(0), ref source, (uint)length);
            return length;
        }

        /// <summary>
        /// Writes a value of type <typeparamref name="T"/> to the underlying <paramref name="span"/> and returns the number of bytes written.
        /// </summary>
        /// <typeparam name="T">The type of value to write.</typeparam>
        /// <param name="span">The span to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <returns>The number of bytes written.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Write<T>(this Span<byte> span, T value) where T : struct
        {
            MemoryMarshal.Write(span, ref value);
            return Unsafe.SizeOf<T>();
        }

        /// <summary>
        /// Writes a UTF8-encoded string to the <paramref name="span"/>. Returns the number of bytes written.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <param name="value">The string to write.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        public static int Write(this Span<byte> span, string value)
        {
            Debug.Assert(value != null);

            var length = Encoding.UTF8.GetByteCount(value);
            var index = Write7BitEncodedInt(span, length);
            Encoding.UTF8.GetBytes(value, span[index..]);
            return index + length;
        }

        private static int Read7BitEncodedInt(Span<byte> span, out int value)
        {
            value = 0;

            var index = 0;
            for (; index < 5; ++index)
            {
                var b = span[index];
                value |= (b & 0x7f) << 7 * index;

                if (b < 0x80)
                {
                    return index + 1;
                }
            }

            throw new ArgumentException("7-bit encoded integer too large", nameof(span));
        }

        private static int Write7BitEncodedInt(Span<byte> span, int value)
        {
            var index = 0;
            while (value >= 0x80)
            {
                span[index++] = (byte)(value | 0x80);
                value >>= 7;
            }
            span[index++] = (byte)value;
            return index;
        }
    }
}
