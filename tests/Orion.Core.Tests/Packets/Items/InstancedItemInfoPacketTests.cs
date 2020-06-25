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
using Orion.Core.DataStructures;
using Orion.Core.Items;
using Xunit;

namespace Orion.Core.Packets.Items
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class InstancedItemInfoPacketTests
    {
        private readonly byte[] _bytes =
        {
            27, 0, 90, 144, 1, 128, 51, 131, 71, 0, 112, 212, 69, 0, 0, 128, 64, 0, 0, 0, 192, 1, 0, 82, 0, 17, 6
        };

        [Fact]
        public void ItemIndex_Set_Get()
        {
            var packet = new InstancedItemInfoPacket();

            packet.ItemIndex = 400;

            Assert.Equal(400, packet.ItemIndex);
        }

        [Fact]
        public void Position_Set_Get()
        {
            var packet = new InstancedItemInfoPacket();

            packet.Position = new Vector2f(67175, 6798);

            Assert.Equal(new Vector2f(67175, 6798), packet.Position);
        }

        [Fact]
        public void Velocity_Set_Get()
        {
            var packet = new InstancedItemInfoPacket();

            packet.Velocity = new Vector2f(4, -2);

            Assert.Equal(new Vector2f(4, -2), packet.Velocity);
        }

        [Fact]
        public void StackSize_Set_Get()
        {
            var packet = new InstancedItemInfoPacket();

            packet.StackSize = 1;

            Assert.Equal(1, packet.StackSize);
        }

        [Fact]
        public void Prefix_Set_Get()
        {
            var packet = new InstancedItemInfoPacket();

            packet.Prefix = ItemPrefix.Unreal;

            Assert.Equal(ItemPrefix.Unreal, packet.Prefix);
        }

        [Fact]
        public void AllowSelfPickup_Set_Get()
        {
            var packet = new InstancedItemInfoPacket();

            packet.AllowSelfPickup = false;

            Assert.False(packet.AllowSelfPickup);
        }

        [Fact]
        public void Id_Set_Get()
        {
            var packet = new InstancedItemInfoPacket();

            packet.Id = ItemId.Sdmg;

            Assert.Equal(ItemId.Sdmg, packet.Id);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<InstancedItemInfoPacket>(_bytes, PacketContext.Server);

            Assert.Equal(400, packet.ItemIndex);
            Assert.Equal(new Vector2f(67175, 6798), packet.Position);
            Assert.Equal(new Vector2f(4, -2), packet.Velocity);
            Assert.Equal(1, packet.StackSize);
            Assert.Equal(ItemPrefix.Unreal, packet.Prefix);
            Assert.False(packet.AllowSelfPickup);
            Assert.Equal(ItemId.Sdmg, packet.Id);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket(
                _bytes, PacketContext.Server);
        }
    }
}
