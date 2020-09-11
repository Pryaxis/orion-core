using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to hurt a player.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 37)]
    public struct PlayerHurt : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        [FieldOffset(32)] private byte _bytes2; // Used to obtain an interior reference
        [FieldOffset(8)] private PlayerDeathReason _context;
        [FieldOffset(35)] private Flags8 _flags;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        public PlayerDeathReason Context
        {
            get => _context;
            set => _context = value;
        }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        [field: FieldOffset(32)] public short Damage { get; set; }

        /// <summary>
        /// Gets or sets the hit direction.
        /// </summary>
        [field: FieldOffset(34)] public byte HitDirection { get; set; }

        /// <summary>
        /// Gets or sets the cooldown counter.
        /// </summary>
        [field: FieldOffset(36)] public sbyte CooldownCounter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is critical.
        /// </summary>
        public bool IsCritical
        {
            get => _flags[0];
            set => _flags[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether damage was done via PvP.
        /// </summary>
        public bool IsPvP
        {
            get => _flags[1];
            set => _flags[1] = value;
        }

        PacketId IPacket.Id => PacketId.PlayerHurt;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 1);
            length += PlayerDeathReason.Read(span[length..], out _context);
            length += span[length..].Read(ref _bytes2, 5);
            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 1);
            length += _context.Write(span[length..]);
            length += span[length..].Write(ref _bytes2, 5);
            return length;
        }
    }
}
