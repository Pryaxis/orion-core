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

namespace Orion.Core.Packets.World.TileEntities
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class ItemFrameInfoPacketTests
    {
        private readonly byte[] _bytes = { 12, 0, 89, 0, 1, 100, 0, 17, 6, 82, 1, 0 };

        [Fact]
        public void X_Set_Get()
        {
            var packet = new ItemFrameInfoPacket();

            packet.X = 256;

            Assert.Equal(256, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new ItemFrameInfoPacket();

            packet.Y = 100;

            Assert.Equal(100, packet.Y);
        }

        [Fact]
        public void Id_Set_Get()
        {
            var packet = new ItemFrameInfoPacket();

            packet.Id = ItemId.Sdmg;

            Assert.Equal(ItemId.Sdmg, packet.Id);
        }

        [Fact]
        public void Prefix_Set_Get()
        {
            var packet = new ItemFrameInfoPacket();

            packet.Prefix = ItemPrefix.Unreal;

            Assert.Equal(ItemPrefix.Unreal, packet.Prefix);
        }

        [Fact]
        public void StackSize_Set_Get()
        {
            var packet = new ItemFrameInfoPacket();

            packet.StackSize = 1;

            Assert.Equal(1, packet.StackSize);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ItemFrameInfoPacket>(_bytes, PacketContext.Server);

            Assert.Equal(256, packet.X);
            Assert.Equal(100, packet.Y);
            Assert.Equal(ItemId.Sdmg, packet.Id);
            Assert.Equal(ItemPrefix.Unreal, packet.Prefix);
            Assert.Equal(1, packet.StackSize);
        }
    }
}
