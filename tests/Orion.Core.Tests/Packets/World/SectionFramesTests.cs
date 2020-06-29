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
using Xunit;

namespace Orion.Core.Packets.World
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class SectionFramesTests
    {
        private readonly byte[] _bytes = { 11, 0, 11, 100, 0, 200, 0, 0, 1, 0, 2 };

        [Fact]
        public void StartX_Set_Get()
        {
            var packet = new SectionFrames();

            packet.StartX = 100;

            Assert.Equal(100, packet.StartX);
        }

        [Fact]
        public void StartY_Set_Get()
        {
            var packet = new SectionFrames();

            packet.StartY = 200;

            Assert.Equal(200, packet.StartY);
        }

        [Fact]
        public void EndX_Set_Get()
        {
            var packet = new SectionFrames();

            packet.EndX = 256;

            Assert.Equal(256, packet.EndX);
        }

        [Fact]
        public void EndY_Set_Get()
        {
            var packet = new SectionFrames();

            packet.EndY = 512;

            Assert.Equal(512, packet.EndY);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<SectionFrames>(_bytes, PacketContext.Server);

            Assert.Equal(100, packet.StartX);
            Assert.Equal(200, packet.StartY);
            Assert.Equal(256, packet.EndX);
            Assert.Equal(512, packet.EndY);
        }
    }
}
