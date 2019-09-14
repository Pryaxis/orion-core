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
using Xunit;

namespace Orion.Entities {
    [Collection("TerrariaTestsCollection")]
    public class ItemTypeTests {
        [Fact]
        public void GetId_IsCorrect() {
            var itemType = new ItemType(100);

            itemType.Id.Should().Be(100);
        }

        [Fact]
        public void GetIsAccessory_IsCorrect() {
            TestForEachItemType((itemType, item) => itemType.IsAccessory.Should().Be(item.accessory));
        }

        [Fact]
        public void GetIsMeleeWeapon_IsCorrect() {
            TestForEachItemType((itemType, item) => itemType.IsMeleeWeapon.Should().Be(item.melee));
        }

        [Fact]
        public void GetIsRangedWeapon_IsCorrect() {
            TestForEachItemType((itemType, item) => itemType.IsRangedWeapon.Should().Be(item.ranged && item.ammo == 0));
        }

        [Fact]
        public void GetIsMagicWeapon_IsCorrect() {
            TestForEachItemType((itemType, item) => itemType.IsMagicWeapon.Should().Be(item.magic));
        }

        [Fact]
        public void GetIsSummonWeapon_IsCorrect() {
            TestForEachItemType((itemType, item) => itemType.IsSummonWeapon.Should().Be(item.summon));
        }

        [Fact]
        public void GetIsThrownWeapon_IsCorrect() {
            TestForEachItemType((itemType, item) => itemType.IsThrownWeapon.Should().Be(item.thrown));
        }

        [Fact]
        public void Equals_IsCorrect() {
            var itemType = new ItemType(100);
            var itemType2 = new ItemType(100);
            var itemType3 = new ItemType(101);

            itemType.Equals(itemType2).Should().BeTrue();
            itemType.Equals(itemType3).Should().BeFalse();
        }

        [Fact]
        public void EqualsObject_IsCorrect() {
            var itemType = new ItemType(100);
            var itemType2 = new ItemType(100);

            itemType.Equals((object)itemType2).Should().BeTrue();

            // ReSharper disable once SuspiciousTypeConversion.Global
            itemType.Equals("null").Should().BeFalse();
            itemType.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void EqualsOperator_IsCorrect() {
            var itemType = new ItemType(100);
            var itemType2 = new ItemType(100);
            var itemType3 = new ItemType(101);

            (itemType == itemType2).Should().BeTrue();
            (itemType == itemType3).Should().BeFalse();
        }

        [Fact]
        public void NotEqualsOperator_IsCorrect() {
            var itemType = new ItemType(100);
            var itemType2 = new ItemType(100);
            var itemType3 = new ItemType(101);

            (itemType != itemType2).Should().BeFalse();
            (itemType != itemType3).Should().BeTrue();
        }

        private void TestForEachItemType(Action<ItemType, Terraria.Item> action) {
            for (var i = 0; i < Terraria.Main.maxItemTypes; ++i) {
                var item = new Terraria.Item();
                item.SetDefaults(i);
                if (Terraria.ID.ItemID.Sets.Deprecated[i]) continue;

                var itemType = new ItemType(i);

                action(itemType, item);
            }
        }
    }
}
