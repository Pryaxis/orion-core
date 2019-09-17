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

namespace Orion.World {
    /// <summary>
    /// Represents a block type.
    /// </summary>
    public sealed class BlockType {
#pragma warning disable 1591
        public static readonly BlockType Dirt = new BlockType(0);
        public static readonly BlockType Stone = new BlockType(1);
        public static readonly BlockType Grass = new BlockType(2);
        public static readonly BlockType ShortGrassPlants = new BlockType(3);
        public static readonly BlockType Torches = new BlockType(4);
        public static readonly BlockType Trees = new BlockType(5);
        public static readonly BlockType IronOre = new BlockType(6);
        public static readonly BlockType CopperOre = new BlockType(7);
        public static readonly BlockType GoldOre = new BlockType(8);
        public static readonly BlockType SilverOre = new BlockType(9);
        public static readonly BlockType ClosedDoors = new BlockType(10);
        public static readonly BlockType OpenDoors = new BlockType(11);
        public static readonly BlockType CrystalHeart = new BlockType(12);
        public static readonly BlockType Bottles = new BlockType(13);
        public static readonly BlockType Tables = new BlockType(14);
        public static readonly BlockType Chairs = new BlockType(15);
        public static readonly BlockType Anvils = new BlockType(16);
        public static readonly BlockType Furnace = new BlockType(17);
        public static readonly BlockType WorkBenches = new BlockType(18);
        public static readonly BlockType Platforms = new BlockType(19);
        public static readonly BlockType Saplings = new BlockType(20);
        public static readonly BlockType Chests = new BlockType(21);
        public static readonly BlockType DemoniteOre = new BlockType(22);
        public static readonly BlockType CorruptGrass = new BlockType(23);
        public static readonly BlockType ShortCorruptionPlants = new BlockType(24);
        public static readonly BlockType Ebonstone = new BlockType(25);
        public static readonly BlockType Altars = new BlockType(26);
        public static readonly BlockType Sunflower = new BlockType(27);
        public static readonly BlockType Pot = new BlockType(28);
        public static readonly BlockType PiggyBank = new BlockType(29);
        public static readonly BlockType Wood = new BlockType(30);
        public static readonly BlockType ShadowOrbs = new BlockType(31);
        public static readonly BlockType CorruptionThorn = new BlockType(32);
        public static readonly BlockType Candles = new BlockType(33);
        public static readonly BlockType Chandeliers = new BlockType(34);
        public static readonly BlockType JackOLantern = new BlockType(35);
        public static readonly BlockType Present = new BlockType(36);
        public static readonly BlockType Meteorite = new BlockType(37);
        public static readonly BlockType GrayBrick = new BlockType(38);
        public static readonly BlockType RedBrick = new BlockType(39);
        public static readonly BlockType Clay = new BlockType(40);
        public static readonly BlockType BlueBrick = new BlockType(41);
        public static readonly BlockType Lanterns = new BlockType(42);
        public static readonly BlockType GreenBrick = new BlockType(43);
        public static readonly BlockType PinkBrick = new BlockType(44);
        public static readonly BlockType GoldBrick = new BlockType(45);
        public static readonly BlockType SilverBrick = new BlockType(46);
        public static readonly BlockType CopperBrick = new BlockType(47);
        public static readonly BlockType Spike = new BlockType(48);
        public static readonly BlockType WaterCandle = new BlockType(49);
        public static readonly BlockType Book = new BlockType(50);
        public static readonly BlockType Cobweb = new BlockType(51);
        public static readonly BlockType Vine = new BlockType(52);
        public static readonly BlockType Sand = new BlockType(53);
        public static readonly BlockType Glass = new BlockType(54);
        public static readonly BlockType Sign = new BlockType(55);
        public static readonly BlockType Obsidian = new BlockType(56);
        public static readonly BlockType Ash = new BlockType(57);
        public static readonly BlockType Hellstone = new BlockType(58);
        public static readonly BlockType Mud = new BlockType(59);
        public static readonly BlockType JungleGrass = new BlockType(60);
        public static readonly BlockType ShortJunglePlants = new BlockType(61);
        public static readonly BlockType JungleVine = new BlockType(62);
        public static readonly BlockType SapphireOre = new BlockType(63);
        public static readonly BlockType RubyOre = new BlockType(64);
        public static readonly BlockType EmeraldOre = new BlockType(65);
        public static readonly BlockType TopazOre = new BlockType(66);
        public static readonly BlockType AmethystOre = new BlockType(67);
        public static readonly BlockType DiamondOre = new BlockType(68);
        public static readonly BlockType JungleThorn = new BlockType(69);
        public static readonly BlockType MushroomGrass = new BlockType(70);
        public static readonly BlockType MushroomPlants = new BlockType(71);
        public static readonly BlockType MushroomTrees = new BlockType(72);
        public static readonly BlockType TallGrassPlants = new BlockType(73);
        public static readonly BlockType TallJunglePlants = new BlockType(74);
        public static readonly BlockType ObsidianBrick = new BlockType(75);
        public static readonly BlockType HellstoneBrick = new BlockType(76);
        public static readonly BlockType Hellforge = new BlockType(77);
        public static readonly BlockType ClayPot = new BlockType(78);
        public static readonly BlockType Beds = new BlockType(79);
        public static readonly BlockType CactusPlant = new BlockType(80);
        public static readonly BlockType Coral = new BlockType(81);
        public static readonly BlockType GrowingHerbs = new BlockType(82);
        public static readonly BlockType MatureHerbs = new BlockType(83);
        public static readonly BlockType BloomingHerbs = new BlockType(84);
        public static readonly BlockType Tombstones = new BlockType(85);
        public static readonly BlockType Loom = new BlockType(86);
        public static readonly BlockType Pianos = new BlockType(87);
        public static readonly BlockType Dressers = new BlockType(88);
        public static readonly BlockType Benches = new BlockType(89);
        public static readonly BlockType Bathtubs = new BlockType(90);
        public static readonly BlockType Banners = new BlockType(91);
        public static readonly BlockType LampPost = new BlockType(92);
        public static readonly BlockType Lamps = new BlockType(93);
        public static readonly BlockType Keg = new BlockType(94);
        public static readonly BlockType ChineseLantern = new BlockType(95);
        public static readonly BlockType CookingPots = new BlockType(96);
        public static readonly BlockType Safe = new BlockType(97);
        public static readonly BlockType SkullLantern = new BlockType(98);
        public static readonly BlockType Candelabras = new BlockType(100);
        public static readonly BlockType Bookcases = new BlockType(101);
        public static readonly BlockType Throne = new BlockType(102);
        public static readonly BlockType Bowls = new BlockType(103);
        public static readonly BlockType Clocks = new BlockType(104);
        public static readonly BlockType Statues = new BlockType(105);
        public static readonly BlockType Sawmill = new BlockType(106);
        public static readonly BlockType CobaltOre = new BlockType(107);
        public static readonly BlockType MythrilOre = new BlockType(108);
        public static readonly BlockType HallowedGrass = new BlockType(109);
        public static readonly BlockType ShortHallowedPlants = new BlockType(110);
        public static readonly BlockType AdamantiteOre = new BlockType(111);
        public static readonly BlockType Ebonsand = new BlockType(112);
        public static readonly BlockType TallHallowedPlants = new BlockType(113);
        public static readonly BlockType TinkerersWorkshop = new BlockType(114);
        public static readonly BlockType HallowedVine = new BlockType(115);
        public static readonly BlockType Pearlsand = new BlockType(116);
        public static readonly BlockType Pearlstone = new BlockType(117);
        public static readonly BlockType PearlstoneBrick = new BlockType(118);
        public static readonly BlockType IridescentBrick = new BlockType(119);
        public static readonly BlockType MudstoneBrick = new BlockType(120);
        public static readonly BlockType CobaltBrick = new BlockType(121);
        public static readonly BlockType MythrilBrick = new BlockType(122);
        public static readonly BlockType Silt = new BlockType(123);
        public static readonly BlockType WoodenBeam = new BlockType(124);
        public static readonly BlockType CrystalBall = new BlockType(125);
        public static readonly BlockType DiscoBall = new BlockType(126);
        public static readonly BlockType IceRod = new BlockType(127);
        public static readonly BlockType Mannequin = new BlockType(128);
        public static readonly BlockType CrystalShard = new BlockType(129);
        public static readonly BlockType ActiveStone = new BlockType(130);
        public static readonly BlockType InactiveStone = new BlockType(131);
        public static readonly BlockType Lever = new BlockType(132);
        public static readonly BlockType Forges = new BlockType(133);
        public static readonly BlockType HardmodeAnvils = new BlockType(134);
        public static readonly BlockType PressurePlates = new BlockType(135);
        public static readonly BlockType Switch = new BlockType(136);
        public static readonly BlockType Traps = new BlockType(137);
        public static readonly BlockType Boulder = new BlockType(138);
        public static readonly BlockType MusicBoxes = new BlockType(139);
        public static readonly BlockType DemoniteBrick = new BlockType(140);
        public static readonly BlockType Explosives = new BlockType(141);
        public static readonly BlockType InletPump = new BlockType(142);
        public static readonly BlockType OutletPump = new BlockType(143);
        public static readonly BlockType Timers = new BlockType(144);
        public static readonly BlockType CandyCane = new BlockType(145);
        public static readonly BlockType GreenCandyCane = new BlockType(146);
        public static readonly BlockType Snow = new BlockType(147);
        public static readonly BlockType SnowBrick = new BlockType(148);
        public static readonly BlockType ChristmasLights = new BlockType(149);
        public static readonly BlockType AdamantiteBeam = new BlockType(150);
        public static readonly BlockType SandstoneBrick = new BlockType(151);
        public static readonly BlockType EbonstoneBrick = new BlockType(152);
        public static readonly BlockType RedStucco = new BlockType(153);
        public static readonly BlockType YellowStucco = new BlockType(154);
        public static readonly BlockType GreenStucco = new BlockType(155);
        public static readonly BlockType GrayStucco = new BlockType(156);
        public static readonly BlockType Ebonwood = new BlockType(157);
        public static readonly BlockType RichMahogany = new BlockType(158);
        public static readonly BlockType Pearlwood = new BlockType(159);
        public static readonly BlockType RainbowBrick = new BlockType(160);
        public static readonly BlockType Ice = new BlockType(161);
        public static readonly BlockType ThinIce = new BlockType(162);
        public static readonly BlockType PurpleIce = new BlockType(163);
        public static readonly BlockType PinkIce = new BlockType(164);
        public static readonly BlockType AmbientObjects = new BlockType(165);
        public static readonly BlockType TinOre = new BlockType(166);
        public static readonly BlockType LeadOre = new BlockType(167);
        public static readonly BlockType TungstenOre = new BlockType(168);
        public static readonly BlockType PlatinumOre = new BlockType(169);
        public static readonly BlockType PineTree = new BlockType(170);
        public static readonly BlockType ChristmasTree = new BlockType(171);
        public static readonly BlockType Sinks = new BlockType(172);
        public static readonly BlockType PlatinumCandelabra = new BlockType(173);
        public static readonly BlockType PlatinumCandle = new BlockType(174);
        public static readonly BlockType TinBrick = new BlockType(175);
        public static readonly BlockType TungstenBrick = new BlockType(176);
        public static readonly BlockType PlatinumBrick = new BlockType(177);
        public static readonly BlockType Gems = new BlockType(178);
        public static readonly BlockType TealMoss = new BlockType(179);
        public static readonly BlockType ChartreuseMoss = new BlockType(180);
        public static readonly BlockType RedMoss = new BlockType(181);
        public static readonly BlockType BlueMoss = new BlockType(182);
        public static readonly BlockType PurpleMoss = new BlockType(183);
        public static readonly BlockType MossGrowth = new BlockType(184);
        public static readonly BlockType SmallAmbientObjects = new BlockType(185);
        public static readonly BlockType LargeAmbientObjects = new BlockType(186);
        public static readonly BlockType LargeAmbientObjects2 = new BlockType(187);
        public static readonly BlockType Cactus = new BlockType(188);
        public static readonly BlockType Cloud = new BlockType(189);
        public static readonly BlockType GlowingMushroom = new BlockType(190);
        public static readonly BlockType LivingWood = new BlockType(191);
        public static readonly BlockType Leaf = new BlockType(192);
        public static readonly BlockType Slime = new BlockType(193);
        public static readonly BlockType Bone = new BlockType(194);
        public static readonly BlockType Flesh = new BlockType(195);
        public static readonly BlockType RainCloud = new BlockType(196);
        public static readonly BlockType FrozenSlime = new BlockType(197);
        public static readonly BlockType Asphalt = new BlockType(198);
        public static readonly BlockType CrimsonGrass = new BlockType(199);
        public static readonly BlockType RedIce = new BlockType(200);
        public static readonly BlockType ShortCrimsonPlants = new BlockType(201);
        public static readonly BlockType Sunplate = new BlockType(202);
        public static readonly BlockType Crimstone = new BlockType(203);
        public static readonly BlockType CrimtaneOre = new BlockType(204);
        public static readonly BlockType CrimsonVine = new BlockType(205);
        public static readonly BlockType IceBrick = new BlockType(206);
        public static readonly BlockType WaterFountains = new BlockType(207);
        public static readonly BlockType Shadewood = new BlockType(208);
        public static readonly BlockType Cannons = new BlockType(209);
        public static readonly BlockType LandMine = new BlockType(210);
        public static readonly BlockType ChlorophyteOre = new BlockType(211);
        public static readonly BlockType SnowballLauncher = new BlockType(212);
        public static readonly BlockType Rope = new BlockType(213);
        public static readonly BlockType Chain = new BlockType(214);
        public static readonly BlockType Campfires = new BlockType(215);
        public static readonly BlockType Rockets = new BlockType(216);
        public static readonly BlockType BlendOMatic = new BlockType(217);
        public static readonly BlockType MeatGrinder = new BlockType(218);
        public static readonly BlockType Extractinator = new BlockType(219);
        public static readonly BlockType Solidifier = new BlockType(220);
        public static readonly BlockType PalladiumOre = new BlockType(221);
        public static readonly BlockType OrichalcumOre = new BlockType(222);
        public static readonly BlockType TitaniumOre = new BlockType(223);
        public static readonly BlockType Slush = new BlockType(224);
        public static readonly BlockType Hive = new BlockType(225);
        public static readonly BlockType LihzahrdBrick = new BlockType(226);
        public static readonly BlockType DyePlants = new BlockType(227);
        public static readonly BlockType DyeVat = new BlockType(228);
        public static readonly BlockType Honey = new BlockType(229);
        public static readonly BlockType CrispyHoney = new BlockType(230);
        public static readonly BlockType Larva = new BlockType(231);
        public static readonly BlockType WoodenSpike = new BlockType(232);
        public static readonly BlockType PlantDetritus = new BlockType(233);
        public static readonly BlockType Crimsand = new BlockType(234);
        public static readonly BlockType Teleporter = new BlockType(235);
        public static readonly BlockType LifeFruit = new BlockType(236);
        public static readonly BlockType LihzahrdAltar = new BlockType(237);
        public static readonly BlockType PlanterasBulb = new BlockType(238);
        public static readonly BlockType MetalBars = new BlockType(239);
        public static readonly BlockType Paintings3x3 = new BlockType(240);
        public static readonly BlockType Paintings4x3 = new BlockType(241);
        public static readonly BlockType Paintings6x4 = new BlockType(242);
        public static readonly BlockType ImbuingStation = new BlockType(243);
        public static readonly BlockType BubbleMachine = new BlockType(244);
        public static readonly BlockType Paintings2x3 = new BlockType(245);
        public static readonly BlockType Paintings3x2 = new BlockType(246);
        public static readonly BlockType Autohammer = new BlockType(247);
        public static readonly BlockType PalladiumColumn = new BlockType(248);
        public static readonly BlockType Bubblegum = new BlockType(249);
        public static readonly BlockType Titanstone = new BlockType(250);
        public static readonly BlockType Pumpkin = new BlockType(251);
        public static readonly BlockType Hay = new BlockType(252);
        public static readonly BlockType SpookyWood = new BlockType(253);
        public static readonly BlockType PumpkinPlants = new BlockType(254);
        public static readonly BlockType OfflineAmethystGemspark = new BlockType(255);
        public static readonly BlockType OfflineTopazGemspark = new BlockType(256);
        public static readonly BlockType OfflineSapphireGemspark = new BlockType(257);
        public static readonly BlockType OfflineEmeraldGemspark = new BlockType(258);
        public static readonly BlockType OfflineRubyGemspark = new BlockType(259);
        public static readonly BlockType OfflineDiamondGemspark = new BlockType(260);
        public static readonly BlockType OfflineAmberGemspark = new BlockType(261);
        public static readonly BlockType AmethystGemspark = new BlockType(262);
        public static readonly BlockType TopazGemspark = new BlockType(263);
        public static readonly BlockType SapphireGemspark = new BlockType(264);
        public static readonly BlockType EmeraldGemspark = new BlockType(265);
        public static readonly BlockType RubyGemspark = new BlockType(266);
        public static readonly BlockType DiamondGemspark = new BlockType(267);
        public static readonly BlockType AmberGemspark = new BlockType(268);
        public static readonly BlockType Womannequin = new BlockType(269);
        public static readonly BlockType FireflyInABottle = new BlockType(270);
        public static readonly BlockType LightningBugInABottle = new BlockType(271);
        public static readonly BlockType Cog = new BlockType(272);
        public static readonly BlockType StoneSlab = new BlockType(273);
        public static readonly BlockType SandstoneSlab = new BlockType(274);
        public static readonly BlockType BunnyCage = new BlockType(275);
        public static readonly BlockType SquirrelCage = new BlockType(276);
        public static readonly BlockType MallardDuckCage = new BlockType(277);
        public static readonly BlockType DuckCage = new BlockType(278);
        public static readonly BlockType BirdCage = new BlockType(279);
        public static readonly BlockType BlueJayCage = new BlockType(280);
        public static readonly BlockType CardinalCage = new BlockType(281);
        public static readonly BlockType FishBowl = new BlockType(282);
        public static readonly BlockType HeavyWorkBench = new BlockType(283);
        public static readonly BlockType CopperPlating = new BlockType(284);
        public static readonly BlockType SnailCage = new BlockType(285);
        public static readonly BlockType GlowingSnailCage = new BlockType(286);
        public static readonly BlockType AmmoBox = new BlockType(287);
        public static readonly BlockType MonarchButterflyJar = new BlockType(288);
        public static readonly BlockType PurpleEmperorButterflyJar = new BlockType(289);
        public static readonly BlockType RedAdmiralButterflyJar = new BlockType(290);
        public static readonly BlockType UlyssesButterflyJar = new BlockType(291);
        public static readonly BlockType SulphurButterflyJar = new BlockType(292);
        public static readonly BlockType TreeNymphButterflyJar = new BlockType(293);
        public static readonly BlockType ZebraSwallowtailButterflyJar = new BlockType(294);
        public static readonly BlockType JuliaButterflyJar = new BlockType(295);
        public static readonly BlockType ScorpionCage = new BlockType(296);
        public static readonly BlockType BlackScorpionCage = new BlockType(297);
        public static readonly BlockType FrogCage = new BlockType(298);
        public static readonly BlockType MouseCage = new BlockType(299);
        public static readonly BlockType BoneWelder = new BlockType(300);
        public static readonly BlockType FleshCloningVat = new BlockType(301);
        public static readonly BlockType GlassKiln = new BlockType(302);
        public static readonly BlockType LihzahrdFurnace = new BlockType(303);
        public static readonly BlockType LivingLoom = new BlockType(304);
        public static readonly BlockType SkyMill = new BlockType(305);
        public static readonly BlockType IceMachine = new BlockType(306);
        public static readonly BlockType SteampunkBoiler = new BlockType(307);
        public static readonly BlockType HoneyDispenser = new BlockType(308);
        public static readonly BlockType PenguinCage = new BlockType(309);
        public static readonly BlockType WormCage = new BlockType(310);
        public static readonly BlockType DynastyWood = new BlockType(311);
        public static readonly BlockType RedDynastyShingles = new BlockType(312);
        public static readonly BlockType BlueDynastyShingles = new BlockType(313);
        public static readonly BlockType MinecartTracks = new BlockType(314);
        public static readonly BlockType Coralstone = new BlockType(315);
        public static readonly BlockType BlueJellyfishJar = new BlockType(316);
        public static readonly BlockType GreenJellyfishJar = new BlockType(317);
        public static readonly BlockType PinkJellyfishJar = new BlockType(318);
        public static readonly BlockType ShipInABottle = new BlockType(319);
        public static readonly BlockType SeaweedPlanter = new BlockType(320);
        public static readonly BlockType BorealWood = new BlockType(321);
        public static readonly BlockType PalmWood = new BlockType(322);
        public static readonly BlockType PalmTree = new BlockType(323);
        public static readonly BlockType BeachPiles = new BlockType(324);
        public static readonly BlockType TinPlating = new BlockType(325);
        public static readonly BlockType Waterfall = new BlockType(326);
        public static readonly BlockType Lavafall = new BlockType(327);
        public static readonly BlockType Confetti = new BlockType(328);
        public static readonly BlockType MidnightConfetti = new BlockType(329);
        public static readonly BlockType CopperCoin = new BlockType(330);
        public static readonly BlockType SilverCoin = new BlockType(331);
        public static readonly BlockType GoldCoin = new BlockType(332);
        public static readonly BlockType PlatinumCoin = new BlockType(333);
        public static readonly BlockType WeaponRack = new BlockType(334);
        public static readonly BlockType FireworksBox = new BlockType(335);
        public static readonly BlockType LivingFire = new BlockType(336);
        public static readonly BlockType LetterStatues = new BlockType(337);
        public static readonly BlockType FireworkFountain = new BlockType(338);
        public static readonly BlockType GrasshopperCage = new BlockType(339);
        public static readonly BlockType LivingCursedFire = new BlockType(340);
        public static readonly BlockType LivingDemonFire = new BlockType(341);
        public static readonly BlockType LivingFrostFire = new BlockType(342);
        public static readonly BlockType LivingIchor = new BlockType(343);
        public static readonly BlockType LivingUltrabrightFire = new BlockType(344);
        public static readonly BlockType Honeyfall = new BlockType(345);
        public static readonly BlockType ChlorophyteBrick = new BlockType(346);
        public static readonly BlockType CrimtaneBrick = new BlockType(347);
        public static readonly BlockType ShroomitePlating = new BlockType(348);
        public static readonly BlockType MushroomStatue = new BlockType(349);
        public static readonly BlockType MartianConduitPlating = new BlockType(350);
        public static readonly BlockType Smoke = new BlockType(351);
        public static readonly BlockType CrimtaneThorn = new BlockType(352);
        public static readonly BlockType VineRope = new BlockType(353);
        public static readonly BlockType BewitchingTable = new BlockType(354);
        public static readonly BlockType AlchemyTable = new BlockType(355);
        public static readonly BlockType EnchantedSundial = new BlockType(356);
        public static readonly BlockType SmoothMarble = new BlockType(357);
        public static readonly BlockType GoldBirdCage = new BlockType(358);
        public static readonly BlockType GoldBunnyCage = new BlockType(359);
        public static readonly BlockType GoldButterflyJar = new BlockType(360);
        public static readonly BlockType GoldFrogCage = new BlockType(361);
        public static readonly BlockType GoldGrasshopperCage = new BlockType(362);
        public static readonly BlockType GoldMouseCage = new BlockType(363);
        public static readonly BlockType GoldWormCage = new BlockType(364);
        public static readonly BlockType SilkRope = new BlockType(365);
        public static readonly BlockType WebRope = new BlockType(366);
        public static readonly BlockType Marble = new BlockType(367);
        public static readonly BlockType Granite = new BlockType(368);
        public static readonly BlockType SmoothGranite = new BlockType(369);
        public static readonly BlockType MeteoriteBrick = new BlockType(370);
        public static readonly BlockType PinkSlime = new BlockType(371);
        public static readonly BlockType PeaceCandle = new BlockType(372);
        public static readonly BlockType MagicWaterDropper = new BlockType(373);
        public static readonly BlockType MagicLavaDropper = new BlockType(374);
        public static readonly BlockType MagicHoneyDropper = new BlockType(375);
        public static readonly BlockType Crates = new BlockType(376);
        public static readonly BlockType SharpeningStation = new BlockType(377);
        public static readonly BlockType TargetDummy = new BlockType(378);
        public static readonly BlockType Bubble = new BlockType(379);
        public static readonly BlockType PlanterBoxes = new BlockType(380);
        public static readonly BlockType FireMoss = new BlockType(381);
        public static readonly BlockType VineFlowers = new BlockType(382);
        public static readonly BlockType LivingMahogany = new BlockType(383);
        public static readonly BlockType MahoganyLeaf = new BlockType(384);
        public static readonly BlockType Crystal = new BlockType(385);
        public static readonly BlockType OpenTrapDoor = new BlockType(386);
        public static readonly BlockType ClosedTrapDoor = new BlockType(387);
        public static readonly BlockType ClosedTallGate = new BlockType(388);
        public static readonly BlockType OpenTallGate = new BlockType(389);
        public static readonly BlockType LavaLamp = new BlockType(390);
        public static readonly BlockType EnchantedNightcrawlerCage = new BlockType(391);
        public static readonly BlockType BuggyCage = new BlockType(392);
        public static readonly BlockType GrubbyCage = new BlockType(393);
        public static readonly BlockType SluggyCage = new BlockType(394);
        public static readonly BlockType ItemFrame = new BlockType(395);
        public static readonly BlockType Sandstone = new BlockType(396);
        public static readonly BlockType HardenedSand = new BlockType(397);
        public static readonly BlockType HardenedEbonsand = new BlockType(398);
        public static readonly BlockType HardenedCrimsand = new BlockType(399);
        public static readonly BlockType Ebonsandstone = new BlockType(400);
        public static readonly BlockType Crimsandstone = new BlockType(401);
        public static readonly BlockType HardenedPearlsand = new BlockType(402);
        public static readonly BlockType Pearlsandstone = new BlockType(403);
        public static readonly BlockType DesertFossil = new BlockType(404);
        public static readonly BlockType Fireplace = new BlockType(405);
        public static readonly BlockType Chimney = new BlockType(406);
        public static readonly BlockType SturdyFossil = new BlockType(407);
        public static readonly BlockType Luminite = new BlockType(408);
        public static readonly BlockType LuminiteBrick = new BlockType(409);
        public static readonly BlockType Monoliths = new BlockType(410);
        public static readonly BlockType Detonator = new BlockType(411);
        public static readonly BlockType AncientManipulator = new BlockType(412);
        public static readonly BlockType RedSquirrelCage = new BlockType(413);
        public static readonly BlockType GoldSquirrelCage = new BlockType(414);
        public static readonly BlockType SolarFragment = new BlockType(415);
        public static readonly BlockType VortexFragment = new BlockType(416);
        public static readonly BlockType NebulaFragment = new BlockType(417);
        public static readonly BlockType StardustFragment = new BlockType(418);
        public static readonly BlockType LogicGateLamps = new BlockType(419);
        public static readonly BlockType LogicGates = new BlockType(420);
        public static readonly BlockType ConveyorBeltClockwise = new BlockType(421);
        public static readonly BlockType ConveyorBeltCounterClockwise = new BlockType(422);
        public static readonly BlockType LogicSensors = new BlockType(423);
        public static readonly BlockType JunctionBox = new BlockType(424);
        public static readonly BlockType AnnouncementBox = new BlockType(425);
        public static readonly BlockType RedTeam = new BlockType(426);
        public static readonly BlockType RedTeamPlatform = new BlockType(427);
        public static readonly BlockType WeightedPressurePlates = new BlockType(428);
        public static readonly BlockType WireBulb = new BlockType(429);
        public static readonly BlockType GreenTeam = new BlockType(430);
        public static readonly BlockType BlueTeam = new BlockType(431);
        public static readonly BlockType YellowTeam = new BlockType(432);
        public static readonly BlockType PinkTeam = new BlockType(433);
        public static readonly BlockType WhiteTeam = new BlockType(434);
        public static readonly BlockType GreenTeamPlatform = new BlockType(435);
        public static readonly BlockType BlueTeamPlatform = new BlockType(436);
        public static readonly BlockType YellowTeamPlatform = new BlockType(437);
        public static readonly BlockType PinkTeamPlatform = new BlockType(438);
        public static readonly BlockType WhiteTeamPlatform = new BlockType(439);
        public static readonly BlockType GemLocks = new BlockType(440);
        public static readonly BlockType TrappedChests = new BlockType(441);
        public static readonly BlockType TealPressurePad = new BlockType(442);
        public static readonly BlockType Geyser = new BlockType(443);
        public static readonly BlockType Beehive = new BlockType(444);
        public static readonly BlockType PixelBox = new BlockType(445);
        public static readonly BlockType SillyPinkBalloon = new BlockType(446);
        public static readonly BlockType SillyPurpleBalloon = new BlockType(447);
        public static readonly BlockType SillyGreenBalloon = new BlockType(448);
        public static readonly BlockType BlueStreamer = new BlockType(449);
        public static readonly BlockType GreenStreamer = new BlockType(450);
        public static readonly BlockType PinkStreamer = new BlockType(451);
        public static readonly BlockType SillyBalloonMachine = new BlockType(452);
        public static readonly BlockType SillyTiedBalloons = new BlockType(453);
        public static readonly BlockType Pigronata = new BlockType(454);
        public static readonly BlockType PartyCenter = new BlockType(455);
        public static readonly BlockType SillyTiedBundleOfBalloons = new BlockType(456);
        public static readonly BlockType PartyPresent = new BlockType(457);
        public static readonly BlockType Sandfall = new BlockType(458);
        public static readonly BlockType Snowfall = new BlockType(459);
        public static readonly BlockType SnowCloud = new BlockType(460);
        public static readonly BlockType MagicSandDropper = new BlockType(461);
        public static readonly BlockType DesertSpiritLamp = new BlockType(462);
        public static readonly BlockType DefendersForge = new BlockType(463);
        public static readonly BlockType WarTable = new BlockType(464);
        public static readonly BlockType WarTableBanner = new BlockType(465);
        public static readonly BlockType EterniaCrystalStand = new BlockType(466);
        public static readonly BlockType Chests2 = new BlockType(467);
        public static readonly BlockType TrappedChests2 = new BlockType(468);
        public static readonly BlockType CrystalTable = new BlockType(469);
#pragma warning restore 1591

