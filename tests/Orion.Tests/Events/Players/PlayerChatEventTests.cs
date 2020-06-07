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
using Orion.Players;
using Xunit;

namespace Orion.Events.Players {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PlayerChatEventTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => new PlayerChatEvent(null!, "", ""));
        }

        [Fact]
        public void Ctor_NullCommand_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;

            Assert.Throws<ArgumentNullException>(() => new PlayerChatEvent(player, null!, ""));
        }

        [Fact]
        public void Ctor_NullMessage_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;

            Assert.Throws<ArgumentNullException>(() => new PlayerChatEvent(player, "", null!));
        }

        [Fact]
        public void Command_Get() {
            var player = new Mock<IPlayer>().Object;
            var evt = new PlayerChatEvent(player, "Say", "/command test");

            Assert.Equal("Say", evt.Command);
        }

        [Fact]
        public void Message_Get() {
            var player = new Mock<IPlayer>().Object;
            var evt = new PlayerChatEvent(player, "Say", "/command test");

            Assert.Equal("/command test", evt.Message);
        }

        [Fact]
        public void CancellationReason_Set_Get() {
            var player = new Mock<IPlayer>().Object;
            var evt = new PlayerChatEvent(player, "Say", "/command test");

            evt.CancellationReason = "test";

            Assert.Equal("test", evt.CancellationReason);
        }
    }
}
