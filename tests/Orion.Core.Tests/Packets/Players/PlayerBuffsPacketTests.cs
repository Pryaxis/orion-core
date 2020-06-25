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
using Orion.Core.Buffs;
using Xunit;

namespace Orion.Core.Packets.Players
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PlayerBuffsPacketTests
    {
        private readonly byte[] _bytes =
        {
            48, 0, 50, 5, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
            0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0
        };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerBuffsPacket();

            packet.PlayerIndex = 5;

            Assert.Equal(5, packet.PlayerIndex);
        }

        [Fact]
        public void Ids_Set_Get()
        {
            var packet = new PlayerBuffsPacket();

            for (var i = 0; i < packet.Ids.Length; ++i)
            {
                packet.Ids[i] = BuffId.ObsidianSkin;
            }

            for (var i = 0; i < packet.Ids.Length; ++i)
            {
                Assert.Equal(BuffId.ObsidianSkin, packet.Ids[i]);
            }
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerBuffsPacket>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.PlayerIndex);

            for (var i = 0; i < packet.Ids.Length; ++i)
            {
                Assert.Equal(BuffId.ObsidianSkin, packet.Ids[i]);
            }
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket<PlayerBuffsPacket>(_bytes, PacketContext.Server);
        }
    }
}
