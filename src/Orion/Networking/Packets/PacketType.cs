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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Represents a packet type.
    /// </summary>
    public sealed class PacketType {
#pragma warning disable 1591
        public static PacketType PlayerConnect = new PacketType(1);
        public static PacketType PlayerDisconnect = new PacketType(2);
        public static PacketType PlayerContinueConnecting = new PacketType(3);
        public static PacketType PlayerData = new PacketType(4);
        public static PacketType PlayerInventorySlot = new PacketType(5);
        public static PacketType PlayerJoin = new PacketType(6);
        public static PacketType WorldInfo = new PacketType(7);
        public static PacketType RequestSection = new PacketType(8);
        public static PacketType PlayerStatus = new PacketType(9);
        public static PacketType Section = new PacketType(10);
        public static PacketType SectionFrames = new PacketType(11);
        public static PacketType SpawnPlayer = new PacketType(12);
        public static PacketType PlayerInfo = new PacketType(13);
        public static PacketType PlayerActivity = new PacketType(14);
        public static PacketType PlayerHealth = new PacketType(16);
        public static PacketType TileModification = new PacketType(17);
        public static PacketType Time = new PacketType(18);
        public static PacketType ToggleDoor = new PacketType(19);
        public static PacketType SquareTiles = new PacketType(20);
        public static PacketType ItemInfo = new PacketType(21);
        public static PacketType ItemOwner = new PacketType(22);
        public static PacketType NpcInfo = new PacketType(23);
        public static PacketType DamageNpcHeldItem = new PacketType(24);
        public static PacketType ProjectileInfo = new PacketType(27);
        public static PacketType DamageNpc = new PacketType(28);
        public static PacketType RemoveProjectile = new PacketType(29);
        public static PacketType PlayerPvp = new PacketType(30);
        public static PacketType RequestChest = new PacketType(31);
        public static PacketType ChestContentsSlot = new PacketType(32);
        public static PacketType PlayerChest = new PacketType(33);
        public static PacketType ModifyChest = new PacketType(34);
        public static PacketType HealEffect = new PacketType(35);
        public static PacketType PlayerZones = new PacketType(36);
        public static PacketType PlayerPasswordChallenge = new PacketType(37);
        public static PacketType PlayerPasswordResponse = new PacketType(38);
        public static PacketType RemoveItemOwner = new PacketType(39);
        public static PacketType PlayerTalkingToNpc = new PacketType(40);
        public static PacketType PlayerItemAnimation = new PacketType(41);
        public static PacketType PlayerMana = new PacketType(42);
        public static PacketType ManaEffect = new PacketType(43);
        public static PacketType PlayerTeam = new PacketType(45);
        public static PacketType RequestSign = new PacketType(46);
        public static PacketType SignText = new PacketType(47);
        public static PacketType TileLiquid = new PacketType(48);
        public static PacketType PlayerEnterWorld = new PacketType(49);
        public static PacketType PlayerBuffs = new PacketType(50);
        public static PacketType MiscAction = new PacketType(51);
        public static PacketType UnlockObject = new PacketType(52);
        public static PacketType BuffNpc = new PacketType(53);
        public static PacketType NpcBuffs = new PacketType(54);
        public static PacketType BuffPlayer = new PacketType(55);
        public static PacketType NpcName = new PacketType(56);
        public static PacketType BiomeStats = new PacketType(57);
        public static PacketType PlayerHarpNote = new PacketType(58);
        public static PacketType ActivateWire = new PacketType(59);
        public static PacketType NpcHome = new PacketType(60);
        public static PacketType BossOrInvasion = new PacketType(61);
        public static PacketType PlayerDodge = new PacketType(62);
        public static PacketType PaintBlock = new PacketType(63);
        public static PacketType PaintWall = new PacketType(64);
        public static PacketType EntityTeleportation = new PacketType(65);
        public static PacketType HealPlayer = new PacketType(66);
        public static PacketType PlayerUuid = new PacketType(68);
        public static PacketType ChestName = new PacketType(69);
        public static PacketType CatchNpc = new PacketType(70);
        public static PacketType ReleaseNpc = new PacketType(71);
        public static PacketType TravelingMerchantShop = new PacketType(72);
        public static PacketType PlayerTeleportationPotion = new PacketType(73);
        public static PacketType AnglerQuest = new PacketType(74);
        public static PacketType FinishAnglerQuest = new PacketType(75);
        public static PacketType PlayerAnglerQuests = new PacketType(76);
        public static PacketType TileAnimation = new PacketType(77);
        public static PacketType InvasionInfo = new PacketType(78);
        public static PacketType PlaceObject = new PacketType(79);
        public static PacketType SyncPlayerChest = new PacketType(80);
        public static PacketType CombatNumber = new PacketType(81);
        public static PacketType Module = new PacketType(82);
        public static PacketType NpcKillCount = new PacketType(83);
        public static PacketType PlayerStealth = new PacketType(84);
        public static PacketType MoveIntoChest = new PacketType(85);
        public static PacketType TileEntityInfo = new PacketType(86);
        public static PacketType PlaceTileEntity = new PacketType(87);
        public static PacketType AlterItem = new PacketType(88);
        public static PacketType ItemFrame = new PacketType(89);
        public static PacketType InstancedItemInfo = new PacketType(90);
        public static PacketType EmoteBubble = new PacketType(91);
        public static PacketType NpcStealCoin = new PacketType(92);
        public static PacketType RemovePortal = new PacketType(95);
        public static PacketType TeleportPlayerPortal = new PacketType(96);
        public static PacketType NpcTypeKilledEvent = new PacketType(97);
        public static PacketType ProgressionEvent = new PacketType(98);
        public static PacketType PlayerMinionPosition = new PacketType(99);
        public static PacketType TeleportNpcPortal = new PacketType(100);
        public static PacketType PillarShieldStrengths = new PacketType(101);
        public static PacketType NebulaBuffPlayers = new PacketType(102);
        public static PacketType MoonLordCountdown = new PacketType(103);
        public static PacketType NpcShopSlot = new PacketType(104);
        public static PacketType ToggleGemLock = new PacketType(105);
        public static PacketType PoofOfSmoke = new PacketType(106);
        public static PacketType Chat = new PacketType(107);
        public static PacketType CannonShot = new PacketType(108);
        public static PacketType MassWireOperation = new PacketType(109);
        public static PacketType ConsumeItems = new PacketType(110);
        public static PacketType ToggleBirthdayParty = new PacketType(111);
        public static PacketType TreeGrowingEffect = new PacketType(112);
        public static PacketType StartOldOnesArmy = new PacketType(113);
        public static PacketType EndOldOnesArmy = new PacketType(114);
        public static PacketType PlayerMinionNpc = new PacketType(115);
        public static PacketType OldOnesArmyInfo = new PacketType(116);
        public static PacketType DamagePlayer = new PacketType(117);
        public static PacketType KillPlayer = new PacketType(118);
        public static PacketType CombatText = new PacketType(119);
#pragma warning disable 1591

        private static readonly IDictionary<byte, FieldInfo> IdToField = new Dictionary<byte, FieldInfo>();
        private static readonly IDictionary<byte, PacketType> IdToPacketType = new Dictionary<byte, PacketType>();

        private static readonly IDictionary<PacketType, Func<Packet>> PacketConstructors =
            new Dictionary<PacketType, Func<Packet>>();

        /// <summary>
        /// Gets the packet type's ID.
        /// </summary>
        public byte Id { get; }

        /// <summary>
        /// Gets the packet type's constructor.
        /// </summary>
        public Func<Packet> Constructor => PacketConstructors[this];

        // Initializes lookup tables.
        static PacketType() {
            var fields = typeof(PacketType).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields) {
                if (!(field.GetValue(null) is PacketType packetType)) continue;

                IdToField[packetType.Id] = field;
                IdToPacketType[packetType.Id] = packetType;
            }

            foreach (var type in typeof(Packet).Assembly.ExportedTypes
                                               .Where(t => t.IsSubclassOf(typeof(Packet)) && t != typeof(Packet))) {
                var packetType = ((Packet)Activator.CreateInstance(type)).Type;
                PacketConstructors[packetType] = () => (Packet)Activator.CreateInstance(type);
            }
        }

        private PacketType(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns a packet type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The packet type.</returns>
        public static PacketType FromId(byte id) =>
            IdToPacketType.TryGetValue(id, out var packetType) ? packetType : null;

        /// <inheritdoc />
        public override string ToString() => IdToField[Id].Name;
    }
}
