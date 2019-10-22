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
using Orion.Items;
using Orion.Packets.Players;
using Orion.Players;
using Orion.Utils;

namespace Orion.Events.Players {
    /// <summary>
    /// An event that occurs when a player sends their data. This event can be canceled and modified.
    /// </summary>
    [Event("player-data")]
    public sealed class PlayerDataEvent : PlayerEvent, ICancelable, IDirtiable {
        private readonly PlayerDataPacket _packet;
        
        /// <inheritdoc/>
        public bool IsDirty => _packet.IsDirty;

        /// <inheritdoc/>
        public string? CancellationReason { get; set; }

        /// <summary>
        /// Gets or sets the player's clothes style.
        /// </summary>
        /// <value>The player's clothes style.</value>
        public byte ClothesStyle {
            get => _packet.ClothesStyle;
            set => _packet.ClothesStyle = value;
        }

        /// <summary>
        /// Gets or sets the player's hairstyle.
        /// </summary>
        /// <value>The player's hairstyle.</value>
        public byte Hairstyle {
            get => _packet.Hairstyle;
            set => _packet.Hairstyle = value;
        }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        /// <value>The player's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Name {
            get => _packet.Name;
            set => _packet.Name = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the player's hair dye.
        /// </summary>
        /// <value>The player's hair dye.</value>
        public byte HairDye {
            get => _packet.HairDye;
            set => _packet.HairDye = value;
        }

        /// <summary>
        /// Gets or sets the player's equipment hiding flags.
        /// </summary>
        /// <value>The player's equipment hiding flags.</value>
        /// <remarks>This property's value specifies which of the player's equipment is visible.</remarks>
        public ushort EquipmentHiddenFlags {
            get => _packet.EquipmentHiddenFlags;
            set => _packet.EquipmentHiddenFlags = value;
        }

        /// <summary>
        /// Gets or sets the player's miscellaneous equipment hiding flags.
        /// </summary>
        /// <value>The player's miscellaneous equipment hiding flags.</value>
        /// <remarks>This property's value specifies which of the player's miscellaneous equipment is visible.</remarks>
        public byte MiscEquipmentHiddenFlags {
            get => _packet.MiscEquipmentHiddenFlags;
            set => _packet.MiscEquipmentHiddenFlags = value;
        }

        /// <summary>
        /// Gets or sets the player's hair color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's hair color.</value>
        public Color HairColor {
            get => _packet.HairColor;
            set => _packet.HairColor = value;
        }

        /// <summary>
        /// Gets or sets the player's skin color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's skin color.</value>
        public Color SkinColor {
            get => _packet.SkinColor;
            set => _packet.SkinColor = value;
        }

        /// <summary>
        /// Gets or sets the player's eye color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's eye color.</value>
        public Color EyeColor {
            get => _packet.EyeColor;
            set => _packet.EyeColor = value;
        }

        /// <summary>
        /// Gets or sets the player's shirt color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's shirt color.</value>
        public Color ShirtColor {
            get => _packet.ShirtColor;
            set => _packet.ShirtColor = value;
        }

        /// <summary>
        /// Gets or sets the player's undershirt color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's undershirt color.</value>
        public Color UndershirtColor {
            get => _packet.UndershirtColor;
            set => _packet.UndershirtColor = value;
        }

        /// <summary>
        /// Gets or sets the player's pants color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's pants color.</value>
        public Color PantsColor {
            get => _packet.PantsColor;
            set => _packet.PantsColor = value;
        }

        /// <summary>
        /// Gets or sets the player's shoe color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's shoe color.</value>
        public Color ShoeColor {
            get => _packet.ShoeColor;
            set => _packet.ShoeColor = value;
        }

        /// <summary>
        /// Gets or sets the player's difficulty.
        /// </summary>
        /// <value>The player's difficulty.</value>
        public PlayerDifficulty Difficulty {
            get => _packet.Difficulty;
            set => _packet.Difficulty = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player has an extra accessory slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player has an extra accessory slot; otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// The extra accessory slot can be obtained with a <see cref="ItemType.DemonHeart"/>. There is also support for
        /// a seventh accessory slot but it is not legitimately obtainable.
        /// </remarks>
        public bool HasExtraAccessorySlot {
            get => _packet.HasExtraAccessorySlot;
            set => _packet.HasExtraAccessorySlot = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerDataEvent"/> class with the specified player and packet.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="packet"/> are <see langword="null"/>.
        /// </exception>
        public PlayerDataEvent(IPlayer player, PlayerDataPacket packet) : base(player) {
            _packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }
        
        /// <inheritdoc/>
        public void Clean() => _packet.Clean();
    }
}
