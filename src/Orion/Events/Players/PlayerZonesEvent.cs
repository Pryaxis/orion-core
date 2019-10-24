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
using Destructurama.Attributed;
using Orion.Packets.Players;
using Orion.Players;
using Orion.Utils;

namespace Orion.Events.Players {
    /// <summary>
    /// An event that occurs when a player sends their zones. This event can be canceled and modified.
    /// </summary>
    [Event("player-zones")]
    public sealed class PlayerZonesEvent : PlayerEvent, ICancelable, IDirtiable {
        private readonly PlayerZonesPacket _packet;

        /// <inheritdoc/>
        [NotLogged]
        public string? CancellationReason { get; set; }

        /// <inheritdoc/>
        [NotLogged]
        public bool IsDirty => _packet.IsDirty;

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a dungeon zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a dungeon zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearDungeon {
            get => _packet.IsNearDungeon;
            set => _packet.IsNearDungeon = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a corruption zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a corruption zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearCorruption {
            get => _packet.IsNearCorruption;
            set => _packet.IsNearCorruption = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a hallowed zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a hallowed zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearHallowed {
            get => _packet.IsNearHallowed;
            set => _packet.IsNearHallowed = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a meteor zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a meteor zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearMeteor {
            get => _packet.IsNearMeteor;
            set => _packet.IsNearMeteor = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a jungle zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a jungle zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearJungle {
            get => _packet.IsNearJungle;
            set => _packet.IsNearJungle = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a snow zone.
        /// </summary>
        /// <value><see langword="true"/> if the player is near a snow zone; otherwise, <see langword="false"/>.</value>
        public bool IsNearSnow {
            get => _packet.IsNearSnow;
            set => _packet.IsNearSnow = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a crimson zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a crimson zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearCrimson {
            get => _packet.IsNearCrimson;
            set => _packet.IsNearCrimson = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a water candle zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a water candle zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearWaterCandle {
            get => _packet.IsNearWaterCandle;
            set => _packet.IsNearWaterCandle = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a peace candle zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a peace candle zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearPeaceCandle {
            get => _packet.IsNearPeaceCandle;
            set => _packet.IsNearPeaceCandle = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a solar pillar.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a solar pillar; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearSolarPillar {
            get => _packet.IsNearSolarPillar;
            set => _packet.IsNearSolarPillar = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a vortex pillar.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a vortex pillar; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearVortexPillar {
            get => _packet.IsNearVortexPillar;
            set => _packet.IsNearVortexPillar = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a nebula pillar.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a nebula pillar; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearNebulaPillar {
            get => _packet.IsNearNebulaPillar;
            set => _packet.IsNearNebulaPillar = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a stardust pillar.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a stardust pillar; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearStardustPillar {
            get => _packet.IsNearStardustPillar;
            set => _packet.IsNearStardustPillar = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a desert zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a desert zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearDesert {
            get => _packet.IsNearDesert;
            set => _packet.IsNearDesert = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a glowing mushroom zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a glowing mushroom zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearGlowingMushroom {
            get => _packet.IsNearGlowingMushroom;
            set => _packet.IsNearGlowingMushroom = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near an underground desert zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near an underground desert zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearUndergroundDesert {
            get => _packet.IsNearUndergroundDesert;
            set => _packet.IsNearUndergroundDesert = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the sky height zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the sky height zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearSkyHeight {
            get => _packet.IsNearSkyHeight;
            set => _packet.IsNearSkyHeight = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the overworld height zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the overworld height zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearOverworldHeight {
            get => _packet.IsNearOverworldHeight;
            set => _packet.IsNearOverworldHeight = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the dirt layer height zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the dirt layer height zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearDirtLayerHeight {
            get => _packet.IsNearDirtLayerHeight;
            set => _packet.IsNearDirtLayerHeight = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the rock layer height zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the rock layer height zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearRockLayerHeight {
            get => _packet.IsNearRockLayerHeight;
            set => _packet.IsNearRockLayerHeight = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the underworld layer height zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the underworld layer height zone; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool IsNearUnderworldHeight {
            get => _packet.IsNearUnderworldHeight;
            set => _packet.IsNearUnderworldHeight = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a beach zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a beach zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearBeach {
            get => _packet.IsNearBeach;
            set => _packet.IsNearBeach = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a rain zone.
        /// </summary>
        /// <value><see langword="true"/> if the player is near a rain zone; otherwise, <see langword="false"/>.</value>
        public bool IsNearRain {
            get => _packet.IsNearRain;
            set => _packet.IsNearRain = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a sandstorm zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a sandstorm zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearSandstorm {
            get => _packet.IsNearSandstorm;
            set => _packet.IsNearSandstorm = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near an Old One's Army zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near an Old One's Army zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearOldOnesArmy {
            get => _packet.IsNearOldOnesArmy;
            set => _packet.IsNearOldOnesArmy = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerZonesEvent"/> class with the specified player and packet.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="packet"/> are <see langword="null"/>.
        /// </exception>
        public PlayerZonesEvent(IPlayer player, PlayerZonesPacket packet) : base(player) {
            _packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }

        /// <inheritdoc/>
        public void Clean() => _packet.Clean();
    }
}
