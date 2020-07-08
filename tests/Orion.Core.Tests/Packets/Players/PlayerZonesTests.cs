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
    public class PlayerZonesTests
    {
        private readonly byte[] _bytes = { 8, 0, 36, 5, 0, 0, 0, 0 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerZones();

            packet.PlayerIndex = 5;

            Assert.Equal(5, packet.PlayerIndex);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Dungeon_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Dungeon = value;

            Assert.Equal(value, packet.Dungeon);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Corruption_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Corruption = value;

            Assert.Equal(value, packet.Corruption);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Hallow_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Hallow = value;

            Assert.Equal(value, packet.Hallow);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Meteor_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Meteor = value;

            Assert.Equal(value, packet.Meteor);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Jungle_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Jungle = value;

            Assert.Equal(value, packet.Jungle);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Snow_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Snow = value;

            Assert.Equal(value, packet.Snow);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Crimson_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Crimson = value;

            Assert.Equal(value, packet.Crimson);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void WaterCandle_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.WaterCandle = value;

            Assert.Equal(value, packet.WaterCandle);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void PeaceCandle_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.PeaceCandle = value;

            Assert.Equal(value, packet.PeaceCandle);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SolarTower_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.SolarTower = value;

            Assert.Equal(value, packet.SolarTower);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void VortexTower_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.VortexTower = value;

            Assert.Equal(value, packet.VortexTower);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void NebulaTower_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.NebulaTower = value;

            Assert.Equal(value, packet.NebulaTower);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void StardustTower_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.StardustTower = value;

            Assert.Equal(value, packet.StardustTower);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Desert_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Desert = value;

            Assert.Equal(value, packet.Desert);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Mushroom_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Mushroom = value;

            Assert.Equal(value, packet.Mushroom);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UndergroundDesert_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.UndergroundDesert = value;

            Assert.Equal(value, packet.UndergroundDesert);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Sky_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Sky = value;

            Assert.Equal(value, packet.Sky);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Overworld_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Overworld = value;

            Assert.Equal(value, packet.Overworld);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Underground_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Underground = value;

            Assert.Equal(value, packet.Underground);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Cavern_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Cavern = value;

            Assert.Equal(value, packet.Cavern);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Underworld_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Underworld = value;

            Assert.Equal(value, packet.Underworld);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Beach_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Beach = value;

            Assert.Equal(value, packet.Beach);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Rain_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Rain = value;

            Assert.Equal(value, packet.Rain);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Sandstorm_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Sandstorm = value;

            Assert.Equal(value, packet.Sandstorm);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void OldOnesArmy_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.OldOnesArmy = value;

            Assert.Equal(value, packet.OldOnesArmy);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Granite_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Granite = value;

            Assert.Equal(value, packet.Granite);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Marble_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Marble = value;

            Assert.Equal(value, packet.Marble);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void BeeHive_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.BeeHive = value;

            Assert.Equal(value, packet.BeeHive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GemstoneCave_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.GemstoneCave = value;

            Assert.Equal(value, packet.GemstoneCave);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void LihzahrdTemple_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.LihzahrdTemple = value;

            Assert.Equal(value, packet.LihzahrdTemple);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Graveyard_Set_Get(bool value)
        {
            var packet = new PlayerZones();

            packet.Graveyard = value;

            Assert.Equal(value, packet.Graveyard);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerZones>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.PlayerIndex);
            Assert.False(packet.Dungeon);
            Assert.False(packet.Corruption);
            Assert.False(packet.Hallow);
            Assert.False(packet.Meteor);
            Assert.False(packet.Jungle);
            Assert.False(packet.Snow);
            Assert.False(packet.Crimson);
            Assert.False(packet.WaterCandle);
            Assert.False(packet.PeaceCandle);
            Assert.False(packet.SolarTower);
            Assert.False(packet.VortexTower);
            Assert.False(packet.NebulaTower);
            Assert.False(packet.StardustTower);
            Assert.False(packet.Desert);
            Assert.False(packet.Mushroom);
            Assert.False(packet.UndergroundDesert);
            Assert.False(packet.Sky);
            Assert.False(packet.Overworld);
            Assert.False(packet.Underground);
            Assert.False(packet.Cavern);
            Assert.False(packet.Underworld);
            Assert.False(packet.Beach);
            Assert.False(packet.Rain);
            Assert.False(packet.Sandstorm);
            Assert.False(packet.OldOnesArmy);
            Assert.False(packet.Granite);
            Assert.False(packet.Marble);
            Assert.False(packet.BeeHive);
            Assert.False(packet.GemstoneCave);
            Assert.False(packet.LihzahrdTemple);
            Assert.False(packet.Graveyard);
        }
    }
}
