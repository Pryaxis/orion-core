// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Orion.Items;
using Orion.Packets.Players;
using Orion.Players;

namespace Orion.Events.Players {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PlayerInventorySlot"/> event. This event can be canceled.
    /// </summary>
    [EventArgs("player-inv-slot")]
    public sealed class PlayerInventorySlotEventArgs : PlayerPacketEventArgs<PlayerInventorySlotPacket> {
        /// <summary>
        /// Gets or sets the player's inventory slot index.
        /// </summary>
        /// <value>The player's inventory slot index.</value>
        /// <remarks>
        /// This value can range from <c>0</c> to <c>219</c>. Check <see cref="IPlayerInventory"/> for a more detailed
        /// description on the indices.
        /// </remarks>
        public byte PlayerInventorySlotIndex => _packet.PlayerInventorySlotIndex;

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        /// <value>The item's stack size.</value>
        public short ItemStackSize {
            get => _packet.ItemStackSize;
            set => _packet.ItemStackSize = value;
        }

        /// <summary>
        /// Gets or sets the item's prefix.
        /// </summary>
        /// <value>The item's prefix.</value>
        public ItemPrefix ItemPrefix {
            get => _packet.ItemPrefix;
            set => _packet.ItemPrefix = value;
        }

        /// <summary>
        /// Gets or sets the item's type.
        /// </summary>
        /// <value>The item's type.</value>
        public ItemType ItemType {
            get => _packet.ItemType;
            set => _packet.ItemType = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInventorySlotEventArgs"/> class with the specified player
        /// and packet.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="packet"/> are <see langword="null"/>.
        /// </exception>
        public PlayerInventorySlotEventArgs(IPlayer player, PlayerInventorySlotPacket packet) : base(player, packet) { }
    }
}
