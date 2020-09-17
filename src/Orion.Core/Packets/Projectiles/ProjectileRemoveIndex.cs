using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Projectiles
{
    /// <summary>
    /// A packet sent to remove intersecting portals.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 3)]
    public struct ProjectileRemoveIndex : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the owner index.
        /// </summary>
        [field: FieldOffset(0)] public ushort OwnerIndex { get; set; }

        /// <summary>
        /// Gets or sets the portal form.
        /// </summary>
        [field: FieldOffset(2)] public byte PortalForm { get; set; }

        PacketId IPacket.Id => PacketId.ProjectileRemoveIndex;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 3);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 3);
    }
}
