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
using System.Runtime.InteropServices;
using Orion.Core.Utils;

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    /// <summary>
    /// Represents a serializable sign.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct SerializableSign
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(8)] private string? _text;

        /// <summary>
        /// Gets or sets the sign's index.
        /// </summary>
        /// <value>The sign's index.</value>
        [field: FieldOffset(0)] public short Index { get; set; }

        /// <summary>
        /// Gets or sets the sign's X coordinate.
        /// </summary>
        /// <value>The sign's X coordinate.</value>
        [field: FieldOffset(2)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the sign's Y coordinate.
        /// </summary>
        /// <value>The sign's Y coordinate.</value>
        [field: FieldOffset(4)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the sign's text.
        /// </summary>
        /// <value>The sign's text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Text
        {
            get => _text ??= string.Empty;
            set => _text = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Writes the sign to the given <paramref name="span"/>. Returns the number of bytes written to the
        /// <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        public int Write(Span<byte> span)
        {
            var length = span.Write(ref _bytes, 6);
            length += span[length..].Write(Text);
            return length;
        }

        /// <summary>
        /// Reads a <see cref="SerializableSign"/> instance from the given <paramref name="span"/>. Returns the number
        /// of bytes read from the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="sign">The resulting <see cref="SerializableSign"/> instance.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        public static int Read(Span<byte> span, out SerializableSign sign)
        {
            sign = default;

            var length = span.Read(ref sign._bytes, 6);
            length += span[length..].Read(out sign._text);
            return length;
        }
    }
}
