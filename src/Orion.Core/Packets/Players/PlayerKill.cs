using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to kill a player.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 36)]
    public struct PlayerKill : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        [FieldOffset(32)] private byte _bytes2; // Used to obtain an interior reference
        [FieldOffset(8)] private PlayerDeathReason _deathReason;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the death reason.
        /// </summary>
        public PlayerDeathReason DeathReason
        {
            get => _deathReason;
            set => _deathReason = value;
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
        /// Gets or sets a value indicating whether the player was killed via PvP.
        /// </summary>
        [field: FieldOffset(35)] public bool IsPvP { get; set; }

        PacketId IPacket.Id => PacketId.PlayerKill;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 1);
            length += PlayerDeathReason.Read(span[length..], out _deathReason);
            length += span[length..].Read(ref _bytes2, 4);
            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 1);
            length += _deathReason.Write(span[length..]);
            length += span[length..].Write(ref _bytes2, 4);
            return length;
        }
    }
}
