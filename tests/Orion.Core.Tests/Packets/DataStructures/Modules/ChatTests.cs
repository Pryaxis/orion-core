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
using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Packets.DataStructures.Modules
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class ChatTests
    {
        private readonly byte[] _serverBytes =
        {
            1, 0, 3, 83, 97, 121, 13, 47, 99, 111, 109, 109, 97, 110, 100, 32, 116, 101, 115, 116
        };

        private readonly byte[] _clientBytes = { 1, 0, 1, 0, 4, 116, 101, 115, 116, 255, 255, 255 };

        [Fact]
        public void ClientCommand_SetNullValue_ThrowsArgumentNullException()
        {
            var chat = new Chat();

            Assert.Throws<ArgumentNullException>(() => chat.ClientCommand = null!);
        }

        [Fact]
        public void ClientCommand_Set_Get()
        {
            var chat = new Chat();

            chat.ClientCommand = "Say";

            Assert.Equal("Say", chat.ClientCommand);
        }

        [Fact]
        public void ClientMessage_SetNullValue_ThrowsArgumentNullException()
        {
            var chat = new Chat();

            Assert.Throws<ArgumentNullException>(() => chat.ClientMessage = null!);
        }

        [Fact]
        public void ClientMessage_Set_Get()
        {
            var chat = new Chat();

            chat.ClientMessage = "/command test";

            Assert.Equal("/command test", chat.ClientMessage);
        }

        [Fact]
        public void ServerAuthorIndex_Set_Get()
        {
            var chat = new Chat();

            chat.ServerAuthorIndex = 1;

            Assert.Equal(1, chat.ServerAuthorIndex);
        }

        [Fact]
        public void ServerMessage_SetNullValue_ThrowsArgumentNullException()
        {
            var chat = new Chat();

            Assert.Throws<ArgumentNullException>(() => chat.ServerMessage = null!);
        }

        [Fact]
        public void ServerMessage_Set_Get()
        {
            var chat = new Chat();

            chat.ServerMessage = "test";

            Assert.Equal("test", chat.ServerMessage);
        }

        [Fact]
        public void ServerColor_Set_Get()
        {
            var chat = new Chat();

            chat.ServerColor = Color3.White;

            Assert.Equal(Color3.White, chat.ServerColor);
        }

        [Fact]
        public void Read_AsServer()
        {
            var chat = TestUtils.ReadModule<Chat>(_serverBytes, PacketContext.Server);

            Assert.Equal("Say", chat.ClientCommand);
            Assert.Equal("/command test", chat.ClientMessage);
        }

        [Fact]
        public void Read_AsClient()
        {
            var chat = TestUtils.ReadModule<Chat>(_clientBytes, PacketContext.Client);

            Assert.Equal(1, chat.ServerAuthorIndex);
            Assert.Equal("test", chat.ServerMessage);
            Assert.Equal(Color3.White, chat.ServerColor);
        }
    }
}
