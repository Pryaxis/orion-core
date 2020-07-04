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

namespace Orion.Core.Packets.World.Chests
{
    /// <summary>
    /// A packet sent from the client to the server to request a chest's name or from the server to the client to set a
    /// chest's name.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct ChestName : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(8)] private string? _name;

        /// <summary>
        /// Gets or sets the chest index. A value of <c>-1</c> in <see cref="PacketContext.Server"/> indicates that the
        /// chest index is retrieved using the chest's coordinates.
        /// </summary>
        /// <value>The chest index.</value>
        [field: FieldOffset(0)] public short ChestIndex { get; set; }

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
        /// Gets or sets the chest's name. <i>This is only applicable if read in <see cref="PacketContext.Client"/> or
        /// written in <see cref="PacketContext.Server"/>!</i>
        /// </summary>
        /// <value>The chest's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Name
        {
            get => _name ??= string.Empty;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        PacketId IPacket.Id => PacketId.ChestName;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 6);
            if (context == PacketContext.Server)
            {
                return length;
            }

            length += span[length..].Read(out _name);
            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 6);
            if (context == PacketContext.Client)
            {
                return length;
            }

            length += span[length..].Write(Name);
            return length;
        }
    }
}
