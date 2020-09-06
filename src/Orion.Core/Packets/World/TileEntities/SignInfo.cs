using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;
using Serilog.Data;

namespace Orion.Core.Packets.World.TileEntities
{
    /// <summary>
    /// A packet sent to display and edit signs.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 10)]
    public struct SignInfo : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        [FieldOffset(8)] private string? _text;
        [FieldOffset(16)] private byte _bytes2;
        [FieldOffset(17)] private Flags8 _flags;

        /// <summary>
        /// Gets or sets the sign index.
        /// </summary>
        [field: FieldOffset(0)] public byte SignIndex { get; set; }

        /// <summary>
        /// Gets or sets the sign's X coordinate.
        /// </summary>
        [field: FieldOffset(1)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the sign's Y coordinate.
        /// </summary>
        [field: FieldOffset(3)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(16)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the sign text.
        /// </summary>
        public string Text
        {
            get => _text ??= string.Empty;
            set => _text = value ?? throw new ArgumentNullException(nameof(value));
        }

        // TODO: Requires deeper verification!
        /// <summary>
        /// Gets or sets a value indicating whether the sign is a tombstone.
        /// </summary>
        public bool IsTombstone
        {
            get => _flags[0];
            set => _flags[0] = value;
        }

        PacketId IPacket.Id => PacketId.SignInfo;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 5);
            length += span[length..].Read(out _text);
            length += span[length..].Read(ref _bytes2, 2);
            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 5);
            length += span[length..].Write(Text);
            length += span[length..].Write(ref _bytes2, 2);
            return length;
        }
    }
}
