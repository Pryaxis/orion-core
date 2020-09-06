using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
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
