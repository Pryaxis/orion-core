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
using Microsoft.Xna.Framework;
using Xunit;

namespace Orion.Packets.Modules {
    public class ChatModuleTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var module = new ChatModule();

            module.SimpleProperties_Set_MarkAsDirty();
        }

        [Fact]
        public void ClientChatCommand_Set_NullValue_ThrowsArgumentNullException() {
            var module = new ChatModule();
            Action action = () => module.ClientChatCommand = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ClientChatText_Set_NullValue_ThrowsArgumentNullException() {
            var module = new ChatModule();
            Action action = () => module.ClientChatText = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ServerChatText_Set_NullValue_ThrowsArgumentNullException() {
            var module = new ChatModule();
            Action action = () => module.ServerChatText = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] ClientBytes = {
            23, 0, 82, 1, 0, 3, 83, 97, 121, 13, 47, 99, 111, 109, 109, 97, 110, 100, 32, 116, 101, 115, 116
        };

        [Fact]
        public void ReadFromStream_Client() {
            using var stream = new MemoryStream(ClientBytes);
            var packet = (ModulePacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.Module.Should().BeOfType<ChatModule>();
            packet.Module.As<ChatModule>().ClientChatCommand.Should().Be("Say");
            packet.Module.As<ChatModule>().ClientChatText.Should().Be("/command test");
        }

        [Fact]
        public void DeserializeAndSerialize_Client_SamePacket() {
            using var stream = new MemoryStream(ClientBytes);
            using var stream2 = new MemoryStream();
            var packet = Packet.ReadFromStream(stream, PacketContext.Server);

            packet.WriteToStream(stream2, PacketContext.Client);

            stream2.ToArray().Should().BeEquivalentTo(ClientBytes);
        }

        public static readonly byte[] ServerBytes = { 15, 0, 82, 1, 0, 1, 0, 4, 116, 101, 115, 116, 255, 255, 255 };

        [Fact]
        public void ReadFromStream_Server() {
            using var stream = new MemoryStream(ServerBytes);
            var packet = (ModulePacket)Packet.ReadFromStream(stream, PacketContext.Client);

            packet.Module.Should().BeOfType<ChatModule>();
            packet.Module.As<ChatModule>().ServerChattingPlayerIndex.Should().Be(1);
            packet.Module.As<ChatModule>().ServerChatText.Should().Be("test");
            packet.Module.As<ChatModule>().ServerChatColor.Should().Be(Color.White);
        }

        [Fact]
        public void DeserializeAndSerialize_Server_SamePacket() {
            using var stream = new MemoryStream(ServerBytes);
            using var stream2 = new MemoryStream();
            var packet = Packet.ReadFromStream(stream, PacketContext.Client);

            packet.WriteToStream(stream2, PacketContext.Server);

            stream2.ToArray().Should().BeEquivalentTo(ServerBytes);
        }
    }
}
