using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Projectiles
{
    /// <summary>
    /// A packet sent to destroy a projectile.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 3)]
    public struct ProjectileRemove : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the projectile ID.
        /// </summary>
        [field: FieldOffset(0)] public short ProjectileId { get; set; }

        /// <summary>
        /// Gets or sets the projectile owner's index.
        /// </summary>
        [field: FieldOffset(2)] public byte OwnerId { get; set; }

        PacketId IPacket.Id => PacketId.ProjectileRemove;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 3);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 3);
    }
}
