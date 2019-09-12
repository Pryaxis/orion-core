// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using Orion.Players;
using Orion.Players.Events;
using Xunit;

namespace Orion.Tests.Players.Events {
    public class UpdatedPlayerEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<UpdatedPlayerEventArgs> func = () => new UpdatedPlayerEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetPlayer_IsCorrect() {
            var player = new Mock<IPlayer>().Object;
            var args = new UpdatedPlayerEventArgs(player);

            args.Player.Should().BeSameAs(player);
        }
    }
}
