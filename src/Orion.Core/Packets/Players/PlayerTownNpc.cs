using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to set the interaction between a player and a town NPC.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 3)]
    public struct PlayerTownNpc : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the town NPC index.
        /// </summary>
        [field: FieldOffset(1)] public short NpcIndex { get; set; }

        PacketId IPacket.Id => PacketId.PlayerTownNpc;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 3);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 3);
    }
}
