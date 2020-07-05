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
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to teleport a player via an item.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 1)]
    public struct PlayerTeleportItem : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.

        /// <summary>
        /// Gets or sets the teleportation item.
        /// </summary>
        /// <value>The teleportation item.</value>
        [field: FieldOffset(0)] public TeleportItem Item { get; set; }

        PacketId IPacket.Id => PacketId.PlayerTeleportItem;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 1);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 1);

        /// <summary>
        /// Specifies the teleportation type in a <see cref="PlayerTeleportItem"/>.
        /// </summary>
        public enum TeleportItem : byte
        {
            /// <summary>
            /// Indicates a teleportation potion.
            /// </summary>
            TeleportationPotion = 0,

            /// <summary>
            /// Indicates a magic conch.
            /// </summary>
            MagicConch = 1,

            /// <summary>
            /// Indicates a demon conch.
            /// </summary>
            DemonConch = 2
        }
    }
}
