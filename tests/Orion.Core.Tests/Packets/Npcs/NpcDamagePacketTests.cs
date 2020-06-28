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

namespace Orion.Core.Packets.Npcs
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class NpcDamagePacketTests
    {
        private readonly byte[] _bytes = { 13, 0, 28, 5, 0, 108, 0, 205, 204, 128, 64, 2, 1 };

        [Fact]
        public void NpcIndex_Set_Get()
        {
            var packet = new NpcDamagePacket();

            packet.NpcIndex = 5;

            Assert.Equal(5, packet.NpcIndex);
        }

        [Fact]
        public void Damage_Set_Get()
        {
            var packet = new NpcDamagePacket();

            packet.Damage = 108;

            Assert.Equal(108, packet.Damage);
        }

        [Fact]
        public void Knockback_Set_Get()
        {
            var packet = new NpcDamagePacket();

            packet.Knockback = 4.025f;

            Assert.Equal(4.025f, packet.Knockback);
        }

        [Fact]
        public void Direction_Set_Get()
        {
            var packet = new NpcDamagePacket();

            packet.Direction = NpcDamagePacket.HitDirection.Right;

            Assert.Equal(NpcDamagePacket.HitDirection.Right, packet.Direction);
        }

        [Fact]
        public void IsCritical_Set_Get()
        {
            var packet = new NpcDamagePacket();

            packet.IsCritical = true;

            Assert.True(packet.IsCritical);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcDamagePacket>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.NpcIndex);
            Assert.Equal(108, packet.Damage);
            Assert.Equal(4.025f, packet.Knockback);
            Assert.Equal(NpcDamagePacket.HitDirection.Right, packet.Direction);
            Assert.True(packet.IsCritical);
        }
    }
}
