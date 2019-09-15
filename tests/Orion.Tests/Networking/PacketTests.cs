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

namespace Orion.Networking {
    public class PacketTests {
        [Fact]
        public void ReadFromStream_NullStream_ThrowsArgumentNullException() {
            Func<Packet> func = () => Packet.ReadFromStream(null, PacketContext.Server);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void WriteToStream_NullStream_ThrowsArgumentNullException() {
            var packet = new TestPacket();
            Action action = () => packet.WriteToStream(null, PacketContext.Server);

            action.Should().Throw<ArgumentNullException>();
        }

        private class TestPacket : Packet {
            public override PacketType Type => throw new NotImplementedException();

            private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
                throw new NotImplementedException();

            private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
                throw new NotImplementedException();
        }
    }
}
