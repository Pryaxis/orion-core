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

using System.IO;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's zones.
    /// </summary>
    /// <remarks>This packet is used to synchronize player state.</remarks>
    public sealed class PlayerZonesPacket : Packet {
        private byte _playerIndex;
        private bool _isNearDungeonZone;
        private bool _isNearCorruptionZone;
        private bool _isNearHallowedZone;
        private bool _isNearMeteorZone;
        private bool _isNearJungleZone;
        private bool _isNearSnowZone;
        private bool _isNearCrimsonZone;
        private bool _isNearWaterCandleZone;
        private bool _isNearPeaceCandleZone;
        private bool _isNearSolarTowerZone;
        private bool _isNearVortexTowerZone;
        private bool _isNearNebulaTowerZone;
        private bool _isNearStardustTowerZone;
        private bool _isNearDesertZone;
        private bool _isNearGlowingMushroomZone;
        private bool _isNearUndergroundDesertZone;
        private bool _isNearSkyHeightZone;
        private bool _isNearOverworldHeightZone;
        private bool _isNearDirtLayerHeightZone;
        private bool _isNearRockLayerHeightZone;
        private bool _isNearUnderworldHeightZone;
        private bool _isNearBeachZone;
        private bool _isNearRainZone;
        private bool _isNearSandstormZone;
        private bool _isNearOldOnesArmyZone;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerZones;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        /// <value>The player index.</value>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a dungeon zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a dungeon zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearDungeonZone {
            get => _isNearDungeonZone;
            set {
                _isNearDungeonZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a corruption zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a corruption zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearCorruptionZone {
            get => _isNearCorruptionZone;
            set {
                _isNearCorruptionZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a hallowed zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a hallowed zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearHallowedZone {
            get => _isNearHallowedZone;
            set {
                _isNearHallowedZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a meteor zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a meteor zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearMeteorZone {
            get => _isNearMeteorZone;
            set {
                _isNearMeteorZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a jungle zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a jungle zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearJungleZone {
            get => _isNearJungleZone;
            set {
                _isNearJungleZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a snow zone.
        /// </summary>
        /// <value><see langword="true"/> if the player is near a snow zone; otherwise, <see langword="false"/>.</value>
        public bool IsNearSnowZone {
            get => _isNearSnowZone;
            set {
                _isNearSnowZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a crimson zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a crimson zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearCrimsonZone {
            get => _isNearCrimsonZone;
            set {
                _isNearCrimsonZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a water candle zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a water candle zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearWaterCandleZone {
            get => _isNearWaterCandleZone;
            set {
                _isNearWaterCandleZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a peace candle zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a peace candle zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearPeaceCandleZone {
            get => _isNearPeaceCandleZone;
            set {
                _isNearPeaceCandleZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a solar tower zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a solar tower zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearSolarTowerZone {
            get => _isNearSolarTowerZone;
            set {
                _isNearSolarTowerZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a vortex tower zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a vortex tower zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearVortexTowerZone {
            get => _isNearVortexTowerZone;
            set {
                _isNearVortexTowerZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a nebula tower zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a nebula tower zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearNebulaTowerZone {
            get => _isNearNebulaTowerZone;
            set {
                _isNearNebulaTowerZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a stardust tower zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a stardust tower zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearStardustTowerZone {
            get => _isNearStardustTowerZone;
            set {
                _isNearStardustTowerZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a desert zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a desert zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearDesertZone {
            get => _isNearDesertZone;
            set {
                _isNearDesertZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a glowing mushroom zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a glowing mushroom zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearGlowingMushroomZone {
            get => _isNearGlowingMushroomZone;
            set {
                _isNearGlowingMushroomZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near an underground desert zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near an underground desert zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearUndergroundDesertZone {
            get => _isNearUndergroundDesertZone;
            set {
                _isNearUndergroundDesertZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the sky height zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the sky height zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearSkyHeightZone {
            get => _isNearSkyHeightZone;
            set {
                _isNearSkyHeightZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the overworld height zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the overworld height zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearOverworldHeightZone {
            get => _isNearOverworldHeightZone;
            set {
                _isNearOverworldHeightZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the dirt layer height zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the dirt layer height zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearDirtLayerHeightZone {
            get => _isNearDirtLayerHeightZone;
            set {
                _isNearDirtLayerHeightZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the rock layer height zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the rock layer height zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearRockLayerHeightZone {
            get => _isNearRockLayerHeightZone;
            set {
                _isNearRockLayerHeightZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the underworld layer height zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the underworld layer height zone; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool IsNearUnderworldHeightZone {
            get => _isNearUnderworldHeightZone;
            set {
                _isNearUnderworldHeightZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a beach zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a beach zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearBeachZone {
            get => _isNearBeachZone;
            set {
                _isNearBeachZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a rain zone.
        /// </summary>
        /// <value><see langword="true"/> if the player is near a rain zone; otherwise, <see langword="false"/>.</value>
        public bool IsNearRainZone {
            get => _isNearRainZone;
            set {
                _isNearRainZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a sandstorm zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a sandstorm zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearSandstormZone {
            get => _isNearSandstormZone;
            set {
                _isNearSandstormZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near an Old One's Army zone.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near an Old One's Army zone; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsNearOldOnesArmyZone {
            get => _isNearOldOnesArmyZone;
            set {
                _isNearOldOnesArmyZone = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();

            Terraria.BitsByte zoneFlags = reader.ReadByte();
            Terraria.BitsByte zoneFlags2 = reader.ReadByte();
            Terraria.BitsByte zoneFlags3 = reader.ReadByte();
            Terraria.BitsByte zoneFlags4 = reader.ReadByte();
            _isNearDungeonZone = zoneFlags[0];
            _isNearCorruptionZone = zoneFlags[1];
            _isNearHallowedZone = zoneFlags[2];
            _isNearMeteorZone = zoneFlags[3];
            _isNearJungleZone = zoneFlags[4];
            _isNearSnowZone = zoneFlags[5];
            _isNearCrimsonZone = zoneFlags[6];
            _isNearWaterCandleZone = zoneFlags[7];
            _isNearPeaceCandleZone = zoneFlags2[0];
            _isNearSolarTowerZone = zoneFlags2[1];
            _isNearVortexTowerZone = zoneFlags2[2];
            _isNearNebulaTowerZone = zoneFlags2[3];
            _isNearStardustTowerZone = zoneFlags2[4];
            _isNearDesertZone = zoneFlags2[5];
            _isNearGlowingMushroomZone = zoneFlags2[6];
            _isNearUndergroundDesertZone = zoneFlags2[7];
            _isNearSkyHeightZone = zoneFlags3[0];
            _isNearOverworldHeightZone = zoneFlags3[1];
            _isNearDirtLayerHeightZone = zoneFlags3[2];
            _isNearRockLayerHeightZone = zoneFlags3[3];
            _isNearUnderworldHeightZone = zoneFlags3[4];
            _isNearBeachZone = zoneFlags3[5];
            _isNearRainZone = zoneFlags3[6];
            _isNearSandstormZone = zoneFlags3[7];
            _isNearOldOnesArmyZone = zoneFlags4[0];
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);

            Terraria.BitsByte zoneFlags = 0;
            Terraria.BitsByte zoneFlags2 = 0;
            Terraria.BitsByte zoneFlags3 = 0;
            Terraria.BitsByte zoneFlags4 = 0;
            zoneFlags[0] = _isNearDungeonZone;
            zoneFlags[1] = _isNearCorruptionZone;
            zoneFlags[2] = _isNearHallowedZone;
            zoneFlags[3] = _isNearMeteorZone;
            zoneFlags[4] = _isNearJungleZone;
            zoneFlags[5] = _isNearSnowZone;
            zoneFlags[6] = _isNearCrimsonZone;
            zoneFlags[7] = _isNearWaterCandleZone;
            zoneFlags2[0] = _isNearPeaceCandleZone;
            zoneFlags2[1] = _isNearSolarTowerZone;
            zoneFlags2[2] = _isNearVortexTowerZone;
            zoneFlags2[3] = _isNearNebulaTowerZone;
            zoneFlags2[4] = _isNearStardustTowerZone;
            zoneFlags2[5] = _isNearDesertZone;
            zoneFlags2[6] = _isNearGlowingMushroomZone;
            zoneFlags2[7] = _isNearUndergroundDesertZone;
            zoneFlags3[0] = _isNearSkyHeightZone;
            zoneFlags3[1] = _isNearOverworldHeightZone;
            zoneFlags3[2] = _isNearDirtLayerHeightZone;
            zoneFlags3[3] = _isNearRockLayerHeightZone;
            zoneFlags3[4] = _isNearUnderworldHeightZone;
            zoneFlags3[5] = _isNearBeachZone;
            zoneFlags3[6] = _isNearRainZone;
            zoneFlags3[7] = _isNearSandstormZone;
            zoneFlags4[0] = _isNearOldOnesArmyZone;
            writer.Write(zoneFlags);
            writer.Write(zoneFlags2);
            writer.Write(zoneFlags3);
            writer.Write(zoneFlags4);
        }
    }
}
