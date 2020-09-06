using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World.TileEntities
{
    /// <summary>
    /// A packet sent to unblock occupied chests.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 27, CharSet = CharSet.Auto)]
    public struct ChestInfo : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        [FieldOffset(6)] private readonly byte _nameLength;
        [FieldOffset(8)] private string? _name;

        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        [field: FieldOffset(0)] public short ChestIndex { get; set; }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        [field: FieldOffset(2)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        [field: FieldOffset(4)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the chest's name.
        /// </summary>
        public string Name
        {
            get => _name ??= string.Empty;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        PacketId IPacket.Id => PacketId.ChestInfo;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 6);
            if (span[length] > 0 && span[length] <= 20)
            {
                length += span[length..].Read(out _name);
            }

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 6);
            if (Name.Length > 0 && Name.Length <= 20)
            {
                length += span[length..].Write(Name);
            }

            return length;
        }
    }
}
