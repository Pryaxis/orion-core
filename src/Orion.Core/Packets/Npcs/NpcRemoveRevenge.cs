using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to remove an NPC's revenge.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct NpcRemoveRevenge : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the unique ID used to identify the revenge marker.
        /// </summary>
        [field: FieldOffset(0)] public int UniqueId { get; set; }

        PacketId IPacket.Id => PacketId.NpcRemoveRevenge;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 4);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 4);
    }
}
