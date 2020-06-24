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

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent from the server to the client to set Moon Lord information.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct MoonLordInfoPacket : IPacket
    {
        /// <summary>
        /// Gets or sets the number of ticks before Moon Lord spawns.
        /// </summary>
        /// <value>The number of ticks before Moon Lord spawns.</value>
        [field: FieldOffset(0)] public int TicksBeforeSpawn { get; set; }

        PacketId IPacket.Id => PacketId.MoonLordInfo;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context) => span.Read(ref this.AsRefByte(0), 4);

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) => span.Write(ref this.AsRefByte(0), 4);
    }
}
