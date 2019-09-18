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
using FluentAssertions;
using Orion.Entities;
using Xunit;

namespace Orion.Networking.TileEntities {
    public class NetworkItemFrameTests {
        [Fact]
        public void SetItemType_MarksAsDirty() {
            var itemFrame = new NetworkItemFrame();
            itemFrame.ItemType = ItemType.Sdmg;

            itemFrame.IsDirty.Should().BeTrue();
            itemFrame.Clean();
            itemFrame.IsDirty.Should().BeFalse();
        }

        [Fact]
        public void SetItemType_NullValue_ThrowsArgumentNullException() {
            var itemFrame = new NetworkItemFrame();
            Action action = () => itemFrame.ItemType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetItemStackSize_MarksAsDirty() {
            var itemFrame = new NetworkItemFrame();
            itemFrame.ItemStackSize = 0;

            itemFrame.IsDirty.Should().BeTrue();
            itemFrame.Clean();
            itemFrame.IsDirty.Should().BeFalse();
        }

        [Fact]
        public void SetItemPrefix_MarksAsDirty() {
            var itemFrame = new NetworkItemFrame();
            itemFrame.ItemPrefix = ItemPrefix.Unreal;

            itemFrame.IsDirty.Should().BeTrue();
            itemFrame.Clean();
            itemFrame.IsDirty.Should().BeFalse();
        }

        [Fact]
        public void SetItemPrefix_NullValue_ThrowsArgumentNullException() {
            var itemFrame = new NetworkItemFrame();
            Action action = () => itemFrame.ItemPrefix = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
