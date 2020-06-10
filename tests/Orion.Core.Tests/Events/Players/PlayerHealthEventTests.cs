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
using System.Diagnostics.CodeAnalysis;
using Moq;
using Orion.Core.Players;
using Xunit;

namespace Orion.Core.Events.Players {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PlayerHealthEventTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => new PlayerHealthEvent(null!, 0, 0));
        }

        [Fact]
        public void Health_Get() {
            var player = Mock.Of<IPlayer>();
            var evt = new PlayerHealthEvent(player, 100, 500);

            Assert.Equal(100, evt.Health);
        }

        [Fact]
        public void MaxHealth_Get() {
            var player = Mock.Of<IPlayer>();
            var evt = new PlayerHealthEvent(player, 100, 500);

            Assert.Equal(500, evt.MaxHealth);
        }

        [Fact]
        public void CancellationReason_Set_Get() {
            var player = Mock.Of<IPlayer>();
            var evt = new PlayerHealthEvent(player, 100, 500);

            evt.CancellationReason = "test";

            Assert.Equal("test", evt.CancellationReason);
        }
    }
}
