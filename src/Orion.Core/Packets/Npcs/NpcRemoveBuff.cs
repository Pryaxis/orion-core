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
using Orion.Core.Entities;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent from the client to the server to remove a buff from an NPC.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct NpcRemoveBuff : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        /// <value>The NPC index.</value>
        [field: FieldOffset(0)] public short Index { get; set; }

        /// <summary>
        /// Gets or sets the buff ID.
        /// </summary>
        /// <value>The buff ID.</value>
        [field: FieldOffset(2)] public BuffId Id { get; set; }

        PacketId IPacket.Id => PacketId.NpcRemoveBuff;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 4);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 4);
    }
}
