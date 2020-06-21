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
using Orion.Core.Buffs;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to set a player's buffs.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct PlayerBuffsPacket : IPacket
    {
        [field: FieldOffset(1)] private unsafe fixed short _buffs[22];

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        /// <value>The player index.</value>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets the buff IDs.
        /// </summary>
        /// <value>The buff IDs.</value>
        public unsafe Span<BuffId> Ids => MemoryMarshal.CreateSpan(ref Unsafe.As<short, BuffId>(ref _buffs[0]), 22);

        PacketId IPacket.Id => PacketId.PlayerBuffs;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context) => span.Read(ref this.AsRefByte(0), 45);

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) => span.Write(ref this.AsRefByte(0), 45);
    }
}
