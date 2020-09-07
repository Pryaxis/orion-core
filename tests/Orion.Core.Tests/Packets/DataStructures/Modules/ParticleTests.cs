using System;
using System.Collections.Generic;
using Orion.Core.Utils;
using System.Text;
using ReLogic.Content;
using Xunit;

namespace Orion.Core.Packets.DataStructures.Modules
{
    public sealed class ParticleTests
    {
        private readonly byte[] _bytes =
        {
            9, 0, 1, 0, 0, 128, 63, 0, 0, 0, 64, 0, 0, 160, 64, 0, 0, 32, 65, 0, 0, 0, 0, 10
        };

        [Theory]
        [InlineData(ParticleType.BlackLightningHit)]
        [InlineData(ParticleType.BlackLightningSmall)]
        [InlineData(ParticleType.FlameWaders)]
        public void Type_Set_Get(ParticleType particleType)
        {
            var module = new Particle();

            module.Type = particleType;

            Assert.Equal(particleType, module.Type);
        }

        [Fact]
        public void Position_Set_Get()
        {
            var module = new Particle();

            module.Position = new Vector2f(123, 456);

            Assert.Equal(new Vector2f(123, 456), module.Position);

        }

        [Fact]
        public void MovementVector_Set_Get()
        {
            var module = new Particle();

            module.MovementVector = new Vector2f(1, 0);

            Assert.Equal(new Vector2f(1, 0), module.MovementVector);
        }

        [Fact]
        public void ShaderIndex_Set_Get()
        {
            var module = new Particle();

            module.ShaderIndex = 5;

            Assert.Equal(5, module.ShaderIndex);
        }

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var module = new Particle();

            module.PlayerIndex = 250;

            Assert.Equal(250, module.PlayerIndex);
        }

        [Fact]
        public void Read()
        {
            var module = TestUtils.ReadModule<Particle>(_bytes, PacketContext.Server);

            Assert.Equal(ParticleType.FlameWaders, module.Type);
            Assert.Equal(new Vector2f(1, 2), module.Position);
            Assert.Equal(new Vector2f(5, 10), module.MovementVector);
            Assert.Equal(0, module.ShaderIndex);
            Assert.Equal(10, module.PlayerIndex);
        }
    }
}
