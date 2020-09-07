using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.DataStructures.Modules
{
    public sealed class AmbienceTests
    {
        private readonly byte[] _bytes = { 3, 0, 250, 0, 4, 0, 0, 5 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var module = new Ambience();

            module.PlayerIndex = 250;

            Assert.Equal(250, module.PlayerIndex);
        }

        [Fact]
        public void Seed_Set_Get()
        {
            var module = new Ambience();

            module.Seed = 123456789;

            Assert.Equal(123456789, module.Seed);
        }

        [Theory]
        [InlineData(AmbienceSkyType.AirBalloon)]
        [InlineData(AmbienceSkyType.Airship)]
        [InlineData(AmbienceSkyType.Bats)]
        public void SkyEntityType_Set_Get(AmbienceSkyType skyEntityType)
        {
            var module = new Ambience();

            module.SkyEntityType = skyEntityType;

            Assert.Equal(skyEntityType, module.SkyEntityType);
        }

        [Fact]
        public void Read()
        {
            var module = TestUtils.ReadModule<Ambience>(_bytes, PacketContext.Server);

            Assert.Equal(250, module.PlayerIndex);
            Assert.Equal(1024, module.Seed);
            Assert.Equal(AmbienceSkyType.Meteor, module.SkyEntityType);
        }
    }
}
