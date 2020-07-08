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
using System.Linq;

namespace Orion.Core.Players
{
    /// <summary>
    /// Provides inventory slot values.
    /// </summary>
    public static class InventorySlot
    {
        private static readonly int[] _hotbar = Enumerable.Range(0, 10).ToArray();
        private static readonly int[] _inventory = Enumerable.Range(10, 40).ToArray();
        private static readonly int[] _coins = Enumerable.Range(50, 4).ToArray();
        private static readonly int[] _ammo = Enumerable.Range(54, 4).ToArray();
        private static readonly int[] _armor = Enumerable.Range(59, 3).ToArray();
        private static readonly int[] _accessories = Enumerable.Range(62, 7).ToArray();
        private static readonly int[] _socialArmor = Enumerable.Range(69, 3).ToArray();
        private static readonly int[] _socialAccessories = Enumerable.Range(72, 7).ToArray();
        private static readonly int[] _armorDyes = Enumerable.Range(79, 3).ToArray();
        private static readonly int[] _accessoryDyes = Enumerable.Range(82, 7).ToArray();
        private static readonly int[] _piggyBank = Enumerable.Range(99, 40).ToArray();
        private static readonly int[] _safe = Enumerable.Range(139, 40).ToArray();
        private static readonly int[] _defendersForge = Enumerable.Range(180, 40).ToArray();
        private static readonly int[] _voidVault = Enumerable.Range(220, 40).ToArray();

        /// <summary>
        /// Gets the hotbar slots.
        /// </summary>
        /// <value>The hotbar slots.</value>
        public static ReadOnlySpan<int> Hotbar => _hotbar;

        /// <summary>
        /// Gets the inventory slots.
        /// </summary>
        /// <value>The inventory slots.</value>
        public static ReadOnlySpan<int> Inventory => _inventory;

        /// <summary>
        /// Gets the coin slots.
        /// </summary>
        /// <value>The coin slots.</value>
        public static ReadOnlySpan<int> Coins => _coins;

        /// <summary>
        /// Gets the ammo slots.
        /// </summary>
        /// <value>The ammo slots.</value>
        public static ReadOnlySpan<int> Ammo => _ammo;

        /// <summary>
        /// Gets the cursor slot.
        /// </summary>
        /// <value>The cursor slot.</value>
        public static int Cursor => 58;

        /// <summary>
        /// Gets the armor slots.
        /// </summary>
        /// <value>The armor slots.</value>
        public static ReadOnlySpan<int> Armor => _armor;

        /// <summary>
        /// Gets the accessory slots.
        /// </summary>
        /// <value>The accessory slots.</value>
        public static ReadOnlySpan<int> Accessories => _accessories;

        /// <summary>
        /// Gets the social armor slots.
        /// </summary>
        /// <value>The social armor slots.</value>
        public static ReadOnlySpan<int> SocialArmor => _socialArmor;

        /// <summary>
        /// Gets the social accessory slots.
        /// </summary>
        /// <value>The social accessory slots.</value>
        public static ReadOnlySpan<int> SocialAccessories => _socialAccessories;

        /// <summary>
        /// Gets the armor dye slots.
        /// </summary>
        /// <value>The armor dye slots.</value>
        public static ReadOnlySpan<int> ArmorDyes => _armorDyes;

        /// <summary>
        /// Gets the accessory dye slots.
        /// </summary>
        /// <value>The accessory dye slots.</value>
        public static ReadOnlySpan<int> AccessoryDyes => _accessoryDyes;

        /// <summary>
        /// Gets the pet slot.
        /// </summary>
        /// <value>The pet slot.</value>
        public static int Pet => 89;

        /// <summary>
        /// Gets the light pet slot.
        /// </summary>
        /// <value>The light pet slot.</value>
        public static int LightPet => 90;

        /// <summary>
        /// Gets the minecart slot.
        /// </summary>
        /// <value>The minecart slot.</value>
        public static int Minecart => 91;

        /// <summary>
        /// Gets the mount slot.
        /// </summary>
        /// <value>The mount slot.</value>
        public static int Mount => 92;

        /// <summary>
        /// Gets the hook slot.
        /// </summary>
        /// <value>The hook slot.</value>
        public static int Hook => 93;

        /// <summary>
        /// Gets the pet dye slot.
        /// </summary>
        /// <value>The pet dye slot.</value>
        public static int PetDye => 94;

        /// <summary>
        /// Gets the light pet dye slot.
        /// </summary>
        /// <value>The light pet dye slot.</value>
        public static int LightPetDye => 95;

        /// <summary>
        /// Gets the minecart dye slot.
        /// </summary>
        /// <value>The minecart dye slot.</value>
        public static int MinecartDye => 96;

        /// <summary>
        /// Gets the mount dye slot.
        /// </summary>
        /// <value>The mount dye slot.</value>
        public static int MountDye => 97;

        /// <summary>
        /// Gets the hook dye slot.
        /// </summary>
        /// <value>The hook dye slot.</value>
        public static int HookDye => 98;

        /// <summary>
        /// Gets the piggy bank slots.
        /// </summary>
        /// <value>The piggy bank slots.</value>
        public static ReadOnlySpan<int> PiggyBank => _piggyBank;

        /// <summary>
        /// Gets the safe slots.
        /// </summary>
        /// <value>The safe slots.</value>
        public static ReadOnlySpan<int> Safe => _safe;

        /// <summary>
        /// Gets the trash slot.
        /// </summary>
        /// <value>The trash slot.</value>
        public static int Trash => 179;

        /// <summary>
        /// Gets the defender's forge slots.
        /// </summary>
        /// <value>The defender's forge slots.</value>
        public static ReadOnlySpan<int> DefendersForge => _defendersForge;

        /// <summary>
        /// Gets the void vault slots.
        /// </summary>
        /// <value>The void vault slots.</value>
        public static ReadOnlySpan<int> VoidVault => _voidVault;
    }
}
