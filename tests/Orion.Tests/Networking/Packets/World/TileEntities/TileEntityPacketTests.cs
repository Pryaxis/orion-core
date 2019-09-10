using System.IO;
using FluentAssertions;
using Orion.Items;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World.TileEntities;
using Orion.World.TileEntities;
using Xunit;

namespace Orion.Tests.Networking.Packets.World.TileEntities {
    public class TileEntityPacketTests {
        private static readonly byte[] TileEntityBytes = {
            8, 0, 86, 0, 0, 0, 0, 0,
        };

        [Fact]
        public void ReadFromStream_Delete_IsCorrect() {
            using (var stream = new MemoryStream(TileEntityBytes)) {
                var packet = (TileEntityPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.TileEntityIndex.Should().Be(0);
                packet.TileEntity.Should().BeNull();
            }
        }

        [Fact]
        public void WriteToStream_Delete_IsCorrect() {
            using (var stream = new MemoryStream(TileEntityBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(TileEntityBytes);
            }
        }

        private static readonly byte[] TileEntityBytes2 = {
            15, 0, 86, 0, 0, 0, 0, 1, 0, 0, 1, 100, 0, 1, 0
        };

        [Fact]
        public void ReadFromStream_TargetDummy_IsCorrect() {
            using (var stream = new MemoryStream(TileEntityBytes2)) {
                var packet = (TileEntityPacket)Packet.ReadFromStream(stream, PacketContext.Server);

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
        public void WriteToStream_TargetDummy_IsCorrect() {
            using (var stream = new MemoryStream(TileEntityBytes2))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(TileEntityBytes2);
            }
        }

        private static readonly byte[] TileEntityBytes3 = {
            18, 0, 86, 0, 0, 0, 0, 1, 1, 0, 1, 100, 0, 17, 6, 82, 1, 0,
        };

        [Fact]
        public void ReadFromStream_ItemFrame_IsCorrect() {
            using (var stream = new MemoryStream(TileEntityBytes3)) {
                var packet = (TileEntityPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.TileEntityIndex.Should().Be(0);
                packet.TileEntity.Should().NotBeNull();
                packet.TileEntity.Index.Should().Be(0);
                packet.TileEntity.X.Should().Be(256);
                packet.TileEntity.Y.Should().Be(100);
                packet.TileEntity.Should().BeOfType<NetworkItemFrame>();
                packet.TileEntity.As<NetworkItemFrame>().ItemType.Should().Be(ItemType.SDMG);
                packet.TileEntity.As<NetworkItemFrame>().ItemPrefix.Should().Be(ItemPrefix.Unreal);
                packet.TileEntity.As<NetworkItemFrame>().ItemStackSize.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_ItemFrame_IsCorrect() {
            using (var stream = new MemoryStream(TileEntityBytes3))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(TileEntityBytes3);
            }
        }

        private static readonly byte[] TileEntityBytes4 = {
            15, 0, 86, 0, 0, 0, 0, 1, 2, 0, 1, 100, 0, 1, 1
        };

        [Fact]
        public void ReadFromStream_LogicSensor_IsCorrect() {
            using (var stream = new MemoryStream(TileEntityBytes4)) {
                var packet = (TileEntityPacket)Packet.ReadFromStream(stream, PacketContext.Server);

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
        public void WriteToStream_LogicSensor_IsCorrect() {
            using (var stream = new MemoryStream(TileEntityBytes4))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(TileEntityBytes4);
            }
        }
    }
}
