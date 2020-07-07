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
using Moq;
using Orion.Core.Packets.DataStructures.TileEntities;
using Orion.Core.World;
using Xunit;

namespace Orion.Core.Packets.World
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class SectionInfoTests
    {
        private readonly byte[] _bytes =
        {
            93, 0, 10, 0, 0, 1, 0, 0, 100, 0, 0, 0, 100, 0, 10, 0, 64, 99, 2, 0, 34, 0, 1, 2, 4, 1, 0, 2, 0, 3, 1, 8, 0,
            1, 4, 1, 5, 1, 16, 1, 1, 5, 1, 64, 0, 1, 8, 255, 1, 31, 38, 128, 122, 3, 1, 0, 5, 0, 0, 1, 100, 0, 4, 116,
            101, 115, 116, 1, 0, 5, 0, 0, 1, 100, 0, 4, 116, 101, 115, 116, 1, 0, 0, 10, 0, 0, 0, 0, 1, 100, 0, 5, 0
        };

        private readonly byte[] _compressedBytes =
        {
            247, 2, 10, 1, 124, 85, 61, 111, 19, 65, 16, 157, 221, 59, 127, 112, 54, 137, 109, 9, 139, 15, 19, 34, 147,
            196, 64, 46, 224, 230, 250, 149, 131, 2, 13, 200, 130, 6, 165, 66, 40, 80, 33, 202, 212, 20, 41, 210, 184,
            229, 7, 240, 11, 40, 104, 144, 59, 144, 44, 68, 77, 193, 111, 160, 66, 81, 132, 172, 40, 74, 152, 121, 115,
            107, 159, 207, 49, 133, 103, 38, 115, 59, 239, 189, 153, 157, 187, 184, 34, 209, 7, 34, 250, 206, 246, 253,
            143, 215, 237, 109, 243, 142, 254, 156, 187, 17, 7, 177, 15, 154, 18, 24, 60, 170, 145, 251, 198, 1, 249,
            12, 73, 102, 215, 246, 93, 108, 86, 221, 58, 39, 94, 208, 248, 220, 53, 124, 181, 28, 105, 202, 145, 167,
            189, 190, 113, 5, 62, 103, 249, 183, 197, 103, 111, 0, 46, 36, 87, 2, 10, 23, 21, 57, 56, 144, 162, 40, 75,
            128, 234, 39, 189, 190, 117, 33, 27, 18, 152, 251, 92, 126, 29, 143, 242, 229, 177, 47, 111, 206, 232, 219,
            145, 42, 41, 119, 5, 142, 128, 241, 128, 49, 174, 101, 49, 226, 57, 140, 217, 6, 118, 82, 9, 41, 144, 177,
            125, 178, 195, 113, 68, 108, 163, 138, 216, 22, 91, 76, 161, 130, 206, 34, 66, 237, 1, 21, 201, 149, 33, 36,
            79, 68, 83, 177, 71, 74, 20, 211, 18, 185, 71, 224, 144, 73, 109, 215, 18, 35, 68, 50, 51, 144, 53, 64, 38,
            182, 197, 22, 99, 212, 89, 165, 100, 228, 201, 254, 219, 213, 177, 239, 170, 58, 37, 163, 160, 155, 244,
            146, 203, 193, 106, 66, 96, 189, 199, 208, 151, 112, 40, 15, 77, 23, 12, 253, 200, 67, 255, 245, 67, 143,
            210, 161, 7, 192, 189, 194, 184, 206, 0, 120, 131, 129, 5, 38, 94, 12, 220, 20, 224, 0, 179, 59, 243, 120,
            199, 126, 82, 99, 63, 169, 50, 185, 101, 48, 240, 124, 186, 190, 129, 155, 246, 203, 71, 222, 101, 176, 40,
            84, 121, 17, 75, 236, 89, 154, 158, 165, 41, 242, 149, 229, 196, 103, 184, 170, 132, 205, 149, 59, 183, 18,
            181, 152, 175, 222, 75, 86, 94, 13, 76, 224, 58, 204, 83, 240, 7, 85, 215, 133, 55, 32, 60, 241, 148, 103,
            236, 121, 78, 125, 127, 97, 150, 199, 136, 17, 158, 101, 52, 117, 107, 99, 96, 134, 15, 135, 188, 179, 30,
            87, 153, 194, 57, 166, 19, 101, 34, 207, 132, 76, 52, 203, 109, 83, 166, 162, 24, 116, 20, 137, 89, 130, 1,
            223, 170, 242, 5, 110, 11, 149, 92, 160, 26, 173, 231, 19, 8, 100, 78, 115, 124, 104, 72, 249, 200, 87, 113,
            80, 230, 86, 138, 252, 11, 178, 108, 214, 85, 149, 173, 61, 48, 29, 7, 198, 130, 219, 204, 215, 82, 187, 97,
            83, 215, 80, 183, 38, 143, 67, 127, 78, 251, 62, 203, 233, 64, 160, 111, 70, 138, 21, 167, 58, 200, 62, 127,
            12, 128, 94, 98, 101, 217, 229, 229, 130, 12, 35, 43, 42, 111, 29, 150, 170, 130, 92, 91, 7, 81, 188, 93,
            27, 24, 86, 233, 238, 162, 197, 57, 117, 13, 85, 7, 183, 38, 174, 110, 63, 203, 179, 186, 221, 149, 195,
            214, 87, 169, 86, 14, 102, 182, 14, 18, 163, 44, 50, 130, 2, 20, 92, 197, 139, 195, 170, 170, 246, 25, 116,
            203, 215, 51, 148, 23, 73, 247, 125, 157, 101, 169, 200, 176, 211, 101, 255, 134, 127, 34, 85, 202, 238, 48,
            84, 111, 170, 131, 221, 111, 113, 154, 68, 117, 200, 199, 56, 253, 22, 157, 164, 74, 102, 68, 78, 174, 66,
            245, 219, 225, 30, 190, 116, 251, 176, 135, 108, 179, 178, 181, 10, 192, 43, 206, 76, 135, 106, 38, 92, 248,
            104, 226, 175, 205, 233, 165, 171, 112, 105, 130, 115, 122, 43, 179, 138, 249, 63, 69, 137, 7, 202, 215,
            207, 126, 148, 241, 204, 154, 38, 128, 89, 145, 202, 73, 203, 191, 22, 47, 143, 92, 136, 136, 149, 196, 39,
            238, 154, 221, 87, 117, 63, 197, 229, 54, 112, 17, 134, 94, 50, 207, 97, 15, 31, 228, 125, 216, 67, 182,
            169, 6, 0, 104, 0, 121, 47, 179, 247, 132, 86, 75, 44, 88, 215, 145, 193, 70, 114, 178, 142, 165, 162, 127,
            0, 0, 0, 255, 255, 3
        };

        private readonly byte[] _emptyBytes =
        {
            19, 0, 10, 0, 64, 6, 0, 0, 0, 0, 0, 0, 200, 0, 150, 0, 128, 47, 117, 0, 0, 0, 0, 0, 0
        };

        [Fact]
        public void StartX_Set_Get()
        {
            var packet = new SectionInfo();

            packet.StartX = 1600;

            Assert.Equal(1600, packet.StartX);
        }

        [Fact]
        public void StartY_Set_Get()
        {
            var packet = new SectionInfo();

            packet.StartY = 0;

            Assert.Equal(0, packet.StartY);
        }

        [Fact]
        public void Tiles_SetNullValue_ThrowsArgumentNullException()
        {
            var packet = new SectionInfo();

            Assert.Throws<ArgumentNullException>(() => packet.Tiles = null!);
        }

        [Fact]
        public void Tiles_Set_Get()
        {
            var tiles = Mock.Of<ITileSlice>();
            var packet = new SectionInfo();

            packet.Tiles = tiles;

            Assert.Same(tiles, packet.Tiles);
        }

        [Fact]
        public void Chests_Get()
        {
            var packet = new SectionInfo();

            packet.Chests.Add(new SerializableChest());

            Assert.Single(packet.Chests);
        }

        [Fact]
        public void Signs_Get()
        {
            var packet = new SectionInfo();

            packet.Signs.Add(new SerializableSign());

            Assert.Single(packet.Signs);
        }

        [Fact]
        public void TileEntities_Get()
        {
            var tileEntity = Mock.Of<SerializableTileEntity>();
            var packet = new SectionInfo();

            packet.TileEntities.Add(tileEntity);

            Assert.Single(packet.TileEntities);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<SectionInfo>(_bytes, PacketContext.Server);

            Assert.Equal(256, packet.StartX);
            Assert.Equal(100, packet.StartY);
            Assert.Equal(100, packet.Tiles.Width);
            Assert.Equal(10, packet.Tiles.Height);
            Assert.Single(packet.Chests);
            Assert.Single(packet.Signs);
            Assert.Single(packet.TileEntities);
        }

        [Fact]
        public void Read_Empty()
        {
            var packet = TestUtils.ReadPacket<SectionInfo>(_emptyBytes, PacketContext.Server);

            Assert.Equal(1600, packet.StartX);
            Assert.Equal(0, packet.StartY);
            Assert.Equal(200, packet.Tiles.Width);
            Assert.Equal(150, packet.Tiles.Height);
            Assert.Empty(packet.Chests);
            Assert.Empty(packet.Signs);
            Assert.Empty(packet.TileEntities);
        }

        [Fact]
        public void Read_Compressed()
        {
            var packet = TestUtils.ReadPacket<SectionInfo>(_compressedBytes, PacketContext.Server);

            Assert.Equal(1600, packet.StartX);
            Assert.Equal(150, packet.StartY);
            Assert.Equal(200, packet.Tiles.Width);
            Assert.Equal(150, packet.Tiles.Height);
            Assert.Empty(packet.Chests);
            Assert.Empty(packet.Signs);
            Assert.Empty(packet.TileEntities);
        }
    }
}
