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
using Moq;
using Orion.Core.Items;
using Orion.Core.Players;
using Orion.Core.World.TileEntities;
using Xunit;

namespace Orion.Core.Events.World.TileEntities
{
    public class ChestInventoryEventTests
    {
        [Fact]
        public void Ctor_NullChest_ThrowsArgumentNullException()
        {
            var player = Mock.Of<IPlayer>();

            Assert.Throws<ArgumentNullException>(
                () => new ChestInventoryEvent(null!, player, 1, new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1)));
        }

        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException()
        {
            var chest = Mock.Of<IChest>();

            Assert.Throws<ArgumentNullException>(
                () => new ChestInventoryEvent(chest, null!, 1, new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1)));
        }

        [Fact]
        public void Player_Get()
        {
            var chest = Mock.Of<IChest>();
            var player = Mock.Of<IPlayer>();
            var evt = new ChestInventoryEvent(chest, player, 1, new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1));

            Assert.Same(player, evt.Player);
        }

        [Fact]
        public void Slot_Get()
        {
            var chest = Mock.Of<IChest>();
            var player = Mock.Of<IPlayer>();
            var evt = new ChestInventoryEvent(chest, player, 1, new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1));

            Assert.Equal(1, evt.Slot);
        }

        [Fact]
        public void Item_Get()
        {
            var chest = Mock.Of<IChest>();
            var player = Mock.Of<IPlayer>();
            var evt = new ChestInventoryEvent(chest, player, 1, new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1));

            Assert.Equal(new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1), evt.Item);
        }
    }
}
