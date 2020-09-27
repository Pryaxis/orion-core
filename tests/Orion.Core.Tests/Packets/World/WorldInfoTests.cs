using System;
using System.Collections.Generic;
using System.Text;
using Orion.Core.Events.Packets;
using Orion.Core.Packets.Items;
using ReLogic.Content;
using Xunit;

namespace Orion.Core.Packets.World
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class WorldInfoTests
    {
        private readonly byte[] _bytes =
        {
            167, // Packet length
            0, // Ignored
            7, // Packet ID
            16, 14, 0, 0, // Time
            1, // Solar flags byte
            2, // Moon phase
            0, 4, // Max tiles X
            0, 4, // Max tiles Y
            0, 2, // Spawn tile X
            0, 2, // Spawn tile Y
            100, 0, // World surface
            200, 0, // Rock layer
            50, 0, 0, 0, // World ID
            10, 87, 111, 114, 108, 100, 32, 73, 110, 102, 111, // World name
            0, // Gamemode
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, // GUID bytes
            1, 0, 0, 0, 0, 0, 0, 0, // World gen version
            5, // Moon type
            1, // ForestBackgroundStyle
            2,
            3,
            4, // ForestBackgroundStyle4
            1,
            2,
            3,
            4,
            1,
            2,
            3,
            4,
            5,
            1,
            1,
            1,
            0, 0, 0, 64, // Wind speed
            10, // Number of clouds
            100, 0, 0, 0, // Forest1 edge
            0, 1, 0, 0, // Forest2 edge
            0, 0, 1, 0, // Forest3 edge,
            1, // Forest1 style
            2, // Forest2 style
            3, // Forest3 style
            4, // Forest4 style
            0, 1, 0, 0, // Cave1 edge
            0, 0, 1, 0, // Cave2 edge
            0, 0, 0, 1, // Cave3 edge
            1, // Cave1 style
            4, // Cave2 style
            5, // Cave3 style
            6, // Cave4 style
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, // Area style variations
            0, 0, 128, 63, // Rain intensity
            13, // Flags1
            31, // Flags2
            2, // Flags3
            128, // Flags4
            0, // Flags5,
            8, // Flags6
            2, // Flags7
            5, 0, // Copper Tier
            4, 0, // Iron Tier
            3, 0, // Silver Tier
            2, 0, // Gold Tier
            1, 0, // Cobalt Tier
            2, 0, // Mythril Tier
            1, 0, // Adamantite Tier
            10, // Invasion type
            57, 48, 0, 0, 0, 0, 0, 0, // Lobby ID
            0, 0, 0, 0 // Sandstorm severity
        };

        [Fact]
        public void Time_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Time = 1024;

            Assert.Equal(1024, packet.Time);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsDayTime_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsDayTime = value;

            Assert.Equal(value, packet.IsDayTime);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsBloodMoon_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsBloodMoon = value;

            Assert.Equal(value, packet.IsBloodMoon);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsEclipse_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsEclipse = value;

            Assert.Equal(value, packet.IsEclipse);
        }


        [Theory]
        [InlineData(MoonPhase.Empty)]
        [InlineData(MoonPhase.Full)]
        [InlineData(MoonPhase.HalfAtLeft)]
        public void MoonPhase_Set_Get(MoonPhase value)
        {
            var packet = new WorldInfo();

            packet.MoonPhase = value;

            Assert.Equal(value, packet.MoonPhase);
        }


        [Fact]
        public void MaxTilesX_Set_Get()
        {
            var packet = new WorldInfo();

            packet.MaxTilesX = 2048;

            Assert.Equal(2048, packet.MaxTilesX);
        }


        [Fact]
        public void MaxTilesY_Set_Get()
        {
            var packet = new WorldInfo();

            packet.MaxTilesY = 4096;

            Assert.Equal(4096, packet.MaxTilesY);
        }


        [Fact]
        public void SpawnTileX_Set_Get()
        {
            var packet = new WorldInfo();

            packet.SpawnTileX = 128;

            Assert.Equal(128, packet.SpawnTileX);
        }


        [Fact]
        public void SpawnTileY_Set_Get()
        {
            var packet = new WorldInfo();

            packet.SpawnTileY = 128;

            Assert.Equal(128, packet.SpawnTileY);
        }


        [Fact]
        public void WorldSurface_Set_Get()
        {
            var packet = new WorldInfo();

            packet.WorldSurface = 1024;

            Assert.Equal(1024, packet.WorldSurface);
        }


        [Fact]
        public void RockLayer_Set_Get()
        {
            var packet = new WorldInfo();

            packet.RockLayer = 128;

            Assert.Equal(128, packet.RockLayer);
        }


        [Fact]
        public void WorldId_Set_Get()
        {
            var packet = new WorldInfo();

            packet.WorldId = 65535;

            Assert.Equal(65535, packet.WorldId);
        }

        [Fact]
        public void WorldName_GetNullValue()
        {
            var packet = new WorldInfo();

            Assert.Equal(string.Empty, packet.WorldName);
        }

        [Fact]
        public void WorldName_NullValue_ThrowsArgumentNullException()
        {
            var packet = new WorldInfo();

            Assert.Throws<ArgumentNullException>(() => packet.WorldName = null!);
        }


        [Fact]
        public void WorldName_Set_Get()
        {
            var packet = new WorldInfo();

            packet.WorldName = "Terraria";

            Assert.Equal("Terraria", packet.WorldName);
        }


        [Theory]
        [InlineData(GameMode.Creative)]
        [InlineData(GameMode.Expert)]
        [InlineData(GameMode.Master)]
        public void GameMode_Set_Get(GameMode value)
        {
            var packet = new WorldInfo();

            packet.GameMode = value;

            Assert.Equal(value, packet.GameMode);
        }


        [Fact]
        public void WorldGenerationVersion_Set_Get()
        {
            var packet = new WorldInfo();

            packet.WorldGenerationVersion = 12345UL;

            Assert.Equal(12345UL, packet.WorldGenerationVersion);
        }


        [Fact]
        public void MoonType_Set_Get()
        {
            var packet = new WorldInfo();

            packet.MoonType = 5;

            Assert.Equal(5, packet.MoonType);
        }


        [Fact]
        public void ForestBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.ForestBackgroundStyle = 6;

            Assert.Equal(6, packet.ForestBackgroundStyle);
        }


        [Fact]
        public void ForestBackgroundStyle2_Set_Get()
        {
            var packet = new WorldInfo();

            packet.ForestBackgroundStyle2 = 6;

            Assert.Equal(6, packet.ForestBackgroundStyle2);
        }


        [Fact]
        public void ForestBackgroundStyle3_Set_Get()
        {
            var packet = new WorldInfo();

            packet.ForestBackgroundStyle3 = 6;

            Assert.Equal(6, packet.ForestBackgroundStyle3);
        }


        [Fact]
        public void ForestBackgroundStyle4_Set_Get()
        {
            var packet = new WorldInfo();

            packet.ForestBackgroundStyle4 = 6;

            Assert.Equal(6, packet.ForestBackgroundStyle4);
        }


        [Fact]
        public void CorruptBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.CorruptBackgroundStyle = 3;

            Assert.Equal(3, packet.CorruptBackgroundStyle);
        }


        [Fact]
        public void JungleBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.JungleBackgroundStyle = 3;

            Assert.Equal(3, packet.JungleBackgroundStyle);
        }


        [Fact]
        public void SnowBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.SnowBackgroundStyle = 3;

            Assert.Equal(3, packet.SnowBackgroundStyle);
        }


        [Fact]
        public void HallowBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.HallowBackgroundStyle = 3;

            Assert.Equal(3, packet.HallowBackgroundStyle);
        }


        [Fact]
        public void CrimsonBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.CrimsonBackgroundStyle = 3;

            Assert.Equal(3, packet.CrimsonBackgroundStyle);
        }


        [Fact]
        public void DesertBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.DesertBackgroundStyle = 3;

            Assert.Equal(3, packet.DesertBackgroundStyle);
        }


        [Fact]
        public void OceanBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.OceanBackgroundStyle = 3;

            Assert.Equal(3, packet.OceanBackgroundStyle);
        }


        [Fact]
        public void MushroomBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.MushroomBackgroundStyle = 3;

            Assert.Equal(3, packet.MushroomBackgroundStyle);
        }


        [Fact]
        public void UnderworldBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.UnderworldBackgroundStyle = 3;

            Assert.Equal(3, packet.UnderworldBackgroundStyle);
        }


        [Fact]
        public void UndergroundIceBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.UndergroundIceBackgroundStyle = 3;

            Assert.Equal(3, packet.UndergroundIceBackgroundStyle);
        }


        [Fact]
        public void UndergroundJungleBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.UndergroundJungleBackgroundStyle = 3;

            Assert.Equal(3, packet.UndergroundJungleBackgroundStyle);
        }


        [Fact]
        public void UndergroundHellBackgroundStyle_Set_Get()
        {
            var packet = new WorldInfo();

            packet.UndergroundHellBackgroundStyle = 3;

            Assert.Equal(3, packet.UndergroundHellBackgroundStyle);
        }


        [Fact]
        public void WindSpeed_Set_Get()
        {
            var packet = new WorldInfo();

            packet.WindSpeed = 1F;

            Assert.Equal(1F, packet.WindSpeed);
        }


        [Fact]
        public void NumberOfClouds_Set_Get()
        {
            var packet = new WorldInfo();

            packet.NumberOfClouds = 10;

            Assert.Equal(10, packet.NumberOfClouds);
        }


        [Fact]
        public void UniqueId_Set_Get()
        {
            var packet = new WorldInfo();

            packet.UniqueId = new Guid("11fecf87-4e9a-4ca0-a691-b81814f35bc5");

            Assert.Equal(new Guid("11fecf87-4e9a-4ca0-a691-b81814f35bc5"), packet.UniqueId);
        }


        [Fact]
        public void Forest1Edge_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Forest1Edge = 128;

            Assert.Equal(128, packet.Forest1Edge);
        }


        [Fact]
        public void Forest2Edge_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Forest2Edge = 256;

            Assert.Equal(256, packet.Forest2Edge);
        }


        [Fact]
        public void Forest3Edge_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Forest3Edge = 512;

            Assert.Equal(512, packet.Forest3Edge);
        }


        [Fact]
        public void Forest1Style_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Forest1Style = 1;

            Assert.Equal(1, packet.Forest1Style);
        }


        [Fact]
        public void Forest2Style_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Forest2Style = 2;

            Assert.Equal(2, packet.Forest2Style);
        }


        [Fact]
        public void Forest3Style_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Forest3Style = 3;

            Assert.Equal(3, packet.Forest3Style);
        }


        [Fact]
        public void Forest4Style_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Forest4Style = 4;

            Assert.Equal(4, packet.Forest4Style);
        }


        [Fact]
        public void Cave1Edge_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Cave1Edge = 128;

            Assert.Equal(128, packet.Cave1Edge);
        }


        [Fact]
        public void Cave2Edge_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Cave2Edge = 256;

            Assert.Equal(256, packet.Cave2Edge);
        }


        [Fact]
        public void Cave3Edge_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Cave3Edge = 512;

            Assert.Equal(512, packet.Cave3Edge);
        }


        [Fact]
        public void Cave1Style_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Cave1Style = 1;

            Assert.Equal(1, packet.Cave1Style);
        }

        [Fact]
        public void Cave2Style_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Cave2Style = 2;

            Assert.Equal(2, packet.Cave2Style);
        }

        [Fact]
        public void Cave3Style_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Cave3Style = 3;

            Assert.Equal(3, packet.Cave3Style);
        }

        [Fact]
        public void Cave4Style_Set_Get()
        {
            var packet = new WorldInfo();

            packet.Cave4Style = 4;

            Assert.Equal(4, packet.Cave4Style);
        }


        [Fact]
        public void AreaStyleVariation_Set_Get()
        {
            var packet = new WorldInfo();

            packet.AreaStyleVariation[0] = 1;
            packet.AreaStyleVariation[1] = 2;
            packet.AreaStyleVariation[2] = 3;

            Assert.Equal(1, packet.AreaStyleVariation[0]);
            Assert.Equal(2, packet.AreaStyleVariation[1]);
            Assert.Equal(3, packet.AreaStyleVariation[2]);
        }

        [Fact]
        public void RainIntensity_Set_Get()
        {
            var packet = new WorldInfo();

            packet.RainIntensity = 0.5F;

            Assert.Equal(0.5F, packet.RainIntensity);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void WasShadowOrbSmashed_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.WasShadowOrbSmashed = value;

            Assert.Equal(value, packet.WasShadowOrbSmashed);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsEyeOfCthulhuDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsEyeOfCthulhuDefeated = value;

            Assert.Equal(value, packet.IsEyeOfCthulhuDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsEowOrBrainOfCthulhuDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsEowOrBrainOfCthulhuDefeated = value;

            Assert.Equal(value, packet.IsEowOrBrainOfCthulhuDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsSkeletronDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsSkeletronDefeated = value;

            Assert.Equal(value, packet.IsSkeletronDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsHardmode_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsHardmode = value;

            Assert.Equal(value, packet.IsHardmode);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsClownDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsClownDefeated = value;

            Assert.Equal(value, packet.IsClownDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ServerSideCharactersEnabled_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.ServerSideCharactersEnabled = value;

            Assert.Equal(value, packet.ServerSideCharactersEnabled);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsPlanteraDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsPlanteraDefeated = value;

            Assert.Equal(value, packet.IsPlanteraDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsDestroyerDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsDestroyerDefeated = value;

            Assert.Equal(value, packet.IsDestroyerDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsMechanicalEyeDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsMechanicalEyeDefeated = value;

            Assert.Equal(value, packet.IsMechanicalEyeDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsSkeletronPrimeDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsSkeletronPrimeDefeated = value;

            Assert.Equal(value, packet.IsSkeletronPrimeDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DownedAnyMechanicalBoss_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.DownedAnyMechanicalBoss = value;

            Assert.Equal(value, packet.DownedAnyMechanicalBoss);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsCloudBackgroundActive_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsCloudBackgroundActive = value;

            Assert.Equal(value, packet.IsCloudBackgroundActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsCrimson_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsCrimson = value;

            Assert.Equal(value, packet.IsCrimson);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsPumpkinMoon_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsPumpkinMoon = value;

            Assert.Equal(value, packet.IsPumpkinMoon);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsSnowMoon_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsSnowMoon = value;

            Assert.Equal(value, packet.IsSnowMoon);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void FastForwardTime_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.FastForwardTime = value;

            Assert.Equal(value, packet.FastForwardTime);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsRainingSlime_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsRainingSlime = value;

            Assert.Equal(value, packet.IsRainingSlime);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsSlimeKingDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsSlimeKingDefeated = value;

            Assert.Equal(value, packet.IsSlimeKingDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsQueenBeeDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsQueenBeeDefeated = value;

            Assert.Equal(value, packet.IsQueenBeeDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsFishronDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsFishronDefeated = value;

            Assert.Equal(value, packet.IsFishronDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AreMartiansDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.AreMartiansDefeated = value;

            Assert.Equal(value, packet.AreMartiansDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AreCultistsDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.AreCultistsDefeated = value;

            Assert.Equal(value, packet.AreCultistsDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsMoonLordDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsMoonLordDefeated = value;

            Assert.Equal(value, packet.IsMoonLordDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsHalloweenKingDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsHalloweenKingDefeated = value;

            Assert.Equal(value, packet.IsHalloweenKingDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsHalloweenTreeDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsHalloweenTreeDefeated = value;

            Assert.Equal(value, packet.IsHalloweenTreeDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsChristmasQueenDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsChristmasQueenDefeated = value;

            Assert.Equal(value, packet.IsChristmasQueenDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsChristmasSantankDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsChristmasSantankDefeated = value;

            Assert.Equal(value, packet.IsChristmasSantankDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsChristmasTreeDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsChristmasTreeDefeated = value;

            Assert.Equal(value, packet.IsChristmasTreeDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsGolemDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsGolemDefeated = value;

            Assert.Equal(value, packet.IsGolemDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsManualBirthdayParty_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsManualBirthdayParty = value;

            Assert.Equal(value, packet.IsManualBirthdayParty);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ArePiratesDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.ArePiratesDefeated = value;

            Assert.Equal(value, packet.ArePiratesDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsFrostMoonDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsFrostMoonDefeated = value;

            Assert.Equal(value, packet.IsFrostMoonDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AreGoblinsDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.AreGoblinsDefeated = value;

            Assert.Equal(value, packet.AreGoblinsDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsSandstormHappening_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsSandstormHappening = value;

            Assert.Equal(value, packet.IsSandstormHappening);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsDD2EventHappening_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsDD2EventHappening = value;

            Assert.Equal(value, packet.IsDD2EventHappening);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsFirstDD2InvasionDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsFirstDD2InvasionDefeated = value;

            Assert.Equal(value, packet.IsFirstDD2InvasionDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsSecondDD2InvasionDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsSecondDD2InvasionDefeated = value;

            Assert.Equal(value, packet.IsSecondDD2InvasionDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsThirdDD2InvasionDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsThirdDD2InvasionDefeated = value;

            Assert.Equal(value, packet.IsThirdDD2InvasionDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void WasCombatBookUsed_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.WasCombatBookUsed = value;

            Assert.Equal(value, packet.WasCombatBookUsed);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsManualLanternNight_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsManualLanternNight = value;

            Assert.Equal(value, packet.IsManualLanternNight);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsSolarTowerDowned_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsSolarTowerDowned = value;

            Assert.Equal(value, packet.IsSolarTowerDowned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsVortexTowerDowned_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsVortexTowerDowned = value;

            Assert.Equal(value, packet.IsVortexTowerDowned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsNebulaTowerDowned_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsNebulaTowerDowned = value;

            Assert.Equal(value, packet.IsNebulaTowerDowned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsStardustTowerDowned_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsStardustTowerDowned = value;

            Assert.Equal(value, packet.IsStardustTowerDowned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ForceHalloweenForToday_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.ForceHalloweenForToday = value;

            Assert.Equal(value, packet.ForceHalloweenForToday);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ForceChristmasForToday_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.ForceChristmasForToday = value;

            Assert.Equal(value, packet.ForceChristmasForToday);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void WasCatBought_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.WasCatBought = value;

            Assert.Equal(value, packet.WasCatBought);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void WasDogBought_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.WasDogBought = value;

            Assert.Equal(value, packet.WasDogBought);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void WasBunnyBought_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.WasBunnyBought = value;

            Assert.Equal(value, packet.WasBunnyBought);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsFreeCake_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsFreeCake = value;

            Assert.Equal(value, packet.IsFreeCake);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsDrunkWorld_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsDrunkWorld = value;

            Assert.Equal(value, packet.IsDrunkWorld);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsEmpressOfLightDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsEmpressOfLightDefeated = value;

            Assert.Equal(value, packet.IsEmpressOfLightDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsQueenSlimeDefeated_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsQueenSlimeDefeated = value;

            Assert.Equal(value, packet.IsQueenSlimeDefeated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsForTheWorthyWorld_Set_Get(bool value)
        {
            var packet = new WorldInfo();

            packet.IsForTheWorthyWorld = value;

            Assert.Equal(value, packet.IsForTheWorthyWorld);
        }


        [Fact]
        public void CopperTier_Set_Get()
        {
            var packet = new WorldInfo();

            packet.CopperTier = 1;

            Assert.Equal(1, packet.CopperTier);
        }


        [Fact]
        public void IronTier_Set_Get()
        {
            var packet = new WorldInfo();

            packet.IronTier = 2;

            Assert.Equal(2, packet.IronTier);
        }


        [Fact]
        public void SilverTier_Set_Get()
        {
            var packet = new WorldInfo();

            packet.SilverTier = 3;

            Assert.Equal(3, packet.SilverTier);
        }


        [Fact]
        public void GoldTier_Set_Get()
        {
            var packet = new WorldInfo();

            packet.GoldTier = 4;

            Assert.Equal(4, packet.GoldTier);
        }


        [Fact]
        public void CobaltTier_Set_Get()
        {
            var packet = new WorldInfo();

            packet.CobaltTier = 5;

            Assert.Equal(5, packet.CobaltTier);
        }


        [Fact]
        public void MythrilTier_Set_Get()
        {
            var packet = new WorldInfo();

            packet.MythrilTier = 6;

            Assert.Equal(6, packet.MythrilTier);
        }


        [Fact]
        public void AdamantiteTier_Set_Get()
        {
            var packet = new WorldInfo();

            packet.AdamantiteTier = 7;

            Assert.Equal(7, packet.AdamantiteTier);
        }


        [Fact]
        public void InvasionType_Set_Get()
        {
            var packet = new WorldInfo();

            packet.InvasionType = 10;

            Assert.Equal(10, packet.InvasionType);
        }


        [Fact]
        public void LobbyId_Set_Get()
        {
            var packet = new WorldInfo();

            packet.LobbyId = 65535UL;

            Assert.Equal(65535UL, packet.LobbyId);
        }


        [Fact]
        public void SandstormSeverity_Set_Get()
        {
            var packet = new WorldInfo();

            packet.SandstormSeverity = 5F;

            Assert.Equal(5F, packet.SandstormSeverity);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<WorldInfo>(_bytes, PacketContext.Server);

            Assert.Equal(3600, packet.Time);
            Assert.True(packet.IsDayTime);
            Assert.Equal(MoonPhase.HalfAtLeft, packet.MoonPhase);
            Assert.Equal(1024, packet.MaxTilesX);
            Assert.Equal(1024, packet.MaxTilesY);
            Assert.Equal(512, packet.SpawnTileX);
            Assert.Equal(512, packet.SpawnTileY);
            Assert.Equal(100, packet.WorldSurface);
            Assert.Equal(200, packet.RockLayer);
            Assert.Equal(50, packet.WorldId);
            Assert.Equal("World Info", packet.WorldName);
            Assert.Equal(GameMode.Normal, packet.GameMode);
            Assert.Equal(new Guid(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, }), packet.UniqueId);
            Assert.Equal(1UL, packet.WorldGenerationVersion);
            Assert.Equal(5, packet.MoonType);
            Assert.Equal(1, packet.ForestBackgroundStyle);
            Assert.Equal(2, packet.ForestBackgroundStyle2);
            Assert.Equal(3, packet.ForestBackgroundStyle3);
            Assert.Equal(4, packet.ForestBackgroundStyle4);
            Assert.Equal(1, packet.CorruptBackgroundStyle);
            Assert.Equal(2, packet.JungleBackgroundStyle);
            Assert.Equal(3, packet.SnowBackgroundStyle);
            Assert.Equal(4, packet.HallowBackgroundStyle);
            Assert.Equal(1, packet.CrimsonBackgroundStyle);
            Assert.Equal(2, packet.DesertBackgroundStyle);
            Assert.Equal(3, packet.OceanBackgroundStyle);
            Assert.Equal(4, packet.MushroomBackgroundStyle);
            Assert.Equal(5, packet.UnderworldBackgroundStyle);
            Assert.Equal(1, packet.UndergroundIceBackgroundStyle);
            Assert.Equal(1, packet.UndergroundJungleBackgroundStyle);
            Assert.Equal(1, packet.UndergroundHellBackgroundStyle);
            Assert.Equal(2F, packet.WindSpeed);
            Assert.Equal(10, packet.NumberOfClouds);
            Assert.Equal(100, packet.Forest1Edge);
            Assert.Equal(256, packet.Forest2Edge);
            Assert.Equal(65536, packet.Forest3Edge);
            Assert.Equal(1, packet.Forest1Style);
            Assert.Equal(2, packet.Forest2Style);
            Assert.Equal(3, packet.Forest3Style);
            Assert.Equal(4, packet.Forest4Style);
            Assert.Equal(256, packet.Cave1Edge);
            Assert.Equal(65536, packet.Cave2Edge);
            Assert.Equal(16777216, packet.Cave3Edge);
            Assert.Equal(1, packet.Cave1Style);
            Assert.Equal(4, packet.Cave2Style);
            Assert.Equal(5, packet.Cave3Style);
            Assert.Equal(6, packet.Cave4Style);
            Assert.Equal(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, packet.AreaStyleVariation);
            Assert.Equal(1F, packet.RainIntensity);
            Assert.True(packet.WasShadowOrbSmashed);
            Assert.False(packet.IsEyeOfCthulhuDefeated);
            Assert.True(packet.IsEowOrBrainOfCthulhuDefeated);
            Assert.True(packet.IsSkeletronDefeated);
            Assert.False(packet.IsHardmode);
            Assert.False(packet.IsClownDefeated);
            Assert.False(packet.ServerSideCharactersEnabled);
            Assert.False(packet.IsPlanteraDefeated);
            Assert.True(packet.IsDestroyerDefeated);
            Assert.True(packet.IsMechanicalEyeDefeated);
            Assert.True(packet.IsSkeletronPrimeDefeated);
            Assert.True(packet.DownedAnyMechanicalBoss);
            Assert.True(packet.IsCloudBackgroundActive);
            Assert.False(packet.IsCrimson);
            Assert.False(packet.IsPumpkinMoon);
            Assert.False(packet.IsSnowMoon);
            Assert.True(packet.FastForwardTime);
            Assert.False(packet.IsRainingSlime);
            Assert.False(packet.IsSlimeKingDefeated);
            Assert.False(packet.IsQueenBeeDefeated);
            Assert.False(packet.IsFishronDefeated);
            Assert.False(packet.AreMartiansDefeated);
            Assert.False(packet.AreCultistsDefeated);
            Assert.False(packet.IsMoonLordDefeated);
            Assert.False(packet.IsHalloweenKingDefeated);
            Assert.False(packet.IsHalloweenTreeDefeated);
            Assert.False(packet.IsChristmasQueenDefeated);
            Assert.False(packet.IsChristmasSantankDefeated);
            Assert.False(packet.IsChristmasTreeDefeated);
            Assert.False(packet.IsGolemDefeated);
            Assert.True(packet.IsManualBirthdayParty);
            Assert.False(packet.ArePiratesDefeated);
            Assert.False(packet.IsFrostMoonDefeated);
            Assert.False(packet.AreGoblinsDefeated);
            Assert.False(packet.IsSandstormHappening);
            Assert.False(packet.IsDD2EventHappening);
            Assert.False(packet.IsFirstDD2InvasionDefeated);
            Assert.False(packet.IsSecondDD2InvasionDefeated);
            Assert.False(packet.IsThirdDD2InvasionDefeated);
            Assert.True(packet.IsVortexTowerDowned);
            Assert.True(packet.WasDogBought);
            Assert.Equal(5, packet.CopperTier);
            Assert.Equal(4, packet.IronTier);
            Assert.Equal(3, packet.SilverTier);
            Assert.Equal(2, packet.GoldTier);
            Assert.Equal(1, packet.CobaltTier);
            Assert.Equal(2, packet.MythrilTier);
            Assert.Equal(1, packet.AdamantiteTier);
            Assert.Equal(10, packet.InvasionType);
            Assert.Equal(12345UL, packet.LobbyId);
            Assert.Equal(0, packet.SandstormSeverity);
        }
    }
}
