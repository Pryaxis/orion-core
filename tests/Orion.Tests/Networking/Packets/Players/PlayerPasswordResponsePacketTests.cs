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
    public class PlayerPasswordResponsePacketTests {
        [Fact]
        public void SetPlayerPassword_MarksAsDirty() {
            var packet = new PlayerPasswordResponsePacket();

            packet.PlayerPassword = "";

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetPlayerPassword_NullValue_ThrowsArgumentNullException() {
            var packet = new PlayerPasswordResponsePacket();
            Action action = () => packet.PlayerPassword = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {12, 0, 38, 8, 84, 101, 114, 114, 97, 114, 105, 97};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (PlayerPasswordResponsePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerPassword.Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
