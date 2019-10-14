// Copyright (c) 2019 Pryaxis & Orion Contributors
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

namespace Orion.Packets.Modules {
    public class ModulePacketTests {
        [Fact]
        public void Module_Set_MarksAsDirty() {
            var packet = new ModulePacket();

            packet.Module = new ChatModule();

            packet.ShouldBeDirty();
        }

        [Fact]
        public void Module_Set_NullValue_ThrowsArgumentNullException() {
            var packet = new ModulePacket();
            Action action = () => packet.Module = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void WriteToStream_NullModule_ThrowsPacketException() {
            using var stream = new MemoryStream();
            var packet = new ModulePacket();
            Action action = () => packet.WriteToStream(stream, PacketContext.Server);

            action.Should().Throw<PacketException>();
        }
    }
}
