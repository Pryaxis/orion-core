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
using Orion.Core.DataStructures;
using Xunit;

namespace Orion.Core.Packets.Modules
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class ChatModuleTests
    {
        private readonly byte[] _serverBytes =
        {
            23, 0, 82, 1, 0, 3, 83, 97, 121, 13, 47, 99, 111, 109, 109, 97, 110, 100, 32, 116, 101, 115, 116
        };

        private readonly byte[] _clientBytes = { 15, 0, 82, 1, 0, 1, 0, 4, 116, 101, 115, 116, 255, 255, 255 };

        [Fact]
        public void ClientCommand_Get_Default()
        {
            var module = new ChatModule();

            Assert.Equal(string.Empty, module.ClientCommand);
        }

        [Fact]
        public void ClientCommand_SetNullValue_ThrowsArgumentNullException()
        {
            var module = new ChatModule();

            Assert.Throws<ArgumentNullException>(() => module.ClientCommand = null!);
        }

        [Fact]
        public void ClientCommand_Set_Get()
        {
            var module = new ChatModule();

            module.ClientCommand = "Say";

            Assert.Equal("Say", module.ClientCommand);
        }

        [Fact]
        public void ClientMessage_Get_Default()
        {
            var module = new ChatModule();

            Assert.Equal(string.Empty, module.ClientMessage);
        }

        [Fact]
        public void ClientMessage_SetNullValue_ThrowsArgumentNullException()
        {
            var module = new ChatModule();

            Assert.Throws<ArgumentNullException>(() => module.ClientMessage = null!);
        }

        [Fact]
        public void ClientMessage_Set_Get()
        {
            var module = new ChatModule();

            module.ClientMessage = "/command test";

            Assert.Equal("/command test", module.ClientMessage);
        }

        [Fact]
        public void ServerAuthorIndex_Set_Get()
        {
            var module = new ChatModule();

            module.ServerAuthorIndex = 1;

            Assert.Equal(1, module.ServerAuthorIndex);
        }

        [Fact]
        public void ServerMessage_SetNullValue_ThrowsArgumentNullException()
        {
            var module = new ChatModule();

            Assert.Throws<ArgumentNullException>(() => module.ServerMessage = null!);
        }

        [Fact]
        public void ServerMessage_Get_Default()
        {
            var module = new ChatModule();

            Assert.Equal(NetworkText.Empty, module.ServerMessage);
        }

        [Fact]
        public void ServerMessage_Set_Get()
        {
            var module = new ChatModule();

            module.ServerMessage = "test";

            Assert.Equal("test", module.ServerMessage);
        }

        [Fact]
        public void ServerColor_Set_Get()
        {
            var module = new ChatModule();

            module.ServerColor = Color3.White;

            Assert.Equal(Color3.White, module.ServerColor);
        }

        [Fact]
        public unsafe void Read_AsServer()
        {
            var module = new ChatModule();
            var span = _serverBytes.AsSpan((IPacket.HeaderSize + IModule.HeaderSize)..);
            Assert.Equal(span.Length, module.Read(span, PacketContext.Server));

            Assert.Equal("Say", module.ClientCommand);
            Assert.Equal("/command test", module.ClientMessage);
        }

        [Fact]
        public unsafe void Read_AsClient()
        {
            var module = new ChatModule();
            var span = _clientBytes.AsSpan((IPacket.HeaderSize + IModule.HeaderSize)..);
            Assert.Equal(span.Length, module.Read(span, PacketContext.Client));

            Assert.Equal(1, module.ServerAuthorIndex);
            Assert.Equal("test", module.ServerMessage);
            Assert.Equal(Color3.White, module.ServerColor);
        }

        [Fact]
        public void RoundTrip_AsServer()
        {
            TestUtils.RoundTripModule<ChatModule>(
                _serverBytes.AsSpan((IPacket.HeaderSize + IModule.HeaderSize)..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_AsClient()
        {
            TestUtils.RoundTripModule<ChatModule>(
                _clientBytes.AsSpan((IPacket.HeaderSize + IModule.HeaderSize)..), PacketContext.Client);
        }
    }
}
