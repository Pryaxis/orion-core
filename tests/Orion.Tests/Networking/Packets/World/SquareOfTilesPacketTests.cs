// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using System.IO;
using FluentAssertions;
using Xunit;

namespace Orion.Networking.Packets.World {
    [Collection("TerrariaTestsCollection")]
    public class SquareOfTilesPacketTests {
        [Fact]
        public void SetTiles_NullValue_ThrowsArgumentNullException() {
            var packet = new SquareTilesPacket();
            Action action = () => packet.Tiles = null;

            action.Should().Throw<ArgumentNullException>();
        }

        private static readonly byte[] SquareOfTilesBytes = {
            17, 0, 20, 1, 0, 153, 16, 171, 1, 1, 0, 3, 0, 72, 0, 0, 0
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(SquareOfTilesBytes)) {
                var packet = (SquareTilesPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.SquareSize.Should().Be(1);
                packet.LiquidChangeType.Should().Be(LiquidChangeType.None);
                packet.TileX.Should().Be(4249);
                packet.TileY.Should().Be(427);
                packet.Tiles.GetLength(0).Should().Be(1);
                packet.Tiles.GetLength(1).Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(SquareOfTilesBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(SquareOfTilesBytes);
            }
        }
    }
}
