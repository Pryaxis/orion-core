// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using Xunit;

namespace Orion.Core.Players
{
    public class InventorySlotTests
    {
        [Fact]
        public void Hotbar_Get()
        {
            var hotbar = InventorySlot.Hotbar;

            Assert.Equal(10, hotbar.Length);
            IsSequential(hotbar, 0);
        }

        [Fact]
        public void Inventory_Get()
        {
            var inventory = InventorySlot.Inventory;

            Assert.Equal(40, inventory.Length);
            IsSequential(inventory, InventorySlot.Hotbar[^1] + 1);
        }

        [Fact]
        public void Coins_Get()
        {
            var coins = InventorySlot.Coins;

            Assert.Equal(4, coins.Length);
            IsSequential(coins, InventorySlot.Inventory[^1] + 1);
        }

        [Fact]
        public void Ammo_Get()
        {
            var ammo = InventorySlot.Ammo;

            Assert.Equal(4, ammo.Length);
            IsSequential(ammo, InventorySlot.Coins[^1] + 1);
        }

        [Fact]
        public void Cursor_Get()
        {
            Assert.Equal(InventorySlot.Ammo[^1] + 1, InventorySlot.Cursor);
        }

        [Fact]
        public void Armor_Get()
        {
            var armor = InventorySlot.Armor;

            Assert.Equal(3, armor.Length);
            IsSequential(armor, InventorySlot.Cursor + 1);
        }

        [Fact]
        public void Accessories_Get()
        {
            var accessories = InventorySlot.Accessories;

            Assert.Equal(7, accessories.Length);
            IsSequential(accessories, InventorySlot.Armor[^1] + 1);
        }

        [Fact]
        public void SocialArmor_Get()
        {
            var socialArmor = InventorySlot.SocialArmor;

            Assert.Equal(3, socialArmor.Length);
            IsSequential(socialArmor, InventorySlot.Accessories[^1] + 1);
        }

        [Fact]
        public void SocialAccessories_Get()
        {
            var socialAccessories = InventorySlot.SocialAccessories;

            Assert.Equal(7, socialAccessories.Length);
            IsSequential(socialAccessories, InventorySlot.SocialArmor[^1] + 1);
        }

        [Fact]
        public void ArmorDyes_Get()
        {
            var armorDyes = InventorySlot.ArmorDyes;

            Assert.Equal(3, armorDyes.Length);
            IsSequential(armorDyes, InventorySlot.SocialAccessories[^1] + 1);
        }

        [Fact]
        public void AccessoryDyes_Get()
        {
            var accessoryDyes = InventorySlot.AccessoryDyes;

            Assert.Equal(7, accessoryDyes.Length);
            IsSequential(accessoryDyes, InventorySlot.ArmorDyes[^1] + 1);
        }

        [Fact]
        public void Pet_Get()
        {
            Assert.Equal(InventorySlot.AccessoryDyes[^1] + 1, InventorySlot.Pet);
        }

        [Fact]
        public void LightPet_Get()
        {
            Assert.Equal(InventorySlot.Pet + 1, InventorySlot.LightPet);
        }

        [Fact]
        public void Minecart_Get()
        {
            Assert.Equal(InventorySlot.LightPet + 1, InventorySlot.Minecart);
        }

        [Fact]
        public void Mount_Get()
        {
            Assert.Equal(InventorySlot.Minecart + 1, InventorySlot.Mount);
        }

        [Fact]
        public void Hook_Get()
        {
            Assert.Equal(InventorySlot.Mount + 1, InventorySlot.Hook);
        }

        [Fact]
        public void PetDye_Get()
        {
            Assert.Equal(InventorySlot.Hook + 1, InventorySlot.PetDye);
        }

        [Fact]
        public void LightPetDye_Get()
        {
            Assert.Equal(InventorySlot.PetDye + 1, InventorySlot.LightPetDye);
        }

        [Fact]
        public void MinecartDye_Get()
        {
            Assert.Equal(InventorySlot.LightPetDye + 1, InventorySlot.MinecartDye);
        }

        [Fact]
        public void MountDye_Get()
        {
            Assert.Equal(InventorySlot.MinecartDye + 1, InventorySlot.MountDye);
        }

        [Fact]
        public void HookDye_Get()
        {
            Assert.Equal(InventorySlot.MountDye + 1, InventorySlot.HookDye);
        }

        [Fact]
        public void PiggyBank_Get()
        {
            var piggyBank = InventorySlot.PiggyBank;

            Assert.Equal(40, piggyBank.Length);
            IsSequential(piggyBank, InventorySlot.HookDye + 1);
        }

        [Fact]
        public void Safe_Get()
        {
            var safe = InventorySlot.Safe;

            Assert.Equal(40, safe.Length);
            IsSequential(safe, InventorySlot.PiggyBank[^1] + 1);
        }

        [Fact]
        public void Trash_Get()
        {
            Assert.Equal(InventorySlot.Safe[^1] + 1, InventorySlot.Trash);
        }

        [Fact]
        public void DefendersForge_Get()
        {
            var defendersForge = InventorySlot.DefendersForge;

            Assert.Equal(40, defendersForge.Length);
            IsSequential(defendersForge, InventorySlot.Trash + 1);
        }

        [Fact]
        public void VoidVault_Get()
        {
            var voidVault = InventorySlot.VoidVault;

            Assert.Equal(40, voidVault.Length);
            IsSequential(voidVault, InventorySlot.DefendersForge[^1] + 1);
        }

        private void IsSequential(ReadOnlySpan<int> span, int start)
        {
            Assert.Equal(start, span[0]);
            for (var i = 1; i < span.Length; ++i)
            {
                Assert.Equal(start + i, span[i]);
            }
        }
    }
}
