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

namespace Orion.Core.Packets.Players {
    /// <summary>
    /// A packet sent to cause a player to teleport with an item.
    /// </summary>
    public struct PlayerTeleportItemPacket : IPacket {
        /// <summary>
        /// Gets or sets the teleport item.
        /// </summary>
        /// <value>The teleport item.</value>
        public TeleportItem Item { get; set; }

        PacketId IPacket.Id => PacketId.PlayerTeleportItem;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context) {
            Item = (TeleportItem)span[0];
            return 1;
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) {
            span[0] = (byte)Item;
            return 1;
        }
    }
}
