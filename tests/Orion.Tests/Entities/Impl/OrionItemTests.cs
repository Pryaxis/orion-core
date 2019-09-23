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
using Microsoft.Xna.Framework;
using Xunit;

namespace Orion.Entities.Impl {
    public class OrionItemTests {
        [Fact]
        public void GetIndex_IsCorrect() {
            var terrariaItem = new Terraria.Item {whoAmI = 100};
            var item = new OrionItem(terrariaItem);

            item.Index.Should().Be(100);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsActive_IsCorrect(bool isActive) {
            var terrariaItem = new Terraria.Item {active = isActive};
            var item = new OrionItem(terrariaItem);

            item.IsActive.Should().Be(isActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsActive_IsCorrect(bool isActive) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.IsActive = isActive;

            terrariaItem.active.Should().Be(isActive);
        }

        [Fact]
        public void GetName_IsCorrect() {
            var terrariaItem = new Terraria.Item {_nameOverride = "test"};
            var item = new OrionItem(terrariaItem);

            item.Name.Should().Be("test");
        }

        [Fact]
        public void SetName_IsCorrect() {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Name = "test";

            terrariaItem.Name.Should().Be("test");
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);
            Action action = () => item.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetPosition_IsCorrect() {
            var terrariaItem = new Terraria.Item {position = new Vector2(100, 100)};
            var item = new OrionItem(terrariaItem);

            item.Position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetPosition_IsCorrect() {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Position = new Vector2(100, 100);

            terrariaItem.position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetVelocity_IsCorrect() {
            var terrariaItem = new Terraria.Item {velocity = new Vector2(100, 100)};
            var item = new OrionItem(terrariaItem);

            item.Velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetVelocity_IsCorrect() {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Velocity = new Vector2(100, 100);

            terrariaItem.velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetSize_IsCorrect() {
            var terrariaItem = new Terraria.Item {Size = new Vector2(100, 100)};
            var item = new OrionItem(terrariaItem);

            item.Size.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetSize_IsCorrect() {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Size = new Vector2(100, 100);

            terrariaItem.Size.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetType_IsCorrect() {
            var terrariaItem = new Terraria.Item {type = (int)ItemType.Sdmg};
            var item = new OrionItem(terrariaItem);

            item.Type.Should().Be(ItemType.Sdmg);
        }

        [Fact]
        public void GetStackSize_IsCorrect() {
            var terrariaItem = new Terraria.Item {stack = 100};
            var item = new OrionItem(terrariaItem);

            item.StackSize.Should().Be(100);
        }

        [Fact]
        public void SetStackSize_IsCorrect() {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.StackSize = 100;

            terrariaItem.stack.Should().Be(100);
        }

        [Fact]
        public void GetPrefix_IsCorrect() {
            var terrariaItem = new Terraria.Item {prefix = (byte)ItemPrefix.Unreal};
            var item = new OrionItem(terrariaItem);

            item.Prefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void SetType_IsCorrect() {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.SetType(ItemType.Sdmg);

            terrariaItem.type.Should().Be((int)ItemType.Sdmg);
        }

        [Fact]
        public void SetPrefix_IsCorrect() {
            var terrariaItem = new Terraria.Item();
            terrariaItem.SetDefaults((int)ItemType.Sdmg);
            var item = new OrionItem(terrariaItem);

            item.SetPrefix(ItemPrefix.Unreal);

            terrariaItem.prefix.Should().Be((byte)ItemPrefix.Unreal);
        }
    }
}
