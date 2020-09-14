using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent to damage a tile.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 6)]
    public struct BlockBreaking : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the index of the player breaking the block.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the block's X coordinate.
        /// </summary>
        [field: FieldOffset(1)] public short X { get; set; }

        /// <summary>
        /// Gets or set sthe block's Y coordinate.
        /// </summary>
        [field: FieldOffset(3)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the pick damage.
        /// </summary>
        [field: FieldOffset(5)] public byte PickDamage { get; set; }

        PacketId IPacket.Id => PacketId.BlockBreaking;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 6);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 6);
    }
}
