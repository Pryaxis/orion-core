using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to set an NPC's immunity.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 9)]
    public struct NpcImmunity : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        [FieldOffset(3)] private byte _bytes2; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        [field: FieldOffset(0)] public ushort NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether immunity should be updated.
        /// </summary>
        [field: FieldOffset(2)] public bool ShouldUpdateImmunity { get; set; }

        /// <summary>
        /// Gets or sets the immune time.
        /// </summary>
        [field: FieldOffset(3)] public int ImmuneTime { get; set; }

        /// <summary>
        /// Gets or sets the index of the player the NPC is immune to.
        /// </summary>
        [field: FieldOffset(7)] public short PlayerIndex { get; set; }

        PacketId IPacket.Id => PacketId.NpcImmunity;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 3);
            if (ShouldUpdateImmunity)
            {
                length += span[length..].Read(ref _bytes2, 6);
            }

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 3);
            if (ShouldUpdateImmunity)
            {
                length += span[length..].Write(ref _bytes2, 6);
            }

            return length;
        }
    }
}