        internal static readonly IDictionary<ushort, FieldInfo> IdToField = new Dictionary<ushort, FieldInfo>();
        private static readonly IDictionary<ushort, BlockType> IdToBlockType = new Dictionary<ushort, BlockType>();

        private static readonly ISet<BlockType> FramesImportant = new HashSet<BlockType> {
            ShortGrassPlants,
            Torches,
            Trees,
            ClosedDoors,
            OpenDoors,
            CrystalHeart,
            Bottles,
            Tables,
            Chairs,
            Anvils,
            Furnace,
            WorkBenches,
            Platforms,
            Saplings,
            Chests,
            ShortCorruptionPlants,
            Altars,
            Sunflower,
            Pot,
            PiggyBank,
            ShadowOrbs,
            Candles,
            Chandeliers,
            JackOLantern,
            Present,
            Lanterns,
            Book,
            Sign,
            ShortJunglePlants,
            MushroomPlants,
            MushroomTrees,
            TallGrassPlants,
            TallJunglePlants,
            Hellforge,
            ClayPot,
            Beds,
            Coral,
            GrowingHerbs,
            MatureHerbs,
            BloomingHerbs,
            Tombstones,
            Loom,
            Pianos,
            Dressers,
            Benches,
            Bathtubs,
            Banners,
            LampPost,
            Lamps,
            Keg,
            ChineseLantern,
            CookingPots,
            Safe,
            SkullLantern,
            Candelabras,
            Bookcases,
            Throne,
            Bowls,
            Clocks,
            Statues,
            Sawmill,
            ShortHallowedPlants,
            TallHallowedPlants,
            TinkerersWorkshop,
            CrystalBall,
            DiscoBall,
            Mannequin,
            CrystalShard,
            Lever,
            Forges,
            HardmodeAnvils,
            PressurePlates,
            Switch,
            Traps,
            Boulder,
            MusicBoxes,
            Explosives,
            InletPump,
            OutletPump,
            Timers,
            ChristmasLights,
            AmbientObjects,
            ChristmasTree,
            Sinks,
            PlatinumCandelabra,
            PlatinumCandle,
            Gems,
            MossGrowth,
            SmallAmbientObjects,
            LargeAmbientObjects,
            LargeAmbientObjects2,
            ShortCrimsonPlants,
            WaterFountains,
            Cannons,
            LandMine,
            SnowballLauncher,
            Campfires,
            Rockets,
            BlendOMatic,
            MeatGrinder,
            Extractinator,
            Solidifier,
            DyePlants,
            DyeVat,
            Larva,
            PlantDetritus,
            Teleporter,
            LifeFruit,
            LihzahrdAltar,
            PlanterasBulb,
            MetalBars,
            Paintings3x3,
            Paintings4x3,
            Paintings6x4,
            ImbuingStation,
            BubbleMachine,
            Paintings2x3,
            Paintings3x2,
            Autohammer,
            PumpkinPlants,
            Womannequin,
            FireflyInABottle,
            LightningBugInABottle,
            BunnyCage,
            SquirrelCage,
            MallardDuckCage,
            DuckCage,
            BirdCage,
            BlueJayCage,
            CardinalCage,
            FishBowl,
            HeavyWorkBench,
            SnailCage,
            GlowingSnailCage,
            AmmoBox,
            MonarchButterflyJar,
            PurpleEmperorButterflyJar,
            RedAdmiralButterflyJar,
            UlyssesButterflyJar,
            SulphurButterflyJar,
            TreeNymphButterflyJar,
            ZebraSwallowtailButterflyJar,
            JuliaButterflyJar,
            ScorpionCage,
            BlackScorpionCage,
            FrogCage,
            MouseCage,
            BoneWelder,
            FleshCloningVat,
            GlassKiln,
            LihzahrdFurnace,
            LivingLoom,
            SkyMill,
            IceMachine,
            SteampunkBoiler,
            HoneyDispenser,
            PenguinCage,
            WormCage,
            MinecartTracks,
            BlueJellyfishJar,
            GreenJellyfishJar,
            PinkJellyfishJar,
            ShipInABottle,
            SeaweedPlanter,
            PalmTree,
            BeachPiles,
            WeaponRack,
            FireworksBox,
            LetterStatues,
            FireworkFountain,
            GrasshopperCage,
            MushroomStatue,
            BewitchingTable,
            AlchemyTable,
            EnchantedSundial,
            GoldBirdCage,
            GoldBunnyCage,
            GoldButterflyJar,
            GoldFrogCage,
            GoldGrasshopperCage,
            GoldMouseCage,
            GoldWormCage,
            PeaceCandle,
            MagicWaterDropper,
            MagicLavaDropper,
            MagicHoneyDropper,
            Crates,
            SharpeningStation,
            TargetDummy,
            PlanterBoxes,
            OpenTrapDoor,
            ClosedTrapDoor,
            ClosedTallGate,
            OpenTallGate,
            LavaLamp,
            EnchantedNightcrawlerCage,
            BuggyCage,
            GrubbyCage,
            SluggyCage,
            ItemFrame,
            Fireplace,
            Chimney,
            Monoliths,
            Detonator,
            AncientManipulator,
            RedSquirrelCage,
            GoldSquirrelCage,
            LogicGateLamps,
            LogicGates,
            LogicSensors,
            JunctionBox,
            AnnouncementBox,
            RedTeamPlatform,
            WeightedPressurePlates,
            WireBulb,
            GreenTeamPlatform,
            BlueTeamPlatform,
            YellowTeamPlatform,
            PinkTeamPlatform,
            WhiteTeamPlatform,
            GemLocks,
            TrappedChests,
            TealPressurePad,
            Geyser,
            Beehive,
            PixelBox,
            SillyBalloonMachine,
            SillyTiedBalloons,
            Pigronata,
            PartyCenter,
            SillyTiedBundleOfBalloons,
            PartyPresent,
            MagicSandDropper,
            DesertSpiritLamp,
            DefendersForge,
            WarTable,
            WarTableBanner,
            EterniaCrystalStand,
            Chests2,
            TrappedChests2,
            CrystalTable
        };

        /// <summary>
        /// Gets the block type's ID.
        /// </summary>
        public ushort Id { get; }

        /// <summary>
        /// Gets a value indicating whether the block type's block frames are important.
        /// </summary>
        public bool AreFramesImportant => FramesImportant.Contains(this);

        private BlockType(ushort id) {
            Id = id;
        }

        // Initializes lookup tables.
        static BlockType() {
            var fields = typeof(BlockType).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields) {
                if (!(field.GetValue(null) is BlockType blockType)) continue;

                IdToField[blockType.Id] = field;
                IdToBlockType[blockType.Id] = blockType;
            }
        }

        /// <summary>
        /// Returns a block type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The block type, or <c>null</c> if none exists.</returns>
        public static BlockType FromId(ushort id) =>
            IdToBlockType.TryGetValue(id, out var blockType) ? blockType : null;

        /// <inheritdoc />
        public override string ToString() => IdToField[Id].Name;
    }
}
