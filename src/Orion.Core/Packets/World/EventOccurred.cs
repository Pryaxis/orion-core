using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent to notify players of an event's progression.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public struct EventOccurred : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior refrence

        /// <summary>
        /// Gets or sets the event ID.
        /// </summary>
        [field: FieldOffset(0)] public short EventId { get; set; }

        PacketId IPacket.Id => PacketId.EventOccurred;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 2);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 2);
    }
}
