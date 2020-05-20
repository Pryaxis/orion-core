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

namespace Orion.Packets {
    internal static class SpanUtils {
        public static string ReadString(ref ReadOnlySpan<byte> span, Encoding encoding) {
            Debug.Assert(encoding != null);

            var length = Read7BitEncodedInt(ref span);
            var result = encoding.GetString(span[..length]);
            span = span[length..];
            return result;
        }

        public static void Write(ref Span<byte> span, string value, Encoding encoding) {
            Debug.Assert(value != null);
            Debug.Assert(encoding != null);

            var length = encoding.GetByteCount(value);
            Write7BitEncodedInt(ref span, length);
            encoding.GetBytes(value, span);
            span = span[length..];
        }

        private static int Read7BitEncodedInt(ref ReadOnlySpan<byte> span) {
            var result = 0;
            var shift = 0;
            var index = 0;
            byte b;
            do {
                if (shift == 5 * 7) {
                    // Not localized because this string is developer-facing.
                    throw new ArgumentException("Invalid 7-bit encoded integer (too large)", nameof(span));
                }

                b = span[index++];
                result |= (b & 0x7f) << shift;
                shift += 7;
            } while (b >= 0x80);

            if (result < 0) {
                // Not localized because this string is developer-facing.
                throw new ArgumentException("Invalid 7-bit encoded integer (negative)", nameof(span));
            }

            span = span[index..];
            return result;
        }

        private static void Write7BitEncodedInt(ref Span<byte> span, int value) {
            var index = 0;
            while (value >= 0x80) {
                span[index++] = (byte)(value | 0x80);
                value >>= 7;
            }
            span[index++] = (byte)value;
            span = span[index..];
        }
    }
}
