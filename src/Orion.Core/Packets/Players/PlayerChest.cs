using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to set a player's selected chest index.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 3)]
    public struct PlayerChest : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        [field: FieldOffset(1)] public short ChestIndex { get; set; }

        PacketId IPacket.Id => PacketId.PlayerChest;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 3);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 3);
    }
}
