using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet used to send an emoji.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public struct PlayerEmoji : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)]
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the emoji ID.
        /// </summary>
        [field: FieldOffset(1)]
        public byte EmojiId { get; set; }

        PacketId IPacket.Id => PacketId.PlayerEmoji;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 2);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 2);
    }
}
