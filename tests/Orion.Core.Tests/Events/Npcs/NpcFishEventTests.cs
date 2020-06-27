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
using Orion.Core.Npcs;
using Orion.Core.Players;
using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Events.Npcs
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class NpcFishEventTests
    {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => new NpcFishEvent(null!, new Vector2f(1.23f, 4.56f), NpcId.HemogoblinShark));
        }

        [Fact]
        public void Position_Get()
        {
            var player = Mock.Of<IPlayer>();
            var evt = new NpcFishEvent(player, new Vector2f(1.23f, 4.56f), NpcId.HemogoblinShark);

            Assert.Equal(new Vector2f(1.23f, 4.56f), evt.Position);
        }

        [Fact]
        public void Id_Get()
        {
            var player = Mock.Of<IPlayer>();
            var evt = new NpcFishEvent(player, new Vector2f(1.23f, 4.56f), NpcId.HemogoblinShark);

            Assert.Equal(NpcId.HemogoblinShark, evt.Id);
        }
    }
}
