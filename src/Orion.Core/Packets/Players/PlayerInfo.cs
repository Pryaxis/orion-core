// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Runtime.InteropServices;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet that notifies the server and other clients of player activity (movement, environment interactions etc.).
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 38)]
    public struct PlayerInfo : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        [FieldOffset(14)] private byte _bytes2; // Used to obtain an interior reference
        [FieldOffset(22)] private byte _bytes3; // Used to obtain an interior reference
        [FieldOffset(1)] private Flags8 _controlFlags;
        [FieldOffset(2)] private Flags8 _pulleyFlags;
        [FieldOffset(3)] private Flags8 _miscFlags;
        [FieldOffset(4)] private Flags8 _sleepingFlags;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the selected item slot.
        /// </summary>
        [field: FieldOffset(5)] public byte SelectedItemSlot { get; set; }

        /// <summary>
        /// Gets or sets the player's horizontal position (in pixels).
        /// </summary>
        [field: FieldOffset(6)] public float PositionX { get; set; }

        /// <summary>
        /// Gets or sets the player's vertical position (in pixels).
        /// </summary>
        [field: FieldOffset(10)] public float PositionY { get; set; }

        /// <summary>
        /// Gets or sets the player's horizontal velocity. <i>Sent only when <see cref="ShouldUpdateVelocity"/> is set to <see langword="true"/>!</i>
        /// </summary>
        [field: FieldOffset(14)] public float VelocityX { get; set; }

        /// <summary>
        /// Gets or sets the player's vertical velocity. <i>Sent only when <see cref="ShouldUpdateVelocity"/> is set to <see langword="true"/>!</i>
        /// </summary>
        [field: FieldOffset(18)] public float VelocityY { get; set; }

        /// <summary>
        /// Gets or sets a value that defines the player's horizontal position prior to using Potion of Return. <i>Sent only when <see cref="HasUsedPotionOfReturn"/> is set to <see langword="true"/>!</i>
        /// </summary>
        [field: FieldOffset(22)] public float PotionOfReturnOriginX { get; set; }

        /// <summary>
        /// Gets or sets a value that defines the player's vertical position prior to using Potion of Return. <i>Sent only when <see cref="HasUsedPotionOfReturn"/> is set to <see langword="true"/>!</i>
        /// </summary>
        [field: FieldOffset(26)] public float PotionOfReturnOriginY { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate of the player's home. <i>Sent only when <see cref="HasUsedPotionOfReturn"/> is set to <see langword="true"/>!</i>
        /// </summary>
        [field: FieldOffset(30)] public float HomePositionX { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the player's home. <i>Sent only when <see cref="HasUsedPotionOfReturn"/> is set to <see langword="true"/>!</i>
        /// </summary>
        [field: FieldOffset(34)] public float HomePositionY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the direction up control (W) is active.
        /// </summary>
        public bool IsControlUp
        {
            get => _controlFlags[0];
            set => _controlFlags[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the direction down control (S) is active.
        /// </summary>
        public bool IsControlDown
        {
            get => _controlFlags[1];
            set => _controlFlags[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the right direction control (A) is active.
        /// </summary>
        public bool IsControlLeft
        {
            get => _controlFlags[2];
            set => _controlFlags[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the right direction control (D) is active.
        /// </summary>
        public bool IsControlRight
        {
            get => _controlFlags[3];
            set => _controlFlags[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the jump control (Space) is active.
        /// </summary>
        public bool IsControlJump
        {
            get => _controlFlags[4];
            set => _controlFlags[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player used/consumed an item.
        /// </summary>
        public bool IsControlUseItem
        {
            get => _controlFlags[5];
            set => _controlFlags[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is sleeping.
        /// </summary>
        public bool IsSleeping
        {
            get => _sleepingFlags[0];
            set => _sleepingFlags[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether pulley is enabled.
        /// </summary>
        public bool IsPulleyEnabled
        {
            get => _pulleyFlags[0];
            set => _pulleyFlags[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is facing the right direction.
        /// </summary>
        public bool IsDirectionRight
        {
            get => _pulleyFlags[1];
            set => _pulleyFlags[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="VelocityX"/> and <see cref="VelocityY"/> are sent.
        /// </summary>
        public bool ShouldUpdateVelocity
        {
            get => _pulleyFlags[2];
            set => _pulleyFlags[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Vortex Stealth effect is active.
        /// </summary>
        public bool IsVortexStealthActive
        {
            get => _pulleyFlags[3];
            set => _pulleyFlags[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether gravity is inverted. 
        /// </summary>
        public bool IsGravityInverted
        {
            get => !_pulleyFlags[4];
            set => _pulleyFlags[4] = !value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player has a shield raised.
        /// </summary>
        public bool IsShieldRaised
        {
            get => _pulleyFlags[5];
            set => _pulleyFlags[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is hovering up.
        /// </summary>
        public bool IsHoveringUp
        {
            get => _miscFlags[0];
            set => _miscFlags[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the void vault is enabled.
        /// </summary>
        public bool IsVoidVaultEnabled
        {
            get => _miscFlags[1];
            set => _miscFlags[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is sitting.
        /// </summary>
        public bool IsSitting
        {
            get => _miscFlags[2];
            set => _miscFlags[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player has completed the DD2 event.
        /// </summary>
        public bool HasCompletedDD2Event
        {
            get => _miscFlags[3];
            set => _miscFlags[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is petting an animal.
        /// </summary>
        public bool IsPettingAnimal
        {
            get => _miscFlags[4];
            set => _miscFlags[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is petting a small animal.
        /// </summary>
        public bool IsPettingSmallAnimal
        {
            get => _miscFlags[5];
            set => _miscFlags[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player used Potion of Return.
        /// </summary>
        public bool HasUsedPotionOfReturn
        {
            get => _miscFlags[6];
            set => _miscFlags[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is hovering down (Hoverboards).
        /// </summary>
        public bool IsHoveringDown
        {
            get => _miscFlags[7];
            set => _miscFlags[7] = value;
        }

        PacketId IPacket.Id => PacketId.PlayerInfo;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 14);
            if (ShouldUpdateVelocity)
            {
                length += span[length..].Read(ref _bytes2, 8);
            }

            if (HasUsedPotionOfReturn)
            {
                length += span[length..].Read(ref _bytes3, 16);
            }

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 14);
            if (ShouldUpdateVelocity)
            {
                length += span[length..].Write(ref _bytes2, 8);
            }

            if (HasUsedPotionOfReturn)
            {
                length += span[length..].Write(ref _bytes3, 16);
            }

            return length;
        }
    }
}
