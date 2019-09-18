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
using Orion.Entities;
using Xunit;

namespace Orion.Networking.Packets.Players {
    public class PlayerReleaseNpcPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new PlayerReleaseNpcPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }
        
        [Fact]
        public void SetNpcType_MarksAsDirty() {
            var packet = new PlayerReleaseNpcPacket();
            packet.NpcType = NpcType.BlueSlime;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetNpcType_NullValue_ThrowsArgumentNullException() {
            var packet = new PlayerReleaseNpcPacket();
            Action action = () => packet.NpcType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {14, 0, 71, 0, 1, 0, 0, 100, 0, 0, 0, 1, 0, 0};
        public static readonly byte[] InvalidNpcTypeBytes = {14, 0, 71, 0, 1, 0, 0, 100, 0, 0, 0, 255, 127, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (PlayerReleaseNpcPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.NpcPosition.Should().Be(new Vector2(256, 100));
                packet.NpcType.Should().Be(NpcType.BlueSlime);
                packet.NpcStyle.Should().Be(0);
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
        public void WriteToStream_IsCorrect() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
