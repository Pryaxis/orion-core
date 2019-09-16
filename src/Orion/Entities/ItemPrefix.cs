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

using System.Collections.Generic;
using System.Reflection;

namespace Orion.Entities {
    /// <summary>
    /// Represents an item's prefix.
    /// </summary>
    public sealed class ItemPrefix {
#pragma warning disable 1591
        public static ItemPrefix Reforge = new ItemPrefix(-2);
        public static ItemPrefix Random = new ItemPrefix(-1);
        public static ItemPrefix None = new ItemPrefix(0);
        public static ItemPrefix Large = new ItemPrefix(1);
        public static ItemPrefix Massive = new ItemPrefix(2);
        public static ItemPrefix Dangerous = new ItemPrefix(3);
        public static ItemPrefix Savage = new ItemPrefix(4);
        public static ItemPrefix Sharp = new ItemPrefix(5);
        public static ItemPrefix Pointy = new ItemPrefix(6);
        public static ItemPrefix Tiny = new ItemPrefix(7);
        public static ItemPrefix Terrible = new ItemPrefix(8);
        public static ItemPrefix Small = new ItemPrefix(9);
        public static ItemPrefix Dull = new ItemPrefix(10);
        public static ItemPrefix Unhappy = new ItemPrefix(11);
        public static ItemPrefix Bulky = new ItemPrefix(12);
        public static ItemPrefix Shameful = new ItemPrefix(13);
        public static ItemPrefix Heavy = new ItemPrefix(14);
        public static ItemPrefix Light = new ItemPrefix(15);
        public static ItemPrefix Sighted = new ItemPrefix(16);
        public static ItemPrefix Rapid = new ItemPrefix(17);
        public static ItemPrefix HastyRangedWeapon = new ItemPrefix(18);
        public static ItemPrefix Intimidating = new ItemPrefix(19);
        public static ItemPrefix DeadlyRangedWeapon = new ItemPrefix(20);
        public static ItemPrefix Staunch = new ItemPrefix(21);
        public static ItemPrefix Awful = new ItemPrefix(22);
        public static ItemPrefix Lethargic = new ItemPrefix(23);
        public static ItemPrefix Awkward = new ItemPrefix(24);
        public static ItemPrefix Powerful = new ItemPrefix(25);
        public static ItemPrefix Mystic = new ItemPrefix(26);
        public static ItemPrefix Adept = new ItemPrefix(27);
        public static ItemPrefix Masterful = new ItemPrefix(28);
        public static ItemPrefix Inept = new ItemPrefix(29);
        public static ItemPrefix Ignorant = new ItemPrefix(30);
        public static ItemPrefix Deranged = new ItemPrefix(31);
        public static ItemPrefix Intense = new ItemPrefix(32);
        public static ItemPrefix Taboo = new ItemPrefix(33);
        public static ItemPrefix Celestial = new ItemPrefix(34);
        public static ItemPrefix Furious = new ItemPrefix(35);
        public static ItemPrefix Keen = new ItemPrefix(36);
        public static ItemPrefix Superior = new ItemPrefix(37);
        public static ItemPrefix Forceful = new ItemPrefix(38);
        public static ItemPrefix Broken = new ItemPrefix(39);
        public static ItemPrefix Damaged = new ItemPrefix(40);
        public static ItemPrefix Shoddy = new ItemPrefix(41);
        public static ItemPrefix QuickWeapon = new ItemPrefix(42);
        public static ItemPrefix DeadlyAccessory = new ItemPrefix(43);
        public static ItemPrefix Agile = new ItemPrefix(44);
        public static ItemPrefix Nimble = new ItemPrefix(45);
        public static ItemPrefix Murderous = new ItemPrefix(46);
        public static ItemPrefix Slow = new ItemPrefix(47);
        public static ItemPrefix Sluggish = new ItemPrefix(48);
        public static ItemPrefix Lazy = new ItemPrefix(49);
        public static ItemPrefix Annoying = new ItemPrefix(50);
        public static ItemPrefix Nasty = new ItemPrefix(51);
        public static ItemPrefix Manic = new ItemPrefix(52);
        public static ItemPrefix Hurtful = new ItemPrefix(53);
        public static ItemPrefix Strong = new ItemPrefix(54);
        public static ItemPrefix Unpleasant = new ItemPrefix(55);
        public static ItemPrefix Weak = new ItemPrefix(56);
        public static ItemPrefix Ruthless = new ItemPrefix(57);
        public static ItemPrefix Frenzying = new ItemPrefix(58);
        public static ItemPrefix Godly = new ItemPrefix(59);
        public static ItemPrefix Demonic = new ItemPrefix(60);
        public static ItemPrefix Hard = new ItemPrefix(62);
        public static ItemPrefix Guarding = new ItemPrefix(63);
        public static ItemPrefix Armored = new ItemPrefix(64);
        public static ItemPrefix Warding = new ItemPrefix(65);
        public static ItemPrefix Arcane = new ItemPrefix(66);
        public static ItemPrefix Precise = new ItemPrefix(67);
        public static ItemPrefix Lucky = new ItemPrefix(68);
        public static ItemPrefix Jagged = new ItemPrefix(69);
        public static ItemPrefix Spiked = new ItemPrefix(70);
        public static ItemPrefix Angry = new ItemPrefix(71);
        public static ItemPrefix Menacing = new ItemPrefix(72);
        public static ItemPrefix Brisk = new ItemPrefix(73);
        public static ItemPrefix Fleeting = new ItemPrefix(74);
        public static ItemPrefix HastyAccessory = new ItemPrefix(75);
        public static ItemPrefix QuickAccessory = new ItemPrefix(76);
        public static ItemPrefix Wild = new ItemPrefix(77);
        public static ItemPrefix Rash = new ItemPrefix(78);
        public static ItemPrefix Intrepid = new ItemPrefix(79);
        public static ItemPrefix Violent = new ItemPrefix(80);
        public static ItemPrefix Legendary = new ItemPrefix(81);
        public static ItemPrefix Unreal = new ItemPrefix(82);
        public static ItemPrefix Mythical = new ItemPrefix(83);
#pragma warning restore 1591

        private static readonly IDictionary<int, FieldInfo> IdToField = new Dictionary<int, FieldInfo>();
        private static readonly IDictionary<int, ItemPrefix> IdToItemPrefix = new Dictionary<int, ItemPrefix>();

        /// <summary>
        /// Gets the item prefix's ID.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets a value indicating whether the item prefix is unknown.
        /// </summary>
        public bool IsUnknown { get; }

        // Initializes lookup tables.
        static ItemPrefix() {
            var fields = typeof(ItemPrefix).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields) {
                if (!(field.GetValue(null) is ItemPrefix itemPrefix)) continue;

                IdToField[itemPrefix.Id] = field;
                IdToItemPrefix[itemPrefix.Id] = itemPrefix;
            }
        }

        private ItemPrefix(int id, bool isUnknown = false) {
            Id = id;
            IsUnknown = isUnknown;
        }

        /// <summary>
        /// Returns an item prefix converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The item prefix.</returns>
        public static ItemPrefix FromId(int id) =>
            IdToItemPrefix.TryGetValue(id, out var itemType) ? itemType : new ItemPrefix(id, true);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is ItemPrefix itemPrefix && Id == itemPrefix.Id;

        /// <inheritdoc />
        public override int GetHashCode() => Id;

        /// <inheritdoc />
        public override string ToString() => IsUnknown ? "Unknown" : IdToField[Id].Name;
    }
}
