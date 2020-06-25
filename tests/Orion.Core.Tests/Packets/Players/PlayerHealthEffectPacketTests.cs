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

using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Orion.Core.Packets.Players
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PlayerHealthEffectPacketTests
    {
        private readonly byte[] _bytes = { 6, 0, 35, 5, 100, 0 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerHealthEffectPacket();

            packet.PlayerIndex = 5;

            Assert.Equal(5, packet.PlayerIndex);
        }

        [Fact]
        public void HealthAmount_Set_Get()
        {
            var packet = new PlayerHealthEffectPacket();

            packet.HealthAmount = 100;

            Assert.Equal(100, packet.HealthAmount);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerHealthEffectPacket>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.PlayerIndex);
            Assert.Equal(100, packet.HealthAmount);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket(
                _bytes, PacketContext.Server);
        }
    }
}
