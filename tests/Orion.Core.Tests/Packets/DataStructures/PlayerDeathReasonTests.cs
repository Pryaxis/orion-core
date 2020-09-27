using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.DataStructures
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class PlayerDeathReasonTests
    {
        private readonly byte[] _bytes = { 255, 1, 0, 2, 0, 3, 0, 4, 5, 0, 6, 0, 7, 3, 65, 66, 67 };

        [Fact]
        public void KillerIndex_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.KillerIndex = 1;

            Assert.Equal(1, reason.KillerIndex.Value);
        }

        [Fact]
        public void KillingNpcIndex_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.KillingNpcIndex = 1;

            Assert.Equal(1, reason.KillingNpcIndex.Value);
        }

        [Fact]
        public void KillingProjectileIndex_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.KillingProjectileIndex = 1;

            Assert.Equal(1, reason.KillingProjectileIndex.Value);
        }

        [Fact]
        public void CauseOfDeath_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.CauseOfDeath = CauseOfDeath.Burning;

            Assert.Equal(CauseOfDeath.Burning, reason.CauseOfDeath.Value);
        }

        [Fact]
        public void ProjectileType_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.ProjectileType = 1;

            Assert.Equal(1, reason.ProjectileType.Value);
        }

        [Fact]
        public void ItemType_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.ItemType = 1;

            Assert.Equal(1, reason.ItemType.Value);
        }

        [Fact]
        public void ItemPrefix_Set_Get()
        {
            var reason = new PlayerDeathReason();

            reason.ItemPrefix = 1;

            Assert.Equal(1, reason.ItemPrefix.Value);
        }

        [Fact]
        public void CustomDeathReason_GetNullValue()
        {
            var reason = new PlayerDeathReason();

            Assert.Equal(string.Empty, reason.CustomDeathReason);
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

            Assert.Equal(1, reason.KillerIndex!.Value);
            Assert.Equal(2, reason.KillingNpcIndex!.Value);
            Assert.Equal(3, reason.KillingProjectileIndex!.Value);
            Assert.Equal(CauseOfDeath.DemonAltar, reason.CauseOfDeath!.Value);
            Assert.Equal(5, reason.ProjectileType!.Value);
            Assert.Equal(6, reason.ItemType!.Value);
            Assert.Equal(7, reason.ItemPrefix!.Value);
            Assert.Equal("ABC", reason.CustomDeathReason);

            var buffer = new byte[17];

            var length2 = reason.Write(buffer);

            Assert.Equal(length, length2);
            Assert.Equal(_bytes, buffer);
        }
    }
}
