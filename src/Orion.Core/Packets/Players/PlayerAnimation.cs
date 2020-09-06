using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to set an item animation.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 7)]
    public struct PlayerAnimation : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the item rotation.
        /// </summary>
        [field: FieldOffset(1)] public float ItemRotation { get; set; }

        /// <summary>
        /// Gets or sets the animation time (in frames).
        /// </summary>
        [field: FieldOffset(5)] public short AnimationTime { get; set; }

        PacketId IPacket.Id => PacketId.PlayerAnimation;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 7);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 7);
    }
}
