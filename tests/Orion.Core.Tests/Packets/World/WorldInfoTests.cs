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
    }
}
