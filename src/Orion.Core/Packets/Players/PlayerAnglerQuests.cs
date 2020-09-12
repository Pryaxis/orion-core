using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to set the number of angler quests completed, as well as accumulated golfer score.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 9)]
    public struct PlayerAnglerQuests : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the number of finished angler quests.
        /// </summary>
        [field: FieldOffset(1)] public int AnglerQuestsFinished { get; set; }

        /// <summary>
        /// Gets or sets the accumulated golfer score.
        /// </summary>
        [field: FieldOffset(5)] public int GolferScoreAccumulated { get; set; }

        PacketId IPacket.Id => PacketId.PlayerAnglerQuests;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 9);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 9);
    }
}
