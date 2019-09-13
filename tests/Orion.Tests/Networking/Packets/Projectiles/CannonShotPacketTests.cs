﻿// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using Xunit;

namespace Orion.Networking.Packets.Projectiles {
    public class CannonShotPacketTests {
        private static readonly byte[] CannonShotBytes = {18, 0, 108, 100, 0, 0, 0, 0, 0, 0, 1, 100, 0, 0, 0, 0, 0, 1};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(CannonShotBytes)) {
                var packet = (CannonShotPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ShotDamage.Should().Be(100);
                packet.ShotKnockback.Should().Be(0);
                packet.CannonTileX.Should().Be(256);
                packet.CannonTileY.Should().Be(100);
                packet.ShotAngle.Should().Be(0);
                packet.ShotAmmoType.Should().Be(0);
                packet.ShooterPlayerIndex.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(CannonShotBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(CannonShotBytes);
            }
        }
    }
}
