// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using JetBrains.Annotations;
using Orion.Entities;
using Orion.Packets.Entities;

namespace Orion.Events.Entities {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PlayerInventorySlot"/> event.
    /// </summary>
    [PublicAPI]
    public sealed class PlayerInventorySlotEventArgs : PlayerEventArgs, ICancelable {
        [NotNull] private readonly PlayerInventorySlotPacket _packet;

        /// <inheritdoc />
        public bool IsCanceled { get; set; }

        /// <inheritdoc cref="PlayerInventorySlotPacket.PlayerInventorySlotIndex"/>
        public byte PlayerInventorySlotIndex => _packet.PlayerInventorySlotIndex;

        /// <inheritdoc cref="PlayerInventorySlotPacket.ItemStackSize"/>
        public short ItemStackSize {
            get => _packet.ItemStackSize;
            set => _packet.ItemStackSize = value;
        }

        /// <inheritdoc cref="PlayerInventorySlotPacket.ItemPrefix"/>
        public ItemPrefix ItemPrefix {
            get => _packet.ItemPrefix;
            set => _packet.ItemPrefix = value;
        }

        /// <inheritdoc cref="PlayerInventorySlotPacket.ItemType"/>
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
        /// <paramref name="player"/> or <paramref name="packet"/> are <c>null</c>.
        /// </exception>
        public PlayerInventorySlotEventArgs([NotNull] IPlayer player,
                                            [NotNull] PlayerInventorySlotPacket packet) : base(player) {
            _packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }
    }
}
