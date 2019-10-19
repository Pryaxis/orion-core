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

using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Projectiles;
using Xunit;
using TerrariaProjectile = Terraria.Projectile;

namespace Orion.Packets.Projectiles {
    public class ProjectileInfoPacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new ProjectileInfoPacket();

            packet.SimpleProperties_Set_MarkAsDirty();
        }

        [Fact]
        public void AiValues_SetItem_MarksAsDirty() {
            var packet = new ProjectileInfoPacket();
            packet.AiValues[0] = 0;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void AiValues_Count() {
            var packet = new ProjectileInfoPacket();

            packet.AiValues.Count.Should().Be(TerrariaProjectile.maxAI);
        }


        public static readonly byte[] Bytes = {
            31, 0, 27, 0, 0, 128, 57, 131, 71, 0, 200, 212, 69, 254, 14, 40, 65, 147, 84, 121, 193, 205, 204, 128, 64,
            99, 0, 0, 89, 0, 0
        };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (ProjectileInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.Identity.Should().Be(0);
            packet.Position.Should().Be(new Vector2(67187, 6809));
            packet.Velocity.Should().Be(new Vector2(10.50366f, -15.583148f));
            packet.Knockback.Should().Be(4.025f);
            packet.Damage.Should().Be(99);
            packet.OwnerIndex.Should().Be(0);
            packet.ProjectileType.Should().Be(ProjectileType.CrystalBullet);

            for (var i = 0; i < packet.AiValues.Count; ++i) {
                packet.AiValues[i].Should().Be(0);
            }

            packet.Uuid.Should().Be(-1);
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
