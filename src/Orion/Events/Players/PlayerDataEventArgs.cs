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
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Packets.Players;
using Orion.Players;

namespace Orion.Events.Players {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PlayerData"/> event. This event can be canceled.
    /// </summary>
    [EventArgs("player-data")]
    public sealed class PlayerDataEventArgs : PlayerEventArgs, ICancelable {
        private readonly PlayerDataPacket _packet;

        /// <inheritdoc/>
        public string? CancellationReason { get; set; }

        /// <summary>
        /// Gets or sets the player's clothes style.
        /// </summary>
        /// <value>The player's clothes style.</value>
        public byte PlayerClothesStyle {
            get => _packet.PlayerClothesStyle;
            set => _packet.PlayerClothesStyle = value;
        }

        /// <summary>
        /// Gets or sets the player's hairstyle.
        /// </summary>
        /// <value>The player's hairstyle.</value>
        public byte PlayerHairstyle {
            get => _packet.PlayerHairstyle;
            set => _packet.PlayerHairstyle = value;
        }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        /// <value>The player's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string PlayerName {
            get => _packet.PlayerName;
            set => _packet.PlayerName = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the player's hair dye.
        /// </summary>
        /// <value>The player's hair dye.</value>
        public byte PlayerHairDye {
            get => _packet.PlayerHairDye;
            set => _packet.PlayerHairDye = value;
        }
        
        /// <summary>
        /// Gets or sets the player's equipment hiding flags.
        /// </summary>
        /// <value>The player's equipment hiding flags.</value>
        /// <remarks>This property specifies which of the player's equipment is visible.</remarks>
        public ushort PlayerEquipmentHiddenFlags {
            get => _packet.PlayerEquipmentHiddenFlags;
            set => _packet.PlayerEquipmentHiddenFlags = value;
        }
        
        /// <summary>
        /// Gets or sets the player's miscellaneous equipment hiding flags.
        /// </summary>
        /// <value>The player's miscellaneous equipment hiding flags.</value>
        /// <remarks>This property specifies which of the player's miscellaneous equipment is visible.</remarks>
        public byte PlayerMiscEquipmentHiddenFlags {
            get => _packet.PlayerMiscEquipmentHiddenFlags;
            set => _packet.PlayerMiscEquipmentHiddenFlags = value;
        }
        
        /// <summary>
        /// Gets or sets the player's hair color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's hair color.</value>
        public Color PlayerHairColor {
            get => _packet.PlayerHairColor;
            set => _packet.PlayerHairColor = value;
        }
        
        /// <summary>
        /// Gets or sets the player's skin color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's skin color.</value>
        public Color PlayerSkinColor {
            get => _packet.PlayerSkinColor;
            set => _packet.PlayerSkinColor = value;
        }
        
        /// <summary>
        /// Gets or sets the player's eye color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's eye color.</value>
        public Color PlayerEyeColor {
            get => _packet.PlayerEyeColor;
            set => _packet.PlayerEyeColor = value;
        }
        
        /// <summary>
        /// Gets or sets the player's shirt color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's shirt color.</value>
        public Color PlayerShirtColor {
            get => _packet.PlayerShirtColor;
            set => _packet.PlayerShirtColor = value;
        }
        
        /// <summary>
        /// Gets or sets the player's undershirt color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's undershirt color.</value>
        public Color PlayerUndershirtColor {
            get => _packet.PlayerUndershirtColor;
            set => _packet.PlayerUndershirtColor = value;
        }
        
        /// <summary>
        /// Gets or sets the player's pants color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's pants color.</value>
        public Color PlayerPantsColor {
            get => _packet.PlayerPantsColor;
            set => _packet.PlayerPantsColor = value;
        }

        /// <summary>
        /// Gets or sets the player's shoe color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's shoe color.</value>
        public Color PlayerShoeColor {
            get => _packet.PlayerShoeColor;
            set => _packet.PlayerShoeColor = value;
        }

        /// <summary>
        /// Gets or sets the player's difficulty.
        /// </summary>
        /// <value>The player's difficulty.</value>
        public PlayerDifficulty PlayerDifficulty {
            get => _packet.PlayerDifficulty;
            set => _packet.PlayerDifficulty = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player has an extra accessory slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player has an extra accessory slot; otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// The extra accessory slot can be obtained with the <see cref="ItemType.DemonHeart"/> item. There is also
        /// support for a seventh accessory slot, but it is not legitimately obtainable.
        /// </remarks>
        public bool PlayerHasExtraAccessorySlot {
            get => _packet.PlayerHasExtraAccessorySlot;
            set => _packet.PlayerHasExtraAccessorySlot = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerDataEventArgs"/> class with the specified player and
        /// packet.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="packet"/> are <see langword="null"/>.
        /// </exception>
        public PlayerDataEventArgs(IPlayer player, PlayerDataPacket packet) : base(player) {
            _packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }
        
        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"[{PlayerName}, ...]";
    }
}
