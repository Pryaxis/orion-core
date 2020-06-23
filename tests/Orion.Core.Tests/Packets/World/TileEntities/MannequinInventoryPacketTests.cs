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

namespace Orion.Core.Packets.World.TileEntities
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class MannequinInventoryPacketTests
    {
        private readonly byte[] _bytes = { 14, 0, 121, 5, 2, 0, 0, 0, 2, 17, 6, 1, 0, 82 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new MannequinInventoryPacket();

            packet.PlayerIndex = 5;

            Assert.Equal(5, packet.PlayerIndex);
        }

        [Fact]
        public void TileEntityIndex_Set_Get()
        {
            var packet = new MannequinInventoryPacket();

            packet.TileEntityIndex = 2;

            Assert.Equal(2, packet.TileEntityIndex);
        }

        [Fact]
        public void Slot_Set_Get()
        {
            var packet = new MannequinInventoryPacket();

            packet.Slot = 2;

            Assert.Equal(2, packet.Slot);
        }

        [Fact]
        public void Id_Set_Get()
        {
            var packet = new MannequinInventoryPacket();

            packet.Id = ItemId.Sdmg;

            Assert.Equal(ItemId.Sdmg, packet.Id);
        }

        [Fact]
        public void StackSize_Set_Get()
        {
            var packet = new MannequinInventoryPacket();

            packet.StackSize = 1;

            Assert.Equal(1, packet.StackSize);
        }

        [Fact]
        public void Prefix_Set_Get()
        {
            var packet = new MannequinInventoryPacket();

            packet.Prefix = ItemPrefix.Unreal;

            Assert.Equal(ItemPrefix.Unreal, packet.Prefix);
        }

        [Fact]
        public void Read()
        {
            var packet = new MannequinInventoryPacket();
            var span = _bytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(5, packet.PlayerIndex);
            Assert.Equal(2, packet.TileEntityIndex);
            Assert.Equal(2, packet.Slot);
            Assert.Equal(ItemId.Sdmg, packet.Id);
            Assert.Equal(1, packet.StackSize);
            Assert.Equal(ItemPrefix.Unreal, packet.Prefix);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket<MannequinInventoryPacket>(
                _bytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }
    }
}
