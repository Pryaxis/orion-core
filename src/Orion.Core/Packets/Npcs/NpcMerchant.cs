using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to update a merchant's inventory.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 80)]
    public struct NpcMerchant : IPacket
    {
        [FieldOffset(0)] private short[]? _shopItems;

        /// <summary>
        /// Gets the shop.
        /// </summary>
        public short[] ShopItems => _shopItems ??= new short[40];

        PacketId IPacket.Id => PacketId.NpcMerchant;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) =>
            span.Read(ref Unsafe.As<short, byte>(ref ShopItems[0]), 80);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) =>
            span.Write(ref Unsafe.As<short, byte>(ref ShopItems[0]), 80);
    }
}
