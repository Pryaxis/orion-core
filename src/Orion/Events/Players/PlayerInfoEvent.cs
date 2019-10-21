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
using Microsoft.Xna.Framework;
using Orion.Entities;
using Orion.Items;
using Orion.Packets.Players;
using Orion.Players;
using Orion.Utils;

namespace Orion.Events.Players {
    /// <summary>
    /// An event that occurs when a player sends their information. This event can be canceled and modified.
    /// </summary>
    [EventArgs("player-info")]
    public sealed class PlayerInfoEvent : PlayerEvent, ICancelable, IDirtiable {
        private readonly PlayerInfoPacket _packet;
        
        /// <inheritdoc/>
        public bool IsDirty => _packet.IsDirty;

        /// <inheritdoc/>
        public string? CancellationReason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the up control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the up control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsHoldingUp {
            get => _packet.IsHoldingUp;
            set => _packet.IsHoldingUp = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the down control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the down control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsHoldingDown {
            get => _packet.IsHoldingDown;
            set => _packet.IsHoldingDown = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the left control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the left control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsHoldingLeft {
            get => _packet.IsHoldingLeft;
            set => _packet.IsHoldingLeft = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the right control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the right control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsHoldingRight {
            get => _packet.IsHoldingRight;
            set => _packet.IsHoldingRight = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the jump control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the jump control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsHoldingJump {
            get => _packet.IsHoldingJump;
            set => _packet.IsHoldingJump = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the use item control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the use item control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsHoldingUseItem {
            get => _packet.IsHoldingUseItem;
            set => _packet.IsHoldingUseItem = value;
        }

        /// <summary>
        /// Gets or sets a value indicating the player's direction.
        /// </summary>
        /// <value><see langword="true"/> if the player is facing right; otherwise, <see langword="false"/>.</value>
        public bool Direction {
            get => _packet.Direction;
            set => _packet.Direction = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is climbing a rope.
        /// </summary>
        /// <value><see langword="true"/> if the player is climbing a rope; otherwise, <see langword="false"/>.</value>
        public bool IsClimbingRope {
            get => _packet.IsClimbingRope;
            set => _packet.IsClimbingRope = value;
        }

        /// <summary>
        /// Gets or sets a value indicating the player's direction when climbing a rope.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is facing right while climbing a rope; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool ClimbingRopeDirection {
            get => _packet.ClimbingRopeDirection;
            set => _packet.ClimbingRopeDirection = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is stealthed with vortex armor.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is stealthed with vortex armor; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsVortexStealthed {
            get => _packet.IsVortexStealthed;
            set => _packet.IsVortexStealthed = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is right-side up.
        /// </summary>
        /// <value><see langword="true"/> if the player is right-side up; otherwise, <see langword="false"/>.</value>
        /// <remarks>
        /// Only the <see cref="BuffType.Gravitation"/> buff and the <see cref="ItemType.GravityGlobe"/> accessory can
        /// alter this value normally.
        /// </remarks>
        public bool IsRightSideUp {
            get => _packet.IsRightSideUp;
            set => _packet.IsRightSideUp = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is raising their shield.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is raising their shield; otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>Only the <see cref="ItemType.BrandOfTheInferno"/> weapon can alter this value normally.</remarks>
        public bool IsRaisingShield {
            get => _packet.IsRaisingShield;
            set => _packet.IsRaisingShield = value;
        }

        /// <summary>
        /// Gets or sets the player's held item slot.
        /// </summary>
        /// <value>The player's held item slot.</value>
        /// <remarks>
        /// This value can range from <c>0</c> to <c>58</c>. Check the <see cref="IPlayerInventory"/> interface for a
        /// more detailed description on the slots.
        /// </remarks>
        public byte HeldItemSlot {
            get => _packet.HeldItemSlot;
            set => _packet.HeldItemSlot = value;
        }

        /// <summary>
        /// Gets or sets the player's position. The components are pixels.
        /// </summary>
        /// <remarks>The player's position.</remarks>
        public Vector2 Position {
            get => _packet.Position;
            set => _packet.Position = value;
        }

        /// <summary>
        /// Gets or sets the player's velocity. The components are pixels per tick.
        /// </summary>
        /// <remarks>The player's velocity.</remarks>
        public Vector2 Velocity {
            get => _packet.Velocity;
            set => _packet.Velocity = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInfoEvent"/> class with the specified player and packet.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="packet"/> are <see langword="null"/>.
        /// </exception>
        public PlayerInfoEvent(IPlayer player, PlayerInfoPacket packet) : base(player) {
            _packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }

        /// <inheritdoc/>
        public void Clean() => _packet.Clean();
    }
}
