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

namespace Orion.Core.Packets.Server
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class ServerCombatTextPacketTests
    {
        private readonly byte[] _bytes =
        {
            24, 0, 119, 0, 0, 200, 66, 0, 0, 128, 67, 255, 255, 255, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97
        };

        [Fact]
        public void Position_Set_Get()
        {
            var packet = new ServerCombatTextPacket();

            packet.Position = new Vector2f(100, 256);

            Assert.Equal(new Vector2f(100, 256), packet.Position);
        }

        [Fact]
        public void Color_Set_Get()
        {
            var packet = new ServerCombatTextPacket();

            packet.Color = Color3.White;

            Assert.Equal(Color3.White, packet.Color);
        }

        [Fact]
        public void Text_GetNullValue()
        {
            var packet = new ServerCombatTextPacket();

            Assert.Equal(NetworkText.Empty, packet.Text);
        }

        [Fact]
        public void Text_SetNullValue_ThrowsArgumentNullException()
        {
            var packet = new ServerCombatTextPacket();

            Assert.Throws<ArgumentNullException>(() => packet.Text = null!);
        }

        [Fact]
        public void Text_Set_Get()
        {
            var packet = new ServerCombatTextPacket();

            packet.Text = "Terraria";

            Assert.Equal("Terraria", packet.Text);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ServerCombatTextPacket>(_bytes, PacketContext.Server);

            Assert.Equal(new Vector2f(100, 256), packet.Position);
            Assert.Equal(Color3.White, packet.Color);
            Assert.Equal("Terraria", packet.Text);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket<ServerCombatTextPacket>(_bytes, PacketContext.Server);
        }
    }
}
