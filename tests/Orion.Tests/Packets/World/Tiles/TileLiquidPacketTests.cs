// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using System.IO;
using FluentAssertions;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Packets.World.Tiles {
    public class TileLiquidPacketTests {
        [Fact]
        [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
        [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
        public void SetTileLiquid_MarksAsDirty() {
            var packet = new TileLiquidPacket();

            packet.TileLiquid = new NetworkLiquid();

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetTileLiquid_NullValue_ThrowsArgumentNullException() {
            var packet = new TileLiquidPacket();
            Action action = () => packet.TileLiquid = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {9, 0, 48, 0, 1, 100, 0, 255, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using var stream = new MemoryStream(Bytes);
            var packet = (TileLiquidPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.TileLiquid.TileX.Should().Be(256);
            packet.TileLiquid.TileY.Should().Be(100);
            packet.TileLiquid.LiquidAmount.Should().Be(255);
            packet.TileLiquid.LiquidType.Should().Be(LiquidType.Water);
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
