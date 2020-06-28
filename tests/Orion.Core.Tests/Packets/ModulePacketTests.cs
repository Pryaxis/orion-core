// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Diagnostics.CodeAnalysis;
using Moq;
using Orion.Core.Packets.DataStructures.Modules;
using Xunit;

namespace Orion.Core.Packets
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class ModulePacketTests
    {
        private readonly byte[] _chatBytes =
        {
            23, 0, 82, 1, 0, 3, 83, 97, 121, 13, 47, 99, 111, 109, 109, 97, 110, 100, 32, 116, 101, 115, 116
        };

        private readonly byte[] _unknownBytes = { 5, 0, 82, 255, 255 };

        [Fact]
        public void Module_GetNullValue_ThrowsInvalidOperationException()
        {
            var packet = new ModulePacket<IModule>();

            Assert.Throws<InvalidOperationException>(() => packet.Module);
        }

        [Fact]
        public void Module_SetNullValue_ThrowsArgumentNullException()
        {
            var packet = new ModulePacket<IModule>();

            Assert.Throws<ArgumentNullException>(() => packet.Module = null!);
        }

        [Fact]
        public void Module_Set_Get()
        {
            var module = Mock.Of<IModule>();
            var packet = new ModulePacket<IModule>();

            packet.Module = module;

            Assert.Same(module, packet.Module);
        }

        [Fact]
        public void Read_Chat()
        {
            var packet = TestUtils.ReadPacket<ModulePacket<Chat>>(_chatBytes, PacketContext.Server);

            Assert.Equal("Say", packet.Module.ClientCommand);
            Assert.Equal("/command test", packet.Module.ClientMessage);
        }

        [Fact]
        public void Read_Unknown()
        {
            var packet = TestUtils.ReadPacket<ModulePacket<UnknownModule>>(_unknownBytes, PacketContext.Server);

            Assert.Equal((ModuleId)65535, packet.Module.Id);
            Assert.Equal(0, packet.Module.Data.Length);
        }

        [Fact]
        public void Write_NullModule_ThrowsInvalidOperationException()
        {
            var packet = new ModulePacket<IModule>();
            var bytes = new byte[1000];

            Assert.Throws<InvalidOperationException>(() => packet.Write(bytes, PacketContext.Server));
        }
    }
}
