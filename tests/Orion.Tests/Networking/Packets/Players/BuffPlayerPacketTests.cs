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
using Xunit;

namespace Orion.Networking.Packets.Players {
    public class BuffPlayerPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new BuffPlayerPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetPlayerBuff_MarksAsDirty() {
            var packet = new BuffPlayerPacket();

            packet.PlayerBuff = new Buff(BuffType.None, TimeSpan.Zero);

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetPlayerBuff_NullValue_ThrowsArgumentNullException() {
            var packet = new BuffPlayerPacket();
            Action action = () => packet.PlayerBuff = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {9, 0, 55, 0, 1, 60, 0, 0, 0};
        public static readonly byte[] InvalidBuffTypeBytes = {9, 0, 55, 0, 255, 60, 0, 0, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (BuffPlayerPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerBuff.Should().BeEquivalentTo(new Buff(BuffType.ObsidianSkin, TimeSpan.FromSeconds(1)));
            }
        }

        [Fact]
        public void ReadFromStream_InvalidBuffType_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidBuffTypeBytes)) {
                Func<Packet> func = () => Packet.ReadFromStream(stream, PacketContext.Server);

                func.Should().Throw<PacketException>();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
