﻿// Copyright (c) 2019 Pryaxis & Orion Contributors
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

namespace Orion.Packets.World {
    public class ChatPacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new ChatPacket();

            packet.SimpleProperties_Set_MarkAsDirty();
        }

        [Fact]
        public void Text_SetNullValue_ThrowsArgumentNullException() {
            var packet = new ChatPacket();
            Action action = () => packet.Text = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {
            18, 0, 107, 255, 255, 255, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97, 100, 0
        };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (ChatPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.Color.Should().Be(Color.White);
            packet.Text.Should().Be("Terraria");
            packet.LineWidth.Should().Be(100);
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
