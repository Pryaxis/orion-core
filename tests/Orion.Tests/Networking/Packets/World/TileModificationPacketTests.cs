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

using System.IO;
using FluentAssertions;
using Xunit;

namespace Orion.Networking.Packets.World {
    public class TileModificationPacketTests {
        private static readonly byte[] TileModificationBytes = {11, 0, 17, 0, 16, 14, 194, 1, 1, 0, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(TileModificationBytes)) {
                var packet = (TileModificationPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ModificationType.Should().Be(TileModificationType.DestroyBlock);
                packet.TileX.Should().Be(3600);
                packet.TileY.Should().Be(450);
                packet.ModificationData.Should().Be(1);
                packet.ModificationStyle.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(TileModificationBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(TileModificationBytes);
            }
        }
    }
}
