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
using Orion.Entities;
using Orion.Networking.World.TileEntities;
using Orion.World.TileEntities;
using Xunit;

namespace Orion.Networking.Packets.World.TileEntities {
    public class TileEntityInfoPacketTests {
        private static readonly byte[] Bytes = {8, 0, 86, 0, 0, 0, 0, 0};

        [Fact]
        public void ReadFromStream_Delete_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (TileEntityInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.TileEntityIndex.Should().Be(0);
                packet.TileEntity.Should().BeNull();
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }

        private static readonly byte[] Bytes2 = {15, 0, 86, 0, 0, 0, 0, 1, 0, 0, 1, 100, 0, 1, 0};

        [Fact]
        public void ReadFromStream_TargetDummy_IsCorrect() {
            using (var stream = new MemoryStream(Bytes2)) {
                var packet = (TileEntityInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.TileEntityIndex.Should().Be(0);
                packet.TileEntity.Should().NotBeNull();
                packet.TileEntity.Index.Should().Be(0);
                packet.TileEntity.X.Should().Be(256);
                packet.TileEntity.Y.Should().Be(100);
                packet.TileEntity.Should().BeOfType<NetworkTargetDummy>();
                packet.TileEntity.As<NetworkTargetDummy>().NpcIndex.Should().Be(1);
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket2() {
            Bytes2.ShouldDeserializeAndSerializeSamePacket();
        }

        private static readonly byte[] Bytes3 = {18, 0, 86, 0, 0, 0, 0, 1, 1, 0, 1, 100, 0, 17, 6, 82, 1, 0};

        [Fact]
        public void ReadFromStream_ItemFrame_IsCorrect() {
            using (var stream = new MemoryStream(Bytes3)) {
                var packet = (TileEntityInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.TileEntityIndex.Should().Be(0);
                packet.TileEntity.Should().NotBeNull();
                packet.TileEntity.Index.Should().Be(0);
                packet.TileEntity.X.Should().Be(256);
                packet.TileEntity.Y.Should().Be(100);
                packet.TileEntity.Should().BeOfType<NetworkItemFrame>();
                packet.TileEntity.As<NetworkItemFrame>().ItemType.Should().BeSameAs(ItemType.Sdmg);
                packet.TileEntity.As<NetworkItemFrame>().ItemPrefix.Should().Be(ItemPrefix.Unreal);
                packet.TileEntity.As<NetworkItemFrame>().ItemStackSize.Should().Be(1);
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket3() {
            Bytes3.ShouldDeserializeAndSerializeSamePacket();
        }

        private static readonly byte[] Bytes4 = {15, 0, 86, 0, 0, 0, 0, 1, 2, 0, 1, 100, 0, 1, 1};

        [Fact]
        public void ReadFromStream_LogicSensor_IsCorrect() {
            using (var stream = new MemoryStream(Bytes4)) {
                var packet = (TileEntityInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.TileEntityIndex.Should().Be(0);
                packet.TileEntity.Should().NotBeNull();
                packet.TileEntity.Index.Should().Be(0);
                packet.TileEntity.X.Should().Be(256);
                packet.TileEntity.Y.Should().Be(100);
                packet.TileEntity.Should().BeOfType<NetworkLogicSensor>();
                packet.TileEntity.As<NetworkLogicSensor>().SensorType.Should().Be(LogicSensorType.Day);
                packet.TileEntity.As<NetworkLogicSensor>().IsActivated.Should().BeTrue();
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket4() {
            Bytes4.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
