﻿// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Orion.World.Tiles {
    /// <summary>
    /// Specifies a wall type.
    /// </summary>
    public enum WallType : byte {
#pragma warning disable 1591
        None = 0,
        Stone = 1,
        NaturalDirt = 2,
        NaturalEbonstone = 3,
        Wood = 4,
        GrayBrick = 5,
        RedBrick = 6,
        NaturalBlueBrick = 7,
        NaturalGreenBrick = 8,
        NaturalPinkBrick = 9,
        GoldBrick = 10,
        SilverBrick = 11,
        CopperBrick = 12,
        NaturalHellstoneBrick = 13,
        NaturalObsidianBrick = 14,
        NaturalMud = 15,
        Dirt = 16,
        BlueBrick = 17,
        GreenBrick = 18,
        PinkBrick = 19,
        ObsidianBrick = 20,
        Glass = 21,
        PearlstoneBrick = 22,
        IridescentBrick = 23,
        MudstoneBrick = 24,
        CobaltBrick = 25,
        MythrilBrick = 26,
        Planked = 27,
        NaturalPearlstone = 28,
        CandyCane = 29,
        GreenCandyCane = 30,
        SnowBrick = 31,
        AdamantiteBeam = 32,
        DemoniteBrick = 33,
        SandstoneBrick = 34,
        EbonstoneBrick = 35,
        RedStucco = 36,
        YellowStucco = 37,
        GreenStucco = 38,
        Gray = 39,
        NaturalSnow = 40,
        Ebonwood = 41,
        RichMahogany = 42,
        Pearlwood = 43,
        RainbowBrick = 44,
        TinBrick = 45,
        TungstenBrick = 46,
        PlatinumBrick = 47,
        NaturalAmethyst = 48,
        NaturalTopaz = 49,
        NaturalSapphire = 50,
        NaturalEmerald = 51,
        NaturalRuby = 52,
        NaturalDiamond = 53,
        NaturalCave = 54,
        NaturalCave2 = 55,
        NaturalCave3 = 56,
        NaturalCave4 = 57,
        NaturalCave5 = 58,
        NaturalCave6 = 59,
        LivingLeaf = 60,
        NaturalCave7 = 61,
        NaturalSpider = 62,
        NaturalGrass = 63,
        NaturalJungle = 64,
        NaturalFlower = 65,
        Grass = 66,
        Jungle = 67,
        Flower = 68,
        NaturalCorruptGrass = 69,
        NaturalHallowedGrass = 70,
        NaturalIce = 71,
        Cactus = 72,
        Cloud = 73,
        Mushroom = 74,
        BoneBlock = 75,
        SlimeBlock = 76,
        FleshBlock = 77,
        LivingWood = 78,
        NaturalObsidianBack = 79,
        NaturalMushroom = 80,
        NaturalCrimsonGrass = 81,
        DiscWall = 82,
        NaturalCrimstone = 83,
        IceBrick = 84,
        Shadewood = 85,
        NaturalHive = 86,
        NaturalLihzahrdBrick = 87,
        PurpleStainedGlass = 88,
        YellowStainedGlass = 89,
        BlueStainedGlass = 90,
        GreenStainedGlass = 91,
        RedStainedGlass = 92,
        MulticoloredStainedGlass = 93,
        NaturalBlueSlab = 94,
        NaturalBlueTiled = 95,
        NaturalPinkSlab = 96,
        NaturalPinkTiled = 97,
        NaturalGreenSlab = 98,
        NaturalGreenTiled = 99,
        BlueSlab = 100,
        BlueTiled = 101,
        PinkSlab = 102,
        PinkTiled = 103,
        GreenSlab = 104,
        GreenTiled = 105,
        WoodenFence = 106,
        LeadFence = 107,
        Hive = 108,
        PalladiumColumn = 109,
        BubblegumBlock = 110,
        TitanstoneBlock = 111,
        LihzahrdBrick = 112,
        Pumpkin = 113,
        Hay = 114,
        SpookyWood = 115,
        ChristmasTreeWallpaper = 116,
        OrnamentWallpaper = 117,
        CandyCaneWallpaper = 118,
        FestiveWallpaper = 119,
        StarsWallpaper = 120,
        SquigglesWallpaper = 121,
        SnowflakeWallpaper = 122,
        KrampusHornWallpaper = 123,
        BluegreenWallpaper = 124,
        GrinchFingerWallpaper = 125,
        FancyGrayWallpaper = 126,
        IceFloeWallpaper = 127,
        MusicWallpaper = 128,
        PurpleRainWallpaper = 129,
        RainbowWallpaper = 130,
        SparkleStoneWallpaper = 131,
        StarlitHeavenWallpaper = 132,
        BubbleWallpaper = 133,
        CopperPipeWallpaper = 134,
        DuckyWallpaper = 135,
        Waterfall = 136,
        Lavafall = 137,
        EbonwoodFence = 138,
        RichMahoganyFence = 139,
        PearlwoodFence = 140,
        ShadewoodFence = 141,
        WhiteDynasty = 142,
        BlueDynasty = 143,
        ArcaneRune = 144,
        IronFence = 145,
        CopperPlating = 146,
        StoneSlab = 147,
        Sail = 148,
        BorealWood = 149,
        BorealWoodFence = 150,
        PalmWood = 151,
        PalmWoodFence = 152,
        AmberGemspark = 153,
        AmethystGemspark = 154,
        DiamondGemspark = 155,
        EmeraldGemspark = 156,
        OfflineAmberGemspark = 157,
        OfflineAmethystGemspark = 158,
        OfflineDiamondGemspark = 159,
        OfflineEmeraldGemspark = 160,
        OfflineRubyGemspark = 161,
        OfflineSapphireGemspark = 162,
        OfflineTopazGemspark = 163,
        RubyGemspark = 164,
        SapphireGemspark = 165,
        TopazGemspark = 166,
        TinPlating = 167,
        Confetti = 168,
        MidnightConfetti = 169,
        NaturalCaveWall9 = 170,
        NaturalCaveWall10 = 171,
        Honeyfall = 172,
        ChlorophyteBrick = 173,
        CrimtaneBrick = 174,
        ShroomitePlating = 175,
        MartianConduit = 176,
        HellstoneBrick = 177,
        NaturalMarble = 178,
        SmoothMarble = 179,
        NaturalGranite = 180,
        SmoothGranite = 181,
        MeteoriteBrick = 182,
        Marble = 183,
        Granite = 184,
        NaturalCave8 = 185,
        CrystalBlock = 186,
        Sandstone = 187,
        NaturalCorruption1 = 188,
        NaturalCorruption2 = 189,
        NaturalCorruption3 = 190,
        NaturalCorruption4 = 191,
        NaturalCrimson1 = 192,
        NaturalCrimson2 = 193,
        NaturalCrimson3 = 194,
        NaturalCrimson4 = 195,
        NaturalDirt1 = 196,
        NaturalDirt2 = 197,
        NaturalDirt3 = 198,
        NaturalDirt4 = 199,
        NaturalHallow1 = 200,
        NaturalHallow2 = 201,
        NaturalHallow3 = 202,
        NaturalHallow4 = 203,
        NaturalJungle1 = 204,
        NaturalJungle2 = 205,
        NaturalJungle3 = 206,
        NaturalJungle4 = 207,
        NaturalLava1 = 208,
        NaturalLava2 = 209,
        NaturalLava3 = 210,
        NaturalLava4 = 211,
        NaturalRocks1 = 212,
        NaturalRocks2 = 213,
        NaturalRocks3 = 214,
        NaturalRocks4 = 215,
        NaturalHardenedSand = 216,
        NaturalHardenedEbonsand = 217,
        NaturalHardenedCrimsand = 218,
        NaturalHardenedPearlsand = 219,
        NaturalEbonsandstone = 220,
        NaturalCrimsandstone = 221,
        NaturalPearlsandstone = 222,
        DesertFossil = 223,
        LunarBrick = 224,
        Cog = 225,
        Sandfall = 226,
        Snowfall = 227,
        SillyPinkBalloon = 228,
        SillyPurpleBalloon = 229,
        SillyGreenBalloon = 230
#pragma warning restore 1591
    }
}
