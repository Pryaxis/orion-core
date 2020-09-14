using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to quick stack an item.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 1)]
    public struct PlayerQuickStack : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the slot that denotes the quick-stacked item.
        /// </summary>
        [field: FieldOffset(0)] public byte InventorySlot { get; set; }

        PacketId IPacket.Id => PacketId.PlayerQuickStack;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 1);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 1);
    }
}
