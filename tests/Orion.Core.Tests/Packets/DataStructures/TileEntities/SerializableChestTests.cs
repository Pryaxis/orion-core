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

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class SerializableChestTests
    {
        private readonly byte[] _bytes = { 5, 0, 0, 1, 100, 0, 4, 116, 101, 115, 116 };

        [Fact]
        public void Index_Set_Get()
        {
            var chest = new SerializableChest();

            chest.Index = 5;

            Assert.Equal(5, chest.Index);
        }

        [Fact]
        public void X_Set_Get()
        {
            var chest = new SerializableChest();

            chest.X = 256;

            Assert.Equal(256, chest.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var chest = new SerializableChest();

            chest.Y = 100;

            Assert.Equal(100, chest.Y);
        }

        [Fact]
        public void Name_Set_Get()
        {
            var chest = new SerializableChest();

            chest.Name = "test";

            Assert.Equal("test", chest.Name);
        }

        [Fact]
        public void Read()
        {
            var length = SerializableChest.Read(_bytes, out var chest);
            Assert.Equal(_bytes.Length, length);

            Assert.Equal(5, chest.Index);
            Assert.Equal(256, chest.X);
            Assert.Equal(100, chest.Y);
            Assert.Equal("test", chest.Name);

            var bytes2 = new byte[1000];
            var length2 = chest.Write(bytes2);

            Assert.Equal(length, length2);
            Assert.Equal(_bytes, bytes2[..length2]);
        }
    }
}
