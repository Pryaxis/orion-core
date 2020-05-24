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
using Orion.Packets.Modules;
using Orion.Players;
using Xunit;

namespace Orion.Events.Players {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PlayerChatEventTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var packet = new ModulePacket<ChatModule>();

            Assert.Throws<ArgumentNullException>(() => new PlayerChatEvent(null!, ref packet));
        }

        [Fact]
        public void Command_Get() {
            var player = new Mock<IPlayer>().Object;
            var packet = new ModulePacket<ChatModule> { Module = new ChatModule { ClientCommand = "Say" } };
            var evt = new PlayerChatEvent(player, ref packet);

            Assert.Equal("Say", evt.Command);
        }

        [Fact]
        public void Command_SetNullValue_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            var packet = new ModulePacket<ChatModule>();
            var evt = new PlayerChatEvent(player, ref packet);

            Assert.Throws<ArgumentNullException>(() => evt.Command = null!);
        }

        [Fact]
        public void Command_Set() {
            var player = new Mock<IPlayer>().Object;
            var packet = new ModulePacket<ChatModule>();
            var evt = new PlayerChatEvent(player, ref packet);

            evt.Command = "Say";

            Assert.Equal("Say", packet.Module.ClientCommand);
        }

        [Fact]
        public void Text_Get() {
            var player = new Mock<IPlayer>().Object;
            var packet = new ModulePacket<ChatModule> { Module = new ChatModule { ClientText = "/command test" } };
            var evt = new PlayerChatEvent(player, ref packet);

            Assert.Equal("/command test", evt.Text);
        }

        [Fact]
        public void Text_SetNullValue_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            var packet = new ModulePacket<ChatModule>();
            var evt = new PlayerChatEvent(player, ref packet);

            Assert.Throws<ArgumentNullException>(() => evt.Text = null!);
        }

        [Fact]
        public void Text_Set() {
            var player = new Mock<IPlayer>().Object;
            var packet = new ModulePacket<ChatModule>();
            var evt = new PlayerChatEvent(player, ref packet);

            evt.Text = "/command test";

            Assert.Equal("/command test", packet.Module.ClientText);
        }
    }
}
