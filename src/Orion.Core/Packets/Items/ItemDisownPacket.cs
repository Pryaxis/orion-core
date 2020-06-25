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
using System.Runtime.InteropServices;

namespace Orion.Core.Packets.Items
{
    /// <summary>
    /// A packet sent from the server to the client to disown an item.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public sealed class ItemDisownPacket : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        /// <value>The item index.</value>
        [field: FieldOffset(0)] public short ItemIndex { get; set; }

        PacketId IPacket.Id => PacketId.ItemDisown;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 2);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 2);
    }
}
