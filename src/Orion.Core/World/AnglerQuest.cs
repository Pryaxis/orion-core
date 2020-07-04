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

using System.Collections.Generic;
using Orion.Core.Items;

namespace Orion.Core.World
{
    /// <summary>
    /// Specifies an angler quest.
    /// </summary>
    public enum AnglerQuest : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Batfish = 0,
        BumblebeeTuna = 1,
        Catfish = 2,
        Cloudfish = 3,
        Cursedfish = 4,
        Dirtfish = 5,
        DynamiteFish = 6,
        EaterOfPlankton = 7,
        FallenStarfish = 8,
        TheFishOfCthulhu = 9,
        Fishotron = 10,
        Harpyfish = 11,
        Hungerfish = 12,
        Ichorfish = 13,
        Jewelfish = 14,
        MirageFish = 15,
        MutantFlinxfin = 16,
        Pengfish = 17,
        Pixiefish = 18,
        Spiderfish = 19,
        TundraTrout = 20,
        UnicornFish = 21,
        GuideVoodooFish = 22,
        Wyverntail = 23,
        ZombieFish = 24,
        AmanitaFungifin = 25,
        Angelfish = 26,
        BloodyManowar = 27,
        Bonefish = 28,
        Bunnyfish = 29,
        CapnTunabeard = 30,
        Clownfish = 31,
        DemonicHellfish = 32,
        Derpfish = 33,
        Fishron = 34,
        InfectedScabbardfish = 35,
        Mudfish = 36,
        Slimefish = 37,
        TropicalBarracuda = 38,
        ScarabFish = 39,
        ScorpioFish = 40
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// Provides extensions for the <see cref="AnglerQuest"/> enumeration.
    /// </summary>
    public static class AnglerQuestExtensions
    {
        private static readonly Dictionary<AnglerQuest, ItemId> _itemIds = new Dictionary<AnglerQuest, ItemId>
        {
            [AnglerQuest.Batfish] = Items.ItemId.Batfish,
            [AnglerQuest.BumblebeeTuna] = Items.ItemId.BumblebeeTuna,
            [AnglerQuest.Catfish] = Items.ItemId.Catfish,
            [AnglerQuest.Cloudfish] = Items.ItemId.Cloudfish,
            [AnglerQuest.Cursedfish] = Items.ItemId.Cursedfish,
            [AnglerQuest.Dirtfish] = Items.ItemId.Dirtfish,
            [AnglerQuest.DynamiteFish] = Items.ItemId.DynamiteFish,
            [AnglerQuest.EaterOfPlankton] = Items.ItemId.EaterOfPlankton,
            [AnglerQuest.FallenStarfish] = Items.ItemId.FallenStarfish,
            [AnglerQuest.TheFishOfCthulhu] = Items.ItemId.TheFishOfCthulhu,
            [AnglerQuest.Fishotron] = Items.ItemId.Fishotron,
            [AnglerQuest.Harpyfish] = Items.ItemId.Harpyfish,
            [AnglerQuest.Hungerfish] = Items.ItemId.Hungerfish,
            [AnglerQuest.Ichorfish] = Items.ItemId.Ichorfish,
            [AnglerQuest.Jewelfish] = Items.ItemId.Jewelfish,
            [AnglerQuest.MirageFish] = Items.ItemId.MirageFish,
            [AnglerQuest.MutantFlinxfin] = Items.ItemId.MutantFlinxfin,
            [AnglerQuest.Pengfish] = Items.ItemId.Pengfish,
            [AnglerQuest.Pixiefish] = Items.ItemId.Pixiefish,
            [AnglerQuest.Spiderfish] = Items.ItemId.Spiderfish,
            [AnglerQuest.TundraTrout] = Items.ItemId.TundraTrout,
            [AnglerQuest.UnicornFish] = Items.ItemId.UnicornFish,
            [AnglerQuest.GuideVoodooFish] = Items.ItemId.GuideVoodooFish,
            [AnglerQuest.Wyverntail] = Items.ItemId.Wyverntail,
            [AnglerQuest.ZombieFish] = Items.ItemId.ZombieFish,
            [AnglerQuest.AmanitaFungifin] = Items.ItemId.AmanitaFungifin,
            [AnglerQuest.Angelfish] = Items.ItemId.Angelfish,
            [AnglerQuest.BloodyManowar] = Items.ItemId.BloodyManowar,
            [AnglerQuest.Bonefish] = Items.ItemId.Bonefish,
            [AnglerQuest.Bunnyfish] = Items.ItemId.Bunnyfish,
            [AnglerQuest.CapnTunabeard] = Items.ItemId.CapnTunabeard,
            [AnglerQuest.Clownfish] = Items.ItemId.Clownfish,
            [AnglerQuest.DemonicHellfish] = Items.ItemId.DemonicHellfish,
            [AnglerQuest.Derpfish] = Items.ItemId.Derpfish,
            [AnglerQuest.Fishron] = Items.ItemId.Fishron,
            [AnglerQuest.InfectedScabbardfish] = Items.ItemId.InfectedScabbardfish,
            [AnglerQuest.Mudfish] = Items.ItemId.Mudfish,
            [AnglerQuest.Slimefish] = Items.ItemId.Slimefish,
            [AnglerQuest.TropicalBarracuda] = Items.ItemId.TropicalBarracuda,
            [AnglerQuest.ScarabFish] = Items.ItemId.ScarabFish,
            [AnglerQuest.ScorpioFish] = Items.ItemId.ScorpioFish
        };

        /// <summary>
        /// Gets the angler quest's corresponding item ID.
        /// </summary>
        /// <param name="quest">The angler quest.</param>
        /// <returns>The angler quest's corresponding item ID.</returns>
        public static ItemId ItemId(this AnglerQuest quest) =>
            _itemIds.TryGetValue(quest, out var itemId) ? itemId : Items.ItemId.None;
    }
}
