using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update a player's zones.
    /// </summary>
    public sealed class UpdatePlayerZonesPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a dungeon zone.
        /// </summary>
        public bool IsNearDungeonZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a corruption zone.
        /// </summary>
        public bool IsNearCorruptionZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a hallowed zone.
        /// </summary>
        public bool IsNearHallowedZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a meteor zone.
        /// </summary>
        public bool IsNearMeteorZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a jungle zone.
        /// </summary>
        public bool IsNearJungleZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a snow zone.
        /// </summary>
        public bool IsNearSnowZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a crimson zone.
        /// </summary>
        public bool IsNearCrimsonZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a water candle zone.
        /// </summary>
        public bool IsNearWaterCandleZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a peace candle zone.
        /// </summary>
        public bool IsNearPeaceCandleZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a solar tower zone.
        /// </summary>
        public bool IsNearSolarTowerZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a vortex tower zone.
        /// </summary>
        public bool IsNearVortexTowerZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a nebula tower zone.
        /// </summary>
        public bool IsNearNebulaTowerZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a stardust tower zone.
        /// </summary>
        public bool IsNearStardustTowerZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a desert zone.
        /// </summary>
        public bool IsNearDesertZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a glowing mushroom zone.
        /// </summary>
        public bool IsNearGlowingMushroomZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near an underground desert zone.
        /// </summary>
        public bool IsNearUndergroundDesertZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near the sky height zone.
        /// </summary>
        public bool IsNearSkyHeightZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the overworld height zone.
        /// </summary>
        public bool IsNearOverworldHeightZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the dirt layer height zone.
        /// </summary>
        public bool IsNearDirtLayerHeightZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the rock layer height zone.
        /// </summary>
        public bool IsNearRockLayerHeightZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the underworld layer height zone.
        /// </summary>
        public bool IsNearUnderworldHeightZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a beach zone.
        /// </summary>
        public bool IsNearBeachZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a rain zone.
        /// </summary>
        public bool IsNearRainZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a sandstorm zone.
        /// </summary>
        public bool IsNearSandstormZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near an Old One's Army zone.
        /// </summary>
        public bool IsNearOldOnesArmyZone { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();

            Terraria.BitsByte zoneFlags = reader.ReadByte();
            Terraria.BitsByte zoneFlags2 = reader.ReadByte();
            Terraria.BitsByte zoneFlags3 = reader.ReadByte();
            Terraria.BitsByte zoneFlags4 = reader.ReadByte();
            IsNearDungeonZone = zoneFlags[0];
            IsNearCorruptionZone = zoneFlags[1];
            IsNearHallowedZone = zoneFlags[2];
            IsNearMeteorZone = zoneFlags[3];
            IsNearJungleZone = zoneFlags[4];
            IsNearSnowZone = zoneFlags[5];
            IsNearCrimsonZone = zoneFlags[6];
            IsNearWaterCandleZone = zoneFlags[7];
            IsNearPeaceCandleZone = zoneFlags2[0];
            IsNearSolarTowerZone = zoneFlags2[1];
            IsNearVortexTowerZone = zoneFlags2[2];
            IsNearNebulaTowerZone = zoneFlags2[3];
            IsNearStardustTowerZone = zoneFlags2[4];
            IsNearDesertZone = zoneFlags2[5];
            IsNearGlowingMushroomZone = zoneFlags2[6];
            IsNearUndergroundDesertZone = zoneFlags2[7];
            IsNearSkyHeightZone = zoneFlags3[0];
            IsNearOverworldHeightZone = zoneFlags3[1];
            IsNearDirtLayerHeightZone = zoneFlags3[2];
            IsNearRockLayerHeightZone = zoneFlags3[3];
            IsNearUnderworldHeightZone = zoneFlags3[4];
            IsNearBeachZone = zoneFlags3[5];
            IsNearRainZone = zoneFlags3[6];
            IsNearSandstormZone = zoneFlags3[7];
            IsNearOldOnesArmyZone = zoneFlags4[0];
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);

            Terraria.BitsByte zoneFlags = 0;
            Terraria.BitsByte zoneFlags2 = 0;
            Terraria.BitsByte zoneFlags3 = 0;
            Terraria.BitsByte zoneFlags4 = 0;
            zoneFlags[0] = IsNearDungeonZone;
            zoneFlags[1] = IsNearCorruptionZone;
            zoneFlags[2] = IsNearHallowedZone;
            zoneFlags[3] = IsNearMeteorZone;
            zoneFlags[4] = IsNearJungleZone;
            zoneFlags[5] = IsNearSnowZone;
            zoneFlags[6] = IsNearCrimsonZone;
            zoneFlags[7] = IsNearWaterCandleZone;
            zoneFlags2[0] = IsNearPeaceCandleZone;
            zoneFlags2[1] = IsNearSolarTowerZone;
            zoneFlags2[2] = IsNearVortexTowerZone;
            zoneFlags2[3] = IsNearNebulaTowerZone;
            zoneFlags2[4] = IsNearStardustTowerZone;
            zoneFlags2[5] = IsNearDesertZone;
            zoneFlags2[6] = IsNearGlowingMushroomZone;
            zoneFlags2[7] = IsNearUndergroundDesertZone;
            zoneFlags3[0] = IsNearSkyHeightZone;
            zoneFlags3[1] = IsNearOverworldHeightZone;
            zoneFlags3[2] = IsNearDirtLayerHeightZone;
            zoneFlags3[3] = IsNearRockLayerHeightZone;
            zoneFlags3[4] = IsNearUnderworldHeightZone;
            zoneFlags3[5] = IsNearBeachZone;
            zoneFlags3[6] = IsNearRainZone;
            zoneFlags3[7] = IsNearSandstormZone;
            zoneFlags4[0] = IsNearOldOnesArmyZone;
            writer.Write(zoneFlags);
            writer.Write(zoneFlags2);
            writer.Write(zoneFlags3);
            writer.Write(zoneFlags4);
        }
    }
}
