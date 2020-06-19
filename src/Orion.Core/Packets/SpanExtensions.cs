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
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.DataStructures;

namespace Orion.Core.Packets
{
    /// <summary>
    /// Provides extension for the <see cref="Span{Byte}"/> structure.
    /// </summary>
    internal static class SpanExtensions
    {
        /// <summary>
        /// Reads <paramref name="length"/> bytes from the <paramref name="span"/> into the given
        /// <paramref name="destination"/>. Returns the number of bytes read.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="destination">The destination to write to.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Read(this Span<byte> span, ref byte destination, int length)
        {
            Debug.Assert(length > 0);

            Unsafe.CopyBlockUnaligned(ref destination, ref MemoryMarshal.GetReference(span), (uint)length);
            return length;
        }

        /// <summary>
        /// Reads an encoded string from the <paramref name="span"/> with the given <paramref name="encoding"/>. Returns
        /// the number of bytes read.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="value">The resulting string.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        public static int Read(this Span<byte> span, Encoding encoding, out string value)
        {
            Debug.Assert(encoding != null);

            var index = Read7BitEncodedInt(span, out var length);
            value = encoding.GetString(span[index..(index + length)]);
            return index + length;
        }

        /// <summary>
        /// Reads an encoded <see cref="NetworkText"/> instance from the <paramref name="span"/> with the given
        /// <paramref name="encoding"/>. Returns the number of bytse read.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="value">The resulting <see cref="NetworkText"/> instance.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        public static int Read(this Span<byte> span, Encoding encoding, out NetworkText value)
        {
            Debug.Assert(encoding != null);

            var mode = (NetworkText.Mode)span[0];
            var index = 1 + span[1..].Read(encoding, out string text);
            var substitutions = Array.Empty<NetworkText>();

            byte numSubstitutions = 0;
            if (mode != NetworkText.Mode.Literal)
            {
                numSubstitutions = span[index++];
                substitutions = new NetworkText[numSubstitutions];
            }

            for (var i = 0; i < numSubstitutions; ++i)
            {
                index += Read(span[index..], encoding, out substitutions[i]);
            }

            value = new NetworkText(mode, text, substitutions);
            return index;
        }

        /// <summary>
        /// Writes <paramref name="length"/> bytes into the <paramref name="span"/> from the given
        /// <paramref name="source"/>. Returns the number of bytes written.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <param name="source">The source to read from.</param>
        /// <param name="length">The number of bytes to write.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Write(this Span<byte> span, ref byte source, int length)
        {
            Debug.Assert(length > 0);

            Unsafe.CopyBlockUnaligned(ref MemoryMarshal.GetReference(span), ref source, (uint)length);
            return length;
        }

        /// <summary>
        /// Writes an encoded string to the <paramref name="span"/> with the given <paramref name="encoding"/>. Returns
        /// the number of bytes written.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <param name="value">The string to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        public static int Write(this Span<byte> span, string value, Encoding encoding)
        {
            Debug.Assert(value != null);
            Debug.Assert(encoding != null);

            var length = encoding.GetByteCount(value);
            var index = Write7BitEncodedInt(span, length);
            encoding.GetBytes(value, span[index..]);
            return index + length;
        }

        /// <summary>
        /// Writes an encoded <see cref="NetworkText"/> instance to the <paramref name="span"/> with the given
        /// <paramref name="encoding"/>. Returns the number of bytes written.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <param name="value">The <see cref="NetworkText"/> instance to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        public static int Write(this Span<byte> span, NetworkText value, Encoding encoding)
        {
            Debug.Assert(value != null);
            Debug.Assert(encoding != null);
            
            span[0] = (byte)value._mode;
            var index = 1 + span[1..].Write(value._format, encoding);

            byte numSubstitutions = 0;
            if (value._mode != NetworkText.Mode.Literal)
            {
                numSubstitutions = (byte)value._args.Length;
                span[index++] = numSubstitutions;
            }

            for (var i = 0; i < numSubstitutions; ++i)
            {
                index += span[index..].Write(value._args[i], encoding);
            }

            return index;
        }

        private static int Read7BitEncodedInt(Span<byte> span, out int value)
        {
            value = 0;

            var index = 0;
            for (; index < 5; ++index)
            {
                var b = span[index];
                value |= (b & 0x7f) << (7 * index);

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
