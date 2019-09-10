using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Orion.Npcs;
using Xunit;

namespace Orion.Tests.Networking.Packets.Events {
    public class BossOrInvasionPacketTests {
        [Fact]
        public void GetIsBoss_True_IsCorrect() {
            var packet = new BossOrInvasionPacket {BossType = NpcType.BlueSlime};

            packet.IsBoss.Should().BeTrue();
        }

        [Fact]
        public void GetIsBoss_False_IsCorrect() {
            var packet = new BossOrInvasionPacket {InvasionType = NetworkInvasionType.Goblins};

            packet.IsBoss.Should().BeFalse();
        }

        [Fact]
        public void GetBossType_IsNotBoss_ThrowsInvalidOperationException() {
            var packet = new BossOrInvasionPacket {InvasionType = NetworkInvasionType.Goblins};
            Func<NpcType> func = () => packet.BossType;

            func.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void GetInvasionType_IsNotInvasion_ThrowsInvalidOperationException() {
            var packet = new BossOrInvasionPacket {BossType = NpcType.BlueSlime};
            Func<NetworkInvasionType> func = () => packet.InvasionType;

            func.Should().Throw<InvalidOperationException>();
        }

        public static readonly byte[] BossOrInvasionBytes = {7, 0, 61, 0, 0, 255, 255};

        [Fact]
        public void ReadFromStream_Invasion_IsCorrect() {
            using (var stream = new MemoryStream(BossOrInvasionBytes)) {
                var packet = (BossOrInvasionPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.SummmonOnPlayerIndex.Should().Be(0);
                packet.InvasionType.Should().Be(NetworkInvasionType.Goblins);
            }
        }

        [Fact]
        public void WriteToStream_Invasion_IsCorrect() {
            using (var stream = new MemoryStream(BossOrInvasionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(BossOrInvasionBytes);
            }
        }

        public static readonly byte[] BossOrInvasionBytes2 = {7, 0, 61, 0, 0, 1, 0};

        [Fact]
        public void ReadFromStream_Boss_IsCorrect() {
            using (var stream = new MemoryStream(BossOrInvasionBytes2)) {
                var packet = (BossOrInvasionPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.SummmonOnPlayerIndex.Should().Be(0);
                packet.BossType.Should().Be(NpcType.BlueSlime);
            }
        }

        [Fact]
        public void WriteToStream_Boss_IsCorrect() {
            using (var stream = new MemoryStream(BossOrInvasionBytes2))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(BossOrInvasionBytes2);
            }
        }
    }
}
