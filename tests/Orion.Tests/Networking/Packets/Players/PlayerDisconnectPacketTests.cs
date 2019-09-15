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
    public class PlayerDisconnectPacketTests {
        [Fact]
        public void SetReason_NullValue_ThrowsArgumentNullException() {
            var packet = new PlayerDisconnectPacket();
            Action action = () => packet.DisconnectReason = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] PlayerDisconnectBytes = {
            21, 0, 2, 2, 15, 67, 76, 73, 46, 75, 105, 99, 107, 77, 101, 115, 115, 97, 103, 101, 0
        };


        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerDisconnectBytes)) {
                var packet = (PlayerDisconnectPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.DisconnectReason.ToString().Should().Be("CLI.KickMessage");
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerDisconnectBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(PlayerDisconnectBytes);
            }
        }
    }
}
