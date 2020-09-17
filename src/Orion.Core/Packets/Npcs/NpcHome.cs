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

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to set an NPC's home.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 7)]
    public struct NpcHome : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        [field: FieldOffset(0)] public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the home's X tile coordinate.
        /// </summary>
        [field: FieldOffset(2)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the home's Y tile coordinate.
        /// </summary>
        [field: FieldOffset(4)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the NPC is homeless.
        /// </summary>
        [field: FieldOffset(6)] public bool IsHomeless { get; set; }

        PacketId IPacket.Id => PacketId.NpcHome;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 7);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 7);
    }
}
