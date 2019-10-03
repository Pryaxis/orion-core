// Copyright (c) 2019 Pryaxis & Orion Contributors
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

using System.Diagnostics;
using System.IO;
using FluentAssertions;
using Orion.Items;
using Orion.World.TileEntities;
using Xunit;

namespace Orion.Packets.World.TileEntities {
    public class TileEntityInfoPacketTests {
        private static readonly byte[] RemoveBytes = {8, 0, 86, 0, 0, 0, 0, 0};

        [Fact]
        public void ReadFromStream_Remove() {
            using var stream = new MemoryStream(RemoveBytes);
            var packet = (TileEntityInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.TileEntityIndex.Should().Be(0);
            packet.TileEntity.Should().BeNull();
        }

        [Fact]
        public void DeserializeAndSerialize_Remove_SamePacket() {
            RemoveBytes.ShouldDeserializeAndSerializeSamePacket();
        }

        private static readonly byte[] TargetDummyBytes = {15, 0, 86, 0, 0, 0, 0, 1, 0, 0, 1, 100, 0, 1, 0};

        [Fact]
        public void ReadFromStream_TargetDummy() {
            using var stream = new MemoryStream(TargetDummyBytes);
            var packet = (TileEntityInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.TileEntityIndex.Should().Be(0);
            packet.TileEntity.Should().NotBeNull();
            packet.TileEntity.Index.Should().Be(0);
            packet.TileEntity.X.Should().Be(256);
            packet.TileEntity.Y.Should().Be(100);
            packet.TileEntity.Should().BeOfType<NetworkTargetDummy>();
            packet.TileEntity.As<NetworkTargetDummy>().NpcIndex.Should().Be(1);
        }

        [Fact]
        public void DeserializeAndSerialize_TargetDummy_SamePacket() {
            TargetDummyBytes.ShouldDeserializeAndSerializeSamePacket();
        }

        private static readonly byte[] ItemFrameBytes = {18, 0, 86, 0, 0, 0, 0, 1, 1, 0, 1, 100, 0, 17, 6, 82, 1, 0};

        [Fact]
        public void ReadFromStream_ItemFrame() {
            using var stream = new MemoryStream(ItemFrameBytes);
            var packet = (TileEntityInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.TileEntityIndex.Should().Be(0);
            packet.TileEntity.Should().NotBeNull();
            packet.TileEntity.Index.Should().Be(0);
            packet.TileEntity.X.Should().Be(256);
            packet.TileEntity.Y.Should().Be(100);
            packet.TileEntity.Should().BeOfType<NetworkItemFrame>();
            packet.TileEntity.As<NetworkItemFrame>().ItemType.Should().Be(ItemType.Sdmg);
            packet.TileEntity.As<NetworkItemFrame>().ItemPrefix.Should().Be(ItemPrefix.Unreal);
            packet.TileEntity.As<NetworkItemFrame>().ItemStackSize.Should().Be(1);
        }

        [Fact]
        public void DeserializeAndSerialize_ItemFrame_SamePacket() {
            ItemFrameBytes.ShouldDeserializeAndSerializeSamePacket();
        }

        private static readonly byte[] LogicSensorBytes = {15, 0, 86, 0, 0, 0, 0, 1, 2, 0, 1, 100, 0, 1, 1};

        [Fact]
        public void ReadFromStream_LogicSensor() {
            using var stream = new MemoryStream(LogicSensorBytes);
            var packet = (TileEntityInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.TileEntityIndex.Should().Be(0);
            packet.TileEntity.Should().NotBeNull();
            packet.TileEntity.Index.Should().Be(0);
            packet.TileEntity.X.Should().Be(256);
            packet.TileEntity.Y.Should().Be(100);
            packet.TileEntity.Should().BeOfType<NetworkLogicSensor>();
            packet.TileEntity.As<NetworkLogicSensor>().LogicSensorType.Should().Be(LogicSensorType.Daytime);
            packet.TileEntity.As<NetworkLogicSensor>().IsActivated.Should().BeTrue();
        }

        [Fact]
        public void DeserializeAndSerialize_LogicSensor_SamePacket() {
            LogicSensorBytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
