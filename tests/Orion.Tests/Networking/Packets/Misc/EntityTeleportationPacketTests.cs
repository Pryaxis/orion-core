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
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Misc;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class EntityTeleportationPacketTests {
        public static readonly byte[] EntityTeleportationBytes = {14, 0, 65, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(EntityTeleportationBytes)) {
                var packet = (EntityTeleportationPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.TeleportationType.Should().Be(EntityTeleportationType.TeleportPlayerToOtherPlayer);
                packet.TeleportationStyle.Should().Be(0);
                packet.EntityIndex.Should().Be(0);
                packet.EntityNewPosition.Should().Be(Vector2.Zero);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(EntityTeleportationBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(EntityTeleportationBytes);
            }
        }
    }
}
