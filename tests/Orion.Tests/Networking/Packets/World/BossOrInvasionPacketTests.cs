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
using Orion.Entities;
using Orion.Networking.World;
using Xunit;

namespace Orion.Networking.Packets.World {
    public class BossOrInvasionPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new BossOrInvasionPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void GetIsBoss_True_IsCorrect() {
            var packet = new BossOrInvasionPacket {Boss = NpcType.BlueSlime};

            packet.IsBoss.Should().BeTrue();
        }

        [Fact]
        public void GetIsBoss_False_IsCorrect() {
            var packet = new BossOrInvasionPacket {Invasion = NetworkInvasion.Goblins};

            packet.IsBoss.Should().BeFalse();
        }

        [Fact]
        public void GetBoss_IsNotBoss_ThrowsInvalidOperationException() {
            var packet = new BossOrInvasionPacket {Invasion = NetworkInvasion.Goblins};
            Func<NpcType> func = () => packet.Boss;

            func.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void SetBoss_MarksAsDirty() {
            var packet = new BossOrInvasionPacket();

            packet.Boss = NpcType.BlueSlime;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetBoss_NullValue_ThrowsArgumentNullException() {
            var packet = new BossOrInvasionPacket();
            Action action = () => packet.Boss = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetInvasionType_IsNotInvasion_ThrowsInvalidOperationException() {
            var packet = new BossOrInvasionPacket {Boss = NpcType.BlueSlime};
            Func<NetworkInvasion> func = () => packet.Invasion;

            func.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void SetInvasion_MarksAsDirty() {
            var packet = new BossOrInvasionPacket();

            packet.Invasion = NetworkInvasion.Eclipse;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetInvasion_NullValue_ThrowsArgumentNullException() {
            var packet = new BossOrInvasionPacket();
            Action action = () => packet.Invasion = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] InvasionBytes = {7, 0, 61, 0, 0, 255, 255};
        public static readonly byte[] InvalidInvasionBytes = {7, 0, 61, 0, 0, 0, 128};

        [Fact]
        public void ReadFromStream_Invasion_IsCorrect() {
            using (var stream = new MemoryStream(InvasionBytes)) {
                var packet = (BossOrInvasionPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.SummmonOnPlayerIndex.Should().Be(0);
                packet.Invasion.Should().Be(NetworkInvasion.Goblins);
            }
        }

        [Fact]
        public void ReadFromStream_InvalidInvasion_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidInvasionBytes)) {
                Func<Packet> func = () => Packet.ReadFromStream(stream, PacketContext.Server);

                func.Should().Throw<PacketException>();
            }
        }

        [Fact]
        public void WriteToStream_Invasion_IsCorrect() {
            InvasionBytes.ShouldDeserializeAndSerializeSamePacket();
        }

        public static readonly byte[] BossBytes = {7, 0, 61, 0, 0, 1, 0};
        public static readonly byte[] InvalidNpcTypeBytes = {7, 0, 61, 0, 0, 255, 127};

        [Fact]
        public void ReadFromStream_Boss_IsCorrect() {
            using (var stream = new MemoryStream(BossBytes)) {
                var packet = (BossOrInvasionPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.SummmonOnPlayerIndex.Should().Be(0);
                packet.Boss.Should().Be(NpcType.BlueSlime);
            }
        }

        [Fact]
        public void ReadFromStream_InvalidNpcType_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidNpcTypeBytes)) {
                Func<Packet> func = () => Packet.ReadFromStream(stream, PacketContext.Server);

                func.Should().Throw<PacketException>();
            }
        }

        [Fact]
        public void WriteToStream_Boss_IsCorrect() {
            BossBytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
