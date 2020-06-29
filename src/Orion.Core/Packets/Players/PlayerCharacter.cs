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
using Orion.Core.Players;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to set a player's character.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 44)]
    public sealed class PlayerCharacter : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(8)] private string _name = string.Empty;
        [FieldOffset(16)] private byte _bytes2;  // Used to obtain an interior reference.
        [FieldOffset(17)] private Flags8 _hideAccessorySlotsFlags;
        [FieldOffset(18)] private Flags8 _hideAccessorySlotsFlags2;
        [FieldOffset(19)] private Flags8 _hideMiscSlotsFlags;
        [FieldOffset(41)] private Flags8 _difficultyFlags;
        [FieldOffset(42)] private Flags8 _torchFlags;

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
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the player's hair dye.
        /// </summary>
        /// <value>The player's hair dye.</value>
        [field: FieldOffset(16)] public byte HairDye { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the player's first accessory slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to hide the player's first accessory slot; otherwise, <see langword="false"/>.
        /// </value>
        public bool HideAccessorySlot1
        {
            get => _hideAccessorySlotsFlags[3];
            set => _hideAccessorySlotsFlags[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the player's second accessory slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to hide the player's second accessory slot; otherwise, <see langword="false"/>.
        /// </value>
        public bool HideAccessorySlot2
        {
            get => _hideAccessorySlotsFlags[4];
            set => _hideAccessorySlotsFlags[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the player's third accessory slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to hide the player's third accessory slot; otherwise, <see langword="false"/>.
        /// </value>
        public bool HideAccessorySlot3
        {
            get => _hideAccessorySlotsFlags[5];
            set => _hideAccessorySlotsFlags[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the player's fourth accessory slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to hide the player's fourth accessory slot; otherwise, <see langword="false"/>.
        /// </value>
        public bool HideAccessorySlot4
        {
            get => _hideAccessorySlotsFlags[6];
            set => _hideAccessorySlotsFlags[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the player's fifth accessory slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to hide the player's fifth accessory slot; otherwise, <see langword="false"/>.
        /// </value>
        public bool HideAccessorySlot5
        {
            get => _hideAccessorySlotsFlags[7];
            set => _hideAccessorySlotsFlags[7] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the player's sixth accessory slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to hide the player's sixth accessory slot; otherwise, <see langword="false"/>.
        /// </value>
        public bool HideAccessorySlot6
        {
            get => _hideAccessorySlotsFlags2[0];
            set => _hideAccessorySlotsFlags2[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the player's seventh accessory slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to hide the player's seventh accessory slot; otherwise, <see langword="false"/>.
        /// </value>
        public bool HideAccessorySlot7
        {
            get => _hideAccessorySlotsFlags2[1];
            set => _hideAccessorySlotsFlags2[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the player's pet slot.
        /// </summary>
        /// <value><see langword="true"/> to hide the player's pet slot; otherwise, <see langword="false"/>.</value>
        public bool HidePetSlot
        {
            get => _hideMiscSlotsFlags[0];
            set => _hideMiscSlotsFlags[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the player's light pet slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to hide the player's light pet slot; otherwise, <see langword="false"/>.
        /// </value>
        public bool HideLightPetSlot
        {
            get => _hideMiscSlotsFlags[1];
            set => _hideMiscSlotsFlags[1] = value;
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
                _ when _difficultyFlags[3] => CharacterDifficulty.Journey,
                _ when _difficultyFlags[1] => CharacterDifficulty.Hardcore,
                _ when _difficultyFlags[0] => CharacterDifficulty.Mediumcore,
                _ => CharacterDifficulty.Classic
            };

            set
            {
                _difficultyFlags[3] = false;
                _difficultyFlags[1] = false;
                _difficultyFlags[0] = false;

                switch (value)
                {
                case CharacterDifficulty.Journey:
                    _difficultyFlags[3] = true;
                    break;
                case CharacterDifficulty.Hardcore:
                    _difficultyFlags[1] = true;
                    break;
                case CharacterDifficulty.Mediumcore:
                    _difficultyFlags[0] = true;
                    break;
                }
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
            get => _difficultyFlags[2];
            set => _difficultyFlags[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is using biome torches.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is using biome torches; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsUsingBiomeTorches
        {
            get => _torchFlags[0];
            set => _torchFlags[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is fighting The Torch God.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is fighting The Torch God; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsFightingTheTorchGod
        {
            get => _torchFlags[1];
            set => _torchFlags[1] = value;
        }

        PacketId IPacket.Id => PacketId.PlayerCharacter;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 3);
            length += span[length..].Read(out _name);
            length += span[length..].Read(ref _bytes2, 27);
            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 3);
            length += span[length..].Write(Name);
            length += span[length..].Write(ref _bytes2, 27);
            return length;
        }
    }
}
