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
using Orion.Packets.Client;
using Orion.Players;
using Xunit;

namespace Orion.Events.Players {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PlayerUuidEventTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var packet = new ClientUuidPacket();

            Assert.Throws<ArgumentNullException>(() => new PlayerUuidEvent(null!, ref packet));
        }

        [Fact]
        public void Uuid_Get() {
            var player = new Mock<IPlayer>().Object;
            var packet = new ClientUuidPacket { Uuid = "Terraria" };
            var evt = new PlayerUuidEvent(player, ref packet);

            Assert.Equal("Terraria", evt.Uuid);
        }

        [Fact]
        public void Uuid_SetNullValue_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            var packet = new ClientUuidPacket();
            var evt = new PlayerUuidEvent(player, ref packet);

            Assert.Throws<ArgumentNullException>(() => evt.Uuid = null!);
        }

        [Fact]
        public void Uuid_Set() {
            var player = new Mock<IPlayer>().Object;
            var packet = new ClientUuidPacket();
            var evt = new PlayerUuidEvent(player, ref packet);

            evt.Uuid = "Terraria";

            Assert.Equal("Terraria", packet.Uuid);
        }
    }
}
