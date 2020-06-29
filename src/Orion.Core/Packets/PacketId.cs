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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Orion.Core.Packets.Items;
using Orion.Core.Packets.Misc;
using Orion.Core.Packets.Npcs;
using Orion.Core.Packets.Players;
using Orion.Core.Packets.Server;
using Orion.Core.Packets.World;
using Orion.Core.Packets.World.Chests;
using Orion.Core.Packets.World.Signs;
using Orion.Core.Packets.World.TileEntities;
using Orion.Core.Packets.World.Tiles;

namespace Orion.Core.Packets
{
    /// <summary>
    /// Specifies a packet ID.
    /// </summary>
    public enum PacketId : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        ClientConnect = 1,
        ClientDisconnect = 2,
        ClientIndex = 3,
        PlayerCharacter = 4,
        PlayerInventory = 5,
        PlayerJoin = 6,
        ClientStatus = 9,
        SectionFrames = 11,
        PlayerActive = 14,
        PlayerHealth = 16,
        TileModify = 17,
        TileSquare = 20,
        ItemInfo = 21,
        ItemOwn = 22,
        NpcInfo = 23,
        NpcDamage = 28,
        PlayerPvp = 30,
        ChestOpen = 31,
        ChestInventory = 32,
        PlayerHealthEffect = 35,
        PasswordRequest = 37,
        PasswordResponse = 38,
        ItemDisown = 39,
        PlayerMana = 42,
        PlayerManaEffect = 43,
        PlayerTeam = 45,
        SignRead = 46,
        TileLiquid = 48,
        PlayerBuffs = 50,
        ObjectUnlock = 52,
        NpcAddBuff = 53,
        PlayerAddBuff = 55,
        NpcName = 56,
        WireActivate = 59,
        PlayerDodge = 62,
        BlockPaint = 63,
        WallPaint = 64,
        PlayerHeal = 66,
        ClientUuid = 68,
        ChestName = 69,
        NpcCatch = 70,
        NpcRelease = 71,
        PlayerTeleport = 73,
        AnglerQuestInfo = 74,
        AnglerQuestComplete = 75,
        CombatNumber = 81,
        Module = 82,
        NpcKillCount = 83,
        PlayerStealth = 84,
        TileEntityInfo = 86,
        TileEntityPlace = 87,
        ItemFrameInfo = 89,
        InstancedItemInfo = 90,
        PlayerMinionPosition = 99,
        PlayerNebulaBuff = 102,
        MoonLordInfo = 103,
        GemLockToggle = 105,
        ServerChat = 107,
        WireOperations = 109,
        PartyToggle = 111,
        OldOnesArmyStart = 113,
        OldOnesArmyEnd = 114,
        PlayerMinionTarget = 115,
        OldOnesArmyInfo = 116,
        CombatText = 119,
        MannequinInventory = 121,
        WeaponRackInfo = 123,
        HatRackInventory = 124,
        NpcFish = 130,
        PlateInfo = 133,
        PlayerLuck = 134,
        PlayerDead = 135,
        NpcRemoveBuff = 137,
        PlayerHost = 139,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// Provides extensions for the <see cref="PacketId"/> enumeration.
    /// </summary>
    public static class PacketIdExtensions
    {
        private static readonly IDictionary<PacketId, Type> _types = new Dictionary<PacketId, Type>
        {
            [PacketId.ClientConnect] = typeof(ClientConnect),
            [PacketId.ClientDisconnect] = typeof(ClientDisconnect),
            [PacketId.ClientIndex] = typeof(ClientIndex),
            [PacketId.PlayerCharacter] = typeof(PlayerCharacter),
            [PacketId.PlayerInventory] = typeof(PlayerInventoryPacket),
            [PacketId.PlayerJoin] = typeof(PlayerJoinPacket),
            [PacketId.ClientStatus] = typeof(ClientStatus),
            [PacketId.SectionFrames] = typeof(SectionFramesPacket),
            [PacketId.PlayerActive] = typeof(PlayerActive),
            [PacketId.PlayerHealth] = typeof(PlayerHealthPacket),
            [PacketId.TileModify] = typeof(TileModifyPacket),
            [PacketId.TileSquare] = typeof(TileSquarePacket),
            [PacketId.ItemInfo] = typeof(ItemInfo),
            [PacketId.ItemOwn] = typeof(ItemOwn),
            [PacketId.PlayerMana] = typeof(PlayerManaPacket),
            [PacketId.PlayerManaEffect] = typeof(PlayerManaEffectPacket),
            [PacketId.NpcDamage] = typeof(NpcDamage),
            [PacketId.PlayerPvp] = typeof(PlayerPvpPacket),
            [PacketId.ChestOpen] = typeof(ChestOpenPacket),
            [PacketId.ChestInventory] = typeof(ChestInventoryPacket),
            [PacketId.PlayerHealthEffect] = typeof(PlayerHealthEffectPacket),
            [PacketId.PasswordRequest] = typeof(PasswordRequest),
            [PacketId.PasswordResponse] = typeof(PasswordResponse),
            [PacketId.ItemDisown] = typeof(ItemDisown),
            [PacketId.PlayerTeam] = typeof(PlayerTeamPacket),
            [PacketId.SignRead] = typeof(SignReadPacket),
            [PacketId.TileLiquid] = typeof(TileLiquidPacket),
            [PacketId.PlayerBuffs] = typeof(PlayerBuffsPacket),
            [PacketId.ObjectUnlock] = typeof(ObjectUnlockPacket),
            [PacketId.NpcAddBuff] = typeof(NpcAddBuff),
            [PacketId.PlayerAddBuff] = typeof(PlayerAddBuff),
            [PacketId.NpcName] = typeof(NpcName),
            [PacketId.WireActivate] = typeof(WireActivatePacket),
            [PacketId.PlayerDodge] = typeof(PlayerDodgePacket),
            [PacketId.BlockPaint] = typeof(BlockPaintPacket),
            [PacketId.WallPaint] = typeof(WallPaintPacket),
            [PacketId.PlayerHeal] = typeof(PlayerHealPacket),
            [PacketId.ClientUuid] = typeof(ClientUuid),
            [PacketId.ChestName] = typeof(ChestNamePacket),
            [PacketId.NpcCatch] = typeof(NpcCatch),
            [PacketId.NpcRelease] = typeof(NpcRelease),
            [PacketId.PlayerTeleport] = typeof(PlayerTeleportPacket),
            [PacketId.AnglerQuestInfo] = typeof(AnglerQuestInfoPacket),
            [PacketId.AnglerQuestComplete] = typeof(AnglerQuestCompletePacket),
            [PacketId.CombatNumber] = typeof(CombatNumber),
            [PacketId.NpcKillCount] = typeof(NpcKillCount),
            [PacketId.PlayerStealth] = typeof(PlayerStealthPacket),
            [PacketId.TileEntityInfo] = typeof(TileEntityInfoPacket),
            [PacketId.TileEntityPlace] = typeof(TileEntityPlacePacket),
            [PacketId.ItemFrameInfo] = typeof(ItemFrameInfoPacket),
            [PacketId.InstancedItemInfo] = typeof(InstancedItemInfo),
            [PacketId.PlayerMinionPosition] = typeof(PlayerMinionPositionPacket),
            [PacketId.PlayerNebulaBuff] = typeof(PlayerNebulaBuffPacket),
            [PacketId.MoonLordInfo] = typeof(MoonLordInfoPacket),
            [PacketId.GemLockToggle] = typeof(GemLockTogglePacket),
            [PacketId.ServerChat] = typeof(ServerChat),
            [PacketId.WireOperations] = typeof(WireOperationsPacket),
            [PacketId.PartyToggle] = typeof(PartyTogglePacket),
            [PacketId.OldOnesArmyStart] = typeof(OldOnesArmyStartPacket),
            [PacketId.OldOnesArmyEnd] = typeof(OldOnesArmyEndPacket),
            [PacketId.PlayerMinionTarget] = typeof(PlayerMinionTargetPacket),
            [PacketId.OldOnesArmyInfo] = typeof(OldOnesArmyInfoPacket),
            [PacketId.CombatText] = typeof(CombatText),
            [PacketId.MannequinInventory] = typeof(MannequinInventoryPacket),
            [PacketId.WeaponRackInfo] = typeof(WeaponRackInfoPacket),
            [PacketId.HatRackInventory] = typeof(HatRackInventoryPacket),
            [PacketId.NpcFish] = typeof(NpcFish),
            [PacketId.PlateInfo] = typeof(PlateInfoPacket),
            [PacketId.PlayerLuck] = typeof(PlayerLuckPacket),
            [PacketId.PlayerDead] = typeof(PlayerDeadPacket),
            [PacketId.NpcRemoveBuff] = typeof(NpcRemoveBuff),
            [PacketId.PlayerHost] = typeof(PlayerHost)
        };

        /// <summary>
        /// Gets the corresponding type for the packet ID.
        /// </summary>
        /// <param name="id">The packet ID.</param>
        /// <returns>The corresponding type for the packet ID.</returns>
        [Pure]
        public static Type Type(this PacketId id) =>
            _types.TryGetValue(id, out var type) ? type : typeof(UnknownPacket);
    }
}
