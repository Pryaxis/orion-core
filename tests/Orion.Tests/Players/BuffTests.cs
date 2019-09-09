using System;
using FluentAssertions;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Players {
    public class BuffTests {
        [Fact]
        public void Ctor_NegativeDuration_ThrowsArgumentOutOfRangeException() {
            Func<Buff> func = () => new Buff(BuffType.ObsidianSkin, TimeSpan.MinValue);

            func.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(BuffType.ObsidianSkin)]
        public void GetBuffType_IsCorrect(BuffType buffType) {
            var buff = new Buff(buffType, TimeSpan.MaxValue);

            buff.BuffType.Should().Be(buffType);
        }

        [Fact]
        public void GetDuration_IsCorrect() {
            var duration = TimeSpan.FromHours(1);
            var buff = new Buff(BuffType.ObsidianSkin, duration);

            buff.Duration.Should().Be(duration);
        }
    }
}
