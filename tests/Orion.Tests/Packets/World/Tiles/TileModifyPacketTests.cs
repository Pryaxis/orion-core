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
using Orion.World.Tiles;
using Xunit;

namespace Orion.Packets.World.Tiles {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class TileModifyPacketTests {
        public static readonly byte[] BreakBlockBytes = { 11, 0, 17, 0, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] PlaceBlockBytes = { 11, 0, 17, 1, 100, 0, 0, 1, 4, 0, 1 };
        public static readonly byte[] BreakWallBytes = { 11, 0, 17, 2, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] PlaceWallBytes = { 11, 0, 17, 3, 100, 0, 0, 1, 1, 0, 0 };
        public static readonly byte[] BreakBlockNoItemsBytes = { 11, 0, 17, 4, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] PlaceRedWireBytes = { 11, 0, 17, 5, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] BreakRedWireBytes = { 11, 0, 17, 6, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] HammerBlockBytes = { 11, 0, 17, 7, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] PlaceActuatorBytes = { 11, 0, 17, 8, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] BreakActuatorBytes = { 11, 0, 17, 9, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] PlaceBlueWireBytes = { 11, 0, 17, 10, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] BreakBlueWireBytes = { 11, 0, 17, 11, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] PlaceGreenWireBytes = { 11, 0, 17, 12, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] BreakGreenWireBytes = { 11, 0, 17, 13, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] SlopeBlockBytes = { 11, 0, 17, 14, 100, 0, 0, 1, 1, 0, 0 };
        public static readonly byte[] ModifyTrackBytes = { 11, 0, 17, 15, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] PlaceYellowWireBytes = { 11, 0, 17, 16, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] BreakYellowWireBytes = { 11, 0, 17, 17, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] ModifyLogicGateBytes = { 11, 0, 17, 18, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] ActuateBlockBytes = { 11, 0, 17, 19, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] BreakContainerBytes = { 11, 0, 17, 20, 100, 0, 0, 1, 0, 0, 0 };
        public static readonly byte[] ReplaceBlockBytes = { 11, 0, 17, 21, 100, 0, 0, 1, 4, 0, 1 };
        public static readonly byte[] ReplaceWallBytes = { 11, 0, 17, 22, 100, 0, 0, 1, 1, 0, 0 };
        public static readonly byte[] SlopeAndHammerBlockBytes = { 11, 0, 17, 23, 100, 0, 0, 1, 1, 0, 0 };

        [Fact]
        public void Modification_Set_Get() {
            var packet = new TileModifyPacket();

            packet.Modification = TileModification.BreakBlock;

            Assert.Equal(TileModification.BreakBlock, packet.Modification);
        }

        [Fact]
        public void X_Set_Get() {
            var packet = new TileModifyPacket();

            packet.X = 100;

            Assert.Equal(100, packet.X);
        }

