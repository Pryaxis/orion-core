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
using Orion.Packets;
using Xunit;

namespace Orion.Events.Packets {
    public class PacketEventArgsTests {
        [Fact]
        public void Ctor_NotDirty() {
            var packet = new TestPacket();
            var args = new TestArgs(packet);

            args.IsDirty.Should().BeFalse();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            Func<PacketEventArgs> func = () => new TestArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetPacket_IsCorrect() {
            var packet = new TestPacket();
            var args = new TestArgs(packet);

            args.Packet.Should().BeSameAs(packet);
        }

        [Fact]
        public void SetPacket_MarksAsDirty() {
            var packet = new TestPacket();
            var args = new TestArgs(packet);
            args.Packet = new TestPacket();

            args.IsDirty.Should().BeTrue();
        }

        [Fact]
        public void SetPacket_NullValue_ThrowsArgumentNullException() {
            var packet = new TestPacket();
            var args = new TestArgs(packet);
            Action action = () => args.Packet = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetIsDirty_IsCorrect() {
            var packet = new TestPacket();
            var args = new TestArgs(packet);
            packet.MarkAsDirty();

            args.IsDirty.Should().BeTrue();
        }

        private class TestArgs : PacketEventArgs {
            public TestArgs(Packet packet) : base(packet) { }
        }

        private class TestPacket : Packet {
            public override PacketType Type => throw new NotImplementedException();

            public void MarkAsDirty() {
                _isDirty = true;
            }

            private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
                throw new NotImplementedException();

            private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
                throw new NotImplementedException();
        }
    }
}
