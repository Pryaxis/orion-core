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
    /// A packet sent to teleport NPC's through portals.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct NpcPortal : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        [field: FieldOffset(0)] public ushort NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the portal color index.
        /// </summary>
        [field: FieldOffset(2)] public short PortalColorIndex { get; set; }

        /// <summary>
        /// Gets or sets the new position.
        /// </summary>
        [field: FieldOffset(4)] public Vector2f NewPosition { get; set; }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        [field: FieldOffset(12)] public Vector2f Velocity { get; set; }

        PacketId IPacket.Id => PacketId.NpcPortal;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 20);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 20);
    }
}
