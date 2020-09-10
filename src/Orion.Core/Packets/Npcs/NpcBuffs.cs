using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
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
        [FieldOffset(8)] private NpcBuff[]? _buffs;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        [field: FieldOffset(0)] public short NpcIndex { get; set; }

        /// <summary>
        /// Gets the buffs.
        /// </summary>
        public NpcBuff[] Buffs => _buffs ??= new NpcBuff[MaxNpcBuffs];

        PacketId IPacket.Id => PacketId.NpcBuffs;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 22);
            //for (var i = 0; i < Buffs.Length; ++i)
            //{
            //    length += NpcBuff.Read(span[length..], out Buffs[i]);
            //}

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 22);
            //for (var i = 0; i < Buffs.Length; ++i)
            //{
            //    length += Buffs[i].Write(span[length..]);
            //}

            return length;
        }
    }

    /// <summary>
    /// Represents a serializable NPC buff.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct NpcBuff
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the type of buff.
        /// </summary>
        [field: FieldOffset(0)] public ushort Type { get; set; }

        /// <summary>
        /// Gets or sets the buff time.
        /// </summary>
        [field: FieldOffset(2)] public short Time { get; set; }

        /// <summary>
        /// Writes the current buff to the specified span and returns the number of bytes written.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <returns>The number of bytes written.</returns>
        public int Write(Span<byte> span) => span.Write(ref _bytes, 4);

        /// <summary>
        /// Reads an <see cref="NpcBuff"/> from the specified span and returns the number of bytes read.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="npcBuff">The resulting <see cref="NpcBuff"/></param>
        /// <returns>The read <see cref="NpcBuff"/>.</returns>
        public static int Read(Span<byte> span, out NpcBuff npcBuff)
        {
            npcBuff = default;
            return span.Read(ref npcBuff._bytes, 4);
        }
    }
}
