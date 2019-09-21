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
using Orion.Networking.Misc;
using Xunit;

namespace Orion.Networking.Packets.Misc {
    public class MiscActionPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new MiscActionPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetEntityAction_MarksAsDirty() {
            var packet = new MiscActionPacket();

            packet.Action = MiscAction.CreateMimicSmoke;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetEntityAction_NullValue_ThrowsArgumentNullException() {
            var packet = new MiscActionPacket();
            Action action = () => packet.Action = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {5, 0, 51, 0, 1};
        public static readonly byte[] InvalidEntityActionBytes = {5, 0, 51, 0, 255};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (MiscActionPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerOrNpcIndex.Should().Be(0);
                packet.Action.Should().Be(MiscAction.SpawnSkeletron);
            }
        }

        [Fact]
        public void ReadFromStream_InvalidEntityAction_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidEntityActionBytes)) {
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
