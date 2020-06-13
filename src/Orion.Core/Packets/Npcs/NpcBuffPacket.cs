﻿// Copyright (c) 2020 Pryaxis & Orion Contributors
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

namespace Orion.Core.Packets.Npcs {
    /// <summary>
    /// Packet sent to buff an NPC.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct NpcBuffPacket : IPacket {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        /// <value>The NPC index.</value>
        [field: FieldOffset(0)] public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the buff ID.
        /// </summary>
        /// <value>The buff ID.</value>
        [field: FieldOffset(2)] public BuffId Id { get; set; }

        /// <summary>
        /// Gets or sets the buff time, in ticks.
        /// </summary>
        /// <value>The buff time, in ticks.</value>
        [field: FieldOffset(4)] public short Ticks { get; set; }

        PacketId IPacket.Id => PacketId.NpcBuff;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context) {
            Unsafe.CopyBlockUnaligned(ref this.AsRefByte(0), ref span[0], 6);
            return 6;
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) {
            Unsafe.CopyBlockUnaligned(ref span[0], ref this.AsRefByte(0), 6);
            return 6;
        }
    }
}