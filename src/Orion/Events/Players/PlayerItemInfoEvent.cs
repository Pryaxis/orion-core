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
using Destructurama.Attributed;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Packets.Items;
using Orion.Players;
using Orion.Utils;

namespace Orion.Events.Players {
    // TODO: split this event up into item drops and item modifications

    /// <summary>
    /// An event that occurs when a player sends item information. This event can be canceled and modified.
    /// </summary>
    [Event("player-item-info")]
    public sealed class PlayerItemInfoEvent : PlayerEvent, ICancelable, IDirtiable {
        private readonly ItemInfoPacket _packet;

        /// <inheritdoc/>
        [NotLogged]
        public string? CancellationReason { get; set; }

        /// <inheritdoc/>
        [NotLogged]
        public bool IsDirty => _packet.IsDirty;
        
        /// <summary>
        /// Gets the item index. If <c>400</c>, then an item will be created.
        /// </summary>
        /// <value>The item index.</value>
        public short ItemIndex => _packet.ItemIndex;
        
        /// <summary>
        /// Gets or sets the item's position. The components are pixels.
        /// </summary>
        /// <value>The item's position.</value>
        public Vector2 Position {
            get => _packet.Position;
            set => _packet.Position = value;
        }
        
        /// <summary>
        /// Gets or sets the item's velocity. The components are pixels per tick.
        /// </summary>
        /// <value>The item's velocity.</value>
        public Vector2 Velocity {
            get => _packet.Velocity;
            set => _packet.Velocity = value;
        }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        /// <value>The item's stack size.</value>
        public short StackSize {
            get => _packet.StackSize;
            set => _packet.StackSize = value;
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
        /// Gets or sets a value indicating whether the item can be picked up immediately.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the item can be picked up immediately; otherwise, <see langword="false"/>.
        /// </value>
        public bool CanBePickedUpImmediately {
            get => _packet.CanBePickedUpImmediately;
            set => _packet.CanBePickedUpImmediately = value;
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
        /// Initializes a new instance of the <see cref="PlayerItemInfoEvent"/> class with the specified player and
        /// packet.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="packet"/> are <see langword="null"/>.
        /// </exception>
        public PlayerItemInfoEvent(IPlayer player, ItemInfoPacket packet) : base(player) {
            _packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }

        /// <inheritdoc/>
        public void Clean() => _packet.Clean();
    }
}
