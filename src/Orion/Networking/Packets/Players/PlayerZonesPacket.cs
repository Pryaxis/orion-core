using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's zones.
    /// </summary>
    public sealed class PlayerZonesPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a dungeon zone.
        /// </summary>
        public bool IsPlayerNearDungeonZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a corruption zone.
        /// </summary>
        public bool IsPlayerNearCorruptionZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a hallowed zone.
        /// </summary>
        public bool IsPlayerNearHallowedZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a meteor zone.
        /// </summary>
        public bool IsPlayerNearMeteorZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a jungle zone.
        /// </summary>
        public bool IsPlayerNearJungleZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a snow zone.
        /// </summary>
        public bool IsPlayerNearSnowZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a crimson zone.
        /// </summary>
        public bool IsPlayerNearCrimsonZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a water candle zone.
        /// </summary>
        public bool IsPlayerNearWaterCandleZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a peace candle zone.
        /// </summary>
        public bool IsPlayerNearPeaceCandleZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a solar tower zone.
        /// </summary>
        public bool IsPlayerNearSolarTowerZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a vortex tower zone.
        /// </summary>
        public bool IsPlayerNearVortexTowerZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a nebula tower zone.
        /// </summary>
        public bool IsPlayerNearNebulaTowerZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a stardust tower zone.
        /// </summary>
        public bool IsPlayerNearStardustTowerZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a desert zone.
        /// </summary>
        public bool IsPlayerNearDesertZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a glowing mushroom zone.
        /// </summary>
        public bool IsPlayerNearGlowingMushroomZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near an underground desert zone.
        /// </summary>
        public bool IsPlayerNearUndergroundDesertZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near the sky height zone.
        /// </summary>
        public bool IsPlayerNearSkyHeightZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the overworld height zone.
        /// </summary>
        public bool IsPlayerNearOverworldHeightZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the dirt layer height zone.
        /// </summary>
        public bool IsPlayerNearDirtLayerHeightZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the rock layer height zone.
        /// </summary>
        public bool IsPlayerNearRockLayerHeightZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the underworld layer height zone.
        /// </summary>
        public bool IsPlayerNearUnderworldHeightZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a beach zone.
        /// </summary>
        public bool IsPlayerNearBeachZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a rain zone.
        /// </summary>
        public bool IsPlayerNearRainZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near a sandstorm zone.
        /// </summary>
        public bool IsPlayerNearSandstormZone { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player is near an Old One's Army zone.
        /// </summary>
        public bool IsPlayerNearOldOnesArmyZone { get; set; }

        private protected override PacketType Type => PacketType.PlayerZones;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();

            Terraria.BitsByte zoneFlags = reader.ReadByte();
            Terraria.BitsByte zoneFlags2 = reader.ReadByte();
            Terraria.BitsByte zoneFlags3 = reader.ReadByte();
            Terraria.BitsByte zoneFlags4 = reader.ReadByte();
            IsPlayerNearDungeonZone = zoneFlags[0];
            IsPlayerNearCorruptionZone = zoneFlags[1];
            IsPlayerNearHallowedZone = zoneFlags[2];
            IsPlayerNearMeteorZone = zoneFlags[3];
            IsPlayerNearJungleZone = zoneFlags[4];
            IsPlayerNearSnowZone = zoneFlags[5];
            IsPlayerNearCrimsonZone = zoneFlags[6];
            IsPlayerNearWaterCandleZone = zoneFlags[7];
            IsPlayerNearPeaceCandleZone = zoneFlags2[0];
            IsPlayerNearSolarTowerZone = zoneFlags2[1];
            IsPlayerNearVortexTowerZone = zoneFlags2[2];
            IsPlayerNearNebulaTowerZone = zoneFlags2[3];
            IsPlayerNearStardustTowerZone = zoneFlags2[4];
            IsPlayerNearDesertZone = zoneFlags2[5];
            IsPlayerNearGlowingMushroomZone = zoneFlags2[6];
            IsPlayerNearUndergroundDesertZone = zoneFlags2[7];
            IsPlayerNearSkyHeightZone = zoneFlags3[0];
            IsPlayerNearOverworldHeightZone = zoneFlags3[1];
            IsPlayerNearDirtLayerHeightZone = zoneFlags3[2];
            IsPlayerNearRockLayerHeightZone = zoneFlags3[3];
            IsPlayerNearUnderworldHeightZone = zoneFlags3[4];
            IsPlayerNearBeachZone = zoneFlags3[5];
            IsPlayerNearRainZone = zoneFlags3[6];
            IsPlayerNearSandstormZone = zoneFlags3[7];
            IsPlayerNearOldOnesArmyZone = zoneFlags4[0];
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);

            Terraria.BitsByte zoneFlags = 0;
            Terraria.BitsByte zoneFlags2 = 0;
            Terraria.BitsByte zoneFlags3 = 0;
            Terraria.BitsByte zoneFlags4 = 0;
            zoneFlags[0] = IsPlayerNearDungeonZone;
            zoneFlags[1] = IsPlayerNearCorruptionZone;
            zoneFlags[2] = IsPlayerNearHallowedZone;
            zoneFlags[3] = IsPlayerNearMeteorZone;
            zoneFlags[4] = IsPlayerNearJungleZone;
            zoneFlags[5] = IsPlayerNearSnowZone;
            zoneFlags[6] = IsPlayerNearCrimsonZone;
            zoneFlags[7] = IsPlayerNearWaterCandleZone;
            zoneFlags2[0] = IsPlayerNearPeaceCandleZone;
            zoneFlags2[1] = IsPlayerNearSolarTowerZone;
            zoneFlags2[2] = IsPlayerNearVortexTowerZone;
            zoneFlags2[3] = IsPlayerNearNebulaTowerZone;
            zoneFlags2[4] = IsPlayerNearStardustTowerZone;
            zoneFlags2[5] = IsPlayerNearDesertZone;
            zoneFlags2[6] = IsPlayerNearGlowingMushroomZone;
            zoneFlags2[7] = IsPlayerNearUndergroundDesertZone;
            zoneFlags3[0] = IsPlayerNearSkyHeightZone;
            zoneFlags3[1] = IsPlayerNearOverworldHeightZone;
            zoneFlags3[2] = IsPlayerNearDirtLayerHeightZone;
            zoneFlags3[3] = IsPlayerNearRockLayerHeightZone;
            zoneFlags3[4] = IsPlayerNearUnderworldHeightZone;
            zoneFlags3[5] = IsPlayerNearBeachZone;
            zoneFlags3[6] = IsPlayerNearRainZone;
            zoneFlags3[7] = IsPlayerNearSandstormZone;
            zoneFlags4[0] = IsPlayerNearOldOnesArmyZone;
            writer.Write(zoneFlags);
            writer.Write(zoneFlags2);
            writer.Write(zoneFlags3);
            writer.Write(zoneFlags4);
        }
    }
}
