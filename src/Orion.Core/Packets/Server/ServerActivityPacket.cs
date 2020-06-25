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

namespace Orion.Core.Packets.Server
{
    /// <summary>
    /// A packet sent from the server to the client to set client activity.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public sealed class ServerActivityPacket : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the client's index.
        /// </summary>
        /// <value>The client's index.</value>
        [field: FieldOffset(0)] public byte Index { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the client is active.
        /// </summary>
        /// <value><see langword="true"/> if the client is active; otherwise, <see langword="false"/>.</value>
        [field: FieldOffset(1)] public bool IsActive { get; set; }

        PacketId IPacket.Id => PacketId.ServerActivity;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 2);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 2);
    }
}
