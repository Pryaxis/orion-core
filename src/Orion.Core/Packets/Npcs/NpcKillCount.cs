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
    /// A packet sent from the server to the client to set an NPC ID's kill count.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct NpcKillCount : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the NPC ID.
        /// </summary>
        /// <value>The NPC ID.</value>
        [field: FieldOffset(0)] public NpcId Id { get; set; }
        
        /// <summary>
        /// Gets or sets the NPC ID's kill count.
        /// </summary>
        /// <value>The NPC ID's kill count.</value>
        [field: FieldOffset(2)] public int KillCount { get; set; }

        PacketId IPacket.Id => PacketId.NpcKillCount;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 6);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 6);
    }
}
