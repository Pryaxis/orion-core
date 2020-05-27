﻿// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using Orion.Packets.Players;
using Orion.Players;
using Xunit;

namespace Orion.Events.Players {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PlayerManaEventTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var packet = new PlayerManaPacket();

            Assert.Throws<ArgumentNullException>(() => new PlayerManaEvent(null!, ref packet));
        }

        [Fact]
        public void Mana_Get() {
            var player = new Mock<IPlayer>().Object;
            var packet = new PlayerManaPacket { Mana = 100 };
            var evt = new PlayerManaEvent(player, ref packet);

            Assert.Equal(100, evt.Mana);
        }

        [Fact]
        public void Mana_Set() {
            var player = new Mock<IPlayer>().Object;
            var packet = new PlayerManaPacket();
            var evt = new PlayerManaEvent(player, ref packet);

            evt.Mana = 100;

            Assert.Equal(100, packet.Mana);
        }

        [Fact]
        public void MaxMana_Get() {
            var player = new Mock<IPlayer>().Object;
            var packet = new PlayerManaPacket { MaxMana = 200 };
            var evt = new PlayerManaEvent(player, ref packet);

            Assert.Equal(200, evt.MaxMana);
        }

        [Fact]
        public void MaxMana_Set() {
            var player = new Mock<IPlayer>().Object;
            var packet = new PlayerManaPacket();
            var evt = new PlayerManaEvent(player, ref packet);

            evt.MaxMana = 200;

            Assert.Equal(200, packet.MaxMana);
        }
    }
}