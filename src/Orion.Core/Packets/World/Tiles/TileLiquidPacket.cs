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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Orion.Core.World.Tiles;

namespace Orion.Core.Packets.World.Tiles
{
    /// <summary>
    /// A packet sent to set a tile's liquid.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct TileLiquidPacket : IPacket
    {
        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        /// <value>The tile's X coordinate.</value>
        [field: FieldOffset(0)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        /// <value>The tile's Y coordinate.</value>
        [field: FieldOffset(2)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the tile's liquid amount. This ranges from <c>0</c> to <c>255</c>.
        /// </summary>
        /// <value>The tile's liquid amount.</value>
        [field: FieldOffset(4)] public byte LiquidAmount { get; set; }

        /// <summary>
        /// Gets or sets the tile's liquid.
        /// </summary>
        /// <value>The tile's liquid.</value>
        [field: FieldOffset(5)] public Liquid Liquid { get; set; }

        PacketId IPacket.Id => PacketId.TileLiquid;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context)
        {
            Unsafe.CopyBlockUnaligned(ref this.AsRefByte(0), ref span[0], 6);
            return 6;
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context)
        {
            Unsafe.CopyBlockUnaligned(ref span[0], ref this.AsRefByte(0), 6);
            return 6;
        }
    }
}
