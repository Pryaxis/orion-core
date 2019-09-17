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
    /// Represents a buff type.
    /// </summary>
    public sealed class BuffType {
#pragma warning disable 1591
        public static readonly BuffType None = new BuffType(0);
        public static readonly BuffType ObsidianSkin = new BuffType(1);
        public static readonly BuffType Regeneration = new BuffType(2);
        public static readonly BuffType Swiftness = new BuffType(3);
        public static readonly BuffType Gills = new BuffType(4);
        public static readonly BuffType Ironskin = new BuffType(5);
        public static readonly BuffType ManaRegeneration = new BuffType(6);
        public static readonly BuffType MagicPower = new BuffType(7);
        public static readonly BuffType Featherfall = new BuffType(8);
        public static readonly BuffType Spelunker = new BuffType(9);
        public static readonly BuffType Invisibility = new BuffType(10);
        public static readonly BuffType Shine = new BuffType(11);
        public static readonly BuffType NightOwl = new BuffType(12);
        public static readonly BuffType Battle = new BuffType(13);
        public static readonly BuffType Thorns = new BuffType(14);
        public static readonly BuffType WaterWalking = new BuffType(15);
        public static readonly BuffType Archery = new BuffType(16);
        public static readonly BuffType Hunter = new BuffType(17);
        public static readonly BuffType Gravitation = new BuffType(18);
        public static readonly BuffType ShadowOrb = new BuffType(19);
        public static readonly BuffType Poisoned = new BuffType(20);
        public static readonly BuffType PotionSickness = new BuffType(21);
        public static readonly BuffType Darkness = new BuffType(22);
        public static readonly BuffType Cursed = new BuffType(23);
        public static readonly BuffType OnFire = new BuffType(24);
        public static readonly BuffType Tipsy = new BuffType(25);
        public static readonly BuffType WellFed = new BuffType(26);
        public static readonly BuffType Fairy = new BuffType(27);
        public static readonly BuffType Werewolf = new BuffType(28);
        public static readonly BuffType Clairvoyance = new BuffType(29);
        public static readonly BuffType Bleeding = new BuffType(30);
        public static readonly BuffType Confused = new BuffType(31);
        public static readonly BuffType Slow = new BuffType(32);
        public static readonly BuffType Weak = new BuffType(33);
        public static readonly BuffType Merfolk = new BuffType(34);
        public static readonly BuffType Silenced = new BuffType(35);
        public static readonly BuffType BrokenArmor = new BuffType(36);
        public static readonly BuffType Horrified = new BuffType(37);
        public static readonly BuffType TheTongue = new BuffType(38);
        public static readonly BuffType CursedInferno = new BuffType(39);
        public static readonly BuffType PetBunny = new BuffType(40);
        public static readonly BuffType BabyPenguin = new BuffType(41);
        public static readonly BuffType PetTurtle = new BuffType(42);
        public static readonly BuffType PaladinsShield = new BuffType(43);
        public static readonly BuffType Frostburn = new BuffType(44);
        public static readonly BuffType BabyEater = new BuffType(45);
        public static readonly BuffType Chilled = new BuffType(46);
        public static readonly BuffType Frozen = new BuffType(47);
        public static readonly BuffType Honey = new BuffType(48);
        public static readonly BuffType Pygmies = new BuffType(49);
        public static readonly BuffType BabySkeletronHead = new BuffType(50);
        public static readonly BuffType BabyHornet = new BuffType(51);
        public static readonly BuffType TikiSpirit = new BuffType(52);
        public static readonly BuffType PetLizard = new BuffType(53);
        public static readonly BuffType PetParrot = new BuffType(54);
        public static readonly BuffType BabyTruffle = new BuffType(55);
        public static readonly BuffType PetSapling = new BuffType(56);
        public static readonly BuffType Wisp = new BuffType(57);
        public static readonly BuffType RapidHealing = new BuffType(58);
        public static readonly BuffType ShadowDodge = new BuffType(59);
        public static readonly BuffType LeafCrystal = new BuffType(60);
        public static readonly BuffType BabyDinosaur = new BuffType(61);
        public static readonly BuffType IceBarrier = new BuffType(62);
        public static readonly BuffType Panic = new BuffType(63);
        public static readonly BuffType BabySlime = new BuffType(64);
        public static readonly BuffType EyeballSpring = new BuffType(65);
        public static readonly BuffType BabySnowman = new BuffType(66);
        public static readonly BuffType Burning = new BuffType(67);
        public static readonly BuffType Suffocation = new BuffType(68);
        public static readonly BuffType Ichor = new BuffType(69);
        public static readonly BuffType Venom = new BuffType(70);
        public static readonly BuffType WeaponImbueVenom = new BuffType(71);
        public static readonly BuffType Midas = new BuffType(72);
        public static readonly BuffType WeaponImbueCursedFlames = new BuffType(73);
        public static readonly BuffType WeaponImbueFire = new BuffType(74);
        public static readonly BuffType WeaponImbueGold = new BuffType(75);
        public static readonly BuffType WeaponImbueIchor = new BuffType(76);
        public static readonly BuffType WeaponImbueNanites = new BuffType(77);
        public static readonly BuffType WeaponImbueConfetti = new BuffType(78);
        public static readonly BuffType WeaponImbuePoison = new BuffType(79);
        public static readonly BuffType Blackout = new BuffType(80);
        public static readonly BuffType PetSpider = new BuffType(81);
        public static readonly BuffType Squashling = new BuffType(82);
        public static readonly BuffType Ravens = new BuffType(83);
        public static readonly BuffType BlackCat = new BuffType(84);
        public static readonly BuffType CursedSapling = new BuffType(85);
        public static readonly BuffType WaterCandle = new BuffType(86);
        public static readonly BuffType CozyFire = new BuffType(87);
        public static readonly BuffType ChaosState = new BuffType(88);
        public static readonly BuffType HeartLamp = new BuffType(89);
        public static readonly BuffType Rudolph = new BuffType(90);
        public static readonly BuffType Puppy = new BuffType(91);
        public static readonly BuffType BabyGrinch = new BuffType(92);
        public static readonly BuffType AmmoBox = new BuffType(93);
        public static readonly BuffType ManaSickness = new BuffType(94);
        public static readonly BuffType BeetleEndurance1 = new BuffType(95);
        public static readonly BuffType BeetleEndurance2 = new BuffType(96);
        public static readonly BuffType BeetleEndurance3 = new BuffType(97);
        public static readonly BuffType BeetleMight1 = new BuffType(98);
        public static readonly BuffType BeetleMight2 = new BuffType(99);
        public static readonly BuffType BeetleMight3 = new BuffType(100);
        public static readonly BuffType FairyRed = new BuffType(101);
        public static readonly BuffType FairyGreen = new BuffType(102);
        public static readonly BuffType Wet = new BuffType(103);
        public static readonly BuffType Mining = new BuffType(104);
        public static readonly BuffType Heartreach = new BuffType(105);
        public static readonly BuffType Calm = new BuffType(106);
        public static readonly BuffType Builder = new BuffType(107);
        public static readonly BuffType Titan = new BuffType(108);
        public static readonly BuffType Flipper = new BuffType(109);
        public static readonly BuffType Summoning = new BuffType(110);
        public static readonly BuffType Dangersense = new BuffType(111);
        public static readonly BuffType AmmoReservation = new BuffType(112);
        public static readonly BuffType Lifeforce = new BuffType(113);
        public static readonly BuffType Endurance = new BuffType(114);
        public static readonly BuffType Rage = new BuffType(115);
        public static readonly BuffType Inferno = new BuffType(116);
        public static readonly BuffType Wrath = new BuffType(117);
        public static readonly BuffType MinecartLeft = new BuffType(118);
        public static readonly BuffType Lovestruck = new BuffType(119);
        public static readonly BuffType Stinky = new BuffType(120);
        public static readonly BuffType Fishing = new BuffType(121);
        public static readonly BuffType Sonar = new BuffType(122);
        public static readonly BuffType Crate = new BuffType(123);
        public static readonly BuffType Warmth = new BuffType(124);
        public static readonly BuffType Hornet = new BuffType(125);
        public static readonly BuffType Imp = new BuffType(126);
        public static readonly BuffType ZephyrFish = new BuffType(127);
        public static readonly BuffType BunnyMount = new BuffType(128);
        public static readonly BuffType PigronMount = new BuffType(129);
        public static readonly BuffType SlimeMount = new BuffType(130);
        public static readonly BuffType TurtleMount = new BuffType(131);
        public static readonly BuffType BeeMount = new BuffType(132);
        public static readonly BuffType Spider = new BuffType(133);
        public static readonly BuffType Twins = new BuffType(134);
        public static readonly BuffType Pirate = new BuffType(135);
        public static readonly BuffType MiniMinotaur = new BuffType(136);
        public static readonly BuffType Slime = new BuffType(137);
        public static readonly BuffType MinecartRight = new BuffType(138);
        public static readonly BuffType Sharknado = new BuffType(139);
        public static readonly BuffType Ufo = new BuffType(140);
        public static readonly BuffType UfoMount = new BuffType(141);
        public static readonly BuffType DrillMount = new BuffType(142);
        public static readonly BuffType ScutlixMount = new BuffType(143);
        public static readonly BuffType Electrified = new BuffType(144);
        public static readonly BuffType MoonBite = new BuffType(145);
        public static readonly BuffType Happy = new BuffType(146);
        public static readonly BuffType Banner = new BuffType(147);
        public static readonly BuffType FeralBite = new BuffType(148);
        public static readonly BuffType Webbed = new BuffType(149);
        public static readonly BuffType Bewitched = new BuffType(150);
        public static readonly BuffType LifeDrain = new BuffType(151);
        public static readonly BuffType MagicLantern = new BuffType(152);
        public static readonly BuffType Shadowflame = new BuffType(153);
        public static readonly BuffType BabyFaceMonster = new BuffType(154);
        public static readonly BuffType CrimsonHeart = new BuffType(155);
        public static readonly BuffType Stoned = new BuffType(156);
        public static readonly BuffType PeaceCandle = new BuffType(157);
        public static readonly BuffType StarInABottle = new BuffType(158);
        public static readonly BuffType Sharpened = new BuffType(159);
        public static readonly BuffType Dazed = new BuffType(160);
        public static readonly BuffType DeadlySphere = new BuffType(161);
        public static readonly BuffType UnicornMount = new BuffType(162);
        public static readonly BuffType Obstructed = new BuffType(163);
        public static readonly BuffType Distorted = new BuffType(164);
        public static readonly BuffType DryadsBlessing = new BuffType(165);
        public static readonly BuffType MinecartRightMechanical = new BuffType(166);
        public static readonly BuffType MinecartLeftMechanical = new BuffType(167);
        public static readonly BuffType CuteFishronMount = new BuffType(168);
        public static readonly BuffType Penetrated = new BuffType(169);
        public static readonly BuffType SolarBlaze1 = new BuffType(170);
        public static readonly BuffType SolarBlaze2 = new BuffType(171);
        public static readonly BuffType SolarBlaze3 = new BuffType(172);
        public static readonly BuffType LifeNebula1 = new BuffType(173);
        public static readonly BuffType LifeNebula2 = new BuffType(174);
        public static readonly BuffType LifeNebula3 = new BuffType(175);
        public static readonly BuffType ManaNebula1 = new BuffType(176);
        public static readonly BuffType ManaNebula2 = new BuffType(177);
        public static readonly BuffType ManaNebula3 = new BuffType(178);
        public static readonly BuffType DamageNebula1 = new BuffType(179);
        public static readonly BuffType DamageNebula2 = new BuffType(180);
        public static readonly BuffType DamageNebula3 = new BuffType(181);
        public static readonly BuffType StardustCell = new BuffType(182);
        public static readonly BuffType Celled = new BuffType(183);
        public static readonly BuffType MinecartLeftWooden = new BuffType(184);
        public static readonly BuffType MinecartRightWooden = new BuffType(185);
        public static readonly BuffType DryadsBane = new BuffType(186);
        public static readonly BuffType StardustGuardian = new BuffType(187);
        public static readonly BuffType StardustDragon = new BuffType(188);
        public static readonly BuffType Daybroken = new BuffType(189);
        public static readonly BuffType SuspiciousLookingEye = new BuffType(190);
        public static readonly BuffType CompanionCube = new BuffType(191);
        public static readonly BuffType SugarRush = new BuffType(192);
        public static readonly BuffType BasiliskMount = new BuffType(193);
        public static readonly BuffType MightyWind = new BuffType(194);
        public static readonly BuffType WitheredArmor = new BuffType(195);
        public static readonly BuffType WitheredWeapon = new BuffType(196);
        public static readonly BuffType Oozed = new BuffType(197);
        public static readonly BuffType StrikingMoment = new BuffType(198);
        public static readonly BuffType CreativeShock = new BuffType(199);
        public static readonly BuffType PropellerGato = new BuffType(200);
        public static readonly BuffType Flickerwick = new BuffType(201);
        public static readonly BuffType Hoardagron = new BuffType(202);
        public static readonly BuffType BetsysCurse = new BuffType(203);
        public static readonly BuffType Oiled = new BuffType(204);
        public static readonly BuffType BallistaPanic = new BuffType(205);
#pragma warning restore 1591

        private static readonly IDictionary<byte, FieldInfo> IdToField = new Dictionary<byte, FieldInfo>();
        private static readonly IDictionary<byte, BuffType> IdToBuffType = new Dictionary<byte, BuffType>();

        private static readonly ISet<BuffType> Debuffs = new HashSet<BuffType> {
            Poisoned,
            PotionSickness,
            Darkness,
            Cursed,
            OnFire,
            Tipsy,
            Werewolf,
            Bleeding,
            Confused,
            Slow,
            Weak,
            Merfolk,
            Silenced,
            BrokenArmor,
            Horrified,
            TheTongue,
            CursedInferno,
            Frostburn,
            Chilled,
            Frozen,
            Burning,
            Suffocation,
            Ichor,
            Venom,
            Blackout,
            WaterCandle,
            CozyFire,
            ChaosState,
            HeartLamp,
            ManaSickness,
            Wet,
            Lovestruck,
            Stinky,
            Slime,
            Electrified,
            MoonBite,
            Happy,
            Banner,
            FeralBite,
            Webbed,
            Stoned,
            PeaceCandle,
            StarInABottle,
            Dazed,
            Obstructed,
            Distorted,
            MightyWind,
            WitheredArmor,
            WitheredWeapon,
            Oozed,
            CreativeShock
        };

        /// <summary>
        /// Gets the buff type's ID.
        /// </summary>
        public byte Id { get; }

        /// <summary>
        /// Gets a value indicating whether the buff type is a debuff.
        /// </summary>
        public bool IsDebuff => Debuffs.Contains(this);

        private BuffType(byte id) {
            Id = id;
        }

        // Initializes lookup tables.
        static BuffType() {
            var fields = typeof(BuffType).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields) {
                if (!(field.GetValue(null) is BuffType buffType)) continue;

                IdToField[buffType.Id] = field;
                IdToBuffType[buffType.Id] = buffType;
            }
        }

        /// <summary>
        /// Returns a buff type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The buff type, or <c>null</c> if none exists.</returns>
        public static BuffType FromId(byte id) => IdToBuffType.TryGetValue(id, out var buffType) ? buffType : null;

        /// <inheritdoc />
        public override string ToString() => IdToField[Id].Name;
    }
}
