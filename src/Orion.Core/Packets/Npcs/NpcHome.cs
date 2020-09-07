using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to set an NPC's home.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 7)]
    public struct NpcHome : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        [field: FieldOffset(0)] public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the home's X tile coordinate.
        /// </summary>
        [field: FieldOffset(2)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the home's Y tile coordinate.
        /// </summary>
        [field: FieldOffset(4)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the NPC is homeless.
        /// </summary>
        [field: FieldOffset(6)] public bool IsHomeless { get; set; }

        PacketId IPacket.Id => PacketId.NpcHome;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 7);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 7);
    }
}
