// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using Orion.World.TileEntities;
using Xunit;

namespace Orion.Networking.TileEntities {
    public class NetworkTileEntityTests {
        [Fact]
        public void SetIndex_MarksDirty() {
            var tileEntity = new TestTileEntity();
            tileEntity.Index = 0;

            tileEntity.IsDirty.Should().BeTrue();
            tileEntity.Clean();
            tileEntity.IsDirty.Should().BeFalse();
        }

        [Fact]
        public void SetX_MarksDirty() {
            var tileEntity = new TestTileEntity();
            tileEntity.X = 0;

            tileEntity.IsDirty.Should().BeTrue();
            tileEntity.Clean();
            tileEntity.IsDirty.Should().BeFalse();
        }

        [Fact]
        public void SetY_MarksDirty() {
            var tileEntity = new TestTileEntity();
            tileEntity.Y = 0;

            tileEntity.IsDirty.Should().BeTrue();
            tileEntity.Clean();
            tileEntity.IsDirty.Should().BeFalse();
        }

        public sealed class TestTileEntity : NetworkTileEntity {
            public override TileEntityType Type => throw new NotImplementedException();

            private protected override void ReadFromReaderImpl(BinaryReader reader) {
                throw new NotImplementedException();
            }

            private protected override void WriteToWriterImpl(BinaryWriter writer) {
                throw new NotImplementedException();
            }
        }
    }
}
