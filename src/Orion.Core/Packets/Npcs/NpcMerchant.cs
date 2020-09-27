// Copyright (c) 2020 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

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
