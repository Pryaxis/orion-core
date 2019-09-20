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
using static Orion.Networking.Packets.World.ToggleDoorPacket;

namespace Orion.Networking.Packets.World {
    public class ToggleDoorPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new ToggleDoorPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetToggleDoorAction_MarksAsDirty() {
            var packet = new ToggleDoorPacket();

            packet.ToggleDoorAction = DoorAction.CloseDoor;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetToggleDoorAction_NullValue_ThrowsArgumentNullException() {
            var packet = new ToggleDoorPacket();
            Action action = () => packet.ToggleDoorAction = null;

            action.Should().Throw<ArgumentNullException>();
        }

        private static readonly byte[] Bytes = {9, 0, 19, 0, 16, 14, 194, 1, 1};
        private static readonly byte[] InvalidDoorActionBytes = {9, 0, 19, 255, 16, 14, 194, 1, 1};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (ToggleDoorPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ToggleDoorAction.Should().BeSameAs(DoorAction.OpenDoor);
                packet.DoorX.Should().Be(3600);
                packet.DoorY.Should().Be(450);
                packet.ToggleDirection.Should().BeTrue();
            }
        }

        [Fact]
        public void ReadFromStream_InvalidDoorAction_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidDoorActionBytes)) {
                Func<Packet> func = () => Packet.ReadFromStream(stream, PacketContext.Server);

                func.Should().Throw<PacketException>();
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }

        [Fact]
        public void DoorAction_FromId_IsCorrect() {
            for (byte i = 0; i < 6; ++i) {
                DoorAction.FromId(i).Id.Should().Be(i);
            }

            DoorAction.FromId(6).Should().BeNull();
        }

        [Fact]
        public void DoorAction_FromId_ReturnsSameInstance() {
            var doorAction = DoorAction.FromId(1);
            var doorAction2 = DoorAction.FromId(1);

            doorAction.Should().BeSameAs(doorAction2);
        }
    }
}
