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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Orion.World.Tiles {
    /// <summary>
    /// Represents a wall type.
    /// </summary>
    public sealed class WallType {
#pragma warning disable 1591
        public static readonly WallType None = new WallType(0);
        public static readonly WallType Stone = new WallType(1);
        public static readonly WallType NaturalDirt = new WallType(2);
        public static readonly WallType NaturalEbonstone = new WallType(3);
        public static readonly WallType Wood = new WallType(4);
        public static readonly WallType GrayBrick = new WallType(5);
        public static readonly WallType RedBrick = new WallType(6);
        public static readonly WallType NaturalBlueBrick = new WallType(7);
        public static readonly WallType NaturalGreenBrick = new WallType(8);
        public static readonly WallType NaturalPinkBrick = new WallType(9);
        public static readonly WallType GoldBrick = new WallType(10);
        public static readonly WallType SilverBrick = new WallType(11);
        public static readonly WallType CopperBrick = new WallType(12);
        public static readonly WallType NaturalHellstoneBrick = new WallType(13);
        public static readonly WallType NaturalObsidianBrick = new WallType(14);
        public static readonly WallType NaturalMud = new WallType(15);
        public static readonly WallType Dirt = new WallType(16);
        public static readonly WallType BlueBrick = new WallType(17);
        public static readonly WallType GreenBrick = new WallType(18);
        public static readonly WallType PinkBrick = new WallType(19);
        public static readonly WallType ObsidianBrick = new WallType(20);
        public static readonly WallType Glass = new WallType(21);
        public static readonly WallType PearlstoneBrick = new WallType(22);
        public static readonly WallType IridescentBrick = new WallType(23);
        public static readonly WallType MudstoneBrick = new WallType(24);
        public static readonly WallType CobaltBrick = new WallType(25);
        public static readonly WallType MythrilBrick = new WallType(26);
        public static readonly WallType Planked = new WallType(27);
        public static readonly WallType NaturalPearlstone = new WallType(28);
        public static readonly WallType CandyCane = new WallType(29);
        public static readonly WallType GreenCandyCane = new WallType(30);
        public static readonly WallType SnowBrick = new WallType(31);
        public static readonly WallType AdamantiteBeam = new WallType(32);
        public static readonly WallType DemoniteBrick = new WallType(33);
        public static readonly WallType SandstoneBrick = new WallType(34);
        public static readonly WallType EbonstoneBrick = new WallType(35);
        public static readonly WallType RedStucco = new WallType(36);
        public static readonly WallType YellowStucco = new WallType(37);
        public static readonly WallType GreenStucco = new WallType(38);
        public static readonly WallType Gray = new WallType(39);
        public static readonly WallType NaturalSnow = new WallType(40);
        public static readonly WallType Ebonwood = new WallType(41);
        public static readonly WallType RichMahogany = new WallType(42);
        public static readonly WallType Pearlwood = new WallType(43);
        public static readonly WallType RainbowBrick = new WallType(44);
        public static readonly WallType TinBrick = new WallType(45);
        public static readonly WallType TungstenBrick = new WallType(46);
        public static readonly WallType PlatinumBrick = new WallType(47);
        public static readonly WallType NaturalAmethyst = new WallType(48);
        public static readonly WallType NaturalTopaz = new WallType(49);
        public static readonly WallType NaturalSapphire = new WallType(50);
        public static readonly WallType NaturalEmerald = new WallType(51);
        public static readonly WallType NaturalRuby = new WallType(52);
        public static readonly WallType NaturalDiamond = new WallType(53);
        public static readonly WallType NaturalCave = new WallType(54);
        public static readonly WallType NaturalCave2 = new WallType(55);
        public static readonly WallType NaturalCave3 = new WallType(56);
        public static readonly WallType NaturalCave4 = new WallType(57);
        public static readonly WallType NaturalCave5 = new WallType(58);
        public static readonly WallType NaturalCave6 = new WallType(59);
        public static readonly WallType LivingLeaf = new WallType(60);
        public static readonly WallType NaturalCave7 = new WallType(61);
        public static readonly WallType NaturalSpider = new WallType(62);
        public static readonly WallType NaturalGrass = new WallType(63);
        public static readonly WallType NaturalJungle = new WallType(64);
        public static readonly WallType NaturalFlower = new WallType(65);
        public static readonly WallType Grass = new WallType(66);
        public static readonly WallType Jungle = new WallType(67);
        public static readonly WallType Flower = new WallType(68);
        public static readonly WallType NaturalCorruptGrass = new WallType(69);
        public static readonly WallType NaturalHallowedGrass = new WallType(70);
        public static readonly WallType NaturalIce = new WallType(71);
        public static readonly WallType Cactus = new WallType(72);
        public static readonly WallType Cloud = new WallType(73);
        public static readonly WallType Mushroom = new WallType(74);
        public static readonly WallType BoneBlock = new WallType(75);
        public static readonly WallType SlimeBlock = new WallType(76);
        public static readonly WallType FleshBlock = new WallType(77);
        public static readonly WallType LivingWood = new WallType(78);
        public static readonly WallType NaturalObsidianBack = new WallType(79);
        public static readonly WallType NaturalMushroom = new WallType(80);
        public static readonly WallType NaturalCrimsonGrass = new WallType(81);
        public static readonly WallType DiscWall = new WallType(82);
        public static readonly WallType NaturalCrimstone = new WallType(83);
        public static readonly WallType IceBrick = new WallType(84);
        public static readonly WallType Shadewood = new WallType(85);
        public static readonly WallType NaturalHive = new WallType(86);
        public static readonly WallType NaturalLihzahrdBrick = new WallType(87);
        public static readonly WallType PurpleStainedGlass = new WallType(88);
        public static readonly WallType YellowStainedGlass = new WallType(89);
        public static readonly WallType BlueStainedGlass = new WallType(90);
        public static readonly WallType GreenStainedGlass = new WallType(91);
        public static readonly WallType RedStainedGlass = new WallType(92);
        public static readonly WallType MulticoloredStainedGlass = new WallType(93);
        public static readonly WallType NaturalBlueSlab = new WallType(94);
        public static readonly WallType NaturalBlueTiled = new WallType(95);
        public static readonly WallType NaturalPinkSlab = new WallType(96);
        public static readonly WallType NaturalPinkTiled = new WallType(97);
        public static readonly WallType NaturalGreenSlab = new WallType(98);
        public static readonly WallType NaturalGreenTiled = new WallType(99);
        public static readonly WallType BlueSlab = new WallType(100);
        public static readonly WallType BlueTiled = new WallType(101);
        public static readonly WallType PinkSlab = new WallType(102);
        public static readonly WallType PinkTiled = new WallType(103);
        public static readonly WallType GreenSlab = new WallType(104);
        public static readonly WallType GreenTiled = new WallType(105);
        public static readonly WallType WoodenFence = new WallType(106);
        public static readonly WallType LeadFence = new WallType(107);
        public static readonly WallType Hive = new WallType(108);
        public static readonly WallType PalladiumColumn = new WallType(109);
        public static readonly WallType BubblegumBlock = new WallType(110);
        public static readonly WallType TitanstoneBlock = new WallType(111);
        public static readonly WallType LihzahrdBrick = new WallType(112);
        public static readonly WallType Pumpkin = new WallType(113);
        public static readonly WallType Hay = new WallType(114);
        public static readonly WallType SpookyWood = new WallType(115);
        public static readonly WallType ChristmasTreeWallpaper = new WallType(116);
        public static readonly WallType OrnamentWallpaper = new WallType(117);
        public static readonly WallType CandyCaneWallpaper = new WallType(118);
        public static readonly WallType FestiveWallpaper = new WallType(119);
        public static readonly WallType StarsWallpaper = new WallType(120);
        public static readonly WallType SquigglesWallpaper = new WallType(121);
        public static readonly WallType SnowflakeWallpaper = new WallType(122);
        public static readonly WallType KrampusHornWallpaper = new WallType(123);
        public static readonly WallType BluegreenWallpaper = new WallType(124);
        public static readonly WallType GrinchFingerWallpaper = new WallType(125);
        public static readonly WallType FancyGrayWallpaper = new WallType(126);
        public static readonly WallType IceFloeWallpaper = new WallType(127);
        public static readonly WallType MusicWallpaper = new WallType(128);
        public static readonly WallType PurpleRainWallpaper = new WallType(129);
        public static readonly WallType RainbowWallpaper = new WallType(130);
        public static readonly WallType SparkleStoneWallpaper = new WallType(131);
        public static readonly WallType StarlitHeavenWallpaper = new WallType(132);
        public static readonly WallType BubbleWallpaper = new WallType(133);
        public static readonly WallType CopperPipeWallpaper = new WallType(134);
        public static readonly WallType DuckyWallpaper = new WallType(135);
        public static readonly WallType Waterfall = new WallType(136);
        public static readonly WallType Lavafall = new WallType(137);
        public static readonly WallType EbonwoodFence = new WallType(138);
        public static readonly WallType RichMahoganyFence = new WallType(139);
        public static readonly WallType PearlwoodFence = new WallType(140);
        public static readonly WallType ShadewoodFence = new WallType(141);
        public static readonly WallType WhiteDynasty = new WallType(142);
        public static readonly WallType BlueDynasty = new WallType(143);
        public static readonly WallType ArcaneRune = new WallType(144);
        public static readonly WallType IronFence = new WallType(145);
        public static readonly WallType CopperPlating = new WallType(146);
        public static readonly WallType StoneSlab = new WallType(147);
        public static readonly WallType Sail = new WallType(148);
        public static readonly WallType BorealWood = new WallType(149);
        public static readonly WallType BorealWoodFence = new WallType(150);
        public static readonly WallType PalmWood = new WallType(151);
        public static readonly WallType PalmWoodFence = new WallType(152);
        public static readonly WallType AmberGemspark = new WallType(153);
        public static readonly WallType AmethystGemspark = new WallType(154);
        public static readonly WallType DiamondGemspark = new WallType(155);
        public static readonly WallType EmeraldGemspark = new WallType(156);
        public static readonly WallType OfflineAmberGemspark = new WallType(157);
        public static readonly WallType OfflineAmethystGemspark = new WallType(158);
        public static readonly WallType OfflineDiamondGemspark = new WallType(159);
        public static readonly WallType OfflineEmeraldGemspark = new WallType(160);
        public static readonly WallType OfflineRubyGemspark = new WallType(161);
        public static readonly WallType OfflineSapphireGemspark = new WallType(162);
        public static readonly WallType OfflineTopazGemspark = new WallType(163);
        public static readonly WallType RubyGemspark = new WallType(164);
        public static readonly WallType SapphireGemspark = new WallType(165);
        public static readonly WallType TopazGemspark = new WallType(166);
        public static readonly WallType TinPlating = new WallType(167);
        public static readonly WallType Confetti = new WallType(168);
        public static readonly WallType MidnightConfetti = new WallType(169);
        public static readonly WallType NaturalCaveWall9 = new WallType(170);
        public static readonly WallType NaturalCaveWall10 = new WallType(171);
        public static readonly WallType Honeyfall = new WallType(172);
        public static readonly WallType ChlorophyteBrick = new WallType(173);
        public static readonly WallType CrimtaneBrick = new WallType(174);
        public static readonly WallType ShroomitePlating = new WallType(175);
        public static readonly WallType MartianConduit = new WallType(176);
        public static readonly WallType HellstoneBrick = new WallType(177);
        public static readonly WallType NaturalMarble = new WallType(178);
        public static readonly WallType SmoothMarble = new WallType(179);
        public static readonly WallType NaturalGranite = new WallType(180);
        public static readonly WallType SmoothGranite = new WallType(181);
        public static readonly WallType MeteoriteBrick = new WallType(182);
        public static readonly WallType Marble = new WallType(183);
        public static readonly WallType Granite = new WallType(184);
        public static readonly WallType NaturalCave8 = new WallType(185);
        public static readonly WallType CrystalBlock = new WallType(186);
        public static readonly WallType Sandstone = new WallType(187);
        public static readonly WallType NaturalCorruption1 = new WallType(188);
        public static readonly WallType NaturalCorruption2 = new WallType(189);
        public static readonly WallType NaturalCorruption3 = new WallType(190);
        public static readonly WallType NaturalCorruption4 = new WallType(191);
        public static readonly WallType NaturalCrimson1 = new WallType(192);
        public static readonly WallType NaturalCrimson2 = new WallType(193);
        public static readonly WallType NaturalCrimson3 = new WallType(194);
        public static readonly WallType NaturalCrimson4 = new WallType(195);
        public static readonly WallType NaturalDirt1 = new WallType(196);
        public static readonly WallType NaturalDirt2 = new WallType(197);
        public static readonly WallType NaturalDirt3 = new WallType(198);
        public static readonly WallType NaturalDirt4 = new WallType(199);
        public static readonly WallType NaturalHallow1 = new WallType(200);
        public static readonly WallType NaturalHallow2 = new WallType(201);
        public static readonly WallType NaturalHallow3 = new WallType(202);
        public static readonly WallType NaturalHallow4 = new WallType(203);
        public static readonly WallType NaturalJungle1 = new WallType(204);
        public static readonly WallType NaturalJungle2 = new WallType(205);
        public static readonly WallType NaturalJungle3 = new WallType(206);
        public static readonly WallType NaturalJungle4 = new WallType(207);
        public static readonly WallType NaturalLava1 = new WallType(208);
        public static readonly WallType NaturalLava2 = new WallType(209);
        public static readonly WallType NaturalLava3 = new WallType(210);
        public static readonly WallType NaturalLava4 = new WallType(211);
        public static readonly WallType NaturalRocks1 = new WallType(212);
        public static readonly WallType NaturalRocks2 = new WallType(213);
        public static readonly WallType NaturalRocks3 = new WallType(214);
        public static readonly WallType NaturalRocks4 = new WallType(215);
        public static readonly WallType NaturalHardenedSand = new WallType(216);
        public static readonly WallType NaturalHardenedEbonsand = new WallType(217);
        public static readonly WallType NaturalHardenedCrimsand = new WallType(218);
        public static readonly WallType NaturalHardenedPearlsand = new WallType(219);
        public static readonly WallType NaturalEbonsandstone = new WallType(220);
        public static readonly WallType NaturalCrimsandstone = new WallType(221);
        public static readonly WallType NaturalPearlsandstone = new WallType(222);
        public static readonly WallType DesertFossil = new WallType(223);
        public static readonly WallType LunarBrick = new WallType(224);
        public static readonly WallType Cog = new WallType(225);
        public static readonly WallType Sandfall = new WallType(226);
        public static readonly WallType Snowfall = new WallType(227);
        public static readonly WallType SillyPinkBalloon = new WallType(228);
        public static readonly WallType SillyPurpleBalloon = new WallType(229);
        public static readonly WallType SillyGreenBalloon = new WallType(230);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + Terraria.ID.WallID.Count;
        private static readonly WallType[] Walls = new WallType[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        private static readonly bool[] SafeForHouse = new Terraria.ID.SetFactory(ArraySize).CreateBoolSet(
            1, 4, 5, 6, 10, 11, 12, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 29, 30, 31, 32, 33, 34, 35, 36, 37,
            38, 39, 41, 42, 43, 44, 45, 46, 47, 60, 66, 67, 68, 72, 73, 74, 75, 76, 77, 78, 82, 84, 85, 88, 89, 90, 91,
            92, 93, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119,
            120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140,
            141, 142, 143, 144, 146, 147, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163,
            164, 165, 166, 167, 168, 169, 172, 173, 174, 175, 176, 177, 179, 181, 182, 183, 184, 186, 223, 224, 225,
            226, 227, 228, 229, 230);

        /// <summary>
        /// Gets the wall type's ID.
        /// </summary>
        public byte Id { get; }

        /// <summary>
        /// Gets a value indicating whether the wall type is safe for use as a house.
        /// </summary>
        public bool IsSafeForHouse => SafeForHouse[Id];

        private WallType(byte id) {
            Id = id;
        }

        static WallType() {
            foreach (var field in typeof(WallType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var wallType = (WallType)field.GetValue(null);
                Walls[ArrayOffset + wallType.Id] = wallType;
                Names[ArrayOffset + wallType.Id] = field.Name;
            }
        }

        /// <summary>
        /// Returns a wall type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The wall type, or <c>null</c> if none exists.</returns>
        public static WallType FromId(byte id) => ArrayOffset + id < ArraySize ? Walls[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
