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
using Orion.Packets.Players;
using Orion.Players;
using Xunit;

namespace Orion.Events.Players {
    public class PlayerUuidEventArgsTests {
        [Fact]
        public void Ctor_NotDirty() {
            var player = new Mock<IPlayer>().Object;
            var args = new PlayerUuidEventArgs(player, new PlayerUuidPacket());

            args.IsDirty.Should().BeFalse();
        }

        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<PlayerUuidEventArgs> func = () => new PlayerUuidEventArgs(null, new PlayerUuidPacket());

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            Func<PlayerUuidEventArgs> func = () => new PlayerUuidEventArgs(player, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var player = new Mock<IPlayer>().Object;
            var args = new PlayerUuidEventArgs(player, new PlayerUuidPacket());

            args.SimpleProperties_Set_MarkAsDirty();
        }

        [Fact]
        public void SetGetProperties_ReflectsInPacket() {
            var player = new Mock<IPlayer>().Object;
            var args = new PlayerUuidEventArgs(player, new PlayerUuidPacket());

            args.Properties_GetSetShouldReflect("_packet");
        }

        [Fact]
        public void PlayerUuid_Set_NullValue_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            var args = new PlayerUuidEventArgs(player, new PlayerUuidPacket());
            Action action = () => args.PlayerUuid = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
