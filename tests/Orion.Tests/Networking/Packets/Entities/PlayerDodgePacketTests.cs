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
using Orion.Networking.Entities;
using Xunit;

namespace Orion.Networking.Packets.Entities {
    public class PlayerDodgePacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new PlayerDodgePacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetPlayerDodge_MarksAsDirty() {
            var packet = new PlayerDodgePacket();

            packet.PlayerDodge = PlayerDodge.NinjaDodge;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetPlayerDodge_NullValue_ThrowsArgumentNullException() {
            var packet = new PlayerDodgePacket();
            Action action = () => packet.PlayerDodge = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {5, 0, 62, 0, 1};
        public static readonly byte[] InvalidDodgeTypeBytes = {5, 0, 62, 0, 255};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (PlayerDodgePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerDodge.Should().BeSameAs(PlayerDodge.NinjaDodge);
            }
        }

        [Fact]
        public void ReadFromStream_InvalidDodgeType_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidDodgeTypeBytes)) {
                Func<Packet> func = () => Packet.ReadFromStream(stream, PacketContext.Server);

                func.Should().Throw<PacketException>();
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
