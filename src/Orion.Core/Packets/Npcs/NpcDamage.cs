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
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to damage an NPC.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public struct NpcDamage : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        /// <value>The NPC index.</value>
        [field: FieldOffset(0)] public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the damage. A value of <c>-1</c> in <see cref="PacketContext.Client"/> indicates that the NPC
        /// is being removed.
        /// </summary>
        /// <value>The damage.</value>
        [field: FieldOffset(2)] public short Damage { get; set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        /// <value>The knockback.</value>
        [field: FieldOffset(4)] public float Knockback { get; set; }

        /// <summary>
        /// Gets or sets the hit direction.
        /// </summary>
        /// <value>The hit direction.</value>
        [field: FieldOffset(8)] public HitDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a critical hit.
        /// </summary>
        /// <value><see langword="true"/> if this is a critical hit; otherwise, <see langword="false"/>.</value>
        [field: FieldOffset(9)] public bool IsCritical { get; set; }

        PacketId IPacket.Id => PacketId.NpcDamage;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 10);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 10);
    }
}
