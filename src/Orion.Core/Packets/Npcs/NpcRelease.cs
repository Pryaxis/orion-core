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
using Orion.Core.Npcs;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent from the client to the server to release an NPC.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public struct NpcRelease : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.

        /// <summary>
        /// Gets or sets the NPC's X position.
        /// </summary>
        /// <value>The NPC's X position.</value>
        [field: FieldOffset(0)] public int X { get; set; }

        /// <summary>
        /// Gets or sets the NPC's Y position.
        /// </summary>
        /// <value>The NPC's Y position.</value>
        [field: FieldOffset(4)] public int Y { get; set; }

        /// <summary>
        /// Gets or sets the NPC ID.
        /// </summary>
        /// <value>The NPC ID.</value>
        [field: FieldOffset(8)] public NpcId Id { get; set; }

        /// <summary>
        /// Gets or sets the NPC style.
        /// </summary>
        /// <value>The NPC style.</value>
        [field: FieldOffset(10)] public byte Style { get; set; }

        PacketId IPacket.Id => PacketId.NpcRelease;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 11);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 11);
    }
}
