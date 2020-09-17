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

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent to notify clients of the current world time.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 9)]
    public struct WorldTime : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets a value indicating whether it is currently day time.
        /// </summary>
        [field: FieldOffset(0)] public bool IsDayTime { get; set; }

        /// <summary>
        /// Gets or sets the time. One hour in-game lasts one minute real-world.
        /// </summary>
        [field: FieldOffset(1)] public int Time { get; set; }

        /// <summary>
        /// Gets or sets a value that moves the sun texture across the Y axis.
        /// </summary>
        [field: FieldOffset(5)] public short SunOffsetY { get; set; }

        /// <summary>
        /// Gets or sets a value that moves the moon texture across the Y axis.
        /// </summary>
        [field: FieldOffset(7)] public short MoonOffsetY { get; set; }

        PacketId IPacket.Id => PacketId.WorldTime;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 9);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 9);
    }
}
