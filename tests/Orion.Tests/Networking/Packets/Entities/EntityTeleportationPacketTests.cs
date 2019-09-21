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
using Microsoft.Xna.Framework;
using Orion.Networking.Entities;
using Xunit;

namespace Orion.Networking.Packets.Entities {
    public class EntityTeleportationPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new EntityTeleportationPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetEntityTeleportationType_MarksAsDirty() {
            var packet = new EntityTeleportationPacket();

            packet.TeleportationType = EntityTeleportationType.PlayerToPlayer;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetEntityTeleportationType_NullValue_ThrowsArgumentNullException() {
            var packet = new EntityTeleportationPacket();
            Action action = () => packet.TeleportationType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {14, 0, 65, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (EntityTeleportationPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.TeleportationType.Should().BeSameAs(EntityTeleportationType.PlayerToPlayer);
                packet.TeleportationStyle.Should().Be(0);
                packet.EntityIndex.Should().Be(0);
                packet.EntityNewPosition.Should().Be(Vector2.Zero);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
