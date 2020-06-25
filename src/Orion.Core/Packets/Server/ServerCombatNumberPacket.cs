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
using Orion.Core.DataStructures;

namespace Orion.Core.Packets.Server
{
    /// <summary>
    /// A packet sent from the server to the client to show a combat number.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 15)]
    public sealed class ServerCombatNumberPacket : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the combat number's position.
        /// </summary>
        /// <value>The combat number's position.</value>
        [field: FieldOffset(0)] public Vector2f Position { get; set; }

        /// <summary>
        /// Gets or sets the combat number's color.
        /// </summary>
        /// <value>The combat number's color.</value>
        [field: FieldOffset(8)] public Color3 Color { get; set; }

        /// <summary>
        /// Gets or sets the combat number.
        /// </summary>
        /// <value>The combat number.</value>
        [field: FieldOffset(11)] public int Number { get; set; }

        PacketId IPacket.Id => PacketId.ServerCombatNumber;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 15);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 15);
    }
}
