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

namespace Orion.Core.Packets
{
    /// <summary>
    /// Specifies a packet ID.
    /// </summary>
    public enum PacketId : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        ClientConnect = 1,
        ServerDisconnect = 2,
        ServerIndex = 3,
        PlayerCharacter = 4,
        PlayerInventory = 5,
        PlayerJoin = 6,
        ClientStatus = 9,
        ServerActivity = 14,
        PlayerHealth = 16,
        TileModify = 17,
        TileSquare = 20,
        ItemInfo = 21,
        ItemOwner = 22,
        NpcInfo = 23,
        NpcDamage = 28,
        PlayerPvp = 30,
        ChestOpen = 31,
        ChestInventory = 32,
        PlayerHealthEffect = 35,
        ServerPassworded = 37,
        ClientPassword = 38,
        ItemDisown = 39,
        PlayerMana = 42,
        PlayerManaEffect = 43,
        PlayerTeam = 45,
        SignRead = 46,
        TileLiquid = 48,
        PlayerBuffs = 50,
        ObjectUnlock = 52,
        NpcBuff = 53,
        PlayerBuff = 55,
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
        ServerCombatNumber = 81,
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
        ServerCombatText = 119,
        MannequinInventory = 121,
        WeaponRackInfo = 123,
        HatRackInventory = 124,
        NpcFish = 130,
        PlateInfo = 133,
        ServerHost = 139,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
