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
using System.Linq;
using FluentAssertions;
using Orion.World.TileEntities;
using Terraria;
using Xunit;

namespace Orion.Tests.World.TileEntities {
    [Collection("TerrariaTestsCollection")]
    public class OrionChestServiceTests : IDisposable {
        private readonly IChestService _chestService;

        public OrionChestServiceTests() {
            for (int i = 0; i < Main.maxChests; ++i) {
                Main.chest[i] = null;
            }

            _chestService = new OrionChestService();
        }

        public void Dispose() {
            _chestService.Dispose();
        }

        [Fact]
        public void GetItem_IsCorrect() {
            Main.chest[0] = new Chest();
            var chest = (OrionChest)_chestService[0];

            chest.Wrapped.Should().BeSameAs(Main.chest[0]);
            chest.Index.Should().Be(0);
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
            Main.chest[0] = new Chest();
            var chest = _chestService[0];
            var chest2 = _chestService[0];

            chest.Should().BeSameAs(chest2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void GetItem_InvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            Func<IChest> func = () => _chestService[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void GetItem_ChestIsNull_IsCorrect() {
            var chest = (OrionChest)_chestService[0];

            chest.Should().BeNull();
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            for (var i = 0; i < Main.maxChests; ++i) {
                Main.chest[i] = new Chest();
            }

            var chests = _chestService.ToList();

            for (var i = 0; i < chests.Count; ++i) {
                ((OrionChest)chests[i]).Wrapped.Should().BeSameAs(Main.chest[i]);
            }
        }

        [Fact]
        public void AddChest_IsCorrect() {
            var chest = _chestService.AddChest(100, 100);

            chest.Should().NotBeNull();
            chest.X.Should().Be(100);
            chest.Y.Should().Be(100);
        }

        [Fact]
        public void AddChest_Exists_ReturnsNull() {
            _chestService.AddChest(100, 100);

            var chest = _chestService.AddChest(100, 100);

            chest.Should().BeNull();
        }

        [Fact]
        public void AddChest_TooMany_ReturnsNull() {
            for (var i = 0; i < Main.maxChests; ++i) {
                Main.chest[i] = new Chest();
            }

            var chest = _chestService.AddChest(100, 100);

            chest.Should().BeNull();
        }

        [Fact]
        public void GetChest_IsCorrect() {
            Main.chest[0] = new Chest {
                x = 100,
                y = 100,
                name = "test"
            };

            var chest = _chestService.GetChest(100, 100);

            chest.Should().NotBeNull();
            chest.X.Should().Be(100);
            chest.Y.Should().Be(100);
            chest.Name.Should().Be("test");
        }

        [Fact]
        public void GetChest_NoChest_ReturnsNull() {
            var chest = _chestService.GetChest(100, 100);

            chest.Should().BeNull();
        }

        [Fact]
        public void RemoveChest_IsCorrect() {
            Main.chest[0] = new Chest {
                x = 100,
                y = 100
            };
            var chest = _chestService.GetChest(100, 100);

            var result = _chestService.RemoveChest(chest);

            result.Should().BeTrue();
            Main.chest[0].Should().BeNull();
        }

        [Fact]
        public void RemoveChest_NoChest_ReturnsFalse() {
            Main.chest[0] = new Chest {
                x = 100,
                y = 100
            };
            var chest = _chestService.GetChest(100, 100);
            Main.chest[0] = null;

            var result = _chestService.RemoveChest(chest);

            result.Should().BeFalse();
        }
    }
}
