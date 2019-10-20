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
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
using Orion.Packets.Modules;
using Orion.Players;
using Xunit;

namespace Orion.Events.Players {
    public class PlayerChatEventArgsTests {
        [Fact]
        public void Ctor_NotDirty() {
            var player = new Mock<IPlayer>().Object;
            var args = new PlayerChatEventArgs(player, new ChatModule());

            args.IsDirty.Should().BeFalse();
        }

        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<PlayerChatEventArgs> func = () => new PlayerChatEventArgs(null, new ChatModule());

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullModule_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            Func<PlayerChatEventArgs> func = () => new PlayerChatEventArgs(player, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var player = new Mock<IPlayer>().Object;
            var args = new PlayerChatEventArgs(player, new ChatModule());

            args.SimpleProperties_Set_MarkAsDirty();
        }

        [Fact]
        public void Command_Get() {
            var player = new Mock<IPlayer>().Object;
            var args = new PlayerChatEventArgs(player, new ChatModule { ClientCommand = "test" });

            args.Command.Should().Be("test");
        }

        [Fact]
        [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
        public void Command_Set() {
            var player = new Mock<IPlayer>().Object;
            var module = new ChatModule();
            var args = new PlayerChatEventArgs(player, module);

            args.Command = "COMMAND";

            module.ClientCommand.Should().Be("COMMAND");
        }

        [Fact]
        public void Command_SetNullValue_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            var args = new PlayerChatEventArgs(player, new ChatModule());
            Action action = () => args.Command = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Text_Get() {
            var player = new Mock<IPlayer>().Object;
            var args = new PlayerChatEventArgs(player, new ChatModule { ClientText = "test" });

            args.Text.Should().Be("test");
        }

        [Fact]
        public void Text_Set() {
            var player = new Mock<IPlayer>().Object;
            var module = new ChatModule();
            var args = new PlayerChatEventArgs(player, module) {
                Text = "TEXT"
            };

            module.ClientText.Should().Be("TEXT");
        }

        [Fact]
        public void Text_SetNullValue_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            var args = new PlayerChatEventArgs(player, new ChatModule());
            Action action = () => args.Text = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
