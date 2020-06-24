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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.DataStructures;
using Orion.Core.Players;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to set a player's character.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct PlayerCharacterPacket : IPacket
    {
        private const ushort HideAccessorySlot1Mask /*  */ = 0b_00000000_00001000;
        private const ushort HideAccessorySlot2Mask /*  */ = 0b_00000000_00010000;
        private const ushort HideAccessorySlot3Mask /*  */ = 0b_00000000_00100000;
        private const ushort HideAccessorySlot4Mask /*  */ = 0b_00000000_01000000;
        private const ushort HideAccessorySlot5Mask /*  */ = 0b_00000000_10000000;
        private const ushort HideAccessorySlot6Mask /*  */ = 0b_00000001_00000000;
        private const ushort HideAccessorySlot7Mask /*  */ = 0b_00000010_00000000;

        private const byte HidePetSlotMask /*           */ = 0b_00000001;
        private const byte HideLightPetSlotMask /*      */ = 0b_00000010;

        private const byte MediumcoreMask /*            */ = 0b_00000001;
        private const byte HardcoreMask /*              */ = 0b_00000010;
        private const byte HasExtraAccessorySlotMask /* */ = 0b_00000100;
        private const byte JourneyMask /*               */ = 0b_00001000;
        private const byte DifficultyMask /*            */ = 0b_00001011;

        private const byte IsUsingBiomeTorchesMask /*  */ = 0b_00000001;
        private const byte IsFightingTheTorchGodMask /* */ = 0b_00000010;

        [FieldOffset(8)] private string? _name;
        [FieldOffset(17)] private ushort _hideAccessorySlotsFlags;
        [FieldOffset(19)] private byte _hideMiscSlotsFlags;
        [FieldOffset(41)] private byte _difficultyFlags;
        [FieldOffset(42)] private byte _torchFlags;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        /// <value>The player index.</value>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's skin variant.
        /// </summary>
        /// <value>The player's skin variant.</value>
        [field: FieldOffset(1)] public byte SkinVariant { get; set; }

        /// <summary>
        /// Gets or sets the player's hair.
        /// </summary>
        /// <value>The player's hair.</value>
        [field: FieldOffset(2)] public byte Hair { get; set; }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        /// <value>The player's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Name
        {
            get => _name ?? string.Empty;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the player's hair dye.
        /// </summary>
        /// <value>The player's hair dye.</value>
        [field: FieldOffset(16)] public byte HairDye { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the first accessory slot.
        /// </summary>
        /// <value><see langword="true"/> to hide the first accessory slot; otherwise, <see langword="false"/>.</value>
        public bool HideAccessorySlot1
        {
            get => GetAccessoryHideSlotsFlags(HideAccessorySlot1Mask);
            set => SetAccessoryHideSlotsFlags(HideAccessorySlot1Mask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the second accessory slot.
        /// </summary>
        /// <value><see langword="true"/> to hide the second accessory slot; otherwise, <see langword="false"/>.</value>
        public bool HideAccessorySlot2
        {
            get => GetAccessoryHideSlotsFlags(HideAccessorySlot2Mask);
            set => SetAccessoryHideSlotsFlags(HideAccessorySlot2Mask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the third accessory slot.
        /// </summary>
        /// <value><see langword="true"/> to hide the third accessory slot; otherwise, <see langword="false"/>.</value>
        public bool HideAccessorySlot3
        {
            get => GetAccessoryHideSlotsFlags(HideAccessorySlot3Mask);
            set => SetAccessoryHideSlotsFlags(HideAccessorySlot3Mask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the fourth accessory slot.
        /// </summary>
        /// <value><see langword="true"/> to hide the fourth accessory slot; otherwise, <see langword="false"/>.</value>
        public bool HideAccessorySlot4
        {
            get => GetAccessoryHideSlotsFlags(HideAccessorySlot4Mask);
            set => SetAccessoryHideSlotsFlags(HideAccessorySlot4Mask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the fifth accessory slot.
        /// </summary>
        /// <value><see langword="true"/> to hide the fifth accessory slot; otherwise, <see langword="false"/>.</value>
        public bool HideAccessorySlot5
        {
            get => GetAccessoryHideSlotsFlags(HideAccessorySlot5Mask);
            set => SetAccessoryHideSlotsFlags(HideAccessorySlot5Mask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the sixth accessory slot.
        /// </summary>
        /// <value><see langword="true"/> to hide the sixth accessory slot; otherwise, <see langword="false"/>.</value>
        public bool HideAccessorySlot6
        {
            get => GetAccessoryHideSlotsFlags(HideAccessorySlot6Mask);
            set => SetAccessoryHideSlotsFlags(HideAccessorySlot6Mask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the seventh accessory slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to hide the seventh accessory slot; otherwise, <see langword="false"/>.
        /// </value>
        public bool HideAccessorySlot7
        {
            get => GetAccessoryHideSlotsFlags(HideAccessorySlot7Mask);
            set => SetAccessoryHideSlotsFlags(HideAccessorySlot7Mask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the pet slot.
        /// </summary>
        /// <value><see langword="true"/> to hide the pet slot; otherwise, <see langword="false"/>.</value>
        public bool HidePetSlot
        {
            get => GetHideMiscSlotsFlags(HidePetSlotMask);
            set => SetHideMiscSlotsFlags(HidePetSlotMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the light pet slot.
        /// </summary>
        /// <value><see langword="true"/> to hide the light pet slot; otherwise, <see langword="false"/>.</value>
        public bool HideLightPetSlot
        {
            get => GetHideMiscSlotsFlags(HideLightPetSlotMask);
            set => SetHideMiscSlotsFlags(HideLightPetSlotMask, value);
        }

        /// <summary>
        /// Gets or sets the player's hair color.
        /// </summary>
        /// <value>The player's hair color.</value>
        [field: FieldOffset(20)] public Color3 HairColor { get; set; }

        /// <summary>
        /// Gets or sets the player's skin color.
        /// </summary>
        /// <value>The player's skin color.</value>
        [field: FieldOffset(23)] public Color3 SkinColor { get; set; }

        /// <summary>
        /// Gets or sets the player's eye color.
        /// </summary>
        /// <value>The player's eye color.</value>
        [field: FieldOffset(26)] public Color3 EyeColor { get; set; }

        /// <summary>
        /// Gets or sets the player's shirt color.
        /// </summary>
        /// <value>The player's shirt color.</value>
        [field: FieldOffset(29)] public Color3 ShirtColor { get; set; }

        /// <summary>
        /// Gets or sets the player's undershirt color.
        /// </summary>
        /// <value>The player's undershirt color.</value>
        [field: FieldOffset(32)] public Color3 UndershirtColor { get; set; }

        /// <summary>
        /// Gets or sets the player's pants color.
        /// </summary>
        /// <value>The player's pants color.</value>
        [field: FieldOffset(35)] public Color3 PantsColor { get; set; }

        /// <summary>
        /// Gets or sets the player's shoe color.
        /// </summary>
        /// <value>The player's shoe color.</value>
        [field: FieldOffset(38)] public Color3 ShoeColor { get; set; }

        /// <summary>
        /// Gets or sets the player's difficulty.
        /// </summary>
        /// <value>The player's difficulty.</value>
        public CharacterDifficulty Difficulty
        {
            get => true switch
            {
                // The following checks are structured this way to emulate vanilla Terraria behavior.
                _ when GetDifficultyFlags(JourneyMask) => CharacterDifficulty.Journey,
                _ when GetDifficultyFlags(HardcoreMask) => CharacterDifficulty.Hardcore,
                _ when GetDifficultyFlags(MediumcoreMask) => CharacterDifficulty.Mediumcore,

                _ => CharacterDifficulty.Classic
            };

            set
            {
                _difficultyFlags &= unchecked((byte)~DifficultyMask);

                SetDifficultyFlags(value switch
                {
                    CharacterDifficulty.Journey => JourneyMask,
                    CharacterDifficulty.Hardcore => HardcoreMask,
                    CharacterDifficulty.Mediumcore => MediumcoreMask,
                    _ => (byte)0
                }, true);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player has an extra accessory slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player has an extra accessory slot; otherwise, <see langword="false"/>.
        /// </value>
        public bool HasExtraAccessorySlot
        {
            get => GetDifficultyFlags(HasExtraAccessorySlotMask);
            set => SetDifficultyFlags(HasExtraAccessorySlotMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is using biome torches.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is using biome torches; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsUsingBiomeTorches
        {
            get => GetTorchFlags(IsUsingBiomeTorchesMask);
            set => SetTorchFlags(IsUsingBiomeTorchesMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is fighting The Torch God.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is fighting The Torch God; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsFightingTheTorchGod
        {
            get => GetTorchFlags(IsFightingTheTorchGodMask);
            set => SetTorchFlags(IsFightingTheTorchGodMask, value);
        }

        PacketId IPacket.Id => PacketId.PlayerCharacter;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context)
        {
            var index = span.Read(ref this.AsRefByte(0), 3);
            index += span[index..].Read(Encoding.UTF8, out _name);
            return index + span[index..].Read(ref this.AsRefByte(16), 27);
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context)
        {
            var index = span.Write(ref this.AsRefByte(0), 3);
            index += span[index..].Write(Name, Encoding.UTF8);
            return index + span[index..].Write(ref this.AsRefByte(16), 27);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool GetAccessoryHideSlotsFlags(ushort mask) => (_hideAccessorySlotsFlags & mask) != 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetAccessoryHideSlotsFlags(ushort mask, bool value)
        {
            if (value)
            {
                _hideAccessorySlotsFlags |= mask;
            }
            else
            {
                _hideAccessorySlotsFlags &= (ushort)~mask;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool GetHideMiscSlotsFlags(byte mask) => (_hideMiscSlotsFlags & mask) != 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetHideMiscSlotsFlags(byte mask, bool value)
        {
            if (value)
            {
                _hideMiscSlotsFlags |= mask;
            }
            else
            {
                _hideMiscSlotsFlags &= (byte)~mask;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool GetDifficultyFlags(byte mask) => (_difficultyFlags & mask) != 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetDifficultyFlags(byte mask, bool value)
        {
            if (value)
            {
                _difficultyFlags |= mask;
            }
            else
            {
                _difficultyFlags &= (byte)~mask;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool GetTorchFlags(byte mask) => (_torchFlags & mask) != 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetTorchFlags(byte mask, bool value)
        {
            if (value)
            {
                _torchFlags |= mask;
            }
            else
            {
                _torchFlags &= (byte)~mask;
            }
        }
    }
}
