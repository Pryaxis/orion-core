// Copyright (c) 2019 Pryaxis & Orion Contributors
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

namespace Orion.Items {
    /// <summary>
    /// Specifies an item prefix.
    /// </summary>
    /// <remarks>
    /// Item prefixes modify items by, e.g., raising or lowering the item's use time, damage, critical hit chance, etc.
    /// </remarks>
    public enum ItemPrefix : sbyte {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Reforge = -2,
        Random = -1,
        None = 0,
        Large = 1,
        Massive = 2,
        Dangerous = 3,
        Savage = 4,
        Sharp = 5,
        Pointy = 6,
        Tiny = 7,
        Terrible = 8,
        Small = 9,
        Dull = 10,
        Unhappy = 11,
        Bulky = 12,
        Shameful = 13,
        Heavy = 14,
        Light = 15,
        Sighted = 16,
        Rapid = 17,
        HastyRangedWeapon = 18,
        Intimidating = 19,
        DeadlyRangedWeapon = 20,
        Staunch = 21,
        Awful = 22,
        Lethargic = 23,
        Awkward = 24,
        Powerful = 25,
        Mystic = 26,
        Adept = 27,
        Masterful = 28,
        Inept = 29,
        Ignorant = 30,
        Deranged = 31,
        Intense = 32,
        Taboo = 33,
        Celestial = 34,
        Furious = 35,
        Keen = 36,
        Superior = 37,
        Forceful = 38,
        Broken = 39,
        Damaged = 40,
        Shoddy = 41,
        QuickWeapon = 42,
        DeadlyAccessory = 43,
        Agile = 44,
        Nimble = 45,
        Murderous = 46,
        Slow = 47,
        Sluggish = 48,
        Lazy = 49,
        Annoying = 50,
        Nasty = 51,
        Manic = 52,
        Hurtful = 53,
        Strong = 54,
        Unpleasant = 55,
        Weak = 56,
        Ruthless = 57,
        Frenzying = 58,
        Godly = 59,
        Demonic = 60,
        Zealous = 61,
        Hard = 62,
        Guarding = 63,
        Armored = 64,
        Warding = 65,
        Arcane = 66,
        Precise = 67,
        Lucky = 68,
        Jagged = 69,
        Spiked = 70,
        Angry = 71,
        Menacing = 72,
        Brisk = 73,
        Fleeting = 74,
        HastyAccessory = 75,
        QuickAccessory = 76,
        Wild = 77,
        Rash = 78,
        Intrepid = 79,
        Violent = 80,
        Legendary = 81,
        Unreal = 82,
        Mythical = 83
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
