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
using Orion.Core.World.Tiles;
using Xunit;

namespace Orion.Core.Packets.World.Tiles {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class BlockPaintPacketTests {
        public static readonly byte[] Bytes = { 8, 0, 63, 0, 1, 100, 0, 1 };

        [Fact]
        public void X_Set_Get() {
            var packet = new BlockPaintPacket();

            packet.X = 256;

            Assert.Equal(256, packet.X);
        }

        [Fact]
        public void Y_Set_Get() {
            var packet = new BlockPaintPacket();

            packet.Y = 100;

            Assert.Equal(100, packet.Y);
        }

        [Fact]
        public void Color_Set_Get() {
            var packet = new BlockPaintPacket();

            packet.Color = PaintColor.Red;

            Assert.Equal(PaintColor.Red, packet.Color);
        }

        [Fact]
        public void Read() {
            var packet = new BlockPaintPacket();
            var span = Bytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(256, packet.X);
            Assert.Equal(100, packet.Y);
            Assert.Equal(PaintColor.Red, packet.Color);
        }

        [Fact]
        public void RoundTrip() {
            TestUtils.RoundTripPacket<BlockPaintPacket>(Bytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }
    }
}
