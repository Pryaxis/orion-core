using System;
using System.Runtime.InteropServices;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Projectiles
{
    /// <summary>
    /// A packet sent to shoot projectiles from cannons.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 15)]
    public struct ProjectileCannon : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        [field: FieldOffset(0)] public short Damage { get; set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        [field: FieldOffset(2)] public float Knockback { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        [field: FieldOffset(6)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        [field: FieldOffset(8)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the shooting angle.
        /// </summary>
        [field: FieldOffset(10)] public short Angle { get; set; }

        /// <summary>
        /// Gets or sets the ammo.
        /// </summary>
        [field: FieldOffset(12)] public short Ammo { get; set; }

        /// <summary>
        /// Gets or sets the owner index.
        /// </summary>
        [field: FieldOffset(14)] public byte OwnerIndex { get; set; }

        PacketId IPacket.Id => PacketId.ProjectileCannon;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 15);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 15);
    }
}
