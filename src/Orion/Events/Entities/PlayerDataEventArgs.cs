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

using System;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Orion.Entities;
using Orion.Networking.Packets.Entities;

namespace Orion.Events.Entities {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PlayerData"/> event.
    /// </summary>
    [PublicAPI]
    public sealed class PlayerDataEventArgs : PlayerEventArgs, ICancelable {
        [NotNull] private readonly PlayerDataPacket _packet;

        /// <inheritdoc />
        public bool IsCanceled { get; set; }

        /// <summary>
        /// Gets or sets the player's skin type.
        /// </summary>
        public byte PlayerSkinType {
            get => _packet.PlayerSkinType;
            set => _packet.PlayerSkinType = value;
        }

        /// <summary>
        /// Gets or sets the player's hair type.
        /// </summary>
        public byte PlayerHairType {
            get => _packet.PlayerHairType;
            set => _packet.PlayerHairType = value;
        }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        [NotNull]
        public string PlayerName {
            get => _packet.PlayerName;
            set => _packet.PlayerName = value;
        }

        /// <summary>
        /// Gets or sets the player's hair dye.
        /// </summary>
        public byte PlayerHairDye {
            get => _packet.PlayerHairDye;
            set => _packet.PlayerHairDye = value;
        }

        /// <summary>
        /// Gets or sets the player's hidden visuals flags.
        /// </summary>
        public ushort PlayerHiddenVisualsFlags {
            get => _packet.PlayerHiddenVisualsFlags;
            set => _packet.PlayerHiddenVisualsFlags = value;
        }

        /// <summary>
        /// Gets or sets the player's hidden misc flags.
        /// </summary>
        public byte PlayerHiddenMiscFlags {
            get => _packet.PlayerHiddenMiscFlags;
            set => _packet.PlayerHiddenMiscFlags = value;
        }

        /// <summary>
        /// Gets or sets the player's hair color.
        /// </summary>
        public Color PlayerHairColor {
            get => _packet.PlayerHairColor;
            set => _packet.PlayerHairColor = value;
        }

        /// <summary>
        /// Gets or sets the player's skin color.
        /// </summary>
        public Color PlayerSkinColor {
            get => _packet.PlayerSkinColor;
            set => _packet.PlayerSkinColor = value;
        }

        /// <summary>
        /// Gets or sets the player's eye color.
        /// </summary>
        public Color PlayerEyeColor {
            get => _packet.PlayerEyeColor;
            set => _packet.PlayerEyeColor = value;
        }

        /// <summary>
        /// Gets or sets the player's shirt color.
        /// </summary>
        public Color PlayerShirtColor {
            get => _packet.PlayerShirtColor;
            set => _packet.PlayerShirtColor = value;
        }

        /// <summary>
        /// Gets or sets the player's undershirt color.
        /// </summary>
        public Color PlayerUndershirtColor {
            get => _packet.PlayerUndershirtColor;
            set => _packet.PlayerUndershirtColor = value;
        }

        /// <summary>
        /// Gets or sets the player's pants color.
        /// </summary>
        public Color PlayerPantsColor {
            get => _packet.PlayerPantsColor;
            set => _packet.PlayerPantsColor = value;
        }

        /// <summary>
        /// Gets or sets the player's shoe color.
        /// </summary>
        public Color PlayerShoeColor {
            get => _packet.PlayerShoeColor;
            set => _packet.PlayerShoeColor = value;
        }

        /// <summary>
        /// Gets or sets the player's difficulty.
        /// </summary>
        public PlayerDifficulty PlayerDifficulty {
            get => _packet.PlayerDifficulty;
            set => _packet.PlayerDifficulty = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerDataEventArgs"/> class with the specified player and
        /// packet.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="packet"/> are <c>null</c>.
        /// </exception>
        public PlayerDataEventArgs([NotNull] IPlayer player, [NotNull] PlayerDataPacket packet) : base(player) {
            _packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }
    }
}
