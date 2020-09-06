using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent to update world biomes.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 3)]
    public struct WorldBiomes : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the percentage of 'Good'.
        /// </summary>
        [field: FieldOffset(0)] public byte Good { get; set; }

        /// <summary>
        /// Gets or sets the percentage of 'Evil'.
        /// </summary>
        [field: FieldOffset(1)] public byte Evil { get; set; }

        /// <summary>
        /// Gets or sets the percentage of 'Crimson'.
        /// </summary>
        [field: FieldOffset(2)] public byte Crimson { get; set; }

        PacketId IPacket.Id => PacketId.WorldBiomes;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 3);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 3);
    }
}
