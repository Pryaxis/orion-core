using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent to report invasion progress.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 10)]
    public struct InvasionProgress : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the current progress.
        /// </summary>
        [field: FieldOffset(0)] public int CurrentProgress { get; set; }

        /// <summary>
        /// Gets or sets the maximum progress.
        /// </summary>
        [field: FieldOffset(4)] public int MaxProgress { get; set; }

        /// <summary>
        /// Gets or sets the invasion icon.
        /// </summary>
        [field: FieldOffset(8)] public sbyte Icon { get; set; }

        /// <summary>
        /// Gets or sets the current wave.
        /// </summary>
        [field: FieldOffset(9)] public sbyte CurrentWave { get; set; }

        PacketId IPacket.Id => PacketId.InvasionProgress;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 10);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 10);
    }
}
