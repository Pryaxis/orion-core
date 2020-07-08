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
using Orion.Core.Items;
using Xunit;

namespace Orion.Core.Packets.Players
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PlayerInventoryTests
    {
        private readonly byte[] _bytes = { 11, 0, 5, 5, 1, 0, 1, 0, 59, 179, 13 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerInventory();

            packet.PlayerIndex = 5;

            Assert.Equal(5, packet.PlayerIndex);
        }

        [Fact]
        public void Slot_Set_Get()
        {
            var packet = new PlayerInventory();

            packet.Slot = 1;

            Assert.Equal(1, packet.Slot);
        }

        [Fact]
        public void StackSize_Set_Get()
        {
            var packet = new PlayerInventory();

            packet.StackSize = 1;

            Assert.Equal(1, packet.StackSize);
        }

        [Fact]
        public void Prefix_Set_Get()
        {
            var packet = new PlayerInventory();

            packet.Prefix = ItemPrefix.Godly;

            Assert.Equal(ItemPrefix.Godly, packet.Prefix);
        }

        [Fact]
        public void Id_Set_Get()
        {
            var packet = new PlayerInventory();

            packet.Id = ItemId.CopperShortsword;

            Assert.Equal(ItemId.CopperShortsword, packet.Id);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerInventory>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.PlayerIndex);
            Assert.Equal(1, packet.Slot);
            Assert.Equal(1, packet.StackSize);
            Assert.Equal(ItemPrefix.Godly, packet.Prefix);
            Assert.Equal(ItemId.CopperShortsword, packet.Id);
        }
    }
}
