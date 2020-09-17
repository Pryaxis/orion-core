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
    /// A packet sent to issue chest modifications, i.e placing and destroying chests.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 9)]
    public struct ChestModify : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the type of modification.
        /// </summary>
        [field: FieldOffset(0)] public ChestModification Modification { get; set; }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        [field: FieldOffset(1)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        [field: FieldOffset(3)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the chest style.
        /// </summary>
        [field: FieldOffset(5)] public short Style { get; set; }

        /// <summary>
        /// Gets or sets the chest index. This specifies the chest to destroy when the corresponding modification is supplied.
        /// </summary>
        [field: FieldOffset(7)] public short ChestIndex { get; set; }

        PacketId IPacket.Id => PacketId.ChestModify;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 9);

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 9);
            if (context == PacketContext.Client)
            {
                // Only the server sends the chest index
                span[(length - 2)..].Fill(0);
            }

            return length;
        }
    }

    /// <summary>
    /// Specifies a <see cref="ChestModify"/> action.
    /// </summary>
    public enum ChestModification : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        PlaceChest,
        DestroyChest,
        PlaceDresser,
        DestroyDresser,
        PlaceContainer,
        DestroyContainer
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
