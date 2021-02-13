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
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World.TileEntities
{
    /// <summary>
    /// A packet sent to display and edit signs.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 10)]
    public struct SignInfo : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        [FieldOffset(8)] private string? _text;
        [FieldOffset(16)] private byte _bytes2;
        [FieldOffset(17)] private Flags8 _flags;

        /// <summary>
        /// Gets or sets the sign index.
        /// </summary>
        [field: FieldOffset(0)] public short SignIndex { get; set; }

        /// <summary>
        /// Gets or sets the sign's X coordinate.
        /// </summary>
        [field: FieldOffset(2)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the sign's Y coordinate.
        /// </summary>
        [field: FieldOffset(4)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(16)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the sign text.
        /// </summary>
        public string Text
        {
            get => _text ??= string.Empty;
            set => _text = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets a value indicating whether the sign is a tombstone.
        /// </summary>
        public bool IsTombstone
        {
            get => _flags[0];
            set => _flags[0] = value;
        }

        PacketId IPacket.Id => PacketId.SignInfo;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 6);
            length += span[length..].Read(out _text);
            length += span[length..].Read(ref _bytes2, 2);
            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 6);
            length += span[length..].Write(Text);
            length += span[length..].Write(ref _bytes2, 2);
            return length;
        }
    }
}
