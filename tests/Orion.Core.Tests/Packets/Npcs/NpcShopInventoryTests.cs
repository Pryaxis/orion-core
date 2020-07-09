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

using System.Diagnostics.CodeAnalysis;
using Orion.Core.Items;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class NpcShopInventoryTests
    {
        private readonly byte[] _bytes = { 14, 0, 104, 3, 17, 6, 1, 0, 82, 100, 0, 0, 0, 1 };

        [Fact]
        public void Slot_Set_Get()
        {
            var packet = new NpcShopInventory();

            packet.Slot = 3;

            Assert.Equal(3, packet.Slot);
        }

        [Fact]
        public void Id_Set_Get()
        {
            var packet = new NpcShopInventory();

            packet.Id = ItemId.Sdmg;

            Assert.Equal(ItemId.Sdmg, packet.Id);
        }

        [Fact]
        public void StackSize_Set_Get()
        {
            var packet = new NpcShopInventory();

            packet.StackSize = 1;

            Assert.Equal(1, packet.StackSize);
        }

        [Fact]
        public void Prefix_Set_Get()
        {
            var packet = new NpcShopInventory();

            packet.Prefix = ItemPrefix.Unreal;

            Assert.Equal(ItemPrefix.Unreal, packet.Prefix);
        }

        [Fact]
        public void Price_Set_Get()
        {
            var packet = new NpcShopInventory();

            packet.Price = 100;

            Assert.Equal(100, packet.Price);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SinglePurchase_Set_Get(bool value)
        {
            var packet = new NpcShopInventory();

            packet.SinglePurchase = value;

            Assert.Equal(value, packet.SinglePurchase);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcShopInventory>(_bytes, PacketContext.Server);

            Assert.Equal(3, packet.Slot);
            Assert.Equal(ItemId.Sdmg, packet.Id);
            Assert.Equal(1, packet.StackSize);
            Assert.Equal(ItemPrefix.Unreal, packet.Prefix);
            Assert.Equal(100, packet.Price);
            Assert.True(packet.SinglePurchase);
        }
    }
}
