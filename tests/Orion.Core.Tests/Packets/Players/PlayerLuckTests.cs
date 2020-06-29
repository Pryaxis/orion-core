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
    public class PlayerLuckTests
    {
        private readonly byte[] _bytes = { 14, 0, 134, 5, 60, 0, 0, 0, 0, 0, 0, 0, 3, 1 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerLuck();

            packet.PlayerIndex = 5;

            Assert.Equal(5, packet.PlayerIndex);
        }

        [Fact]
        public void LadybugTicks_Set_Get()
        {
            var packet = new PlayerLuck();

            packet.LadybugTicks = 60;

            Assert.Equal(60, packet.LadybugTicks);
        }

        [Fact]
        public void Torch_Set_Get()
        {
            var packet = new PlayerLuck();

            packet.Torch = 0f;

            Assert.Equal(0f, packet.Torch);
        }

        [Fact]
        public void PotionStrength_Set_Get()
        {
            var packet = new PlayerLuck();

            packet.PotionStrength = 3;

            Assert.Equal(3, packet.PotionStrength);
        }

        [Fact]
        public void IsNearGardenGnome_Set_Get()
        {
            var packet = new PlayerLuck();

            packet.IsNearGardenGnome = true;

            Assert.True(packet.IsNearGardenGnome);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerLuck>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.PlayerIndex);
            Assert.Equal(60, packet.LadybugTicks);
            Assert.Equal(0f, packet.Torch);
            Assert.Equal(3, packet.PotionStrength);
            Assert.True(packet.IsNearGardenGnome);
        }
    }
}
