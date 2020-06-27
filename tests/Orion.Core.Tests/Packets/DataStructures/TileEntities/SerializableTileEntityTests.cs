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
using Orion.Core.World.TileEntities;
using Xunit;

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class SerializableTileEntityTests
    {
        [Fact]
        public void Index_Set_Get()
        {
            var tileEntity = new TestTileEntity();

            tileEntity.Index = 10;

            Assert.Equal(10, tileEntity.Index);
        }

        [Fact]
        public void X_Set_Get()
        {
            var tileEntity = new TestTileEntity();

            tileEntity.X = 256;

            Assert.Equal(256, tileEntity.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var tileEntity = new TestTileEntity();

            tileEntity.Y = 100;

            Assert.Equal(100, tileEntity.Y);
        }

        [Fact]
        public void Write_WithIndex()
        {
            var tileEntity = new TestTileEntity { Index = 10, X = 256, Y = 100, Value = 42 };
            var bytes = new byte[1000];

            var length = tileEntity.Write(bytes, true);

            Assert.Equal(new byte[] { 255, 10, 0, 0, 0, 0, 1, 100, 0, 42 }, bytes[..length]);
        }

        [Fact]
        public void Write_WithoutIndex()
        {
            var tileEntity = new TestTileEntity { Index = 10, X = 256, Y = 100, Value = 42 };
            var bytes = new byte[1000];

            var length = tileEntity.Write(bytes, false);

            Assert.Equal(new byte[] { 255, 0, 1, 100, 0, 42 }, bytes[..length]);
        }

        [Fact]
        public void Read_WithIndex()
        {
            var bytes = new byte[] { 255, 10, 0, 0, 0, 0, 1, 100, 0, 42 };

            var length = SerializableTileEntity.Read(bytes, true, out var tileEntity);
            Assert.Equal(bytes.Length, length);
            Assert.IsType<UnknownTileEntity>(tileEntity);

            Assert.Equal((TileEntityId)255, tileEntity.Id);
            Assert.Equal(10, tileEntity.Index);
            Assert.Equal(256, tileEntity.X);
            Assert.Equal(100, tileEntity.Y);
            Assert.Equal(1, ((UnknownTileEntity)tileEntity).Data.Length);
            Assert.Equal(42, ((UnknownTileEntity)tileEntity).Data[0]);
        }

        [Fact]
        public void Read_WithoutIndex()
        {
            var bytes = new byte[] { 255, 0, 1, 100, 0, 42 };

            var length = SerializableTileEntity.Read(bytes, false, out var tileEntity);
            Assert.Equal(bytes.Length, length);
            Assert.IsType<UnknownTileEntity>(tileEntity);

            Assert.Equal((TileEntityId)255, tileEntity.Id);
            Assert.Equal(256, tileEntity.X);
            Assert.Equal(100, tileEntity.Y);
            Assert.Equal(1, ((UnknownTileEntity)tileEntity).Data.Length);
            Assert.Equal(42, ((UnknownTileEntity)tileEntity).Data[0]);
        }

        private sealed class TestTileEntity : SerializableTileEntity
        {
            public override TileEntityId Id => (TileEntityId)255;

            public byte Value { get; set; }

            protected override int ReadBody(Span<byte> span)
            {
                Value = span[0];
                return 1;
            }

            protected override int WriteBody(Span<byte> span)
            {
                span[0] = Value;
                return 1;
            }
        }
    }
}
