using System;
using System.Linq;
using FluentAssertions;
using Orion.World.TileEntities;
using Xunit;

namespace Orion.Tests.World.TileEntities {
    public class OrionChestServiceTests : IDisposable {
        private readonly IChestService _chestService;

        public OrionChestServiceTests() {
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
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
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
            for (var i = 0; i < 1000; ++i) {
                Terraria.Main.chest[i] = new Terraria.Chest();
            }
            var chests = _chestService.ToList();

            for (var i = 0; i < chests.Count; ++i) {
                ((OrionChest)chests[i]).Wrapped.Should().BeSameAs(Terraria.Main.chest[i]);
            }
        }
    }
}
