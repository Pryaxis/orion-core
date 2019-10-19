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

namespace Orion.Events.Players {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PlayerInfo"/> event. This event can be canceled.
    /// </summary>
    [EventArgs("player-info")]
    public sealed class PlayerInfoEventArgs : PlayerPacketEventArgs<PlayerInfoPacket> {
        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the up control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the up control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsPlayerHoldingUp {
            get => _packet.IsPlayerHoldingUp;
            set => _packet.IsPlayerHoldingUp = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the down control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the down control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsPlayerHoldingDown {
            get => _packet.IsPlayerHoldingDown;
            set => _packet.IsPlayerHoldingDown = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the left control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the left control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsPlayerHoldingLeft {
            get => _packet.IsPlayerHoldingLeft;
            set => _packet.IsPlayerHoldingLeft = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the right control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the right control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsPlayerHoldingRight {
            get => _packet.IsPlayerHoldingRight;
            set => _packet.IsPlayerHoldingRight = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the jump control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the jump control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsPlayerHoldingJump {
            get => _packet.IsPlayerHoldingJump;
            set => _packet.IsPlayerHoldingJump = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the use item control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the use item control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsPlayerHoldingUseItem {
            get => _packet.IsPlayerHoldingUseItem;
            set => _packet.IsPlayerHoldingUseItem = value;
        }

        /// <summary>
        /// Gets or sets a value indicating the direction of the player.
        /// </summary>
        /// <value><see langword="true"/> if the player is facing right; otherwise, <see langword="false"/>.</value>
        public bool PlayerDirection {
            get => _packet.PlayerDirection;
            set => _packet.PlayerDirection = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is climbing a rope.
        /// </summary>
        /// <value><see langword="true"/> if the player is climbing a rope; otherwise, <see langword="false"/>.</value>
        public bool IsPlayerClimbingRope {
            get => _packet.IsPlayerClimbingRope;
            set => _packet.IsPlayerClimbingRope = value;
        }

        /// <summary>
        /// Gets or sets a value indicating the direction of the player when climbing a rope.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is facing right while climbing a rope; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool PlayerClimbingRopeDirection {
            get => _packet.PlayerClimbingRopeDirection;
            set => _packet.PlayerClimbingRopeDirection = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is stealthed with vortex armor.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is stealthed with vortex armor; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsPlayerVortexStealthed {
            get => _packet.IsPlayerVortexStealthed;
            set => _packet.IsPlayerVortexStealthed = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is right-side up.
        /// </summary>
        /// <value><see langword="true"/> if the player is right-side up; otherwise, <see langword="false"/>.</value>
        /// <remarks>
        /// Only the <see cref="BuffType.Gravitation"/> buff and the <see cref="ItemType.GravityGlobe"/> accessory can
        /// alter this value normally.
        /// </remarks>
        public bool IsPlayerRightSideUp {
            get => _packet.IsPlayerRightSideUp;
            set => _packet.IsPlayerRightSideUp = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is raising their shield.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is raising their shield; otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>Only the <see cref="ItemType.BrandOfTheInferno"/> weapon can alter this value normally.</remarks>
        public bool IsPlayerRaisingShield {
            get => _packet.IsPlayerRaisingShield;
            set => _packet.IsPlayerRaisingShield = value;
        }

        /// <summary>
        /// Gets or sets the player's held item slot index.
        /// </summary>
        /// <value>The player's held item slot index.</value>
        /// <remarks>
        /// This value can range from <c>0</c> to <c>58</c>. Check <see cref="IPlayerInventory"/> for a more detailed
        /// description on the indices.
        /// </remarks>
        public byte PlayerHeldItemSlotIndex {
            get => _packet.PlayerHeldItemSlotIndex;
            set => _packet.PlayerHeldItemSlotIndex = value;
        }

        /// <summary>
        /// Gets or sets the player's position. The components are pixels.
        /// </summary>
        /// <remarks>The player's position.</remarks>
        public Vector2 PlayerPosition {
            get => _packet.PlayerPosition;
            set => _packet.PlayerPosition = value;
        }

        /// <summary>
        /// Gets or sets the player's velocity. The components are pixels per tick.
        /// </summary>
        /// <remarks>The player's velocity.</remarks>
        public Vector2 PlayerVelocity {
            get => _packet.PlayerVelocity;
            set => _packet.PlayerVelocity = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInfoEventArgs"/> class with the specified player and
        /// packet.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="packet"/> are <see langword="null"/>.
        /// </exception>
        public PlayerInfoEventArgs(IPlayer player, PlayerInfoPacket packet) : base(player, packet) { }
    }
}
