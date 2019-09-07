using System;
using FluentAssertions;
using Orion.Items;
using Orion.World.TileEntities;
using Xunit;

namespace Orion.Tests.World.TileEntities {
    public class OrionChestTests {
        [Theory]
        [InlineData(100)]
        public void GetX_IsCorrect(int x) {
            var terrariaChest = new Terraria.Chest {x = x};
            var chest = new OrionChest(terrariaChest);

            chest.X.Should().Be(x);
        }

        [Theory]
        [InlineData(100)]
        public void SetX_IsCorrect(int x) {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);

            chest.X = x;

            terrariaChest.x.Should().Be(x);
        }

        [Theory]
        [InlineData(100)]
        public void GetY_IsCorrect(int y) {
            var terrariaChest = new Terraria.Chest {y = y};
            var chest = new OrionChest(terrariaChest);

            chest.Y.Should().Be(y);
        }

        [Theory]
        [InlineData(100)]
        public void SetY_IsCorrect(int y) {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);

            chest.Y = y;

            terrariaChest.y.Should().Be(y);
        }
        
        [Theory]
        [InlineData("test")]
        public void GetName_IsCorrect(string name) {
            var terrariaChest = new Terraria.Chest {name = name};
            var chest = new OrionChest(terrariaChest);

            chest.Name.Should().Be(name);
        }
        
        [Theory]
        [InlineData("test")]
        public void SetName_IsCorrect(string name) {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);

            chest.Name = name;

            terrariaChest.name.Should().Be(name);
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);
            Action action = () => chest.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(ItemType.SDMG, 1, ItemPrefix.Unreal)]
        public void GetItems_IsCorrect(ItemType itemType, int itemStackSize, ItemPrefix itemPrefix) {
            var terrariaChest = new Terraria.Chest();
            terrariaChest.item[0] = new Terraria.Item {
                type = (int)itemType,
                stack = itemStackSize,
                prefix = (byte)itemPrefix
            };
            var chest = new OrionChest(terrariaChest);

            chest.Items[0].Type.Should().Be(itemType);
            chest.Items[0].StackSize.Should().Be(itemStackSize);
            chest.Items[0].Prefix.Should().Be(itemPrefix);
        }
    }
}
