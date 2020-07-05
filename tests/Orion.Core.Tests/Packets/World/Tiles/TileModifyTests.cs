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
using Orion.Core.World.Tiles;
using Xunit;

namespace Orion.Core.Packets.World.Tiles
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class TileModifyTests
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
        private readonly byte[] _breakBlockRequestBytes = { 11, 0, 17, 20, 100, 0, 0, 1, 0, 0, 0 };
        private readonly byte[] _replaceBlockBytes = { 11, 0, 17, 21, 100, 0, 0, 1, 1, 0, 0 };
        private readonly byte[] _replaceWallBytes = { 11, 0, 17, 22, 100, 0, 0, 1, 1, 0, 0 };
        private readonly byte[] _slopeAndHammerBlockBytes = { 11, 0, 17, 23, 100, 0, 0, 1, 1, 0, 0 };

        [Fact]
        public void Modification_Set_Get()
        {
            var packet = new TileModify();

            packet.Modification = TileModify.TileModification.BreakBlock;

            Assert.Equal(TileModify.TileModification.BreakBlock, packet.Modification);
        }

        [Fact]
        public void X_Set_Get()
        {
            var packet = new TileModify();

            packet.X = 100;

            Assert.Equal(100, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new TileModify();

            packet.Y = 256;

            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Data_Set_Get()
        {
            var packet = new TileModify();

            packet.Data = 12345;

            Assert.Equal(12345, packet.Data);
        }

        [Fact]
        public void Data2_Set_Get()
        {
            var packet = new TileModify();

            packet.Data2 = 123;

            Assert.Equal(123, packet.Data2);
        }

        [Fact]
        public void Read_BreakBlock()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_breakBlockBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.BreakBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(0, packet.Data);
        }

        [Fact]
        public void Read_BreakBlockFailure()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_breakBlockFailureBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.BreakBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(1, packet.Data);
        }

        [Fact]
        public void Read_PlaceBlock()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_placeBlockBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.PlaceBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(BlockId.Torches, (BlockId)packet.Data);
            Assert.Equal(1, packet.Data2);
        }

        [Fact]
        public void Read_BreakWall()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_breakWallBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.BreakWall, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(0, packet.Data);
        }

        [Fact]
        public void Read_BreakWallFailure()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_breakWallFailureBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.BreakWall, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(1, packet.Data);
        }

        [Fact]
        public void Read_PlaceWall()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_placeWallBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.PlaceWall, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(WallId.Stone, (WallId)packet.Data);
        }

        [Fact]
        public void Read_BreakBlockItemless()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_breakBlockItemlessBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.BreakBlockItemless, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(0, packet.Data);
        }

        [Fact]
        public void Read_BreakBlockItemlessFailure()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_breakBlockItemlessFailureBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.BreakBlockItemless, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(1, packet.Data);
        }

        [Fact]
        public void Read_PlaceRedWire()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_placeRedWireBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.PlaceRedWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakRedWire()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_breakRedWireBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.BreakRedWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_HammerBlock()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_hammerBlockBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.HammerBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_PlaceActuator()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_placeActuatorBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.PlaceActuator, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakActuator()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_breakActuatorBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.BreakActuator, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_PlaceBlueWire()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_placeBlueWireBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.PlaceBlueWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakBlueWire()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_breakBlueWireBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.BreakBlueWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_PlaceGreenWire()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_placeGreenWireBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.PlaceGreenWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakGreenWire()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_breakGreenWireBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.BreakGreenWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_SlopeBlock()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_slopeBlockBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.SlopeBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(BlockShape.TopRight - 1, (BlockShape)packet.Data);
        }

        [Fact]
        public void Read_ModifyTrack()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_modifyTrackBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.ModifyTrack, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_PlaceYellowWire()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_placeYellowWireBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.PlaceYellowWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakYellowWire()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_breakYellowWireBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.BreakYellowWire, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_ModifyLogicGate()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_modifyLogicGateBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.ModifyLogicGate, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_ActuateBlock()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_actuateBlockBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.ActuateBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
        }

        [Fact]
        public void Read_BreakBlockRequest()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_breakBlockRequestBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.BreakBlockRequest, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(0, packet.Data);
        }

        [Fact]
        public void Read_ReplaceBlock()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_replaceBlockBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.ReplaceBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(BlockId.Stone, (BlockId)packet.Data);
            Assert.Equal(0, packet.Data2);
        }

        [Fact]
        public void Read_ReplaceWall()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_replaceWallBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.ReplaceWall, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(WallId.Stone, (WallId)packet.Data);
        }

        [Fact]
        public void Read_SlopeAndHammerBlock()
        {
            var packet = TestUtils.ReadPacket<TileModify>(_slopeAndHammerBlockBytes, PacketContext.Server);

            Assert.Equal(TileModify.TileModification.SlopeAndHammerBlock, packet.Modification);
            Assert.Equal(100, packet.X);
            Assert.Equal(256, packet.Y);
            Assert.Equal(BlockShape.TopRight - 1, (BlockShape)packet.Data);
        }
    }
}
