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
using Moq;
using Orion.Entities;
using Orion.Networking;
using Xunit;

namespace Orion.Events.Networking {
    public class PacketSendEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var packet = new TestPacket();
            Func<PacketSendEventArgs> func = () => new PacketSendEventArgs(null, packet);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetPlayer_IsCorrect() {
            var receiver = new Mock<IPlayer>().Object;
            var packet = new TestPacket();
            var args = new PacketSendEventArgs(receiver, packet);

            args.Receiver.Should().BeSameAs(receiver);
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
