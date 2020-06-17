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

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to set a player's stealth status.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct PlayerStealthPacket : IPacket
    {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        /// <value>The player index.</value>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's stealth status. This ranges from <c>0.0f</c> to <c>1.0f</c>, indicating full
        /// stealth and no stealth, respectively.
        /// </summary>
        /// <value>The player's stealth status.</value>
        [field: FieldOffset(1)] public float Status { get; set; }

        PacketId IPacket.Id => PacketId.PlayerStealth;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context)
        {
            Unsafe.CopyBlockUnaligned(ref this.AsRefByte(0), ref span[0], 5);
            return 5;
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context)
        {
            Unsafe.CopyBlockUnaligned(ref span[0], ref this.AsRefByte(0), 5);
            return 5;
        }
    }
}
