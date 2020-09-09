using System;
using System.Collections.Generic;
using System.Text;
using Orion.Core.Packets.Items;
using Xunit;

namespace Orion.Core.Packets.World
{
    public sealed class WorldInfoTests
    {
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
        public void WorldName_NullValue_ThrowsArgumentNullException()
        {
            var packet = new WorldInfo();

            Assert.Throws<ArgumentNullException>(() => packet.WorldName = null);
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
    }
}
