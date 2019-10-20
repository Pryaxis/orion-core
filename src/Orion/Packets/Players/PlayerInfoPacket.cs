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

using System.IO;
using Microsoft.Xna.Framework;
using Orion.Entities;
using Orion.Items;
using Orion.Players;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set player information.
    /// </summary>
    /// <remarks>This packet is used to synchronize player state.</remarks>
    public sealed class PlayerInfoPacket : Packet {
        private byte _playerIndex;
        private bool _isHoldingUp;
        private bool _isHoldingDown;
        private bool _isHoldingLeft;
        private bool _isHoldingRight;
        private bool _isHoldingJump;
        private bool _isHoldingUseItem;
        private bool _direction;
        private bool _isClimbingRope;
        private bool _climbingRopeDirection;
        private bool _isVortexStealthed;
        private bool _isRightSideUp;
        private bool _isRaisingShield;
        private byte _heldItemSlot;
        private Vector2 _position;
        private Vector2 _velocity;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerInfo;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        /// <value>The player index.</value>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the up control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the up control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsHoldingUp {
            get => _isHoldingUp;
            set {
                _isHoldingUp = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the down control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the down control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsHoldingDown {
            get => _isHoldingDown;
            set {
                _isHoldingDown = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the left control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the left control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsHoldingLeft {
            get => _isHoldingLeft;
            set {
                _isHoldingLeft = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the right control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the right control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsHoldingRight {
            get => _isHoldingRight;
            set {
                _isHoldingRight = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the jump control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the jump control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsHoldingJump {
            get => _isHoldingJump;
            set {
                _isHoldingJump = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is holding the use item control.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is holding the use item control; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsHoldingUseItem {
            get => _isHoldingUseItem;
            set {
                _isHoldingUseItem = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating the player's direction.
        /// </summary>
        /// <value><see langword="true"/> if the player is facing right; otherwise, <see langword="false"/>.</value>
        public bool Direction {
            get => _direction;
            set {
                _direction = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is climbing a rope.
        /// </summary>
        /// <value><see langword="true"/> if the player is climbing a rope; otherwise, <see langword="false"/>.</value>
        public bool IsClimbingRope {
            get => _isClimbingRope;
            set {
                _isClimbingRope = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating the player's direction when climbing a rope.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is facing right while climbing a rope; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool ClimbingRopeDirection {
            get => _climbingRopeDirection;
            set {
                _climbingRopeDirection = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is stealthed with vortex armor.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is stealthed with vortex armor; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsVortexStealthed {
            get => _isVortexStealthed;
            set {
                _isVortexStealthed = value;
                _isDirty = true;
            }
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
            get => _isRightSideUp;
            set {
                _isRightSideUp = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is raising their shield.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is raising their shield; otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>Only the <see cref="ItemType.BrandOfTheInferno"/> weapon can alter this value normally.</remarks>
        public bool IsRaisingShield {
            get => _isRaisingShield;
            set {
                _isRaisingShield = value;
                _isDirty = true;
            }
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
            get => _heldItemSlot;
            set {
                _heldItemSlot = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's position. The components are pixels.
        /// </summary>
        /// <remarks>The player's position.</remarks>
        public Vector2 Position {
            get => _position;
            set {
                _position = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's velocity. The components are pixels per tick.
        /// </summary>
        /// <remarks>The player's velocity.</remarks>
        public Vector2 Velocity {
            get => _velocity;
            set {
                _velocity = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();

            Terraria.BitsByte flags = reader.ReadByte();
            Terraria.BitsByte flags2 = reader.ReadByte();
            _isHoldingUp = flags[0];
            _isHoldingDown = flags[1];
            _isHoldingLeft = flags[2];
            _isHoldingRight = flags[3];
            _isHoldingJump = flags[4];
            _isHoldingUseItem = flags[5];
            _direction = flags[6];
            _isClimbingRope = flags2[0];
            _climbingRopeDirection = flags2[1];
            _isVortexStealthed = flags2[3];
            _isRightSideUp = flags2[4];
            _isRaisingShield = flags2[5];

            _heldItemSlot = reader.ReadByte();
            _position = reader.ReadVector2();
            if (flags2[2]) _velocity = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);

            Terraria.BitsByte flags = 0;
            Terraria.BitsByte flags2 = 0;
            flags[0] = _isHoldingUp;
            flags[1] = _isHoldingDown;
            flags[2] = _isHoldingLeft;
            flags[3] = _isHoldingRight;
            flags[4] = _isHoldingJump;
            flags[5] = _isHoldingUseItem;
            flags[6] = _direction;
            flags2[0] = _isClimbingRope;
            flags2[1] = _climbingRopeDirection;
            flags2[2] = _velocity != Vector2.Zero;
            flags2[3] = _isVortexStealthed;
            flags2[4] = _isRightSideUp;
            flags2[5] = _isRaisingShield;
            writer.Write(flags);
            writer.Write(flags2);

            writer.Write(_heldItemSlot);
            writer.Write(in _position);
            if (flags2[2]) writer.Write(in _velocity);
        }
    }
}
