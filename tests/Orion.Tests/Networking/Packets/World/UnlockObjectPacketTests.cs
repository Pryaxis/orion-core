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
using static Orion.Networking.Packets.World.UnlockObjectPacket;

namespace Orion.Networking.Packets.World {
    public class UnlockObjectPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new UnlockObjectPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetUnlockObjectType_MarksAsDirty() {
            var packet = new UnlockObjectPacket();

            packet.UnlockObjectType = ObjectType.Chest;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetUnlockObjectType_NullValue_ThrowsArgumentNullException() {
            var packet = new UnlockObjectPacket();
            Action action = () => packet.UnlockObjectType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {8, 0, 52, 1, 0, 1, 100, 0};
        public static readonly byte[] InvalidObjectTypeBytes = {8, 0, 52, 255, 0, 1, 100, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (UnlockObjectPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.UnlockObjectType.Should().BeSameAs(ObjectType.Chest);
                packet.ObjectX.Should().Be(256);
                packet.ObjectY.Should().Be(100);
            }
        }

        [Fact]
        public void ReadFromStream_InvalidObjectType_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidObjectTypeBytes)) {
                Func<Packet> func = () => Packet.ReadFromStream(stream, PacketContext.Server);

                func.Should().Throw<PacketException>();
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }

        [Fact]
        public void ObjectType_FromId_IsCorrect() {
            for (byte i = 1; i < 3; ++i) {
                ObjectType.FromId(i).Id.Should().Be(i);
            }

            ObjectType.FromId(0).Should().BeNull();
            ObjectType.FromId(3).Should().BeNull();
        }

        [Fact]
        public void ObjectType_FromId_ReturnsSameInstance() {
            var objectType = ObjectType.FromId(1);
            var objectType2 = ObjectType.FromId(1);

            objectType.Should().BeSameAs(objectType2);
        }
    }
}
