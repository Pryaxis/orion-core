using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World.Tiles
{
    /// <summary>
    /// A packet sent to set a tile animation.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct TileAnimation : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the animation type.
        /// </summary>
        [field: FieldOffset(0)] public short AnimationType { get; set; }

        /// <summary>
        /// Gets or sets the tile type.
        /// </summary>
        [field: FieldOffset(2)] public short TileType { get; set; }

        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        [field: FieldOffset(4)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        [field: FieldOffset(6)] public short Y { get; set; }

        PacketId IPacket.Id => PacketId.TileAnimation;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 8);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 8);
    }
}
