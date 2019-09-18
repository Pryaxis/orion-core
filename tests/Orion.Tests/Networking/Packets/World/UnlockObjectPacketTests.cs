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
    public class UnlockObjectPacketTests {
        [Fact]
        public void SetUnlockObjectType_NullValue_ThrowsArgumentNullException() {
            var packet = new UnlockObjectPacket();
            Action action = () => packet.UnlockObjectType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {8, 0, 52, 1, 0, 1, 100, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (UnlockObjectPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.UnlockObjectType.Should().BeSameAs(UnlockObjectPacket.ObjectType.Chest);
                packet.ObjectX.Should().Be(256);
                packet.ObjectY.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            TestUtils.WriteToStream_SameBytes(Bytes);
        }
    }
}
