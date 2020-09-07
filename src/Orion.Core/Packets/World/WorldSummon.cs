using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent to summon a boss or an invasion.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct WorldSummon : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the player index. Enemies will spawn at this player's position.
        /// </summary>
        [field: FieldOffset(0)] public short PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC ID.
        /// </summary>
        [field: FieldOffset(2)] public short NpcId { get; set; }

        PacketId IPacket.Id => PacketId.WorldSummon;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 4);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 4);
    }
}
