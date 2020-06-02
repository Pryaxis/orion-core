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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Packets.World.Tiles {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class TileSquarePacketTests {
        public static readonly byte[] Bytes = {
            63, 0, 20, 3, 0, 158, 8, 56, 1, 5, 0, 0, 0, 2, 0, 12, 0, 2, 0, 255, 0, 12, 0, 2, 0, 255, 0, 12, 0, 2, 0,
            255, 0, 12, 0, 2, 0, 255, 0, 12, 0, 2, 0, 255, 0, 12, 0, 2, 0, 255, 0, 12, 0, 2, 0, 255, 0, 12, 0, 2, 0,
            255, 0
        };

        [Fact]
        public void X_Set_Get() {
            var packet = new TileSquarePacket();

            packet.X = 2206;

            Assert.Equal(2206, packet.X);
        }

        [Fact]
        public void Y_Set_Get() {
            var packet = new TileSquarePacket();

            packet.Y = 312;

            Assert.Equal(312, packet.Y);
        }

        [Fact]
        public void Tiles_SetNullValue_ThrowsArgumentNullException() {
            var packet = new TileSquarePacket();

            Assert.Throws<ArgumentNullException>(() => packet.Tiles = null!);
        }

        [Fact]
        public void Tiles_SetNotSquareArray_ThrowsArgumentException() {
            var packet = new TileSquarePacket();

            Assert.Throws<ArgumentException>(() => packet.Tiles = new Tile[1, 2]);
        }

        [Fact]
        public void Tiles_Set_Get() {
            var tiles = new Tile[1, 1];
            var packet = new TileSquarePacket();

            packet.Tiles = tiles;

            Assert.Same(tiles, packet.Tiles);
        }

        [Fact]
        public void Read() {
            var packet = new TileSquarePacket();
            var span = Bytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(2206, packet.X);
            Assert.Equal(312, packet.Y);
            Assert.Equal(3, packet.Tiles.GetLength(0));
            Assert.Equal(3, packet.Tiles.GetLength(1));
        }

        [Fact]
        public void RoundTrip_BreakBlock() {
            TestUtils.RoundTripPacket<TileSquarePacket>(Bytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }
    }
}
