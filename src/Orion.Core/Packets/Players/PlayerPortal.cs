using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to send a player through a portal.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 19)]
    public struct PlayerPortal : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the portal color index.
        /// </summary>
        [field: FieldOffset(1)] public short PortalColorIndex { get; set; }

        /// <summary>
        /// Gets or sets the X position.
        /// </summary>
        [field: FieldOffset(3)] public float X { get; set; }

        /// <summary>
        /// Gets or sets the Y position.
        /// </summary>
        [field: FieldOffset(7)] public float Y { get; set; }

        /// <summary>
        /// Gets or sets the horizontal velocity.
        /// </summary>
        [field: FieldOffset(11)] public float VelocityX { get; set; }

        /// <summary>
        /// Gets or sets the vertical velocity.
        /// </summary>
        [field: FieldOffset(15)] public float VelocityY { get; set; }

        PacketId IPacket.Id => PacketId.PlayerPortal;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 19);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 19);
    }
}
