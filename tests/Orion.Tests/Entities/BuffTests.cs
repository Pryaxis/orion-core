// Copyright (c) 2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using FluentAssertions;
using Xunit;

namespace Orion.Entities {
    public class BuffTests {
        [Fact]
        public void Ctor_NegativeDuration_ThrowsArgumentOutOfRangeException() {
            Func<Buff> func = () => new Buff(BuffType.ObsidianSkin, TimeSpan.MinValue);

            func.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void BuffType_Get() {
            var buff = new Buff(BuffType.ObsidianSkin, TimeSpan.MaxValue);

            buff.BuffType.Should().Be(BuffType.ObsidianSkin);
        }

        [Fact]
        public void Duration_Get() {
            var duration = TimeSpan.FromHours(1);
            var buff = new Buff(BuffType.ObsidianSkin, duration);

            buff.Duration.Should().Be(duration);
        }
    }
}