        [Fact]
        public void Y_Set_Get() {
            var packet = new TileModifyPacket();

            packet.Y = 256;

            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void BlockId_GetInvalidModification_ThrowsInvalidOperationException() {
            var packet = new TileModifyPacket { Modification = TileModification.BreakBlock };

            Assert.Throws<InvalidOperationException>(() => packet.BlockId);
        }

        [Fact]
        public void BlockId_SetInvalidModification_ThrowsInvalidOperationException() {
            var packet = new TileModifyPacket { Modification = TileModification.BreakBlock };

            Assert.Throws<InvalidOperationException>(() => packet.BlockId = BlockId.Torches);
        }

        [Theory]
        [InlineData(TileModification.PlaceBlock)]
        [InlineData(TileModification.ReplaceBlock)]
        public void BlockId_Set_Get(TileModification modification) {
            var packet = new TileModifyPacket { Modification = modification };

            packet.BlockId = BlockId.Torches;

            Assert.Equal(BlockId.Torches, packet.BlockId);
        }

        [Fact]
        public void WallId_GetInvalidModification_ThrowsInvalidOperationException() {
            var packet = new TileModifyPacket { Modification = TileModification.BreakWall };

            Assert.Throws<InvalidOperationException>(() => packet.WallId);
        }

        [Fact]
        public void WallId_SetInvalidModification_ThrowsInvalidOperationException() {
            var packet = new TileModifyPacket { Modification = TileModification.BreakWall };

            Assert.Throws<InvalidOperationException>(() => packet.WallId = WallId.Stone);
        }

        [Theory]
        [InlineData(TileModification.PlaceWall)]
        [InlineData(TileModification.ReplaceWall)]
        public void WallId_Set_Get(TileModification modification) {
            var packet = new TileModifyPacket { Modification = modification };

            packet.WallId = WallId.Stone;

            Assert.Equal(WallId.Stone, packet.WallId);
        }

        [Fact]
        public void Slope_GetInvalidModification_ThrowsInvalidOperationException() {
            var packet = new TileModifyPacket { Modification = TileModification.BreakBlock };

            Assert.Throws<InvalidOperationException>(() => packet.Slope);
        }

        [Fact]
        public void Slope_SetInvalidModification_ThrowsInvalidOperationException() {
            var packet = new TileModifyPacket { Modification = TileModification.BreakBlock };

            Assert.Throws<InvalidOperationException>(() => packet.Slope = Slope.TopRight);
        }

        [Theory]
        [InlineData(TileModification.SlopeBlock)]
        [InlineData(TileModification.SlopeAndHammerBlock)]
        public void Slope_Set_Get(TileModification modification) {
            var packet = new TileModifyPacket { Modification = modification };

            packet.Slope = Slope.TopRight;

            Assert.Equal(Slope.TopRight, packet.Slope);
        }

        [Fact]
        public void IsFailure_GetInvalidModification_ThrowsInvalidOperationException() {
            var packet = new TileModifyPacket { Modification = TileModification.PlaceBlock };

            Assert.Throws<InvalidOperationException>(() => packet.IsFailure);
        }

        [Fact]
        public void IsFailure_SetInvalidModification_ThrowsInvalidOperationException() {
            var packet = new TileModifyPacket { Modification = TileModification.PlaceBlock };

            Assert.Throws<InvalidOperationException>(() => packet.IsFailure = true);
        }

        [Theory]
        [InlineData(TileModification.BreakBlock)]
        [InlineData(TileModification.BreakWall)]
        [InlineData(TileModification.BreakBlockNoItems)]
        [InlineData(TileModification.BreakContainer)]
        public void IsFailure_Set_Get(TileModification modification) {
            var packet = new TileModifyPacket { Modification = modification };

            packet.IsFailure = true;

            Assert.True(packet.IsFailure);
        }

        [Fact]
        public void Read_BreakBlock() {
            var packet = new TileModifyPacket();
            var span = BreakBlockBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.BreakBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.False(packet.IsFailure);
        }

        [Fact]
        public void Read_PlaceBlock() {
            var packet = new TileModifyPacket();
            var span = PlaceBlockBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.PlaceBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(BlockId.Torches, packet.BlockId);
            Assert.Equal(1, packet.BlockStyle);
        }

        [Fact]
        public void Read_BreakWall() {
            var packet = new TileModifyPacket();
            var span = BreakWallBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.BreakWall, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.False(packet.IsFailure);
        }

        [Fact]
        public void Read_PlaceWall() {
            var packet = new TileModifyPacket();
            var span = PlaceWallBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.PlaceWall, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(WallId.Stone, packet.WallId);
        }

        [Fact]
        public void Read_BreakBlockNoItems() {
            var packet = new TileModifyPacket();
            var span = BreakBlockNoItemsBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.BreakBlockNoItems, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.False(packet.IsFailure);
        }

        [Fact]
        public void Read_PlaceRedWire() {
            var packet = new TileModifyPacket();
            var span = PlaceRedWireBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.PlaceRedWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakRedWire() {
            var packet = new TileModifyPacket();
            var span = BreakRedWireBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.BreakRedWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_HammerBlock() {
            var packet = new TileModifyPacket();
            var span = HammerBlockBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.HammerBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_PlaceActuator() {
            var packet = new TileModifyPacket();
            var span = PlaceActuatorBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.PlaceActuator, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakActuator() {
            var packet = new TileModifyPacket();
            var span = BreakActuatorBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.BreakActuator, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_PlaceBlueWire() {
            var packet = new TileModifyPacket();
            var span = PlaceBlueWireBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.PlaceBlueWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakBlueWire() {
            var packet = new TileModifyPacket();
            var span = BreakBlueWireBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.BreakBlueWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_PlaceGreenWire() {
            var packet = new TileModifyPacket();
            var span = PlaceGreenWireBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.PlaceGreenWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakGreenWire() {
            var packet = new TileModifyPacket();
            var span = BreakGreenWireBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.BreakGreenWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_SlopeBlock() {
            var packet = new TileModifyPacket();
            var span = SlopeBlockBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.SlopeBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(Slope.TopRight, packet.Slope);
        }

        [Fact]
        public void Read_ModifyTrack() {
            var packet = new TileModifyPacket();
            var span = ModifyTrackBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.ModifyTrack, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_PlaceYellowWire() {
            var packet = new TileModifyPacket();
            var span = PlaceYellowWireBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.PlaceYellowWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakYellowWire() {
            var packet = new TileModifyPacket();
            var span = BreakYellowWireBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.BreakYellowWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_ModifyLogicGate() {
            var packet = new TileModifyPacket();
            var span = ModifyLogicGateBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.ModifyLogicGate, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_ActuateBlock() {
            var packet = new TileModifyPacket();
            var span = ActuateBlockBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.ActuateBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakContainer() {
            var packet = new TileModifyPacket();
            var span = BreakContainerBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.BreakContainer, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.False(packet.IsFailure);
        }

        [Fact]
        public void Read_ReplaceBlock() {
            var packet = new TileModifyPacket();
            var span = ReplaceBlockBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.ReplaceBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(BlockId.Torches, packet.BlockId);
            Assert.Equal(1, packet.BlockStyle);
        }

        [Fact]
        public void Read_ReplaceWall() {
            var packet = new TileModifyPacket();
            var span = ReplaceWallBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.ReplaceWall, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(WallId.Stone, packet.WallId);
        }

        [Fact]
        public void Read_SlopeAndHammerBlock() {
            var packet = new TileModifyPacket();
            var span = SlopeAndHammerBlockBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(TileModification.SlopeAndHammerBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(Slope.TopRight, packet.Slope);
        }

        [Fact]
        public void RoundTrip_BreakBlock() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                BreakBlockBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceBlock() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                PlaceBlockBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakWall() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                BreakWallBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceWall() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                PlaceWallBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakBlockNoItems() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                BreakBlockNoItemsBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceRedWire() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                PlaceRedWireBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakRedWire() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                BreakRedWireBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_HammerBlock() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                HammerBlockBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceActuator() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                PlaceActuatorBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakActuator() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                BreakActuatorBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceBlueWire() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                PlaceBlueWireBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakBlueWire() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                BreakBlueWireBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceGreenWire() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                PlaceGreenWireBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakGreenWire() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                BreakGreenWireBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_SlopeBlock() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                SlopeBlockBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_ModifyTrack() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                ModifyTrackBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceYellowWire() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                PlaceYellowWireBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakYellowWire() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                BreakYellowWireBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_ModifyLogicGate() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                ModifyLogicGateBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_ActuateBlock() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                ActuateBlockBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakContainer() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                BreakContainerBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_ReplaceBlock() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                ReplaceBlockBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_ReplaceWall() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                ReplaceWallBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_SlopeAndHammerBlock() {
            TestUtils.RoundTripPacket<TileModifyPacket>(
                SlopeAndHammerBlockBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }
    }
}
