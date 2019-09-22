﻿// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using Orion.Networking.World;
using Xunit;

namespace Orion.Networking.Packets.World {
    public class BossOrInvasionPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new BossOrInvasionPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        public static readonly byte[] Bytes = {7, 0, 61, 0, 0, 255, 255};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (BossOrInvasionPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.SummmonOnPlayerIndex.Should().Be(0);
                packet.NetworkBossOrInvasion.Should().Be(NetworkBossOrInvasion.Goblins);
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
