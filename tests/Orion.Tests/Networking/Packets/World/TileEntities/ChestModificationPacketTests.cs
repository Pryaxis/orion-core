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

using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.World.TileEntities;
using Xunit;

namespace Orion.Networking.Packets.World.TileEntities {
    public class ChestModificationPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new ChestModificationPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetChestModification_MarksAsDirty() {
            var packet = new ChestModificationPacket();

            packet.ChestModification = ChestModification.BreakChest;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetChestModification_NullValue_ThrowsArgumentNullException() {
            var packet = new ChestModificationPacket();
            Action action = () => packet.ChestModification = null;

            action.Should().Throw<ArgumentNullException>();
        }

        private static readonly byte[] Bytes = {12, 0, 34, 0, 100, 0, 100, 0, 1, 0, 0, 0};
        private static readonly byte[] InvalidModificationTypeBytes = {12, 0, 34, 255, 100, 0, 100, 0, 1, 0, 0, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (ChestModificationPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ChestModification.Should().BeSameAs(ChestModification.PlaceChest);
                packet.ChestX.Should().Be(100);
                packet.ChestY.Should().Be(100);
                packet.ChestStyle.Should().Be(1);
                packet.ChestIndex.Should().Be(0);
            }
        }

        [Fact]
        public void ReadFromStream_InvalidModificationType_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidModificationTypeBytes)) {
                Func<Packet> func = () => Packet.ReadFromStream(stream, PacketContext.Server);

                func.Should().Throw<PacketException>();
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
