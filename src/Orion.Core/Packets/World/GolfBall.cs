using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent when a player pockets a golf ball.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 9)]
    public struct GolfBall : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the cup's X position.
        /// </summary>
        [field: FieldOffset(1)] public ushort X { get; set; }

        /// <summary>
        /// Gets or sets the cup's Y position.
        /// </summary>
        [field: FieldOffset(3)] public ushort Y { get; set; }

        /// <summary>
        /// Gets or sets the number of hits.
        /// </summary>
        [field: FieldOffset(5)] public ushort NumberOfHits { get; set; }

        /// <summary>
        /// Gets or sets the ball's projectile ID.
        /// </summary>
        [field: FieldOffset(7)] public ushort ProjectileId { get; set; }

        PacketId IPacket.Id => PacketId.GolfBall;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 9);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 9);
    }
}
