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
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;
using Terraria;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set player information.
    /// </summary>
    public sealed class PlayerInfoPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding up.
        /// </summary>
        public bool IsPlayerHoldingUp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding down.
        /// </summary>
        public bool IsPlayerHoldingDown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding left.
        /// </summary>
        public bool IsPlayerHoldingLeft { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding right.
        /// </summary>
        public bool IsPlayerHoldingRight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding jump.
        /// </summary>
        public bool IsPlayerHoldingJump { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding 'use item'.
        /// </summary>
        public bool IsPlayerHoldingUseItem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the direction of the player.
        /// </summary>
        public bool PlayerDirection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is climbing a rope.
        /// </summary>
        public bool IsPlayerClimbingRope { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the direction of the player when climbing a rope.
        /// </summary>
        public bool PlayerClimbingRopeDirection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is stealthed with vortex armor.
        /// </summary>
        public bool IsPlayerVortexStealthed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is right-side up.
        /// </summary>
        public bool IsPlayerRightSideUp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is raising a shield.
        /// </summary>
        public bool IsPlayerRaisingShield { get; set; }

        /// <summary>
        /// Gets or sets the player's selected item index.
        /// </summary>
        public byte PlayerSelectedItemIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's position.
        /// </summary>
        public Vector2 PlayerPosition { get; set; }

        /// <summary>
        /// Gets or sets the player's velocity.
        /// </summary>
        public Vector2 PlayerVelocity { get; set; }

        public override PacketType Type => PacketType.PlayerInfo;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} @ {PlayerPosition}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();

            BitsByte flags = reader.ReadByte();
            BitsByte flags2 = reader.ReadByte();
            IsPlayerHoldingUp = flags[0];
            IsPlayerHoldingDown = flags[1];
            IsPlayerHoldingLeft = flags[2];
            IsPlayerHoldingRight = flags[3];
            IsPlayerHoldingJump = flags[4];
            IsPlayerHoldingUseItem = flags[5];
            PlayerDirection = flags[6];
            IsPlayerClimbingRope = flags2[0];
            PlayerClimbingRopeDirection = flags2[1];
            IsPlayerVortexStealthed = flags2[3];
            IsPlayerRightSideUp = flags2[4];
            IsPlayerRaisingShield = flags2[5];

            PlayerSelectedItemIndex = reader.ReadByte();
            PlayerPosition = BinaryExtensions.ReadVector2(reader);
            if (flags2[2]) PlayerVelocity = BinaryExtensions.ReadVector2(reader);
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);

            BitsByte flags = 0;
            BitsByte flags2 = 0;
            flags[0] = IsPlayerHoldingUp;
            flags[1] = IsPlayerHoldingDown;
            flags[2] = IsPlayerHoldingLeft;
            flags[3] = IsPlayerHoldingRight;
            flags[4] = IsPlayerHoldingJump;
            flags[5] = IsPlayerHoldingUseItem;
            flags[6] = PlayerDirection;
            flags2[0] = IsPlayerClimbingRope;
            flags2[1] = PlayerClimbingRopeDirection;
            flags2[2] = PlayerVelocity != Vector2.Zero;
            flags2[3] = IsPlayerVortexStealthed;
            flags2[4] = IsPlayerRightSideUp;
            flags2[5] = IsPlayerRaisingShield;
            writer.Write(flags);
            writer.Write(flags2);

            writer.Write(PlayerSelectedItemIndex);
            writer.Write(PlayerPosition);
            if (flags2[2]) writer.Write(PlayerVelocity);
        }
    }
}
