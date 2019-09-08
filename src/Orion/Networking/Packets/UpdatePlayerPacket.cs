using System;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update a player.
    /// </summary>
    public sealed class UpdatePlayerPacket : TerrariaPacket {
        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.UpdatePlayer;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding up.
        /// </summary>
        public bool IsHoldingUp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding down.
        /// </summary>
        public bool IsHoldingDown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding left.
        /// </summary>
        public bool IsHoldingLeft { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding right.
        /// </summary>
        public bool IsHoldingRight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding jump.
        /// </summary>
        public bool IsHoldingJump { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is holding 'use item'.
        /// </summary>
        public bool IsHoldingUseItem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is facing right.
        /// </summary>
        public bool IsFacingRight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is climbing a rope.
        /// </summary>
        public bool IsClimbingRope { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is climbing a rope facing right.
        /// </summary>
        public bool IsClimbingRopeFacingRight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is moving.
        /// </summary>
        public bool IsMoving { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is stealthed with vortex armor.
        /// </summary>
        public bool IsVortexStealthed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is right-side up.
        /// </summary>
        public bool IsRightSideUp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is raising a shield.
        /// </summary>
        public bool IsRaisingShield { get; set; }

        /// <summary>
        /// Gets or sets the player's selected item index.
        /// </summary>
        public byte SelectedItemIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the player's velocity.
        /// </summary>
        public Vector2 Velocity { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();

            Terraria.BitsByte flags = reader.ReadByte();
            Terraria.BitsByte flags2 = reader.ReadByte();
            IsHoldingUp = flags[0];
            IsHoldingDown = flags[1];
            IsHoldingLeft = flags[2];
            IsHoldingRight = flags[3];
            IsHoldingJump = flags[4];
            IsHoldingUseItem = flags[5];
            IsFacingRight = flags[6];
            IsClimbingRope = flags2[0];
            IsClimbingRopeFacingRight = flags2[1];
            IsMoving = flags2[2];
            IsVortexStealthed = flags2[3];
            IsRightSideUp = flags2[4];
            IsRaisingShield = flags2[5];

            SelectedItemIndex = reader.ReadByte();
            Position = reader.ReadVector2();
            if (IsMoving) {
                Velocity = reader.ReadVector2();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);

            Terraria.BitsByte flags = 0;
            Terraria.BitsByte flags2 = 0;
            flags[0] = IsHoldingUp;
            flags[1] = IsHoldingDown;
            flags[2] = IsHoldingLeft;
            flags[3] = IsHoldingRight;
            flags[4] = IsHoldingJump;
            flags[5] = IsHoldingUseItem;
            flags[6] = IsFacingRight;
            flags2[0] = IsClimbingRope;
            flags2[1] = IsClimbingRopeFacingRight;
            flags2[2] = IsMoving;
            flags2[3] = IsVortexStealthed;
            flags2[4] = IsRightSideUp;
            flags2[5] = IsRaisingShield;
            writer.Write(flags);
            writer.Write(flags2);

            writer.Write(SelectedItemIndex);
            writer.Write(Position);
            if (IsMoving) {
                writer.Write(Velocity);
            }
        }
    }
}
