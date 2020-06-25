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
using Orion.Core.World.Tiles;
using Xunit;

namespace Orion.Core.Packets.World.Tiles
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class TileModifyPacketTests
    {
        private readonly byte[] _breakBlockBytes = { 11, 0, 17, 0, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _breakBlockFailureBytes = { 11, 0, 17, 0, 100, 0, 0, 1, 1, 0, 0 };
        private readonly byte[] _placeBlockBytes = { 11, 0, 17, 1, 100, 0, 0, 1, 4, 0, 1 };
        private readonly byte[] _breakWallBytes = { 11, 0, 17, 2, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _breakWallFailureBytes = { 11, 0, 17, 2, 100, 0, 0, 1, 1, 0, 0 };
        private readonly byte[] _placeWallBytes = { 11, 0, 17, 3, 100, 0, 0, 1, 1, 0, 0 };
        private readonly byte[] _breakBlockItemlessBytes = { 11, 0, 17, 4, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _breakBlockItemlessFailureBytes = { 11, 0, 17, 4, 100, 0, 0, 1, 1, 0, 0 };
        private readonly byte[] _placeRedWireBytes = { 11, 0, 17, 5, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _breakRedWireBytes = { 11, 0, 17, 6, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _hammerBlockBytes = { 11, 0, 17, 7, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _placeActuatorBytes = { 11, 0, 17, 8, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _breakActuatorBytes = { 11, 0, 17, 9, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _placeBlueWireBytes = { 11, 0, 17, 10, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _breakBlueWireBytes = { 11, 0, 17, 11, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _placeGreenWireBytes = { 11, 0, 17, 12, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _breakGreenWireBytes = { 11, 0, 17, 13, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _slopeBlockBytes = { 11, 0, 17, 14, 100, 0, 0, 1, 1, 0, 0 };
        private readonly byte[] _modifyTrackBytes = { 11, 0, 17, 15, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _placeYellowWireBytes = { 11, 0, 17, 16, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _breakYellowWireBytes = { 11, 0, 17, 17, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _modifyLogicGateBytes = { 11, 0, 17, 18, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _actuateBlockBytes = { 11, 0, 17, 19, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _breakContainerBytes = { 11, 0, 17, 20, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _replaceBlockBytes = { 11, 0, 17, 21, 100, 0, 0, 1, 1, 0, 0 };
        private readonly byte[] _replaceWallBytes = { 11, 0, 17, 22, 100, 0, 0, 1, 1, 0, 0 };
        private readonly byte[] _slopeAndHammerBlockBytes = { 11, 0, 17, 23, 100, 0, 0, 1, 1, 0, 0 };

        [Fact]
        public void Modification_Set_Get()
        {
            var packet = new TileModifyPacket();

            packet.Modification = TileModification.BreakBlock;

            Assert.Equal(TileModification.BreakBlock, packet.Modification);
        }

        [Fact]
        public void X_Set_Get()
        {
            var packet = new TileModifyPacket();

            packet.X = 100;

            Assert.Equal(100, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new TileModifyPacket();

            packet.Y = 256;

            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void BlockId_GetInvalidModification_ThrowsInvalidOperationException()
        {
            var packet = new TileModifyPacket { Modification = TileModification.BreakBlock };

            Assert.Throws<InvalidOperationException>(() => packet.BlockId);
        }

        [Fact]
        public void BlockId_SetInvalidModification_ThrowsInvalidOperationException()
        {
            var packet = new TileModifyPacket { Modification = TileModification.BreakBlock };

            Assert.Throws<InvalidOperationException>(() => packet.BlockId = BlockId.Torches);
        }

        [Theory]
        [InlineData(TileModification.PlaceBlock)]
        [InlineData(TileModification.ReplaceBlock)]
        public void BlockId_Set_Get(TileModification modification)
        {
            var packet = new TileModifyPacket { Modification = modification };

            packet.BlockId = BlockId.Torches;

            Assert.Equal(BlockId.Torches, packet.BlockId);
        }

        [Fact]
        public void BlockStyle_GetInvalidModification_ThrowsInvalidOperationException()
        {
            var packet = new TileModifyPacket { Modification = TileModification.BreakBlock };

            Assert.Throws<InvalidOperationException>(() => packet.BlockStyle);
        }

        [Fact]
        public void BlockStyle_SetInvalidModification_ThrowsInvalidOperationException()
        {
            var packet = new TileModifyPacket { Modification = TileModification.BreakBlock };

            Assert.Throws<InvalidOperationException>(() => packet.BlockStyle = 1);
        }

        [Theory]
        [InlineData(TileModification.PlaceBlock)]
        [InlineData(TileModification.ReplaceBlock)]
        public void BlockStyle_Set_Get(TileModification modification)
        {
            var packet = new TileModifyPacket { Modification = modification };

            packet.BlockStyle = 1;

            Assert.Equal(1, packet.BlockStyle);
        }

        [Fact]
        public void WallId_GetInvalidModification_ThrowsInvalidOperationException()
        {
            var packet = new TileModifyPacket { Modification = TileModification.BreakWall };

            Assert.Throws<InvalidOperationException>(() => packet.WallId);
        }

        [Fact]
        public void WallId_SetInvalidModification_ThrowsInvalidOperationException()
        {
            var packet = new TileModifyPacket { Modification = TileModification.BreakWall };

            Assert.Throws<InvalidOperationException>(() => packet.WallId = WallId.Stone);
        }

        [Theory]
        [InlineData(TileModification.PlaceWall)]
        [InlineData(TileModification.ReplaceWall)]
        public void WallId_Set_Get(TileModification modification)
        {
            var packet = new TileModifyPacket { Modification = modification };

            packet.WallId = WallId.Stone;

            Assert.Equal(WallId.Stone, packet.WallId);
        }

        [Fact]
        public void Slope_GetInvalidModification_ThrowsInvalidOperationException()
        {
            var packet = new TileModifyPacket { Modification = TileModification.BreakBlock };

            Assert.Throws<InvalidOperationException>(() => packet.Slope);
        }

        [Fact]
        public void Slope_SetInvalidModification_ThrowsInvalidOperationException()
        {
            var packet = new TileModifyPacket { Modification = TileModification.BreakBlock };

            Assert.Throws<InvalidOperationException>(() => packet.Slope = Slope.TopRight);
        }

        [Theory]
        [InlineData(TileModification.SlopeBlock)]
        [InlineData(TileModification.SlopeAndHammerBlock)]
        public void Slope_Set_Get(TileModification modification)
        {
            var packet = new TileModifyPacket { Modification = modification };

            packet.Slope = Slope.TopRight;

            Assert.Equal(Slope.TopRight, packet.Slope);
        }

        [Fact]
        public void IsFailure_GetInvalidModification_ThrowsInvalidOperationException()
        {
            var packet = new TileModifyPacket { Modification = TileModification.PlaceBlock };

            Assert.Throws<InvalidOperationException>(() => packet.IsFailure);
        }

        [Fact]
        public void IsFailure_SetInvalidModification_ThrowsInvalidOperationException()
        {
            var packet = new TileModifyPacket { Modification = TileModification.PlaceBlock };

            Assert.Throws<InvalidOperationException>(() => packet.IsFailure = true);
        }

        [Theory]
        [InlineData(TileModification.BreakBlock)]
        [InlineData(TileModification.BreakWall)]
        [InlineData(TileModification.BreakBlockItemless)]
        [InlineData(TileModification.BreakContainer)]
        public void IsFailure_Set_Get(TileModification modification)
        {
            var packet = new TileModifyPacket { Modification = modification };

            packet.IsFailure = true;

            Assert.True(packet.IsFailure);

            packet.IsFailure = false;

            Assert.False(packet.IsFailure);
        }

        [Fact]
        public void Read_BreakBlock()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_breakBlockBytes, PacketContext.Server);

            Assert.Equal(TileModification.BreakBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.False(packet.IsFailure);
        }

        [Fact]
        public void Read_BreakBlockFailure()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_breakBlockFailureBytes, PacketContext.Server);

            Assert.Equal(TileModification.BreakBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.True(packet.IsFailure);
        }

        [Fact]
        public void Read_PlaceBlock()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_placeBlockBytes, PacketContext.Server);

            Assert.Equal(TileModification.PlaceBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(BlockId.Torches, packet.BlockId);
            Assert.Equal(1, packet.BlockStyle);
        }

        [Fact]
        public void Read_BreakWall()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_breakWallBytes, PacketContext.Server);

            Assert.Equal(TileModification.BreakWall, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.False(packet.IsFailure);
        }

        [Fact]
        public void Read_BreakWallFailure()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_breakWallFailureBytes, PacketContext.Server);

            Assert.Equal(TileModification.BreakWall, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.True(packet.IsFailure);
        }

        [Fact]
        public void Read_PlaceWall()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_placeWallBytes, PacketContext.Server);

            Assert.Equal(TileModification.PlaceWall, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(WallId.Stone, packet.WallId);
        }

        [Fact]
        public void Read_BreakBlockItemless()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_breakBlockItemlessBytes, PacketContext.Server);

            Assert.Equal(TileModification.BreakBlockItemless, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.False(packet.IsFailure);
        }

        [Fact]
        public void Read_BreakBlockItemlessFailure()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_breakBlockItemlessFailureBytes, PacketContext.Server);

            Assert.Equal(TileModification.BreakBlockItemless, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.True(packet.IsFailure);
        }

        [Fact]
        public void Read_PlaceRedWire()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_placeRedWireBytes, PacketContext.Server);

            Assert.Equal(TileModification.PlaceRedWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakRedWire()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_breakRedWireBytes, PacketContext.Server);

            Assert.Equal(TileModification.BreakRedWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_HammerBlock()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_hammerBlockBytes, PacketContext.Server);

            Assert.Equal(TileModification.HammerBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_PlaceActuator()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_placeActuatorBytes, PacketContext.Server);

            Assert.Equal(TileModification.PlaceActuator, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakActuator()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_breakActuatorBytes, PacketContext.Server);

            Assert.Equal(TileModification.BreakActuator, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_PlaceBlueWire()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_placeBlueWireBytes, PacketContext.Server);

            Assert.Equal(TileModification.PlaceBlueWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakBlueWire()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_breakBlueWireBytes, PacketContext.Server);

            Assert.Equal(TileModification.BreakBlueWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_PlaceGreenWire()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_placeGreenWireBytes, PacketContext.Server);

            Assert.Equal(TileModification.PlaceGreenWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakGreenWire()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_breakGreenWireBytes, PacketContext.Server);

            Assert.Equal(TileModification.BreakGreenWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_SlopeBlock()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_slopeBlockBytes, PacketContext.Server);

            Assert.Equal(TileModification.SlopeBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(Slope.TopRight, packet.Slope);
        }

        [Fact]
        public void Read_ModifyTrack()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_modifyTrackBytes, PacketContext.Server);

            Assert.Equal(TileModification.ModifyTrack, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_PlaceYellowWire()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_placeYellowWireBytes, PacketContext.Server);

            Assert.Equal(TileModification.PlaceYellowWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakYellowWire()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_breakYellowWireBytes, PacketContext.Server);

            Assert.Equal(TileModification.BreakYellowWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_ModifyLogicGate()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_modifyLogicGateBytes, PacketContext.Server);

            Assert.Equal(TileModification.ModifyLogicGate, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_ActuateBlock()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_actuateBlockBytes, PacketContext.Server);

            Assert.Equal(TileModification.ActuateBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakContainer()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_breakContainerBytes, PacketContext.Server);

            Assert.Equal(TileModification.BreakContainer, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.False(packet.IsFailure);
        }

        [Fact]
        public void Read_ReplaceBlock()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_replaceBlockBytes, PacketContext.Server);

            Assert.Equal(TileModification.ReplaceBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(BlockId.Stone, packet.BlockId);
            Assert.Equal(0, packet.BlockStyle);
        }

        [Fact]
        public void Read_ReplaceWall()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_replaceWallBytes, PacketContext.Server);

            Assert.Equal(TileModification.ReplaceWall, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(WallId.Stone, packet.WallId);
        }

        [Fact]
        public void Read_SlopeAndHammerBlock()
        {
            var packet = TestUtils.ReadPacket<TileModifyPacket>(_slopeAndHammerBlockBytes, PacketContext.Server);

            Assert.Equal(TileModification.SlopeAndHammerBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(Slope.TopRight, packet.Slope);
        }

        [Fact]
        public void RoundTrip_BreakBlock()
        {
            TestUtils.RoundTripPacket(
                _breakBlockBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakBlockFailure()
        {
            TestUtils.RoundTripPacket(
                _breakBlockFailureBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceBlock()
        {
            TestUtils.RoundTripPacket(
                _placeBlockBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakWall()
        {
            TestUtils.RoundTripPacket(
                _breakWallBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakWallFailure()
        {
            TestUtils.RoundTripPacket(
                _breakWallFailureBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceWall()
        {
            TestUtils.RoundTripPacket(
                _placeWallBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakBlockItemless()
        {
            TestUtils.RoundTripPacket(
                _breakBlockItemlessBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakBlockItemlessFailure()
        {
            TestUtils.RoundTripPacket(
                _breakBlockItemlessFailureBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceRedWire()
        {
            TestUtils.RoundTripPacket(
                _placeRedWireBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakRedWire()
        {
            TestUtils.RoundTripPacket(
                _breakRedWireBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_HammerBlock()
        {
            TestUtils.RoundTripPacket(
                _hammerBlockBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceActuator()
        {
            TestUtils.RoundTripPacket(
                _placeActuatorBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakActuator()
        {
            TestUtils.RoundTripPacket(
                _breakActuatorBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceBlueWire()
        {
            TestUtils.RoundTripPacket(
                _placeBlueWireBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakBlueWire()
        {
            TestUtils.RoundTripPacket(
                _breakBlueWireBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceGreenWire()
        {
            TestUtils.RoundTripPacket(
                _placeGreenWireBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakGreenWire()
        {
            TestUtils.RoundTripPacket(
                _breakGreenWireBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_SlopeBlock()
        {
            TestUtils.RoundTripPacket(
                _slopeBlockBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_ModifyTrack()
        {
            TestUtils.RoundTripPacket(
                _modifyTrackBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_PlaceYellowWire()
        {
            TestUtils.RoundTripPacket(
                _placeYellowWireBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakYellowWire()
        {
            TestUtils.RoundTripPacket(
                _breakYellowWireBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_ModifyLogicGate()
        {
            TestUtils.RoundTripPacket(
                _modifyLogicGateBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_ActuateBlock()
        {
            TestUtils.RoundTripPacket(
                _actuateBlockBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_BreakContainer()
        {
            TestUtils.RoundTripPacket(
                _breakContainerBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_ReplaceBlock()
        {
            TestUtils.RoundTripPacket(
                _replaceBlockBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_ReplaceWall()
        {
            TestUtils.RoundTripPacket(
                _replaceWallBytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_SlopeAndHammerBlock()
        {
            TestUtils.RoundTripPacket(
                _slopeAndHammerBlockBytes, PacketContext.Server);
        }
    }
}
