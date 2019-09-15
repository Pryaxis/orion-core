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
using Xunit;

namespace Orion.Networking.Packets.Players {
    public class ClientStatusPacketTests {
        [Fact]
        public void SetPlayerStatusText_NullValue_ThrowsArgumentNullException() {
            var packet = new PlayerStatusPacket();
            Action action = () => packet.PlayerStatusText = null;

            action.Should().Throw<ArgumentNullException>();
        }

        private static readonly byte[] Bytes = {
            28, 0, 9, 15, 0, 0, 0, 2, 18, 76, 101, 103, 97, 99, 121, 73, 110, 116, 101, 114, 102, 97, 99, 101, 46, 52,
            52, 0
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (PlayerStatusPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerStatusIncrease.Should().Be(15);
                packet.PlayerStatusText.ToString().Should().Be("LegacyInterface.44");
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            TestUtils.WriteToStream_SameBytes(Bytes);
        }
    }
}
