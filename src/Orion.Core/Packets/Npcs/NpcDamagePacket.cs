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

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to damage an NPC.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct NpcDamagePacket : IPacket
    {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        /// <value>The NPC index.</value>
        [field: FieldOffset(0)] public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the damage. If <c>-1</c> and read in <see cref="PacketContext.Client"/>, then the NPC is
        /// removed.
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

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context) => span.Read(ref this.AsRefByte(0), 10);

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) => span.Write(ref this.AsRefByte(0), 10);

        /// <summary>
        /// Specifies the hit direction in a <see cref="NpcDamagePacket"/>.
        /// </summary>
        public enum HitDirection : byte
        {
            /// <summary>
            /// Indicates left.
            /// </summary>
            Left = 0,

            /// <summary>
            /// Indicates right.
            /// </summary>
            Right = 2
        }
    }
}
