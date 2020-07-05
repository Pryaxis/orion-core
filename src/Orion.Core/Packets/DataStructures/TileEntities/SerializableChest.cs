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
    /// Represents a serializable chest.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct SerializableChest
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(8)] private string? _name;

        /// <summary>
        /// Gets or sets the chest's index.
        /// </summary>
        /// <value>The chest's index.</value>
        [field: FieldOffset(0)] public short Index { get; set; }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        /// <value>The chest's X coordinate.</value>
        [field: FieldOffset(2)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        /// <value>The chest's Y coordinate.</value>
        [field: FieldOffset(4)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the chest's name.
        /// </summary>
        /// <value>The chest's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Name
        {
            get => _name ??= string.Empty;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Writes the chest to the given <paramref name="span"/>. Returns the number of bytes written to the
        /// <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        public int Write(Span<byte> span)
        {
            var length = span.Write(ref _bytes, 6);
            length += span[length..].Write(Name);
            return length;
        }

        /// <summary>
        /// Reads a <see cref="SerializableChest"/> instance from the given <paramref name="span"/>. Returns the number
        /// of bytes read from the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="chest">The resulting <see cref="SerializableChest"/> instance.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        public static int Read(Span<byte> span, out SerializableChest chest)
        {
            chest = default;

            var length = span.Read(ref chest._bytes, 6);
            length += span[length..].Read(out chest._name);
            return length;
        }
    }
}
