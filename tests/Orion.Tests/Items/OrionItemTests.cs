using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Projectiles;
using Orion.World.Tiles;
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

        public static readonly IEnumerable<object[]> ColorData = new List<object[]> {
            new object[] {new Color(255, 0, 0)},
            new object[] {new Color(0, 255, 0)},
            new object[] {new Color(0, 0, 255)},
        };
        
        [Theory]
        [MemberData(nameof(ColorData))]
        public void GetColor_IsCorrect(Color color) {
            var terrariaItem = new Terraria.Item {color = color};
            var item = new OrionItem(terrariaItem);

            item.Color.Should().Be(color);
        }
        
        [Theory]
        [MemberData(nameof(ColorData))]
        public void SetColor_IsCorrect(Color color) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Color = color;

            terrariaItem.color.Should().Be(color);
        }

        [Theory]
        [InlineData(100)]
        public void GetScale_IsCorrect(float scale) {
            var terrariaItem = new Terraria.Item {scale = scale};
            var item = new OrionItem(terrariaItem);

            item.Scale.Should().Be(scale);
        }

        [Theory]
        [InlineData(100)]
        public void SetScale_IsCorrect(float scale) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Scale = scale;

            terrariaItem.scale.Should().Be(scale);
        }

        [Theory]
        [InlineData(100)]
        public void GetValue_IsCorrect(int value) {
            var terrariaItem = new Terraria.Item {value = value};
            var item = new OrionItem(terrariaItem);

            item.Value.Should().Be(value);
        }

        [Theory]
        [InlineData(100)]
        public void SetValue_IsCorrect(int value) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Value = value;

            terrariaItem.value.Should().Be(value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsMeleeWeapon_IsCorrect(bool isMeleeWeapon) {
            var terrariaItem = new Terraria.Item {melee = isMeleeWeapon};
            var item = new OrionItem(terrariaItem);

            item.IsMeleeWeapon.Should().Be(isMeleeWeapon);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsMeleeWeapon_IsCorrect(bool isMeleeWeapon) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.IsMeleeWeapon = isMeleeWeapon;

            terrariaItem.melee.Should().Be(isMeleeWeapon);
        }
        
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsRangedWeapon_IsCorrect(bool isRangedWeapon) {
            var terrariaItem = new Terraria.Item {ranged = isRangedWeapon};
            var item = new OrionItem(terrariaItem);

            item.IsRangedWeapon.Should().Be(isRangedWeapon);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsRangedWeapon_IsCorrect(bool isRangedWeapon) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.IsRangedWeapon = isRangedWeapon;

            terrariaItem.ranged.Should().Be(isRangedWeapon);
        }
        
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsMagicWeapon_IsCorrect(bool isMagicWeapon) {
            var terrariaItem = new Terraria.Item {magic = isMagicWeapon};
            var item = new OrionItem(terrariaItem);

            item.IsMagicWeapon.Should().Be(isMagicWeapon);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsMagicWeapon_IsCorrect(bool isMagicWeapon) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.IsMagicWeapon = isMagicWeapon;

            terrariaItem.magic.Should().Be(isMagicWeapon);
        }
        
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsSummonWeapon_IsCorrect(bool isSummonWeapon) {
            var terrariaItem = new Terraria.Item {summon = isSummonWeapon};
            var item = new OrionItem(terrariaItem);

            item.IsSummonWeapon.Should().Be(isSummonWeapon);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsSummonWeapon_IsCorrect(bool isSummonWeapon) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.IsSummonWeapon = isSummonWeapon;

            terrariaItem.summon.Should().Be(isSummonWeapon);
        }
        
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsThrownWeapon_IsCorrect(bool isThrownWeapon) {
            var terrariaItem = new Terraria.Item {thrown = isThrownWeapon};
            var item = new OrionItem(terrariaItem);

            item.IsThrownWeapon.Should().Be(isThrownWeapon);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsThrownWeapon_IsCorrect(bool isThrownWeapon) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.IsThrownWeapon = isThrownWeapon;

            terrariaItem.thrown.Should().Be(isThrownWeapon);
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
        public void GetKnockback_IsCorrect(float knockBack) {
            var terrariaItem = new Terraria.Item {knockBack = knockBack};
            var item = new OrionItem(terrariaItem);

            item.Knockback.Should().Be(knockBack);
        }

        [Theory]
        [InlineData(100)]
        public void SetKnockback_IsCorrect(float knockback) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Knockback = knockback;

            terrariaItem.knockBack.Should().Be(knockback);
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
        [InlineData(AmmoType.FallenStar)]
        public void GetUsesAmmoType_IsCorrect(AmmoType usesAmmoType) {
            var terrariaItem = new Terraria.Item {useAmmo = (int)usesAmmoType};
            var item = new OrionItem(terrariaItem);

            item.UsesAmmoType.Should().Be(usesAmmoType);
        }

        [Theory]
        [InlineData(AmmoType.FallenStar)]
        public void SetUsesAmmoType_IsCorrect(AmmoType usesAmmoType) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.UsesAmmoType = usesAmmoType;

            terrariaItem.useAmmo.Should().Be((int)usesAmmoType);
        }

        [Theory]
        [InlineData(100)]
        public void GetProjectileSpeed_IsCorrect(float projectileSpeed) {
            var terrariaItem = new Terraria.Item {shootSpeed = projectileSpeed};
            var item = new OrionItem(terrariaItem);

            item.ProjectileSpeed.Should().Be(projectileSpeed);
        }

        [Theory]
        [InlineData(100)]
        public void SetProjectileSpeed_IsCorrect(float projectileSpeed) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.ProjectileSpeed = projectileSpeed;

            terrariaItem.shootSpeed.Should().Be(projectileSpeed);
        }

        [Theory]
        [InlineData(AmmoType.FallenStar)]
        public void GetAmmoType_IsCorrect(AmmoType ammoType) {
            var terrariaItem = new Terraria.Item {ammo = (int)ammoType};
            var item = new OrionItem(terrariaItem);

            item.AmmoType.Should().Be(ammoType);
        }

        [Theory]
        [InlineData(AmmoType.FallenStar)]
        public void SetAmmoType_IsCorrect(AmmoType ammoType) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.AmmoType = ammoType;

            terrariaItem.ammo.Should().Be((int)ammoType);
        }

        [Theory]
        [InlineData(ProjectileType.StarWrath)]
        public void GetProjectileType_IsCorrect(ProjectileType projectileType) {
            var terrariaItem = new Terraria.Item {shoot = (int)projectileType};
            var item = new OrionItem(terrariaItem);

            item.ProjectileType.Should().Be(projectileType);
        }

        [Theory]
        [InlineData(ProjectileType.StarWrath)]
        public void SetProjectileType_IsCorrect(ProjectileType projectileType) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.ProjectileType = projectileType;

            terrariaItem.shoot.Should().Be((int)projectileType);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(BlockType.Stone)]
        public void GetBlockType_IsCorrect(BlockType? blockType) {
            var terrariaItem = new Terraria.Item {createTile = (int?)blockType ?? -1};
            var item = new OrionItem(terrariaItem);

            item.BlockType.Should().Be(blockType);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(BlockType.Stone)]
        public void SetBlockType_IsCorrect(BlockType? blockType) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.BlockType = blockType;

            terrariaItem.createTile.Should().Be((int?)blockType ?? -1);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(WallType.Stone)]
        public void GetWallType_IsCorrect(WallType? wallType) {
            var terrariaItem = new Terraria.Item {createWall = (int?)wallType ?? -1};
            var item = new OrionItem(terrariaItem);

            item.WallType.Should().Be(wallType);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(WallType.Stone)]
        public void SetWallType_IsCorrect(WallType? wallType) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.WallType = wallType;

            terrariaItem.createWall.Should().Be((int?)wallType ?? -1);
        }

        [Theory]
        [InlineData(100)]
        public void GetPickaxePower_IsCorrect(int pickaxePower) {
            var terrariaItem = new Terraria.Item {pick = pickaxePower};
            var item = new OrionItem(terrariaItem);

            item.PickaxePower.Should().Be(pickaxePower);
        }

        [Theory]
        [InlineData(100)]
        public void SetPickaxePower_IsCorrect(int pickaxePower) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.PickaxePower = pickaxePower;

            terrariaItem.pick.Should().Be(pickaxePower);
        }

        [Theory]
        [InlineData(100)]
        public void GetAxePower_IsCorrect(int axePower) {
            var terrariaItem = new Terraria.Item {axe = axePower};
            var item = new OrionItem(terrariaItem);

            item.AxePower.Should().Be(axePower);
        }

        [Theory]
        [InlineData(100)]
        public void SetAxePower_IsCorrect(int axePower) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.AxePower = axePower;

            terrariaItem.axe.Should().Be(axePower);
        }

        [Theory]
        [InlineData(100)]
        public void GetHammerPower_IsCorrect(int hammerPower) {
            var terrariaItem = new Terraria.Item {hammer = hammerPower};
            var item = new OrionItem(terrariaItem);

            item.HammerPower.Should().Be(hammerPower);
        }

        [Theory]
        [InlineData(100)]
        public void SetHammerPower_IsCorrect(int hammerPower) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.HammerPower = hammerPower;

            terrariaItem.hammer.Should().Be(hammerPower);
        }

        [Theory]
        [InlineData(100)]
        public void GetDefense_IsCorrect(int defense) {
            var terrariaItem = new Terraria.Item {defense = defense};
            var item = new OrionItem(terrariaItem);

            item.Defense.Should().Be(defense);
        }

        [Theory]
        [InlineData(100)]
        public void SetDefense_IsCorrect(int defense) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Defense = defense;

            terrariaItem.defense.Should().Be(defense);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsAccessory_IsCorrect(bool isAccessory) {
            var terrariaItem = new Terraria.Item {accessory = isAccessory};
            var item = new OrionItem(terrariaItem);

            item.IsAccessory.Should().Be(isAccessory);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsAccessory_IsCorrect(bool isAccessory) {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.IsAccessory = isAccessory;

            terrariaItem.accessory.Should().Be(isAccessory);
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
