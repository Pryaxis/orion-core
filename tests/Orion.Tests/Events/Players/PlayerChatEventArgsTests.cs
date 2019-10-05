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
using Moq;
using Orion.Packets.Modules;
using Orion.Players;
using Xunit;

namespace Orion.Events.Players {
    public class PlayerChatEventArgsTests {
        private readonly PlayerChatEventArgs _args;

        public PlayerChatEventArgsTests() {
            var player = new Mock<IPlayer>().Object;
            var module = new ChatModule();
            _args = new PlayerChatEventArgs(player, module);
        }

        [Fact]
        public void Ctor_NullModule_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            Func<PlayerChatEventArgs> func = () => new PlayerChatEventArgs(player, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetGetProperties_ReflectsInModule() => _args.Properties_GetSetShouldReflect("_module");

        [Fact]
        public void SetChatCommand_NullValue_ThrowsArgumentNullException() {
            Action action = () => _args.ChatCommand = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetChatText_NullValue_ThrowsArgumentNullException() {
            Action action = () => _args.ChatText = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
