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
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class ToggleDoorPacketTests {
        private static readonly byte[] ToggleDoorBytes = {9, 0, 19, 0, 16, 14, 194, 1, 1};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ToggleDoorBytes)) {
                var packet = (ToggleDoorPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.DoorAction.Should().Be(ToggleDoorAction.OpenDoor);
                packet.DoorX.Should().Be(3600);
                packet.DoorY.Should().Be(450);
                packet.ToggleDirection.Should().BeTrue();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ToggleDoorBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ToggleDoorBytes);
            }
        }
    }
}
