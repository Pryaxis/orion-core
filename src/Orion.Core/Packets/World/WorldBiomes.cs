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

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent to update world biomes.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 3)]
    public struct WorldBiomes : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the percentage of 'Good'.
        /// </summary>
        [field: FieldOffset(0)] public byte Good { get; set; }

        /// <summary>
        /// Gets or sets the percentage of 'Evil'.
        /// </summary>
        [field: FieldOffset(1)] public byte Evil { get; set; }

        /// <summary>
        /// Gets or sets the percentage of 'Crimson'.
        /// </summary>
        [field: FieldOffset(2)] public byte Crimson { get; set; }

        PacketId IPacket.Id => PacketId.WorldBiomes;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 3);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 3);
    }
}
