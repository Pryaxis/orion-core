using System;
using System.Runtime.InteropServices;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent to spawn a poof of smoke.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct PoofOfSmoke : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [field: FieldOffset(0)] public PackedVector2f Position { get; set; }

        PacketId IPacket.Id => PacketId.PoofOfSmoke;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 4);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 4);
    }
}
