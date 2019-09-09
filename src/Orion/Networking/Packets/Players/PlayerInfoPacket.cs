using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

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

        private protected override PacketType Type => PacketType.PlayerInfo;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();

            Terraria.BitsByte flags = reader.ReadByte();
            Terraria.BitsByte flags2 = reader.ReadByte();
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
            PlayerPosition = reader.ReadVector2();
            if (flags2[2]) {
                PlayerVelocity = reader.ReadVector2();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);

            Terraria.BitsByte flags = 0;
            Terraria.BitsByte flags2 = 0;
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
            if (flags2[2]) {
                writer.Write(PlayerVelocity);
            }
        }
    }
}
