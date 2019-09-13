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
using System.IO;
using System.Text;
using Orion.Networking.Packets.Connections;
using Orion.Networking.Packets.Events;
using Orion.Networking.Packets.Items;
using Orion.Networking.Packets.Misc;
using Orion.Networking.Packets.Modules;
using Orion.Networking.Packets.Npcs;
using Orion.Networking.Packets.Players;
using Orion.Networking.Packets.Projectiles;
using Orion.Networking.Packets.World;
using Orion.Networking.Packets.World.TileEntities;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Represents a packet. This is how clients communicate with the server and vice-versa.
    /// </summary>
    public abstract class Packet {
        private static readonly IDictionary<PacketType, Func<Packet>> PacketConstructors =
            new Dictionary<PacketType, Func<Packet>> {
                [PacketType.PlayerConnect] = () => new PlayerConnectPacket(),
                [PacketType.Disconnect] = () => new DisconnectPacket(),
                [PacketType.ContinueConnecting] = () => new ContinueConnectingPacket(),
                [PacketType.PlayerData] = () => new PlayerDataPacket(),
                [PacketType.PlayerInventorySlot] = () => new PlayerInventorySlotPacket(),
                [PacketType.FinishConnecting] = () => new FinishConnectingPacket(),
                [PacketType.WorldInfo] = () => new WorldInfoPacket(),
                [PacketType.RequestSection] = () => new RequestSectionPacket(),
                [PacketType.ClientStatus] = () => new ClientStatusPacket(),
                [PacketType.Section] = () => new SectionPacket(),
                [PacketType.SectionFrames] = () => new SectionFramesPacket(),
                [PacketType.SpawnPlayer] = () => new SpawnPlayerPacket(),
                [PacketType.PlayerInfo] = () => new PlayerInfoPacket(),
                [PacketType.PlayerStatus] = () => new PlayerStatusPacket(),
                [PacketType.PlayerHealth] = () => new PlayerHealthPacket(),
                [PacketType.TileModification] = () => new TileModificationPacket(),
                [PacketType.Time] = () => new TimePacket(),
                [PacketType.ToggleDoor] = () => new ToggleDoorPacket(),
                [PacketType.SquareTiles] = () => new SquareTilesPacket(),
                [PacketType.ItemInfo] = () => new ItemInfoPacket(),
                [PacketType.ItemOwner] = () => new ItemOwnerPacket(),
                [PacketType.NpcInfo] = () => new NpcInfoPacket(),
                [PacketType.DamageNpcWithItem] = () => new DamageNpcWithItemPacket(),
                [PacketType.ProjectileInfo] = () => new ProjectileInfoPacket(),
                [PacketType.DamageNpc] = () => new DamageNpcPacket(),
                [PacketType.RemoveProjectile] = () => new RemoveProjectilePacket(),
                [PacketType.PlayerPvp] = () => new PlayerPvpPacket(),
                [PacketType.RequestChest] = () => new RequestChestPacket(),
                [PacketType.ChestContentsSlot] = () => new ChestContentsSlotPacket(),
                [PacketType.PlayerChest] = () => new PlayerChestPacket(),
                [PacketType.ModifyChest] = () => new ModifyChestPacket(),
                [PacketType.HealEffect] = () => new HealEffectPacket(),
                [PacketType.PlayerZones] = () => new PlayerZonesPacket(),
                [PacketType.RequestPassword] = () => new RequestPasswordPacket(),
                [PacketType.PasswordResponse] = () => new PasswordResponsePacket(),
                [PacketType.RemoveItemOwner] = () => new RemoveItemOwnerPacket(),
                [PacketType.PlayerTalkingToNpc] = () => new PlayerTalkingToNpcPacket(),
                [PacketType.PlayerItemAnimation] = () => new PlayerItemAnimationPacket(),
                [PacketType.PlayerMana] = () => new PlayerManaPacket(),
                [PacketType.ManaEffect] = () => new ManaEffectPacket(),
                [PacketType.PlayerTeam] = () => new PlayerTeamPacket(),
                [PacketType.RequestSign] = () => new RequestSignPacket(),
                [PacketType.SignText] = () => new SignTextPacket(),
                [PacketType.TileLiquid] = () => new TileLiquidPacket(),
                [PacketType.EnterWorld] = () => new EnterWorldPacket(),
                [PacketType.PlayerBuffs] = () => new PlayerBuffsPacket(),
                [PacketType.MiscAction] = () => new MiscActionPacket(),
                [PacketType.UnlockObject] = () => new UnlockObjectPacket(),
                [PacketType.BuffNpc] = () => new BuffNpcPacket(),
                [PacketType.NpcBuffs] = () => new NpcBuffsPacket(),
                [PacketType.BuffPlayer] = () => new BuffPlayer(),
                [PacketType.NpcName] = () => new NpcNamePacket(),
                [PacketType.BiomeStats] = () => new BiomeStatsPacket(),
                [PacketType.PlayerHarpNote] = () => new PlayerHarpNotePacket(),
                [PacketType.ActivateWire] = () => new ActivateWirePacket(),
                [PacketType.NpcHome] = () => new NpcHomePacket(),
                [PacketType.BossOrInvasion] = () => new BossOrInvasionPacket(),
                [PacketType.PlayerDodge] = () => new PlayerDodgePacket(),
                [PacketType.PaintBlock] = () => new PaintBlockPacket(),
                [PacketType.PaintWall] = () => new PaintWallPacket(),
                [PacketType.EntityTeleportation] = () => new EntityTeleportationPacket(),
                [PacketType.HealPlayer] = () => new HealPlayerPacket(),
                [PacketType.ClientUuid] = () => new ClientUuidPacket(),
                [PacketType.ChestName] = () => new ChestNamePacket(),
                [PacketType.CatchNpc] = () => new CatchNpcPacket(),
                [PacketType.ReleaseNpc] = () => new ReleaseNpcPacket(),
                [PacketType.TravelingMerchantShop] = () => new TravelingMerchantShopPacket(),
                [PacketType.TeleportationPotion] = () => new TeleportationPotionPacket(),
                [PacketType.AnglerQuest] = () => new AnglerQuestPacket(),
                [PacketType.FinishAnglerQuest] = () => new FinishAnglerQuestPacket(),
                [PacketType.PlayerAnglerQuests] = () => new PlayerAnglerQuestsPacket(),
                [PacketType.TileAnimation] = () => new TileAnimationPacket(),
                [PacketType.InvasionInfo] = () => new InvasionInfoPacket(),
                [PacketType.PlaceObject] = () => new PlaceObjectPacket(),
                [PacketType.SyncPlayerChest] = () => new SyncPlayerChestPacket(),
                [PacketType.CombatNumber] = () => new CombatNumberPacket(),
                [PacketType.Module] = () => new ModulePacket(),
                [PacketType.NpcKillCount] = () => new NpcKillCountPacket(),
                [PacketType.PlayerStealth] = () => new PlayerStealthPacket(),
                [PacketType.MoveIntoChest] = () => new MoveIntoChestPacket(),
                [PacketType.TileEntityInfo] = () => new TileEntityInfoPacket(),
                [PacketType.PlaceTileEntity] = () => new PlaceTileEntityPacket(),
                [PacketType.AlterItem] = () => new AlterItemPacket(),
                [PacketType.ItemFrame] = () => new ItemFramePacket(),
                [PacketType.InstancedItemInfo] = () => new InstancedItemInfoPacket(),
                [PacketType.EmoteBubble] = () => new EmoteBubblePacket(),
                [PacketType.NpcStealCoin] = () => new NpcStealCoinPacket(),
                [PacketType.RemovePortal] = () => new RemovePortalPacket(),
                [PacketType.TeleportPlayerPortal] = () => new TeleportPlayerPortalPacket(),
                [PacketType.NpcTypeKilledEvent] = () => new NpcTypeKilledEventPacket(),
                [PacketType.ProgressionEvent] = () => new ProgressionEventPacket(),
                [PacketType.PlayerMinionPosition] = () => new PlayerMinionPositionPacket(),
                [PacketType.TeleportNpcPortal] = () => new TeleportNpcPortalPacket(),
                [PacketType.PillarShieldStrengths] = () => new PillarShieldStrengthsPacket(),
                [PacketType.NebulaBuff] = () => new NebulaBuffPacket(),
                [PacketType.MoonLordCountdown] = () => new MoonLordCountdownPacket(),
                [PacketType.NpcShopSlot] = () => new NpcShopSlotPacket(),
                [PacketType.ToggleGemLock] = () => new ToggleGemLockPacket(),
                [PacketType.PoofOfSmoke] = () => new PoofOfSmokePacket(),
                [PacketType.Chat] = () => new ChatPacket(),
                [PacketType.CannonShot] = () => new CannonShotPacket(),
                [PacketType.RequestMassWireOperation] = () => new RequestMassWireOperationPacket(),
                [PacketType.ConsumeItems] = () => new ConsumeItemsPacket(),
                [PacketType.ToggleBirthdayParty] = () => new ToggleBirthdayPartyPacket(),
                [PacketType.TreeGrowingEffect] = () => new TreeGrowingEffectPacket(),
                [PacketType.StartOldOnesArmy] = () => new StartOldOnesArmyPacket(),
                [PacketType.EndOldOnesArmy] = () => new EndOldOnesArmyPacket(),
                [PacketType.PlayerMinionNpc] = () => new PlayerMinionNpcPacket(),
                [PacketType.OldOnesArmyInfo] = () => new OldOnesArmyInfoPacket(),
                [PacketType.DamagePlayer] = () => new DamagePlayerPacket(),
                [PacketType.KillPlayer] = () => new KillPlayerPacket(),
                [PacketType.CombatText] = () => new CombatTextPacket()
            };

        internal abstract PacketType Type { get; }

        /// <summary>
        /// Reads a packet from the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">
        /// The context with which to read the packet. For example, the Server context means the packet should be
        /// read as if the client sent it.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <returns>The packet that was read.</returns>
        public static Packet ReadFromStream(Stream stream, PacketContext context) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            using (var reader = new BinaryReader(stream, Encoding.UTF8, true)) {
                var position = stream.Position;
                var packetLength = reader.ReadUInt16();
                var packetType = (PacketType)reader.ReadByte();
                var packet = PacketConstructors[packetType]();
                packet.ReadFromReader(reader, context);

                Debug.Assert(stream.Position - position == packetLength, "Packet should be fully consumed.");

                return packet;
            }
        }

        /// <summary>
        /// Writes the packet to the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">
        /// The context with which to write the packet. For example, the Server context means the packet should be
        /// written as if the server sent it.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">The packet cannot be written due to its length.</exception>
        public void WriteToStream(Stream stream, PacketContext context) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true)) {
                var startPosition = stream.Position;

                writer.Write((ushort)0);
                writer.Write((byte)Type);
                WriteToWriter(writer, context);

                var finalPosition = stream.Position;
                var packetLength = finalPosition - startPosition;

                // Ideally we would have thrown this exception a long time ago, when the packet is actually being
                // modified. Unfortunately, this is a major pain to implement properly.
                if (packetLength > ushort.MaxValue) {
                    throw new InvalidOperationException("Packet is too long.");
                }

                stream.Position = startPosition;
                writer.Write((ushort)packetLength);
                stream.Position = finalPosition;
            }
        }

        private protected abstract void ReadFromReader(BinaryReader reader, PacketContext context);
        private protected abstract void WriteToWriter(BinaryWriter writer, PacketContext context);
    }
}
