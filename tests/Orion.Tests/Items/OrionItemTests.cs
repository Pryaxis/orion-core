using System;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Items;
using Xunit;

namespace Orion.Tests.Items {
    public class OrionItemTests {
        [Theory]
        [InlineData(100)]
        public void GetIndex_IsCorrect(int index) {
            var terrariaItem = new Terraria.Item {whoAmI = index};
            var item = new OrionItem(terrariaItem);

            item.Index.Should().Be(index);
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

        [Theory]
        [InlineData("test")]
        public void GetName_IsCorrect(string name) {
            var terrariaItem = new Terraria.Item {_nameOverride = name};
            var item = new OrionItem(terrariaItem);

            item.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("test")]
        public void SetName_IsCorrect(string name) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Name = name;

            terrariaItem.Name.Should().Be(name);
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

        [Theory]
        [InlineData(ItemType.SDMG)]
        public void GetType_IsCorrect(ItemType itemType) {
            var terrariaItem = new Terraria.Item {type = (int)itemType};
            var item = new OrionItem(terrariaItem);

            item.Type.Should().Be(itemType);
        }

        [Theory]
        [InlineData(100)]
        public void GetStackSize_IsCorrect(int stackSize) {
            var terrariaItem = new Terraria.Item {stack = stackSize};
            var item = new OrionItem(terrariaItem);

            item.StackSize.Should().Be(stackSize);
        }

        [Theory]
        [InlineData(100)]
        public void SetStackSize_IsCorrect(int stackSize) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.StackSize = stackSize;

            terrariaItem.stack.Should().Be(stackSize);
        }

        [Theory]
        [InlineData(ItemPrefix.Unreal)]
        public void GetPrefix_IsCorrect(ItemPrefix itemPrefix) {
            var terrariaItem = new Terraria.Item {prefix = (byte)itemPrefix};
            var item = new OrionItem(terrariaItem);

            item.Prefix.Should().Be(itemPrefix);
        }

        [Theory]
        [InlineData(100)]
        public void GetMaxStackSize_IsCorrect(int maxStackSize) {
            var terrariaItem = new Terraria.Item {maxStack = maxStackSize};
            var item = new OrionItem(terrariaItem);

            item.MaxStackSize.Should().Be(maxStackSize);
        }

        [Theory]
        [InlineData(100)]
        public void SetMaxStackSize_IsCorrect(int maxStackSize) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.MaxStackSize = maxStackSize;

            terrariaItem.maxStack.Should().Be(maxStackSize);
        }

        [Theory]
        [InlineData(ItemRarity.Orange)]
        public void GetRarity_IsCorrect(ItemRarity itemRarity) {
            var terrariaItem = new Terraria.Item {rare = (int)itemRarity};
            var item = new OrionItem(terrariaItem);

            item.Rarity.Should().Be(itemRarity);
        }

        [Theory]
        [InlineData(ItemRarity.Orange)]
        public void SetRarity_IsCorrect(ItemRarity itemRarity) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Rarity = itemRarity;

            terrariaItem.rare.Should().Be((int)itemRarity);
        }

        [Theory]
        [InlineData(100)]
        public void GetDamage_IsCorrect(int damage) {
            var terrariaItem = new Terraria.Item {damage = damage};
            var item = new OrionItem(terrariaItem);

            item.Damage.Should().Be(damage);
        }

        [Theory]
        [InlineData(100)]
        public void SetDamage_IsCorrect(int damage) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Damage = damage;

            terrariaItem.damage.Should().Be(damage);
        }

        [Theory]
        [InlineData(100)]
        public void GetUseTime_IsCorrect(int useTime) {
            var terrariaItem = new Terraria.Item {useTime = useTime};
            var item = new OrionItem(terrariaItem);

            item.UseTime.Should().Be(useTime);
        }

        [Theory]
        [InlineData(100)]
        public void SetUseTime_IsCorrect(int useTime) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.UseTime = useTime;

            terrariaItem.useTime.Should().Be(useTime);
        }
        

        [Theory]
        [InlineData(ItemType.SDMG)]
        public void ApplyType_IsCorrect(ItemType itemType) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.ApplyType(itemType);

            terrariaItem.type.Should().Be((int)itemType);
        }
        
        [Theory]
        [InlineData(ItemPrefix.Unreal)]
        public void ApplyPrefix_IsCorrect(ItemPrefix itemPrefix) {
            var terrariaItem = new Terraria.Item();
            terrariaItem.SetDefaults((int)ItemType.SDMG);
            var item = new OrionItem(terrariaItem);

            item.ApplyPrefix(itemPrefix);

            terrariaItem.prefix.Should().Be((byte)itemPrefix);
        }
    }
}
