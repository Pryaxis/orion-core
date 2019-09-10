using System;
using System.Linq;
using FluentAssertions;
using Orion.World.TileEntities;
using Xunit;

namespace Orion.Tests.World.TileEntities {
    [Collection("TerrariaTestsCollection")]
    public class OrionChestServiceTests : IDisposable {
        private readonly IChestService _chestService;

        public OrionChestServiceTests() {
            for (int i = 0; i < Terraria.Main.maxChests; ++i) {
                Terraria.Main.chest[i] = null;
            }

            _chestService = new OrionChestService();
        }

        public void Dispose() {
            _chestService.Dispose();
        }

        [Fact]
        public void GetItem_IsCorrect() {
            Terraria.Main.chest[0] = new Terraria.Chest();
            var chest = (OrionChest)_chestService[0];

            chest.Wrapped.Should().BeSameAs(Terraria.Main.chest[0]);
            chest.Index.Should().Be(0);
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
            Terraria.Main.chest[0] = new Terraria.Chest();
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
            for (var i = 0; i < Terraria.Main.maxChests; ++i) {
                Terraria.Main.chest[i] = new Terraria.Chest();
            }

            var chests = _chestService.ToList();

            for (var i = 0; i < chests.Count; ++i) {
                ((OrionChest)chests[i]).Wrapped.Should().BeSameAs(Terraria.Main.chest[i]);
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
            for (var i = 0; i < Terraria.Main.maxChests; ++i) {
                Terraria.Main.chest[i] = new Terraria.Chest();
            }

            var chest = _chestService.AddChest(100, 100);

            chest.Should().BeNull();
        }

        [Fact]
        public void GetChest_IsCorrect() {
            Terraria.Main.chest[0] = new Terraria.Chest {
                x = 100,
                y = 100,
                name = "test",
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
            Terraria.Main.chest[0] = new Terraria.Chest {
                x = 100,
                y = 100,
            };
            var chest = _chestService.GetChest(100, 100);

            var result = _chestService.RemoveChest(chest);

            result.Should().BeTrue();
            Terraria.Main.chest[0].Should().BeNull();
        }

        [Fact]
        public void RemoveChest_NoChest_ReturnsFalse() {
            Terraria.Main.chest[0] = new Terraria.Chest {
                x = 100,
                y = 100,
            };
            var chest = _chestService.GetChest(100, 100);
            Terraria.Main.chest[0] = null;

            var result = _chestService.RemoveChest(chest);

            result.Should().BeFalse();
        }
    }
}
