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

namespace Orion.Networking.Packets.World.TileEntities {
    public class ChestNamePacketTests {
        [Fact]
        public void SetChestName_NullValue_ThrowsArgumentNullException() {
            var packet = new ChestNamePacket();
            Action action = () => packet.ChestName = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] ChestNameBytes = {
            18, 0, 69, 0, 0, 0, 1, 100, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ChestNameBytes)) {
                var packet = (ChestNamePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ChestIndex.Should().Be(0);
                packet.ChestX.Should().Be(256);
                packet.ChestY.Should().Be(100);
                packet.ChestName.Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ChestNameBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ChestNameBytes);
            }
        }
    }
}
