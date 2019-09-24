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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;

namespace Orion.Packets.Entities {
    /// <summary>
    /// Packet sent to set a player's zones.
    /// </summary>
    [PublicAPI]
    public sealed class PlayerZonesPacket : Packet {
        private byte _playerIndex;
        private bool _isPlayerNearDungeonZone;
        private bool _isPlayerNearCorruptionZone;
        private bool _isPlayerNearHallowedZone;
        private bool _isPlayerNearMeteorZone;
        private bool _isPlayerNearJungleZone;
        private bool _isPlayerNearSnowZone;
        private bool _isPlayerNearCrimsonZone;
        private bool _isPlayerNearWaterCandleZone;
        private bool _isPlayerNearPeaceCandleZone;
        private bool _isPlayerNearSolarTowerZone;
        private bool _isPlayerNearVortexTowerZone;
        private bool _isPlayerNearNebulaTowerZone;
        private bool _isPlayerNearStardustTowerZone;
        private bool _isPlayerNearDesertZone;
        private bool _isPlayerNearGlowingMushroomZone;
        private bool _isPlayerNearUndergroundDesertZone;
        private bool _isPlayerNearSkyHeightZone;
        private bool _isPlayerNearOverworldHeightZone;
        private bool _isPlayerNearDirtLayerHeightZone;
        private bool _isPlayerNearRockLayerHeightZone;
        private bool _isPlayerNearUnderworldHeightZone;
        private bool _isPlayerNearBeachZone;
        private bool _isPlayerNearRainZone;
        private bool _isPlayerNearSandstormZone;
        private bool _isPlayerNearOldOnesArmyZone;

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerZones;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
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
        public bool IsPlayerNearDungeonZone {
            get => _isPlayerNearDungeonZone;
            set {
                _isPlayerNearDungeonZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a corruption zone.
        /// </summary>
        public bool IsPlayerNearCorruptionZone {
            get => _isPlayerNearCorruptionZone;
            set {
                _isPlayerNearCorruptionZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a hallowed zone.
        /// </summary>
        public bool IsPlayerNearHallowedZone {
            get => _isPlayerNearHallowedZone;
            set {
                _isPlayerNearHallowedZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a meteor zone.
        /// </summary>
        public bool IsPlayerNearMeteorZone {
            get => _isPlayerNearMeteorZone;
            set {
                _isPlayerNearMeteorZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a jungle zone.
        /// </summary>
        public bool IsPlayerNearJungleZone {
            get => _isPlayerNearJungleZone;
            set {
                _isPlayerNearJungleZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a snow zone.
        /// </summary>
        public bool IsPlayerNearSnowZone {
            get => _isPlayerNearSnowZone;
            set {
                _isPlayerNearSnowZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a crimson zone.
        /// </summary>
        public bool IsPlayerNearCrimsonZone {
            get => _isPlayerNearCrimsonZone;
            set {
                _isPlayerNearCrimsonZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a water candle zone.
        /// </summary>
        public bool IsPlayerNearWaterCandleZone {
            get => _isPlayerNearWaterCandleZone;
            set {
                _isPlayerNearWaterCandleZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a peace candle zone.
        /// </summary>
        public bool IsPlayerNearPeaceCandleZone {
            get => _isPlayerNearPeaceCandleZone;
            set {
                _isPlayerNearPeaceCandleZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a solar tower zone.
        /// </summary>
        public bool IsPlayerNearSolarTowerZone {
            get => _isPlayerNearSolarTowerZone;
            set {
                _isPlayerNearSolarTowerZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a vortex tower zone.
        /// </summary>
        public bool IsPlayerNearVortexTowerZone {
            get => _isPlayerNearVortexTowerZone;
            set {
                _isPlayerNearVortexTowerZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a nebula tower zone.
        /// </summary>
        public bool IsPlayerNearNebulaTowerZone {
            get => _isPlayerNearNebulaTowerZone;
            set {
                _isPlayerNearNebulaTowerZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a stardust tower zone.
        /// </summary>
        public bool IsPlayerNearStardustTowerZone {
            get => _isPlayerNearStardustTowerZone;
            set {
                _isPlayerNearStardustTowerZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a desert zone.
        /// </summary>
        public bool IsPlayerNearDesertZone {
            get => _isPlayerNearDesertZone;
            set {
                _isPlayerNearDesertZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a glowing mushroom zone.
        /// </summary>
        public bool IsPlayerNearGlowingMushroomZone {
            get => _isPlayerNearGlowingMushroomZone;
            set {
                _isPlayerNearGlowingMushroomZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near an underground desert zone.
        /// </summary>
        public bool IsPlayerNearUndergroundDesertZone {
            get => _isPlayerNearUndergroundDesertZone;
            set {
                _isPlayerNearUndergroundDesertZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the sky height zone.
        /// </summary>
        public bool IsPlayerNearSkyHeightZone {
            get => _isPlayerNearSkyHeightZone;
            set {
                _isPlayerNearSkyHeightZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the overworld height zone.
        /// </summary>
        public bool IsPlayerNearOverworldHeightZone {
            get => _isPlayerNearOverworldHeightZone;
            set {
                _isPlayerNearOverworldHeightZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the dirt layer height zone.
        /// </summary>
        public bool IsPlayerNearDirtLayerHeightZone {
            get => _isPlayerNearDirtLayerHeightZone;
            set {
                _isPlayerNearDirtLayerHeightZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the rock layer height zone.
        /// </summary>
        public bool IsPlayerNearRockLayerHeightZone {
            get => _isPlayerNearRockLayerHeightZone;
            set {
                _isPlayerNearRockLayerHeightZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the underworld layer height zone.
        /// </summary>
        public bool IsPlayerNearUnderworldHeightZone {
            get => _isPlayerNearUnderworldHeightZone;
            set {
                _isPlayerNearUnderworldHeightZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a beach zone.
        /// </summary>
        public bool IsPlayerNearBeachZone {
            get => _isPlayerNearBeachZone;
            set {
                _isPlayerNearBeachZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a rain zone.
        /// </summary>
        public bool IsPlayerNearRainZone {
            get => _isPlayerNearRainZone;
            set {
                _isPlayerNearRainZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a sandstorm zone.
        /// </summary>
        public bool IsPlayerNearSandstormZone {
            get => _isPlayerNearSandstormZone;
            set {
                _isPlayerNearSandstormZone = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near an Old One's Army zone.
        /// </summary>
        public bool IsPlayerNearOldOnesArmyZone {
            get => _isPlayerNearOldOnesArmyZone;
            set {
                _isPlayerNearOldOnesArmyZone = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();

            Terraria.BitsByte zoneFlags = reader.ReadByte();
            Terraria.BitsByte zoneFlags2 = reader.ReadByte();
            Terraria.BitsByte zoneFlags3 = reader.ReadByte();
            Terraria.BitsByte zoneFlags4 = reader.ReadByte();
            _isPlayerNearDungeonZone = zoneFlags[0];
            _isPlayerNearCorruptionZone = zoneFlags[1];
            _isPlayerNearHallowedZone = zoneFlags[2];
            _isPlayerNearMeteorZone = zoneFlags[3];
            _isPlayerNearJungleZone = zoneFlags[4];
            _isPlayerNearSnowZone = zoneFlags[5];
            _isPlayerNearCrimsonZone = zoneFlags[6];
            _isPlayerNearWaterCandleZone = zoneFlags[7];
            _isPlayerNearPeaceCandleZone = zoneFlags2[0];
            _isPlayerNearSolarTowerZone = zoneFlags2[1];
            _isPlayerNearVortexTowerZone = zoneFlags2[2];
            _isPlayerNearNebulaTowerZone = zoneFlags2[3];
            _isPlayerNearStardustTowerZone = zoneFlags2[4];
            _isPlayerNearDesertZone = zoneFlags2[5];
            _isPlayerNearGlowingMushroomZone = zoneFlags2[6];
            _isPlayerNearUndergroundDesertZone = zoneFlags2[7];
            _isPlayerNearSkyHeightZone = zoneFlags3[0];
            _isPlayerNearOverworldHeightZone = zoneFlags3[1];
            _isPlayerNearDirtLayerHeightZone = zoneFlags3[2];
            _isPlayerNearRockLayerHeightZone = zoneFlags3[3];
            _isPlayerNearUnderworldHeightZone = zoneFlags3[4];
            _isPlayerNearBeachZone = zoneFlags3[5];
            _isPlayerNearRainZone = zoneFlags3[6];
            _isPlayerNearSandstormZone = zoneFlags3[7];
            _isPlayerNearOldOnesArmyZone = zoneFlags4[0];
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);

            Terraria.BitsByte zoneFlags = 0;
            Terraria.BitsByte zoneFlags2 = 0;
            Terraria.BitsByte zoneFlags3 = 0;
            Terraria.BitsByte zoneFlags4 = 0;
            zoneFlags[0] = _isPlayerNearDungeonZone;
            zoneFlags[1] = _isPlayerNearCorruptionZone;
            zoneFlags[2] = _isPlayerNearHallowedZone;
            zoneFlags[3] = _isPlayerNearMeteorZone;
            zoneFlags[4] = _isPlayerNearJungleZone;
            zoneFlags[5] = _isPlayerNearSnowZone;
            zoneFlags[6] = _isPlayerNearCrimsonZone;
            zoneFlags[7] = _isPlayerNearWaterCandleZone;
            zoneFlags2[0] = _isPlayerNearPeaceCandleZone;
            zoneFlags2[1] = _isPlayerNearSolarTowerZone;
            zoneFlags2[2] = _isPlayerNearVortexTowerZone;
            zoneFlags2[3] = _isPlayerNearNebulaTowerZone;
            zoneFlags2[4] = _isPlayerNearStardustTowerZone;
            zoneFlags2[5] = _isPlayerNearDesertZone;
            zoneFlags2[6] = _isPlayerNearGlowingMushroomZone;
            zoneFlags2[7] = _isPlayerNearUndergroundDesertZone;
            zoneFlags3[0] = _isPlayerNearSkyHeightZone;
            zoneFlags3[1] = _isPlayerNearOverworldHeightZone;
            zoneFlags3[2] = _isPlayerNearDirtLayerHeightZone;
            zoneFlags3[3] = _isPlayerNearRockLayerHeightZone;
            zoneFlags3[4] = _isPlayerNearUnderworldHeightZone;
            zoneFlags3[5] = _isPlayerNearBeachZone;
            zoneFlags3[6] = _isPlayerNearRainZone;
            zoneFlags3[7] = _isPlayerNearSandstormZone;
            zoneFlags4[0] = _isPlayerNearOldOnesArmyZone;
            writer.Write(zoneFlags);
            writer.Write(zoneFlags2);
            writer.Write(zoneFlags3);
            writer.Write(zoneFlags4);
        }
    }
}
