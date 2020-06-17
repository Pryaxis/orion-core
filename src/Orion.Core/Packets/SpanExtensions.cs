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
using System.Text;
using Orion.Core.DataStructures;

namespace Orion.Core.Packets
{
    internal static class SpanExtensions
    {
        public static int Read(this Span<byte> span, Encoding encoding, out string value)
        {
            Debug.Assert(encoding != null);

            var index = Read7BitEncodedInt(span, out var length);
            value = encoding.GetString(span[index..(index + length)]);
            return index + length;
        }

        public static int Read(this Span<byte> span, Encoding encoding, out NetworkText value)
        {
            Debug.Assert(encoding != null);

            var index = 0;
            var mode = (NetworkText.Mode)span[index++];
            index += span[index..].Read(encoding, out string text);
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

        public static int Write(this Span<byte> span, string value, Encoding encoding)
        {
            Debug.Assert(value != null);
            Debug.Assert(encoding != null);

            var length = encoding.GetByteCount(value);
            var index = Write7BitEncodedInt(span, length);
            encoding.GetBytes(value, span[index..]);
            return index + length;
        }

        public static int Write(this Span<byte> span, NetworkText value, Encoding encoding)
        {
            Debug.Assert(value != null);
            Debug.Assert(encoding != null);

            var index = 0;
            span[index++] = (byte)value._mode;
            index += span[index..].Write(value._format, encoding);

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
            var shift = 0;
            var index = 0;
            byte b;
            do
            {
                if (shift == 5 * 7)
                {
                    // Not localized because this string is developer-facing.
                    throw new ArgumentException("Invalid 7-bit encoded integer (too large)", nameof(span));
                }

                b = span[index++];
                value |= (b & 0x7f) << shift;
                shift += 7;
            } while (b >= 0x80);

            if (value < 0)
            {
                // Not localized because this string is developer-facing.
                throw new ArgumentException("Invalid 7-bit encoded integer (negative)", nameof(span));
            }

            return index;
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
