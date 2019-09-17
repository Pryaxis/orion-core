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
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Orion.Entities {
    /// <summary>
    /// Represents an NPC's type.
    /// </summary>
    public sealed class NpcType {
#pragma warning disable 1591
        public static readonly NpcType BigStingyHornet = new NpcType(-65);
        public static readonly NpcType LittleStingyHornet = new NpcType(-64);
        public static readonly NpcType BigSpikyHornet = new NpcType(-63);
        public static readonly NpcType LittleSpikyHornet = new NpcType(-62);
        public static readonly NpcType BigLeafyHornet = new NpcType(-61);
        public static readonly NpcType LittleLeafyHornet = new NpcType(-60);
        public static readonly NpcType BigHoneyHornet = new NpcType(-59);
        public static readonly NpcType LittleHoneyHornet = new NpcType(-58);
        public static readonly NpcType BigFattyHornet = new NpcType(-57);
        public static readonly NpcType LittleFattyHornet = new NpcType(-56);
        public static readonly NpcType BigRaincoatZombie = new NpcType(-55);
        public static readonly NpcType SmallRaincoatZombie = new NpcType(-54);
        public static readonly NpcType BigPantlessSkeleton = new NpcType(-53);
        public static readonly NpcType SmallPantlessSkeleton = new NpcType(-52);
        public static readonly NpcType BigMisassembledSkeleton = new NpcType(-51);
        public static readonly NpcType SmallMisassembledSkeleton = new NpcType(-50);
        public static readonly NpcType BigHeadacheSkeleton = new NpcType(-49);
        public static readonly NpcType SmallHeadacheSkeleton = new NpcType(-48);
        public static readonly NpcType BigSkeleton = new NpcType(-47);
        public static readonly NpcType SmallSkeleton = new NpcType(-46);
        public static readonly NpcType BigFemaleZombie = new NpcType(-45);
        public static readonly NpcType SmallFemaleZombie = new NpcType(-44);
        public static readonly NpcType DemonDemonEye2 = new NpcType(-43);
        public static readonly NpcType PurpleDemonEye2 = new NpcType(-42);
        public static readonly NpcType GreenDemonEye2 = new NpcType(-41);
        public static readonly NpcType DialatedDemonEye2 = new NpcType(-40);
        public static readonly NpcType SleepyDemonEye2 = new NpcType(-39);
        public static readonly NpcType CataractDemonEye2 = new NpcType(-38);
        public static readonly NpcType BigTwiggyZombie = new NpcType(-37);
        public static readonly NpcType SmallTwiggyZombie = new NpcType(-36);
        public static readonly NpcType BigSwampZombie = new NpcType(-35);
        public static readonly NpcType SmallSwampZombie = new NpcType(-34);
        public static readonly NpcType BigSlimedZombie = new NpcType(-33);
        public static readonly NpcType SmallSlimedZombie = new NpcType(-32);
        public static readonly NpcType BigPincushionZombie = new NpcType(-31);
        public static readonly NpcType SmallPincushionZombie = new NpcType(-30);
        public static readonly NpcType BigBaldZombie = new NpcType(-29);
        public static readonly NpcType SmallBaldZombie = new NpcType(-28);
        public static readonly NpcType BigZombie = new NpcType(-27);
        public static readonly NpcType SmallZombie = new NpcType(-26);
        public static readonly NpcType BigCrimslime = new NpcType(-25);
        public static readonly NpcType LittleCrimslime = new NpcType(-24);
        public static readonly NpcType BigCrimera = new NpcType(-23);
        public static readonly NpcType LittleCrimera = new NpcType(-22);
        public static readonly NpcType GiantMossHornet = new NpcType(-21);
        public static readonly NpcType BigMossHornet = new NpcType(-20);
        public static readonly NpcType LittleMossHornet = new NpcType(-19);
        public static readonly NpcType TinyMossHornet = new NpcType(-18);
        public static readonly NpcType BigStinger = new NpcType(-17);
        public static readonly NpcType LittleStinger = new NpcType(-16);
        public static readonly NpcType HeavyArmoedSkeleton = new NpcType(-15);
        public static readonly NpcType BigBoned = new NpcType(-14);
        public static readonly NpcType ShortBones = new NpcType(-13);
        public static readonly NpcType BigEaterOfSouls = new NpcType(-12);
        public static readonly NpcType LittleEaterOfSouls = new NpcType(-11);
        public static readonly NpcType JungleSlime = new NpcType(-10);
        public static readonly NpcType YellowSlime = new NpcType(-9);
        public static readonly NpcType RedSlime = new NpcType(-8);
        public static readonly NpcType PurpleSlime = new NpcType(-7);
        public static readonly NpcType BlackSlime = new NpcType(-6);
        public static readonly NpcType BabySlime = new NpcType(-5);
        public static readonly NpcType Pinky = new NpcType(-4);
        public static readonly NpcType GreenSlime = new NpcType(-3);
        public static readonly NpcType WinglessSlimer = new NpcType(-2);
        public static readonly NpcType Slimeling = new NpcType(-1);
        public static readonly NpcType None = new NpcType(0);
        public static readonly NpcType BlueSlime = new NpcType(1);
        public static readonly NpcType DemonEye = new NpcType(2);
        public static readonly NpcType Zombie = new NpcType(3);
        public static readonly NpcType EyeOfCthulhu = new NpcType(4);
        public static readonly NpcType ServantOfCthulhu = new NpcType(5);
        public static readonly NpcType EaterOfSouls = new NpcType(6);
        public static readonly NpcType DevourerHead = new NpcType(7);
        public static readonly NpcType DevourerBody = new NpcType(8);
        public static readonly NpcType DevourerTail = new NpcType(9);
        public static readonly NpcType GiantWormHead = new NpcType(10);
        public static readonly NpcType GiantWormBody = new NpcType(11);
        public static readonly NpcType GiantWormTail = new NpcType(12);
        public static readonly NpcType EaterOfWorldsHead = new NpcType(13);
        public static readonly NpcType EaterOfWorldsBody = new NpcType(14);
        public static readonly NpcType EaterOfWorldsTail = new NpcType(15);
        public static readonly NpcType MotherSlime = new NpcType(16);
        public static readonly NpcType Merchant = new NpcType(17);
        public static readonly NpcType Nurse = new NpcType(18);
        public static readonly NpcType ArmsDealer = new NpcType(19);
        public static readonly NpcType Dryad = new NpcType(20);
        public static readonly NpcType Skeleton = new NpcType(21);
        public static readonly NpcType Guide = new NpcType(22);
        public static readonly NpcType MeteorHead = new NpcType(23);
        public static readonly NpcType FireImp = new NpcType(24);
        public static readonly NpcType BurningSphere = new NpcType(25);
        public static readonly NpcType GoblinPeon = new NpcType(26);
        public static readonly NpcType GoblinThief = new NpcType(27);
        public static readonly NpcType GoblinWarrior = new NpcType(28);
        public static readonly NpcType GoblinSorcerer = new NpcType(29);
        public static readonly NpcType ChaosBall = new NpcType(30);
        public static readonly NpcType AngryBones = new NpcType(31);
        public static readonly NpcType DarkCaster = new NpcType(32);
        public static readonly NpcType WaterSphere = new NpcType(33);
        public static readonly NpcType CursedSkull = new NpcType(34);
        public static readonly NpcType SkeletronHead = new NpcType(35);
        public static readonly NpcType SkeletronHand = new NpcType(36);
        public static readonly NpcType OldMan = new NpcType(37);
        public static readonly NpcType Demolitionist = new NpcType(38);
        public static readonly NpcType BoneSerpentHead = new NpcType(39);
        public static readonly NpcType BoneSerpentBody = new NpcType(40);
        public static readonly NpcType BoneSerpentTail = new NpcType(41);
        public static readonly NpcType Hornet = new NpcType(42);
        public static readonly NpcType ManEater = new NpcType(43);
        public static readonly NpcType UndeadMiner = new NpcType(44);
        public static readonly NpcType Tim = new NpcType(45);
        public static readonly NpcType Bunny = new NpcType(46);
        public static readonly NpcType CorruptBunny = new NpcType(47);
        public static readonly NpcType Harpy = new NpcType(48);
        public static readonly NpcType CaveBat = new NpcType(49);
        public static readonly NpcType KingSlime = new NpcType(50);
        public static readonly NpcType JungleBat = new NpcType(51);
        public static readonly NpcType DoctorBones = new NpcType(52);
        public static readonly NpcType TheGroom = new NpcType(53);
        public static readonly NpcType Clothier = new NpcType(54);
        public static readonly NpcType Goldfish = new NpcType(55);
        public static readonly NpcType Snatcher = new NpcType(56);
        public static readonly NpcType CorruptGoldfish = new NpcType(57);
        public static readonly NpcType Piranha = new NpcType(58);
        public static readonly NpcType LavaSlime = new NpcType(59);
        public static readonly NpcType Hellbat = new NpcType(60);
        public static readonly NpcType Vulture = new NpcType(61);
        public static readonly NpcType Demon = new NpcType(62);
        public static readonly NpcType BlueJellyfish = new NpcType(63);
        public static readonly NpcType PinkJellyfish = new NpcType(64);
        public static readonly NpcType Shark = new NpcType(65);
        public static readonly NpcType VoodooDemon = new NpcType(66);
        public static readonly NpcType Crab = new NpcType(67);
        public static readonly NpcType DungeonGuardian = new NpcType(68);
        public static readonly NpcType Antlion = new NpcType(69);
        public static readonly NpcType SpikeBall = new NpcType(70);
        public static readonly NpcType DungeonSlime = new NpcType(71);
        public static readonly NpcType BlazingWheel = new NpcType(72);
        public static readonly NpcType GoblinScout = new NpcType(73);
        public static readonly NpcType Bird = new NpcType(74);
        public static readonly NpcType Pixie = new NpcType(75);
        public static readonly NpcType ArmoredSkeleton = new NpcType(77);
        public static readonly NpcType Mummy = new NpcType(78);
        public static readonly NpcType DarkMummy = new NpcType(79);
        public static readonly NpcType LightMummy = new NpcType(80);
        public static readonly NpcType CorruptSlime = new NpcType(81);
        public static readonly NpcType Wraith = new NpcType(82);
        public static readonly NpcType CursedHammer = new NpcType(83);
        public static readonly NpcType EnchantedSword = new NpcType(84);
        public static readonly NpcType Mimic = new NpcType(85);
        public static readonly NpcType Unicorn = new NpcType(86);
        public static readonly NpcType WyvernHead = new NpcType(87);
        public static readonly NpcType WyvernLegs = new NpcType(88);
        public static readonly NpcType WyvernBody = new NpcType(89);
        public static readonly NpcType WyvernBody2 = new NpcType(90);
        public static readonly NpcType WyvernBody3 = new NpcType(91);
        public static readonly NpcType WyvernTail = new NpcType(92);
        public static readonly NpcType GiantBat = new NpcType(93);
        public static readonly NpcType Corruptor = new NpcType(94);
        public static readonly NpcType DiggerHead = new NpcType(95);
        public static readonly NpcType DiggerBody = new NpcType(96);
        public static readonly NpcType DiggerTail = new NpcType(97);
        public static readonly NpcType WorldFeederHead = new NpcType(98);
        public static readonly NpcType WorldFeederBody = new NpcType(99);
        public static readonly NpcType WorldFeederTail = new NpcType(100);
        public static readonly NpcType Clinger = new NpcType(101);
        public static readonly NpcType AnglerFish = new NpcType(102);
        public static readonly NpcType GreenJellyfish = new NpcType(103);
        public static readonly NpcType Werewolf = new NpcType(104);
        public static readonly NpcType BoundGoblin = new NpcType(105);
        public static readonly NpcType BoundWizard = new NpcType(106);
        public static readonly NpcType GoblinTinkerer = new NpcType(107);
        public static readonly NpcType Wizard = new NpcType(108);
        public static readonly NpcType Clown = new NpcType(109);
        public static readonly NpcType SkeletonArcher = new NpcType(110);
        public static readonly NpcType GoblinArcher = new NpcType(111);
        public static readonly NpcType VileSpit = new NpcType(112);
        public static readonly NpcType WallofFlesh = new NpcType(113);
        public static readonly NpcType WallofFleshEye = new NpcType(114);
        public static readonly NpcType TheHungry = new NpcType(115);
        public static readonly NpcType TheHungryII = new NpcType(116);
        public static readonly NpcType LeechHead = new NpcType(117);
        public static readonly NpcType LeechBody = new NpcType(118);
        public static readonly NpcType LeechTail = new NpcType(119);
        public static readonly NpcType ChaosElemental = new NpcType(120);
        public static readonly NpcType Slimer = new NpcType(121);
        public static readonly NpcType Gastropod = new NpcType(122);
        public static readonly NpcType BoundMechanic = new NpcType(123);
        public static readonly NpcType Mechanic = new NpcType(124);
        public static readonly NpcType Retinazer = new NpcType(125);
        public static readonly NpcType Spazmatism = new NpcType(126);
        public static readonly NpcType SkeletronPrime = new NpcType(127);
        public static readonly NpcType PrimeCannon = new NpcType(128);
        public static readonly NpcType PrimeSaw = new NpcType(129);
        public static readonly NpcType PrimeVice = new NpcType(130);
        public static readonly NpcType PrimeLaser = new NpcType(131);
        public static readonly NpcType BaldZombie = new NpcType(132);
        public static readonly NpcType WanderingEye = new NpcType(133);
        public static readonly NpcType TheDestroyer = new NpcType(134);
        public static readonly NpcType TheDestroyerBody = new NpcType(135);
        public static readonly NpcType TheDestroyerTail = new NpcType(136);
        public static readonly NpcType IlluminantBat = new NpcType(137);
        public static readonly NpcType IlluminantSlime = new NpcType(138);
        public static readonly NpcType Probe = new NpcType(139);
        public static readonly NpcType PossessedArmor = new NpcType(140);
        public static readonly NpcType ToxicSludge = new NpcType(141);
        public static readonly NpcType SantaClaus = new NpcType(142);
        public static readonly NpcType SnowmanGangsta = new NpcType(143);
        public static readonly NpcType MisterStabby = new NpcType(144);
        public static readonly NpcType SnowBalla = new NpcType(145);
        public static readonly NpcType IceSlime = new NpcType(147);
        public static readonly NpcType Penguin = new NpcType(148);
        public static readonly NpcType BlackPenguin = new NpcType(149);
        public static readonly NpcType IceBat = new NpcType(150);
        public static readonly NpcType Lavabat = new NpcType(151);
        public static readonly NpcType GiantFlyingFox = new NpcType(152);
        public static readonly NpcType GiantTortoise = new NpcType(153);
        public static readonly NpcType IceTortoise = new NpcType(154);
        public static readonly NpcType Wolf = new NpcType(155);
        public static readonly NpcType RedDevil = new NpcType(156);
        public static readonly NpcType Arapaima = new NpcType(157);
        public static readonly NpcType VampireBat = new NpcType(158);
        public static readonly NpcType Vampire = new NpcType(159);
        public static readonly NpcType Truffle = new NpcType(160);
        public static readonly NpcType ZombieEskimo = new NpcType(161);
        public static readonly NpcType Frankenstein = new NpcType(162);
        public static readonly NpcType BlackRecluse = new NpcType(163);
        public static readonly NpcType WallCreeper = new NpcType(164);
        public static readonly NpcType WallCreeperOnWall = new NpcType(165);
        public static readonly NpcType SwampThing = new NpcType(166);
        public static readonly NpcType UndeadViking = new NpcType(167);
        public static readonly NpcType CorruptPenguin = new NpcType(168);
        public static readonly NpcType IceElemental = new NpcType(169);
        public static readonly NpcType CorruptPigron = new NpcType(170);
        public static readonly NpcType HallowedPigron = new NpcType(171);
        public static readonly NpcType RuneWizard = new NpcType(172);
        public static readonly NpcType Crimera = new NpcType(173);
        public static readonly NpcType Herpling = new NpcType(174);
        public static readonly NpcType AngryTrapper = new NpcType(175);
        public static readonly NpcType MossHornet = new NpcType(176);
        public static readonly NpcType Derpling = new NpcType(177);
        public static readonly NpcType Steampunker = new NpcType(178);
        public static readonly NpcType CrimsonAxe = new NpcType(179);
        public static readonly NpcType CrimsonPigron = new NpcType(180);
        public static readonly NpcType FaceMonster = new NpcType(181);
        public static readonly NpcType FloatyGross = new NpcType(182);
        public static readonly NpcType Crimslime = new NpcType(183);
        public static readonly NpcType SpikedIceSlime = new NpcType(184);
        public static readonly NpcType SnowFlinx = new NpcType(185);
        public static readonly NpcType PincushionZombie = new NpcType(186);
        public static readonly NpcType SlimedZombie = new NpcType(187);
        public static readonly NpcType SwampZombie = new NpcType(188);
        public static readonly NpcType TwiggyZombie = new NpcType(189);
        public static readonly NpcType CataractDemonEye = new NpcType(190);
        public static readonly NpcType SleepyDemonEye = new NpcType(191);
        public static readonly NpcType DilatedDemonEye = new NpcType(192);
        public static readonly NpcType GreenDemonEye = new NpcType(193);
        public static readonly NpcType PurpleDemonEye = new NpcType(194);
        public static readonly NpcType LostGirl = new NpcType(195);
        public static readonly NpcType Nymph = new NpcType(196);
        public static readonly NpcType ArmoredViking = new NpcType(197);
        public static readonly NpcType Lihzahrd = new NpcType(198);
        public static readonly NpcType CrawlerLihzahrd = new NpcType(199);
        public static readonly NpcType FemaleZombie = new NpcType(200);
        public static readonly NpcType HeadacheSkeleton = new NpcType(201);
        public static readonly NpcType MisassembledSkeleton = new NpcType(202);
        public static readonly NpcType PantlessSkeleton = new NpcType(203);
        public static readonly NpcType SpikedJungleSlime = new NpcType(204);
        public static readonly NpcType Moth = new NpcType(205);
        public static readonly NpcType IcyMerman = new NpcType(206);
        public static readonly NpcType DyeTrader = new NpcType(207);
        public static readonly NpcType PartyGirl = new NpcType(208);
        public static readonly NpcType Cyborg = new NpcType(209);
        public static readonly NpcType Bee = new NpcType(210);
        public static readonly NpcType SmallBee = new NpcType(211);
        public static readonly NpcType PirateDeckhand = new NpcType(212);
        public static readonly NpcType PirateCorsair = new NpcType(213);
        public static readonly NpcType PirateDeadeye = new NpcType(214);
        public static readonly NpcType PirateCrossbower = new NpcType(215);
        public static readonly NpcType PirateCaptain = new NpcType(216);
        public static readonly NpcType CochinealBeetle = new NpcType(217);
        public static readonly NpcType CyanBeetle = new NpcType(218);
        public static readonly NpcType LacBeetle = new NpcType(219);
        public static readonly NpcType SeaSnail = new NpcType(220);
        public static readonly NpcType Squid = new NpcType(221);
        public static readonly NpcType QueenBee = new NpcType(222);
        public static readonly NpcType RaincoatZombie = new NpcType(223);
        public static readonly NpcType FlyingFish = new NpcType(224);
        public static readonly NpcType UmbrellaSlime = new NpcType(225);
        public static readonly NpcType FlyingSnake = new NpcType(226);
        public static readonly NpcType Painter = new NpcType(227);
        public static readonly NpcType WitchDoctor = new NpcType(228);
        public static readonly NpcType Pirate = new NpcType(229);
        public static readonly NpcType WalkingGoldfish = new NpcType(230);
        public static readonly NpcType FattyHornet = new NpcType(231);
        public static readonly NpcType HoneyHornet = new NpcType(232);
        public static readonly NpcType LeafyHornet = new NpcType(233);
        public static readonly NpcType SpikyHornet = new NpcType(234);
        public static readonly NpcType StingyHornet = new NpcType(235);
        public static readonly NpcType JungleCreeper = new NpcType(236);
        public static readonly NpcType JungleCreeperOnWall = new NpcType(237);
        public static readonly NpcType BlackRecluseOnWall = new NpcType(238);
        public static readonly NpcType BloodCrawler = new NpcType(239);
        public static readonly NpcType BloodCrawlerOnWall = new NpcType(240);
        public static readonly NpcType BloodFeeder = new NpcType(241);
        public static readonly NpcType BloodJelly = new NpcType(242);
        public static readonly NpcType IceGolem = new NpcType(243);
        public static readonly NpcType RainbowSlime = new NpcType(244);
        public static readonly NpcType GolemBody = new NpcType(245);
        public static readonly NpcType GolemHead = new NpcType(246);
        public static readonly NpcType LeftGolemFist = new NpcType(247);
        public static readonly NpcType RightGolemFist = new NpcType(248);
        public static readonly NpcType FreeGolemHead = new NpcType(249);
        public static readonly NpcType AngryNimbus = new NpcType(250);
        public static readonly NpcType Eyezor = new NpcType(251);
        public static readonly NpcType Parrot = new NpcType(252);
        public static readonly NpcType Reaper = new NpcType(253);
        public static readonly NpcType MushroomZombie = new NpcType(254);
        public static readonly NpcType HatMushroomZombie = new NpcType(255);
        public static readonly NpcType FungoFish = new NpcType(256);
        public static readonly NpcType AnomuraFungus = new NpcType(257);
        public static readonly NpcType MushiLadybug = new NpcType(258);
        public static readonly NpcType FungiBulb = new NpcType(259);
        public static readonly NpcType GiantFungiBulb = new NpcType(260);
        public static readonly NpcType FungiSpore = new NpcType(261);
        public static readonly NpcType Plantera = new NpcType(262);
        public static readonly NpcType PlanterasHook = new NpcType(263);
        public static readonly NpcType PlanterasTentacle = new NpcType(264);
        public static readonly NpcType Spore = new NpcType(265);
        public static readonly NpcType BrainOfCthulhu = new NpcType(266);
        public static readonly NpcType Creeper = new NpcType(267);
        public static readonly NpcType IchorSticker = new NpcType(268);
        public static readonly NpcType AxeRustyArmoredBones = new NpcType(269);
        public static readonly NpcType FlailRustyArmoredBones = new NpcType(270);
        public static readonly NpcType SwordRustyArmoredBones = new NpcType(271);
        public static readonly NpcType SwordNoArmorRustyArmoredBones = new NpcType(272);
        public static readonly NpcType BlueArmoredBones = new NpcType(273);
        public static readonly NpcType MaceBlueArmoredBones = new NpcType(274);
        public static readonly NpcType NoPantsBlueArmoredBones = new NpcType(275);
        public static readonly NpcType SwordBlueArmoredBones = new NpcType(276);
        public static readonly NpcType HellArmoredBones = new NpcType(277);
        public static readonly NpcType SpikeShieldHellArmoredBones = new NpcType(278);
        public static readonly NpcType MaceHellArmoredBones = new NpcType(279);
        public static readonly NpcType SwordHellArmoredBones = new NpcType(280);
        public static readonly NpcType RaggedCaster = new NpcType(281);
        public static readonly NpcType OpenCoatRaggedCaster = new NpcType(282);
        public static readonly NpcType Necromancer = new NpcType(283);
        public static readonly NpcType ArmoredNecromancer = new NpcType(284);
        public static readonly NpcType RedDiabolist = new NpcType(285);
        public static readonly NpcType WhiteDiabolist = new NpcType(286);
        public static readonly NpcType BoneLee = new NpcType(287);
        public static readonly NpcType DungeonSpirit = new NpcType(288);
        public static readonly NpcType GiantCursedSkull = new NpcType(289);
        public static readonly NpcType Paladin = new NpcType(290);
        public static readonly NpcType SkeletonSniper = new NpcType(291);
        public static readonly NpcType TacticalSkeleton = new NpcType(292);
        public static readonly NpcType SkeletonCommando = new NpcType(293);
        public static readonly NpcType BigAngryBones = new NpcType(294);
        public static readonly NpcType BigMuscleAngryBones = new NpcType(295);
        public static readonly NpcType BigHelmetAngryBones = new NpcType(296);
        public static readonly NpcType BlueJay = new NpcType(297);
        public static readonly NpcType Cardinal = new NpcType(298);
        public static readonly NpcType Squirrel = new NpcType(299);
        public static readonly NpcType Mouse = new NpcType(300);
        public static readonly NpcType Raven = new NpcType(301);
        public static readonly NpcType BunnySlime = new NpcType(302);
        public static readonly NpcType SlimedBunny = new NpcType(303);
        public static readonly NpcType HoppinJack = new NpcType(304);
        public static readonly NpcType Scarecrow1 = new NpcType(305);
        public static readonly NpcType Scarecrow2 = new NpcType(306);
        public static readonly NpcType Scarecrow3 = new NpcType(307);
        public static readonly NpcType Scarecrow4 = new NpcType(308);
        public static readonly NpcType Scarecrow5 = new NpcType(309);
        public static readonly NpcType Scarecrow6 = new NpcType(310);
        public static readonly NpcType Scarecrow7 = new NpcType(311);
        public static readonly NpcType Scarecrow8 = new NpcType(312);
        public static readonly NpcType Scarecrow9 = new NpcType(313);
        public static readonly NpcType Scarecrow10 = new NpcType(314);
        public static readonly NpcType HeadlessHorseman = new NpcType(315);
        public static readonly NpcType Ghost = new NpcType(316);
        public static readonly NpcType OwlDemonEye = new NpcType(317);
        public static readonly NpcType SpaceshipDemonEye = new NpcType(318);
        public static readonly NpcType DoctorZombie = new NpcType(319);
        public static readonly NpcType SupermanZombie = new NpcType(320);
        public static readonly NpcType PixieZombie = new NpcType(321);
        public static readonly NpcType TopHatSkeleton = new NpcType(322);
        public static readonly NpcType AstronautSkeleton = new NpcType(323);
        public static readonly NpcType AlienSkeleton = new NpcType(324);
        public static readonly NpcType MourningWood = new NpcType(325);
        public static readonly NpcType Splinterling = new NpcType(326);
        public static readonly NpcType Pumpking = new NpcType(327);
        public static readonly NpcType PumpkingBlade = new NpcType(328);
        public static readonly NpcType Hellhound = new NpcType(329);
        public static readonly NpcType Poltergeist = new NpcType(330);
        public static readonly NpcType SantaZombie = new NpcType(331);
        public static readonly NpcType SweaterZombie = new NpcType(332);
        public static readonly NpcType WhiteRibbonSlime = new NpcType(333);
        public static readonly NpcType YellowRibbonSlime = new NpcType(334);
        public static readonly NpcType GreenRibbonSlime = new NpcType(335);
        public static readonly NpcType RedRibbonSlime = new NpcType(336);
        public static readonly NpcType SantaBunny = new NpcType(337);
        public static readonly NpcType ZombieElf = new NpcType(338);
        public static readonly NpcType BeardedZombieElf = new NpcType(339);
        public static readonly NpcType FemaleZombieElf = new NpcType(340);
        public static readonly NpcType PresentMimic = new NpcType(341);
        public static readonly NpcType GingerbreadMan = new NpcType(342);
        public static readonly NpcType Yeti = new NpcType(343);
        public static readonly NpcType Everscream = new NpcType(344);
        public static readonly NpcType IceQueen = new NpcType(345);
        public static readonly NpcType SantaNK1 = new NpcType(346);
        public static readonly NpcType ElfCopter = new NpcType(347);
        public static readonly NpcType Nutcracker = new NpcType(348);
        public static readonly NpcType SpinningNutcracker = new NpcType(349);
        public static readonly NpcType ElfArcher = new NpcType(350);
        public static readonly NpcType Krampus = new NpcType(351);
        public static readonly NpcType Flocko = new NpcType(352);
        public static readonly NpcType Stylist = new NpcType(353);
        public static readonly NpcType WebbedStylist = new NpcType(354);
        public static readonly NpcType Firefly = new NpcType(355);
        public static readonly NpcType Butterfly = new NpcType(356);
        public static readonly NpcType Worm = new NpcType(357);
        public static readonly NpcType LightningBug = new NpcType(358);
        public static readonly NpcType Snail = new NpcType(359);
        public static readonly NpcType GlowingSnail = new NpcType(360);
        public static readonly NpcType Frog = new NpcType(361);
        public static readonly NpcType Duck = new NpcType(362);
        public static readonly NpcType Duck2 = new NpcType(363);
        public static readonly NpcType WhiteDuck = new NpcType(364);
        public static readonly NpcType WhiteDuck2 = new NpcType(365);
        public static readonly NpcType BlackScorpion = new NpcType(366);
        public static readonly NpcType Scorpion = new NpcType(367);
        public static readonly NpcType TravellingMerchant = new NpcType(368);
        public static readonly NpcType Angler = new NpcType(369);
        public static readonly NpcType DukeFishron = new NpcType(370);
        public static readonly NpcType DetonatingBubble = new NpcType(371);
        public static readonly NpcType Sharkron = new NpcType(372);
        public static readonly NpcType Sharkron2 = new NpcType(373);
        public static readonly NpcType TruffleWorm = new NpcType(374);
        public static readonly NpcType BurrowingTruffleWorm = new NpcType(375);
        public static readonly NpcType SleepingAngler = new NpcType(376);
        public static readonly NpcType Grasshopper = new NpcType(377);
        public static readonly NpcType ChatteringTeethBomb = new NpcType(378);
        public static readonly NpcType BlueCultistArcher = new NpcType(379);
        public static readonly NpcType WhiteCultistArcher = new NpcType(380);
        public static readonly NpcType BrainScrambler = new NpcType(381);
        public static readonly NpcType RayGunner = new NpcType(382);
        public static readonly NpcType MartianOfficer = new NpcType(383);
        public static readonly NpcType BubbleShield = new NpcType(384);
        public static readonly NpcType GrayGrunt = new NpcType(385);
        public static readonly NpcType MartianEngineer = new NpcType(386);
        public static readonly NpcType TeslaTurret = new NpcType(387);
        public static readonly NpcType MartianDrone = new NpcType(388);
        public static readonly NpcType Gigazapper = new NpcType(389);
        public static readonly NpcType ScutlixGunner = new NpcType(390);
        public static readonly NpcType Scutlix = new NpcType(391);
        public static readonly NpcType MartianSaucer = new NpcType(392);
        public static readonly NpcType MartianSaucerTurret = new NpcType(393);
        public static readonly NpcType MartianSaucerCannon = new NpcType(394);
        public static readonly NpcType MartianSaucerCore = new NpcType(395);
        public static readonly NpcType MoonLordHead = new NpcType(396);
        public static readonly NpcType MoonLordHand = new NpcType(397);
        public static readonly NpcType MoonLordCore = new NpcType(398);
        public static readonly NpcType MartianProbe = new NpcType(399);
        public static readonly NpcType TrueEyeOfCthulhu = new NpcType(400);
        public static readonly NpcType MoonLeechClot = new NpcType(401);
        public static readonly NpcType MilkywayWeaverHead = new NpcType(402);
        public static readonly NpcType MilkywayWeaverBody = new NpcType(403);
        public static readonly NpcType MilkywayWeaverTail = new NpcType(404);
        public static readonly NpcType StarCell = new NpcType(405);
        public static readonly NpcType SmallStarCell = new NpcType(406);
        public static readonly NpcType FlowInvader = new NpcType(407);
        public static readonly NpcType TwinklePopper = new NpcType(409);
        public static readonly NpcType Twinkle = new NpcType(410);
        public static readonly NpcType Stargazer = new NpcType(411);
        public static readonly NpcType CrawltipedeHead = new NpcType(412);
        public static readonly NpcType CrawltipedeBody = new NpcType(413);
        public static readonly NpcType CrawltipedeTail = new NpcType(414);
        public static readonly NpcType Drakomire = new NpcType(415);
        public static readonly NpcType DrakomireRider = new NpcType(416);
        public static readonly NpcType Sroller = new NpcType(417);
        public static readonly NpcType Corite = new NpcType(418);
        public static readonly NpcType Selenian = new NpcType(419);
        public static readonly NpcType NebulaFloater = new NpcType(420);
        public static readonly NpcType BrainSuckler = new NpcType(421);
        public static readonly NpcType VortexPillar = new NpcType(422);
        public static readonly NpcType EvolutionBeast = new NpcType(423);
        public static readonly NpcType Predictor = new NpcType(424);
        public static readonly NpcType StormDiver = new NpcType(425);
        public static readonly NpcType AlienQueen = new NpcType(426);
        public static readonly NpcType AlienHornet = new NpcType(427);
        public static readonly NpcType AlienLarva = new NpcType(428);
        public static readonly NpcType Vortexian = new NpcType(429);
        public static readonly NpcType ArmedZombie = new NpcType(430);
        public static readonly NpcType ArmedEskimoZombie = new NpcType(431);
        public static readonly NpcType ArmedPincushionZombie = new NpcType(432);
        public static readonly NpcType ArmedSlimedZombie = new NpcType(433);
        public static readonly NpcType ArmedSwampZombie = new NpcType(434);
        public static readonly NpcType ArmedTwiggyZombie = new NpcType(435);
        public static readonly NpcType ArmedFemaleZombie = new NpcType(436);
        public static readonly NpcType MysteriousTablet = new NpcType(437);
        public static readonly NpcType LunaticDevote = new NpcType(438);
        public static readonly NpcType LunaticCultist = new NpcType(439);
        public static readonly NpcType LunaticCultistClone = new NpcType(440);
        public static readonly NpcType TaxCollector = new NpcType(441);
        public static readonly NpcType GoldBird = new NpcType(442);
        public static readonly NpcType GoldBunny = new NpcType(443);
        public static readonly NpcType GoldButterfly = new NpcType(444);
        public static readonly NpcType GoldFrog = new NpcType(445);
        public static readonly NpcType GoldGrasshopper = new NpcType(446);
        public static readonly NpcType GoldMouse = new NpcType(447);
        public static readonly NpcType GoldWorm = new NpcType(448);
        public static readonly NpcType BoneThrowingSkeleton = new NpcType(449);
        public static readonly NpcType BoneThrowingSkeleton2 = new NpcType(450);
        public static readonly NpcType BoneThrowingSkeleton3 = new NpcType(451);
        public static readonly NpcType BoneThrowingSkeleton4 = new NpcType(452);
        public static readonly NpcType SkeletonMerchant = new NpcType(453);
        public static readonly NpcType PhantasmDragonHead = new NpcType(454);
        public static readonly NpcType PhantasmDragonBody1 = new NpcType(455);
        public static readonly NpcType PhantasmDragonBody2 = new NpcType(456);
        public static readonly NpcType PhantasmDragonBody3 = new NpcType(457);
        public static readonly NpcType PhantasmDragonBody4 = new NpcType(458);
        public static readonly NpcType PhantasmDragonTail = new NpcType(459);
        public static readonly NpcType Butcher = new NpcType(460);
        public static readonly NpcType CreatureFromTheDeep = new NpcType(461);
        public static readonly NpcType Fritz = new NpcType(462);
        public static readonly NpcType Nailhead = new NpcType(463);
        public static readonly NpcType CrimtaneBunny = new NpcType(464);
        public static readonly NpcType CrimtaneGoldfish = new NpcType(465);
        public static readonly NpcType Psycho = new NpcType(466);
        public static readonly NpcType DeadlySphere = new NpcType(467);
        public static readonly NpcType DrManFly = new NpcType(468);
        public static readonly NpcType ThePossessed = new NpcType(469);
        public static readonly NpcType ViciousPenguin = new NpcType(470);
        public static readonly NpcType GoblinSummoner = new NpcType(471);
        public static readonly NpcType ShadowflameApparition = new NpcType(472);
        public static readonly NpcType CorruptMimic = new NpcType(473);
        public static readonly NpcType CrimsonMimic = new NpcType(474);
        public static readonly NpcType HallowedMimic = new NpcType(475);
        public static readonly NpcType Mothron = new NpcType(477);
        public static readonly NpcType MothronEgg = new NpcType(478);
        public static readonly NpcType BabyMothron = new NpcType(479);
        public static readonly NpcType Medusa = new NpcType(480);
        public static readonly NpcType Hoplite = new NpcType(481);
        public static readonly NpcType GraniteGolem = new NpcType(482);
        public static readonly NpcType GraniteElemental = new NpcType(483);
        public static readonly NpcType EnchantedNightcrawler = new NpcType(484);
        public static readonly NpcType Grubby = new NpcType(485);
        public static readonly NpcType Sluggy = new NpcType(486);
        public static readonly NpcType Buggy = new NpcType(487);
        public static readonly NpcType TargetDummy = new NpcType(488);
        public static readonly NpcType BloodZombie = new NpcType(489);
        public static readonly NpcType Drippler = new NpcType(490);
        public static readonly NpcType FlyingDutchman = new NpcType(491);
        public static readonly NpcType DutchmanCannon = new NpcType(492);
        public static readonly NpcType StardustPillar = new NpcType(493);
        public static readonly NpcType Crawdad = new NpcType(494);
        public static readonly NpcType Crawdad2 = new NpcType(495);
        public static readonly NpcType GiantShelly = new NpcType(496);
        public static readonly NpcType GiantShelly2 = new NpcType(497);
        public static readonly NpcType Salamander = new NpcType(498);
        public static readonly NpcType Salamander2 = new NpcType(499);
        public static readonly NpcType Salamander3 = new NpcType(500);
        public static readonly NpcType Salamander4 = new NpcType(501);
        public static readonly NpcType Salamander5 = new NpcType(502);
        public static readonly NpcType Salamander6 = new NpcType(503);
        public static readonly NpcType Salamander7 = new NpcType(504);
        public static readonly NpcType Salamander8 = new NpcType(505);
        public static readonly NpcType Salamander9 = new NpcType(506);
        public static readonly NpcType NebulaPillar = new NpcType(507);
        public static readonly NpcType AntlionCharger = new NpcType(508);
        public static readonly NpcType AntlionSwarmer = new NpcType(509);
        public static readonly NpcType DuneSplicerHead = new NpcType(510);
        public static readonly NpcType DuneSplicerBody = new NpcType(511);
        public static readonly NpcType DuneSplicerTail = new NpcType(512);
        public static readonly NpcType TombCrawlerHead = new NpcType(513);
        public static readonly NpcType TombCrawlerBody = new NpcType(514);
        public static readonly NpcType TombCrawlerTail = new NpcType(515);
        public static readonly NpcType SolarFlare = new NpcType(516);
        public static readonly NpcType SolarPillar = new NpcType(517);
        public static readonly NpcType Drakanian = new NpcType(518);
        public static readonly NpcType SolarFragment = new NpcType(519);
        public static readonly NpcType MartianWalker = new NpcType(520);
        public static readonly NpcType AncientVision = new NpcType(521);
        public static readonly NpcType AncientLight = new NpcType(522);
        public static readonly NpcType AncientDoom = new NpcType(523);
        public static readonly NpcType Ghoul = new NpcType(524);
        public static readonly NpcType VileGhoul = new NpcType(525);
        public static readonly NpcType TaintedGhoul = new NpcType(526);
        public static readonly NpcType DreamerGhoul = new NpcType(527);
        public static readonly NpcType LightLamia = new NpcType(528);
        public static readonly NpcType DarkLamia = new NpcType(529);
        public static readonly NpcType SandPoacher = new NpcType(530);
        public static readonly NpcType SandPoacherOnWall = new NpcType(531);
        public static readonly NpcType Basilisk = new NpcType(532);
        public static readonly NpcType DesertSpirit = new NpcType(533);
        public static readonly NpcType TorturedSoul = new NpcType(534);
        public static readonly NpcType SpikedSlime = new NpcType(535);
        public static readonly NpcType TheBride = new NpcType(536);
        public static readonly NpcType SandSlime = new NpcType(537);
        public static readonly NpcType RedSquirrel = new NpcType(538);
        public static readonly NpcType GoldSquirrel = new NpcType(539);
        public static readonly NpcType PartyBunny = new NpcType(540);
        public static readonly NpcType SandElemental = new NpcType(541);
        public static readonly NpcType SandShark = new NpcType(542);
        public static readonly NpcType BoneBiter = new NpcType(543);
        public static readonly NpcType FleshReaver = new NpcType(544);
        public static readonly NpcType CrystalThresher = new NpcType(545);
        public static readonly NpcType AngryTumbler = new NpcType(546);
        public static readonly NpcType EterniaCrystal = new NpcType(548);
        public static readonly NpcType MysteriousPortal = new NpcType(549);
        public static readonly NpcType Tavernkeep = new NpcType(550);
        public static readonly NpcType Betsy = new NpcType(551);
        public static readonly NpcType EtherianGoblinT1 = new NpcType(552);
        public static readonly NpcType EtherianGoblinT2 = new NpcType(553);
        public static readonly NpcType EtherianGoblinT3 = new NpcType(554);
        public static readonly NpcType EtherianGoblinBomberT1 = new NpcType(555);
        public static readonly NpcType EtherianGoblinBomberT2 = new NpcType(556);
        public static readonly NpcType EtherianGoblinBomberT3 = new NpcType(557);
        public static readonly NpcType EtherianWyvernT1 = new NpcType(558);
        public static readonly NpcType EtherianWyvernT2 = new NpcType(559);
        public static readonly NpcType EtherianWyvernT3 = new NpcType(560);
        public static readonly NpcType EtherianJavelinThrowerT1 = new NpcType(561);
        public static readonly NpcType EtherianJavelinThrowerT2 = new NpcType(562);
        public static readonly NpcType EtherianJavelinThrowerT3 = new NpcType(563);
        public static readonly NpcType DarkMageT1 = new NpcType(564);
        public static readonly NpcType DarkMageT3 = new NpcType(565);
        public static readonly NpcType OldOnesSkeletonT1 = new NpcType(566);
        public static readonly NpcType OldOnesSkeletonT3 = new NpcType(567);
        public static readonly NpcType WitherBeastT2 = new NpcType(568);
        public static readonly NpcType WitherBeastT3 = new NpcType(569);
        public static readonly NpcType DrakinT2 = new NpcType(570);
        public static readonly NpcType DrakinT3 = new NpcType(571);
        public static readonly NpcType KoboldT2 = new NpcType(572);
        public static readonly NpcType KoboldT3 = new NpcType(573);
        public static readonly NpcType KoboldGliderT2 = new NpcType(574);
        public static readonly NpcType KoboldGliderT3 = new NpcType(575);
        public static readonly NpcType OgreT2 = new NpcType(576);
        public static readonly NpcType OgreT3 = new NpcType(577);
        public static readonly NpcType EtherianLightningBug = new NpcType(578);
        public static readonly NpcType UnconsciousTavernkeep = new NpcType(579);
#pragma warning restore 1591

        private static readonly IDictionary<short, FieldInfo> IdToField = new Dictionary<short, FieldInfo>();
        private static readonly IDictionary<short, NpcType> IdToNpcType = new Dictionary<short, NpcType>();

        private static readonly ISet<NpcType> Catchables = new HashSet<NpcType> {
            Bunny,
            Goldfish,
            Bird,
            Penguin,
            BlackPenguin,
            BlueJay,
            Cardinal,
            Squirrel,
            Mouse,
            Firefly,
            Butterfly,
            Worm,
            LightningBug,
            Snail,
            GlowingSnail,
            Frog,
            Duck,
            Duck2,
            WhiteDuck,
            WhiteDuck2,
            BlackScorpion,
            Scorpion,
            TruffleWorm,
            Grasshopper,
            EnchantedNightcrawler,
            Grubby,
            Sluggy,
            Buggy,
            GoldBird,
            GoldBunny,
            GoldButterfly,
            GoldFrog,
            GoldGrasshopper,
            GoldMouse,
            GoldWorm,
            RedSquirrel,
            GoldSquirrel
        };

        /// <summary>
        /// Gets the NPC type's ID.
        /// </summary>
        public short Id { get; }

        /// <summary>
        /// Gets a value indicating whether the NPC type is catchable.
        /// </summary>
        public bool IsCatchable => Catchables.Contains(this);

        // Initializes lookup tables.
        static NpcType() {
            var fields = typeof(NpcType).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields) {
                if (!(field.GetValue(null) is NpcType npcType)) continue;

                IdToField[npcType.Id] = field;
                IdToNpcType[npcType.Id] = npcType;
            }
        }

        private NpcType(short id) {
            Id = id;
        }

        /// <summary>
        /// Returns an NPC type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The NPC type, or <c>null</c> if none exists.</returns>
        public static NpcType FromId(short id) => IdToNpcType.TryGetValue(id, out var npcType) ? npcType : null;

        /// <inheritdoc />
        public override string ToString() => IdToField[Id].Name;
    }
}
