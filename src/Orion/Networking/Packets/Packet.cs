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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Orion.Networking.Packets.Players;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Represents a packet.
    /// </summary>
    public abstract class Packet {
        private static readonly IDictionary<PacketType, Func<Packet>> PacketConstructors =
            new Dictionary<PacketType, Func<Packet>> {
                [PacketType.PlayerConnect] = () => new PlayerConnectPacket(),
                [PacketType.PlayerDisconnect] = () => new PlayerDisconnectPacket(),
                [PacketType.PlayerContinueConnecting] = () => new PlayerContinueConnectingPacket(),
                [PacketType.PlayerData] = () => throw new NotImplementedException(),
                [PacketType.PlayerInventorySlot] = () => throw new NotImplementedException(),
                [PacketType.PlayerJoin] = () => new PlayerJoinPacket(),
                [PacketType.WorldInfo] = () => throw new NotImplementedException(),
                [PacketType.RequestSection] = () => throw new NotImplementedException(),
                [PacketType.PlayerStatus] = () => new PlayerStatusPacket(),
                [PacketType.Section] = () => throw new NotImplementedException(),
                [PacketType.SectionFrames] = () => throw new NotImplementedException(),
                [PacketType.SpawnPlayer] = () => throw new NotImplementedException(),
                [PacketType.PlayerInfo] = () => throw new NotImplementedException(),
                [PacketType.PlayerActivity] = () => throw new NotImplementedException(),
                [PacketType.PlayerHealth] = () => throw new NotImplementedException(),
                [PacketType.TileModification] = () => throw new NotImplementedException(),
                [PacketType.Time] = () => throw new NotImplementedException(),
                [PacketType.ToggleDoor] = () => throw new NotImplementedException(),
                [PacketType.SquareTiles] = () => throw new NotImplementedException(),
                [PacketType.ItemInfo] = () => throw new NotImplementedException(),
                [PacketType.ItemOwner] = () => throw new NotImplementedException(),
                [PacketType.NpcInfo] = () => throw new NotImplementedException(),
                [PacketType.DamageNpcWithItem] = () => throw new NotImplementedException(),
                [PacketType.ProjectileInfo] = () => throw new NotImplementedException(),
                [PacketType.DamageNpc] = () => throw new NotImplementedException(),
                [PacketType.RemoveProjectile] = () => throw new NotImplementedException(),
                [PacketType.PlayerPvp] = () => throw new NotImplementedException(),
                [PacketType.RequestChest] = () => throw new NotImplementedException(),
                [PacketType.ChestContentsSlot] = () => throw new NotImplementedException(),
                [PacketType.PlayerChest] = () => throw new NotImplementedException(),
                [PacketType.ModifyChest] = () => throw new NotImplementedException(),
                [PacketType.HealEffect] = () => throw new NotImplementedException(),
                [PacketType.PlayerZones] = () => throw new NotImplementedException(),
                [PacketType.PlayerPasswordChallenge] = () => new PlayerPasswordChallengePacket(),
                [PacketType.PlayerPasswordResponse] = () => new PlayerPasswordResponsePacket(),
                [PacketType.RemoveItemOwner] = () => throw new NotImplementedException(),
                [PacketType.PlayerTalkingToNpc] = () => throw new NotImplementedException(),
                [PacketType.PlayerItemAnimation] = () => throw new NotImplementedException(),
                [PacketType.PlayerMana] = () => throw new NotImplementedException(),
                [PacketType.ManaEffect] = () => throw new NotImplementedException(),
                [PacketType.PlayerTeam] = () => throw new NotImplementedException(),
                [PacketType.RequestSign] = () => throw new NotImplementedException(),
                [PacketType.SignText] = () => throw new NotImplementedException(),
                [PacketType.TileLiquid] = () => throw new NotImplementedException(),
                [PacketType.EnterWorld] = () => throw new NotImplementedException(),
                [PacketType.PlayerBuffs] = () => throw new NotImplementedException(),
                [PacketType.MiscAction] = () => throw new NotImplementedException(),
                [PacketType.UnlockObject] = () => throw new NotImplementedException(),
                [PacketType.BuffNpc] = () => throw new NotImplementedException(),
                [PacketType.NpcBuffs] = () => throw new NotImplementedException(),
                [PacketType.BuffPlayer] = () => throw new NotImplementedException(),
                [PacketType.NpcName] = () => throw new NotImplementedException(),
                [PacketType.BiomeStats] = () => throw new NotImplementedException(),
                [PacketType.PlayerHarpNote] = () => throw new NotImplementedException(),
                [PacketType.ActivateWire] = () => throw new NotImplementedException(),
                [PacketType.NpcHome] = () => throw new NotImplementedException(),
                [PacketType.BossOrInvasion] = () => throw new NotImplementedException(),
                [PacketType.PlayerDodge] = () => throw new NotImplementedException(),
                [PacketType.PaintBlock] = () => throw new NotImplementedException(),
                [PacketType.PaintWall] = () => throw new NotImplementedException(),
                [PacketType.EntityTeleportation] = () => throw new NotImplementedException(),
                [PacketType.HealPlayer] = () => throw new NotImplementedException(),
                [PacketType.PlayerUuid] = () => new PlayerUuidPacket(),
                [PacketType.ChestName] = () => throw new NotImplementedException(),
                [PacketType.CatchNpc] = () => throw new NotImplementedException(),
                [PacketType.ReleaseNpc] = () => throw new NotImplementedException(),
                [PacketType.TravelingMerchantShop] = () => throw new NotImplementedException(),
                [PacketType.TeleportationPotion] = () => throw new NotImplementedException(),
                [PacketType.AnglerQuest] = () => throw new NotImplementedException(),
                [PacketType.FinishAnglerQuest] = () => throw new NotImplementedException(),
                [PacketType.PlayerAnglerQuests] = () => throw new NotImplementedException(),
                [PacketType.TileAnimation] = () => throw new NotImplementedException(),
                [PacketType.InvasionInfo] = () => throw new NotImplementedException(),
                [PacketType.PlaceObject] = () => throw new NotImplementedException(),
                [PacketType.SyncPlayerChest] = () => throw new NotImplementedException(),
                [PacketType.CombatNumber] = () => throw new NotImplementedException(),
                [PacketType.Module] = () => throw new NotImplementedException(),
                [PacketType.NpcKillCount] = () => throw new NotImplementedException(),
                [PacketType.PlayerStealth] = () => throw new NotImplementedException(),
                [PacketType.MoveIntoChest] = () => throw new NotImplementedException(),
                [PacketType.TileEntityInfo] = () => throw new NotImplementedException(),
                [PacketType.PlaceTileEntity] = () => throw new NotImplementedException(),
                [PacketType.AlterItem] = () => throw new NotImplementedException(),
                [PacketType.ItemFrame] = () => throw new NotImplementedException(),
                [PacketType.InstancedItemInfo] = () => throw new NotImplementedException(),
                [PacketType.EmoteBubble] = () => throw new NotImplementedException(),
                [PacketType.NpcStealCoin] = () => throw new NotImplementedException(),
                [PacketType.RemovePortal] = () => throw new NotImplementedException(),
                [PacketType.TeleportPlayerPortal] = () => throw new NotImplementedException(),
                [PacketType.NpcTypeKilledEvent] = () => throw new NotImplementedException(),
                [PacketType.ProgressionEvent] = () => throw new NotImplementedException(),
                [PacketType.PlayerMinionPosition] = () => throw new NotImplementedException(),
                [PacketType.TeleportNpcPortal] = () => throw new NotImplementedException(),
                [PacketType.PillarShieldStrengths] = () => throw new NotImplementedException(),
                [PacketType.NebulaBuff] = () => throw new NotImplementedException(),
                [PacketType.MoonLordCountdown] = () => throw new NotImplementedException(),
                [PacketType.NpcShopSlot] = () => throw new NotImplementedException(),
                [PacketType.ToggleGemLock] = () => throw new NotImplementedException(),
                [PacketType.PoofOfSmoke] = () => throw new NotImplementedException(),
                [PacketType.Chat] = () => throw new NotImplementedException(),
                [PacketType.CannonShot] = () => throw new NotImplementedException(),
                [PacketType.RequestMassWireOperation] = () => throw new NotImplementedException(),
                [PacketType.ConsumeItems] = () => throw new NotImplementedException(),
                [PacketType.ToggleBirthdayParty] = () => throw new NotImplementedException(),
                [PacketType.TreeGrowingEffect] = () => throw new NotImplementedException(),
                [PacketType.StartOldOnesArmy] = () => throw new NotImplementedException(),
                [PacketType.EndOldOnesArmy] = () => throw new NotImplementedException(),
                [PacketType.PlayerMinionNpc] = () => throw new NotImplementedException(),
                [PacketType.OldOnesArmyInfo] = () => throw new NotImplementedException(),
                [PacketType.DamagePlayer] = () => throw new NotImplementedException(),
                [PacketType.KillPlayer] = () => throw new NotImplementedException(),
                [PacketType.CombatText] = () => throw new NotImplementedException()
            };

        /// <summary>
        /// Gets the packet's type.
        /// </summary>
        public abstract PacketType Type { get; }

        /// <summary>
        /// Gets a value indicating whether the packet is dirty.
        /// </summary>
        public bool IsDirty { get; private protected set; }

        /// <summary>
        /// Gets a value indicating whether the packet's length changed.
        /// </summary>
        public bool DidLengthChange { get; private protected set; }

        // Do not allow outside inheritance.
        private protected Packet() { }

        /// <summary>
        /// Reads and returns a packet from the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">The context with which to read the packet.</param>
        /// <returns>The resulting packet.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        public static Packet ReadFromStream(Stream stream, PacketContext context) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var reader = new BinaryReader(stream, Encoding.UTF8, true);
#if DEBUG
            var oldPosition = stream.Position;
            var packetLength = reader.ReadUInt16();
#else
            reader.ReadUInt16();
#endif
            var packetType = new PacketType(reader.ReadByte());
            var packet = PacketConstructors[packetType]();
            packet.ReadFromReader(reader, context);

#if DEBUG
            Debug.Assert(stream.Position - oldPosition == packetLength, "Packet should have been consumed.");
#endif
            return packet;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}";

        /// <summary>
        /// Writes the packet to the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">The context with which to read the packet.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        public void WriteToStream(Stream stream, PacketContext context) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var writer = new BinaryWriter(stream, Encoding.UTF8, true);
            var oldPosition = stream.Position;
            writer.Write((short)0);
            writer.Write(Type.Id);
            WriteToWriter(writer, context);

            var position = stream.Position;
            var packetLength = position - oldPosition;
            if (packetLength > ushort.MaxValue) {
                throw new InvalidOperationException("Packet is too long.");
            }

            stream.Position = oldPosition;
            writer.Write((ushort)packetLength);
            stream.Position = position;
        }

        private protected abstract void ReadFromReader(BinaryReader reader, PacketContext context);
        private protected abstract void WriteToWriter(BinaryWriter writer, PacketContext context);
    }
}
