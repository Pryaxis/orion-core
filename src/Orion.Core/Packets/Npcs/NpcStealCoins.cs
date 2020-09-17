using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent when an NPC steals coins.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct NpcStealCoins : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the NPC's index.
        /// </summary>
        [field: FieldOffset(0)] public int NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the amount of stolen money.
        /// </summary>
        [field: FieldOffset(4)] public int Value { get; set; }

        /// <summary>
        /// Gets or sets the X position. Used to ping the money. 
        /// </summary>
        [field: FieldOffset(8)] public float X { get; set; }

        /// <summary>
        /// Gets or sets the Y position. Used to ping the money.
        /// </summary>
        [field: FieldOffset(12)] public float Y { get; set; }

        PacketId IPacket.Id => PacketId.NpcStealCoins;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 16);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 16);
    }
}
