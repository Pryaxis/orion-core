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
using Orion.Core.Packets.DataStructures.TileEntities;
using Xunit;

namespace Orion.Core.Packets.World.TileEntities
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class TileEntityPlaceTests
    {
        private readonly byte[] _bytes = { 8, 0, 87, 0, 1, 100, 0, 1 };

        [Fact]
        public void X_Set_Get()
        {
            var packet = new TileEntityPlace();

            packet.X = 256;

            Assert.Equal(256, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new TileEntityPlace();

            packet.Y = 100;

            Assert.Equal(100, packet.Y);
        }

        [Fact]
        public void Id_Set_Get()
        {
            var packet = new TileEntityPlace();

            packet.Id = TileEntityId.ItemFrame;

            Assert.Equal(TileEntityId.ItemFrame, packet.Id);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<TileEntityPlace>(_bytes, PacketContext.Server);

            Assert.Equal(256, packet.X);
            Assert.Equal(100, packet.Y);
            Assert.Equal(TileEntityId.ItemFrame, packet.Id);
        }
    }
}
