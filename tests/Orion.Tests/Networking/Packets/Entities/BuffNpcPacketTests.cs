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

namespace Orion.Networking.Packets.Entities {
    public class BuffNpcPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new BuffNpcPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetNpcBuff_MarksAsDirty() {
            var packet = new BuffNpcPacket();

            packet.NpcBuff = new Buff(BuffType.None, TimeSpan.Zero);

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetNpcBuff_NullValue_ThrowsArgumentNullException() {
            var packet = new BuffNpcPacket();
            Action action = () => packet.NpcBuff = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {8, 0, 53, 0, 0, 1, 60, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (BuffNpcPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.NpcIndex.Should().Be(0);
                packet.NpcBuff.Should().BeEquivalentTo(new Buff(BuffType.ObsidianSkin, TimeSpan.FromSeconds(1)));
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
