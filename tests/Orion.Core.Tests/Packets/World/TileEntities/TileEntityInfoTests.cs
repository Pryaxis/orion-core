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
using Moq;
using Orion.Core.Packets.DataStructures.TileEntities;
using Xunit;

namespace Orion.Core.Packets.World.TileEntities
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class TileEntityInfoTests
    {
        private readonly byte[] _bytes = { 15, 0, 86, 10, 0, 0, 0, 1, 0, 0, 1, 100, 0, 5, 0 };
        private readonly byte[] _removeBytes = { 8, 0, 86, 10, 0, 0, 0, 0 };

        [Fact]
        public void TileEntityIndex_Set_Get()
        {
            var packet = new TileEntityInfo();

            packet.TileEntityIndex = 10;

            Assert.Equal(10, packet.TileEntityIndex);
        }

        [Fact]
        public void TileEntity_Set_Get()
        {
            var tileEntity = Mock.Of<SerializableTileEntity>();
            var packet = new TileEntityInfo();

            packet.TileEntity = tileEntity;

            Assert.Same(tileEntity, packet.TileEntity);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<TileEntityInfo>(_bytes, PacketContext.Server);

            Assert.Equal(10, packet.TileEntityIndex);
            Assert.NotNull(packet.TileEntity);
            Assert.Equal(256, packet.TileEntity!.X);
            Assert.Equal(100, packet.TileEntity!.Y);
            Assert.IsType<TargetDummy>(packet.TileEntity!);
            Assert.Equal(5, ((TargetDummy)packet.TileEntity!).NpcIndex);
        }

        [Fact]
        public void Read_Remove()
        {
            var packet = TestUtils.ReadPacket<TileEntityInfo>(_removeBytes, PacketContext.Server);

            Assert.Equal(10, packet.TileEntityIndex);
            Assert.Null(packet.TileEntity);
        }
    }
}
