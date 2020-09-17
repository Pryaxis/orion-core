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
    /// A packet sent to set an NPC's immunity.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 9)]
    public struct NpcImmunity : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        [FieldOffset(3)] private byte _bytes2; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        [field: FieldOffset(0)] public ushort NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether immunity should be updated.
        /// </summary>
        [field: FieldOffset(2)] public bool ShouldUpdateImmunity { get; set; }

        /// <summary>
        /// Gets or sets the immune time.
        /// </summary>
        [field: FieldOffset(3)] public int ImmuneTime { get; set; }

        /// <summary>
        /// Gets or sets the index of the player the NPC is immune to.
        /// </summary>
        [field: FieldOffset(7)] public short PlayerIndex { get; set; }

        PacketId IPacket.Id => PacketId.NpcImmunity;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 3);
            if (ShouldUpdateImmunity)
            {
                length += span[length..].Read(ref _bytes2, 6);
            }

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 3);
            if (ShouldUpdateImmunity)
            {
                length += span[length..].Write(ref _bytes2, 6);
            }

            return length;
        }
    }
}
