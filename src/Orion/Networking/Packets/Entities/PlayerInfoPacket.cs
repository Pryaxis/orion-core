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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Entities {
    /// <summary>
    /// Packet sent to set player information.
    /// </summary>
    [PublicAPI]
    public sealed class PlayerInfoPacket : Packet {
        private byte _playerIndex;
        private bool _isPlayerHoldingUp;
        private bool _isPlayerHoldingDown;
        private bool _isPlayerHoldingLeft;
        private bool _isPlayerHoldingRight;
        private bool _isPlayerHoldingJump;
        private bool _isPlayerHoldingUseItem;
        private bool _playerDirection;
        private bool _isPlayerClimbingRope;
        private bool _playerClimbingRopeDirection;
        private bool _isPlayerVortexStealthed;
        private bool _isPlayerRightSideUp;
        private bool _isPlayerRaisingShield;
        private byte _playerHeldItemSlotIndex;
        private Vector2 _playerPosition;
        private Vector2 _playerVelocity;

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerInfo;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding up.
        /// </summary>
        public bool IsPlayerHoldingUp {
            get => _isPlayerHoldingUp;
            set {
                _isPlayerHoldingUp = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding down.
        /// </summary>
        public bool IsPlayerHoldingDown {
            get => _isPlayerHoldingDown;
            set {
                _isPlayerHoldingDown = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding left.
        /// </summary>
        public bool IsPlayerHoldingLeft {
            get => _isPlayerHoldingLeft;
            set {
                _isPlayerHoldingLeft = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding right.
        /// </summary>
        public bool IsPlayerHoldingRight {
            get => _isPlayerHoldingRight;
            set {
                _isPlayerHoldingRight = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding jump.
        /// </summary>
        public bool IsPlayerHoldingJump {
            get => _isPlayerHoldingJump;
            set {
                _isPlayerHoldingJump = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding 'use item'.
        /// </summary>
        public bool IsPlayerHoldingUseItem {
            get => _isPlayerHoldingUseItem;
            set {
                _isPlayerHoldingUseItem = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the direction of the player.
        /// </summary>
        public bool PlayerDirection {
            get => _playerDirection;
            set {
                _playerDirection = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is climbing a rope.
        /// </summary>
        public bool IsPlayerClimbingRope {
            get => _isPlayerClimbingRope;
            set {
                _isPlayerClimbingRope = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the direction of the player when climbing a rope.
        /// </summary>
        public bool PlayerClimbingRopeDirection {
            get => _playerClimbingRopeDirection;
            set {
                _playerClimbingRopeDirection = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is stealthed with vortex armor.
        /// </summary>
        public bool IsPlayerVortexStealthed {
            get => _isPlayerVortexStealthed;
            set {
                _isPlayerVortexStealthed = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is right-side up.
        /// </summary>
        public bool IsPlayerRightSideUp {
            get => _isPlayerRightSideUp;
            set {
                _isPlayerRightSideUp = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is raising a shield.
        /// </summary>
        public bool IsPlayerRaisingShield {
            get => _isPlayerRaisingShield;
            set {
                _isPlayerRaisingShield = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's held item slot index.
        /// </summary>
        public byte PlayerHeldItemSlotIndex {
            get => _playerHeldItemSlotIndex;
            set {
                _playerHeldItemSlotIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's position.
        /// </summary>
        public Vector2 PlayerPosition {
            get => _playerPosition;
            set {
                _playerPosition = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's velocity.
        /// </summary>
        public Vector2 PlayerVelocity {
            get => _playerVelocity;
            set {
                _playerVelocity = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} @ {PlayerPosition}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();

            Terraria.BitsByte flags = reader.ReadByte();
            Terraria.BitsByte flags2 = reader.ReadByte();
            _isPlayerHoldingUp = flags[0];
            _isPlayerHoldingDown = flags[1];
            _isPlayerHoldingLeft = flags[2];
            _isPlayerHoldingRight = flags[3];
            _isPlayerHoldingJump = flags[4];
            _isPlayerHoldingUseItem = flags[5];
            _playerDirection = flags[6];
            _isPlayerClimbingRope = flags2[0];
            _playerClimbingRopeDirection = flags2[1];
            _isPlayerVortexStealthed = flags2[3];
            _isPlayerRightSideUp = flags2[4];
            _isPlayerRaisingShield = flags2[5];

            _playerHeldItemSlotIndex = reader.ReadByte();
            _playerPosition = reader.ReadVector2();
            if (flags2[2]) _playerVelocity = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);

            Terraria.BitsByte flags = 0;
            Terraria.BitsByte flags2 = 0;
            flags[0] = _isPlayerHoldingUp;
            flags[1] = _isPlayerHoldingDown;
            flags[2] = _isPlayerHoldingLeft;
            flags[3] = _isPlayerHoldingRight;
            flags[4] = _isPlayerHoldingJump;
            flags[5] = _isPlayerHoldingUseItem;
            flags[6] = _playerDirection;
            flags2[0] = _isPlayerClimbingRope;
            flags2[1] = _playerClimbingRopeDirection;
            flags2[2] = _playerVelocity != Vector2.Zero;
            flags2[3] = _isPlayerVortexStealthed;
            flags2[4] = _isPlayerRightSideUp;
            flags2[5] = _isPlayerRaisingShield;
            writer.Write(flags);
            writer.Write(flags2);

            writer.Write(_playerHeldItemSlotIndex);
            writer.Write(_playerPosition);
            if (flags2[2]) writer.Write(_playerVelocity);
        }
    }
}
