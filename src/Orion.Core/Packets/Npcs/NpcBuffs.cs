using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to update NPC buffs.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 22)]
    public struct NpcBuffs : IPacket
    {
        private const int MaxNpcBuffs = 5;

        [FieldOffset(0)] private byte _bytes;
        [FieldOffset(8)] private SerializableNpcBuff[]? _buffs;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        [field: FieldOffset(0)] public short NpcIndex { get; set; }

        /// <summary>
        /// Gets the buffs.
        /// </summary>
        public SerializableNpcBuff[] Buffs => _buffs ??= new SerializableNpcBuff[MaxNpcBuffs];

        PacketId IPacket.Id => PacketId.NpcBuffs;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 2);
            for (var i = 0; i < Buffs.Length; ++i)
            {
                length += SerializableNpcBuff.Read(span[length..], out Buffs[i]);
            }

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 2);
            for (var i = 0; i < Buffs.Length; ++i)
            {
                length += Buffs[i].Write(span[length..]);
            }

            return length;
        }
    }
}
