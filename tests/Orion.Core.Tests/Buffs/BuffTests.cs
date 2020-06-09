// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using Xunit;

namespace Orion.Core.Buffs {
    public class BuffTests {
        [Fact]
        public void Ctor_NegativeDuration_ThrowsArgumentOutOfRangeException() {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Buff(BuffId.ObsidianSkin, TimeSpan.FromMinutes(-8)));
        }

        [Fact]
        public void Id_Get() {
            var buff = new Buff(BuffId.ObsidianSkin, TimeSpan.FromMinutes(8));

            Assert.Equal(BuffId.ObsidianSkin, buff.Id);
        }

        [Fact]
        public void Duration_Get() {
            var buff = new Buff(BuffId.ObsidianSkin, TimeSpan.FromMinutes(8));

            Assert.Equal(TimeSpan.FromMinutes(8), buff.Duration);
        }
    }
}
