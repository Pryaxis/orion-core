using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.DataStructures
{
    public sealed class PlayerDeathReasonTests
    {
        private readonly byte[] _bytes = { 255, 1, 0, 2, 0, 3, 0, 4, 5, 0, 6, 0, 7, 3, 65, 66, 67 };

        [Fact]
        public void KillerIndex_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.KillerIndex = 1;

            Assert.Equal(1, reason.KillerIndex);
        }

        [Fact]
        public void KillingNpcIndex_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.KillingNpcIndex = 1;

            Assert.Equal(1, reason.KillingNpcIndex);
        }

        [Fact]
        public void KillingProjectileIndex_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.KillingProjectileIndex = 1;

            Assert.Equal(1, reason.KillingProjectileIndex);
        }

        [Fact]
        public void CauseOfDeath_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.CauseOfDeath = CauseOfDeath.Burning;

            Assert.Equal(CauseOfDeath.Burning, reason.CauseOfDeath);
        }

        [Fact]
        public void ProjectileType_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.ProjectileType = 1;

            Assert.Equal(1, reason.ProjectileType);
        }

        [Fact]
        public void ItemType_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.ItemType = 1;

            Assert.Equal(1, reason.ItemType);
        }

        [Fact]
        public void ItemPrefix_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.ItemPrefix = 1;

            Assert.Equal(1, reason.ItemPrefix);
        }

        [Fact]
        public void CustomDeathReason_SetNullValue_ThrowsArgumentNullException()
        {
            var reason = new PlayerDeathReason();

            Assert.Throws<ArgumentNullException>(() => reason.CustomDeathReason = null!);
        }


        [Fact]
        public void CustomDeathReason_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.CustomDeathReason = "Hello, World";

            Assert.Equal("Hello, World", reason.CustomDeathReason);
        }

        [Fact]
        public void Read()
        {
            var length = PlayerDeathReason.Read(_bytes, out var reason);

            Assert.Equal(1, reason.KillerIndex);
            Assert.Equal(2, reason.KillingNpcIndex);
            Assert.Equal(3, reason.KillingProjectileIndex);
            Assert.Equal(CauseOfDeath.DemonAltar, reason.CauseOfDeath);
            Assert.Equal(5, reason.ProjectileType);
            Assert.Equal(6, reason.ItemType);
            Assert.Equal(7, reason.ItemPrefix);
            Assert.Equal("ABC", reason.CustomDeathReason);

            var buffer = new byte[17];

            var length2 = reason.Write(buffer);

            Assert.Equal(length, length2);
            Assert.Equal(_bytes, buffer);
        }
    }
}
