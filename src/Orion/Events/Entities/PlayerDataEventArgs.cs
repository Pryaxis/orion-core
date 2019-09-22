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

        /// <inheritdoc cref="PlayerDataPacket.PlayerSkinType"/>
        public byte PlayerSkinType {
            get => _packet.PlayerSkinType;
            set => _packet.PlayerSkinType = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerHairType"/>
        public byte PlayerHairType {
            get => _packet.PlayerHairType;
            set => _packet.PlayerHairType = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerName"/>
        [NotNull]
        public string PlayerName {
            get => _packet.PlayerName;
            set => _packet.PlayerName = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerHairDye"/>
        public byte PlayerHairDye {
            get => _packet.PlayerHairDye;
            set => _packet.PlayerHairDye = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerHiddenVisualsFlags"/>
        public ushort PlayerHiddenVisualsFlags {
            get => _packet.PlayerHiddenVisualsFlags;
            set => _packet.PlayerHiddenVisualsFlags = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerHiddenMiscFlags"/>
        public byte PlayerHiddenMiscFlags {
            get => _packet.PlayerHiddenMiscFlags;
            set => _packet.PlayerHiddenMiscFlags = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerHairColor"/>
        public Color PlayerHairColor {
            get => _packet.PlayerHairColor;
            set => _packet.PlayerHairColor = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerSkinColor"/>
        public Color PlayerSkinColor {
            get => _packet.PlayerSkinColor;
            set => _packet.PlayerSkinColor = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerEyeColor"/>
        public Color PlayerEyeColor {
            get => _packet.PlayerEyeColor;
            set => _packet.PlayerEyeColor = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerShirtColor"/>
        public Color PlayerShirtColor {
            get => _packet.PlayerShirtColor;
            set => _packet.PlayerShirtColor = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerUndershirtColor"/>
        public Color PlayerUndershirtColor {
            get => _packet.PlayerUndershirtColor;
            set => _packet.PlayerUndershirtColor = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerPantsColor"/>
        public Color PlayerPantsColor {
            get => _packet.PlayerPantsColor;
            set => _packet.PlayerPantsColor = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerShoeColor"/>
        public Color PlayerShoeColor {
            get => _packet.PlayerShoeColor;
            set => _packet.PlayerShoeColor = value;
        }
        
        /// <inheritdoc cref="PlayerDataPacket.PlayerDifficulty"/>
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
