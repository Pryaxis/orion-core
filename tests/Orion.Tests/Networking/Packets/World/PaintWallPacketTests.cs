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
using Orion.World;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Networking.Packets.World {
    public class PaintWallPacketTests {
        [Fact]
        public void SetWallColor_NullValue_ThrowsArgumentNullException() {
            var packet = new PaintWallPacket();
            Action action = () => packet.WallColor = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {8, 0, 64, 0, 1, 100, 0, 1};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (PaintWallPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WallX.Should().Be(256);
                packet.WallY.Should().Be(100);
                packet.WallColor.Should().BeSameAs(PaintColor.Red);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
