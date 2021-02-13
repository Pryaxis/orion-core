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

namespace Orion.Core.Packets.World.TileEntities
{
    /// <summary>
    /// A packet sent to synchronize occupied chest indices across clients or even trigger a chest rename when sent from client to server.
    /// </summary>
    /// <remarks>This packet triggers a <see cref="ChestName"/> only when a valid <see cref="Name"/> is supplied.</remarks>
    [StructLayout(LayoutKind.Explicit, Size = 27)]
    public struct ChestInfo : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        [FieldOffset(6)] private readonly byte _nameLength;
        [FieldOffset(8)] private string? _name;

        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        [field: FieldOffset(0)] public short ChestIndex { get; set; }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        [field: FieldOffset(2)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        [field: FieldOffset(4)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the chest's name.
        /// </summary>
        public string Name
        {
            get => _name ??= string.Empty;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        PacketId IPacket.Id => PacketId.ChestInfo;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 7);
            if (_nameLength > 0 && _nameLength <= 20)
            {
                length += span[length..].Read(out _name);
            }

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 7);
            if (Name.Length > 0 && Name.Length <= 20)
            {
                length += span[length..].Write(Name);
            }

            return length;
        }
    }
}
