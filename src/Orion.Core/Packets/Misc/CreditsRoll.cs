using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Misc
{
    /// <summary>
    /// A packet sent to display the credits roll.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 5)]
    public struct CreditsRoll : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference. The first byte is unused.

        /// <summary>
        /// Gets or sets the remaining time.
        /// </summary>
        [field: FieldOffset(1)] public int RemainingTime { get; set; }

        PacketId IPacket.Id => PacketId.CreditsRoll;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 5);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 5);
    }
}
