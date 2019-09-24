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

namespace Orion.Packets.Modules {
    public class ChatModuleTests {
        [Fact]
        public void SetSimpleProperties_MarkAsDirty() {
            var module = new ChatModule();

            module.SetSimplePropertiesShouldMarkAsDirty();
        }

        [Fact]
        public void SetClientChatCommand_NullValue_ThrowsArgumentNullException() {
            var module = new ChatModule();
            Action action = () => module.ClientChatCommand = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetClientChatText_NullValue_ThrowsArgumentNullException() {
            var module = new ChatModule();
            Action action = () => module.ClientChatText = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetServerChatText_NullValue_ThrowsArgumentNullException() {
            var module = new ChatModule();
            Action action = () => module.ServerChatText = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {
            23, 0, 82, 1, 0, 3, 83, 97, 121, 13, 47, 99, 111, 109, 109, 97, 110, 100, 32, 116, 101, 115, 116
        };

        [Fact]
        public void ReadFromStream_Client_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (ModulePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.Module.Should().BeOfType<ChatModule>();
                packet.Module.As<ChatModule>().ClientChatCommand.Should().Be("Say");
                packet.Module.As<ChatModule>().ClientChatText.Should().Be("/command test");
            }
        }

        [Fact]
        public void DeserializeAndSerialize_Client_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
