using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Orion.Networking.Packets.Connections;
using Orion.Networking.Packets.Events;
using Orion.Networking.Packets.Items;
using Orion.Networking.Packets.Misc;
using Orion.Networking.Packets.Npcs;
using Orion.Networking.Packets.Players;
using Orion.Networking.Packets.World;
using Orion.Networking.Packets.World.TileEntities;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Represents a Terraria packet.
    /// </summary>
    /// <remarks>
    /// Terraria packets are limited to a maximum of 65536 bytes, but this restriction is not enforced immediately on
    /// each of the packet types. Instead, this restriction is enforced when the packet is sent.
    /// </remarks>
    public abstract class Packet {
        private static readonly IDictionary<PacketType, Func<Packet>> PacketConstructors =
            new Dictionary<PacketType, Func<Packet>> {
                [PacketType.Connect] = () => new ConnectPacket(),
                [PacketType.Disconnect] = () => new DisconnectPacket(),
                [PacketType.ContinueConnecting] = () => new ContinueConnectingPacket(),
                [PacketType.PlayerInfo] = () => new PlayerInfoPacket(),
                [PacketType.PlayerInventorySlot] = () => new PlayerInventorySlotPacket(),
                [PacketType.FinishConnecting] = () => new FinishConnectingPacket(),
                [PacketType.WorldInfo] = () => new WorldInfoPacket(),
                [PacketType.RequestSection] = () => new RequestSectionPacket(),
                [PacketType.ClientStatus] = () => new ClientStatusPacket(),
                [PacketType.Section] = () => new SectionPacket(),
                [PacketType.SyncTileFrames] = () => new SyncTileFramesPacket(),
                [PacketType.SpawnPlayer] = () => new SpawnPlayerPacket(),
                [PacketType.UpdatePlayer] = () => new UpdatePlayerPacket(),
                [PacketType.UpdatePlayerStatus] = () => new UpdatePlayerStatusPacket(),
                [PacketType.PlayerHealth] = () => new PlayerHealthPacket(),
                [PacketType.ModifyTile] = () => new ModifyTilePacket(),
                [PacketType.Time] = () => new TimePacket(),
                [PacketType.ToggleDoor] = () => new ToggleDoorPacket(),
                [PacketType.SquareOfTiles] = () => new SquareOfTilesPacket(),
                [PacketType.ItemInfo] = () => new ItemInfoPacket(),
                [PacketType.ItemOwner] = () => new ItemOwnerPacket(),
                [PacketType.NpcInfo] = () => new NpcInfoPacket(),
                [PacketType.DamageNpcWithItem] = () => new DamageNpcWithItemPacket(),
                [PacketType.UpdateProjectile] = () => new UpdateProjectilePacket(),
                [PacketType.DamageNpc] = () => new DamageNpcPacket(),
                [PacketType.RemoveProjectile] = () => new RemoveProjectilePacket(),
                [PacketType.PlayerPvp] = () => new PlayerPvpPacket(),
                [PacketType.RequestChest] = () => new RequestChestPacket(),
                [PacketType.ChestContentsSlot] = () => new ChestContentsSlotPacket(),
                [PacketType.UpdatePlayerChest] = () => new UpdatePlayerChestPacket(),
                [PacketType.ModifyChest] = () => new ModifyChestPacket(),
                [PacketType.ShowHealEffect] = () => new ShowHealEffectPacket(),
                [PacketType.PlayerZones] = () => new PlayerZonesPacket(),
                [PacketType.RequestPassword] = () => new RequestPasswordPacket(),
                [PacketType.PasswordResponse] = () => new PasswordResponsePacket(),
                [PacketType.RemoveItemOwner] = () => new RemoveItemOwnerPacket(),
                [PacketType.PlayerTalkingToNpc] = () => new PlayerTalkingToNpcPacket(),
                [PacketType.UpdatePlayerItemAnimation] = () => new UpdatePlayerItemAnimationPacket(),
                [PacketType.PlayerMana] = () => new PlayerManaPacket(),
                [PacketType.ShowManaEffect] = () => new ShowManaEffectPacket(),
                [PacketType.PlayerTeam] = () => new PlayerTeamPacket(),
                [PacketType.RequestSign] = () => new RequestSignPacket(),
                [PacketType.SignText] = () => new SignTextPacket(),
                [PacketType.Liquid] = () => new LiquidPacket(),
                [PacketType.EnterWorld] = () => new EnterWorldPacket(),
                [PacketType.PlayerBuffs] = () => new PlayerBuffsPacket(),
                [PacketType.PerformAction] = () => new PerformActionPacket(),
                [PacketType.UnlockObject] = () => new UnlockObjectPacket(),
                [PacketType.AddBuffToNpc] = () => new AddBuffToNpcPacket(),
                [PacketType.NpcBuffs] = () => new NpcBuffsPacket(),
                [PacketType.AddBuffToPlayer] = () => new AddBuffToPlayerPacket(),
                [PacketType.NpcName] = () => new NpcNamePacket(),
                [PacketType.UpdateBiomeStats] = () => new UpdateBiomeStatsPacket(),
                [PacketType.PlayHarpNote] = () => new PlayHarpNotePacket(),
                [PacketType.ActivateWire] = () => new ActivateWirePacket(),
                [PacketType.NpcHome] = () => new NpcHomePacket(),
                [PacketType.SummonBossOrInvasion] = () => new SummonBossOrInvasionPacket(),
                [PacketType.PlayerDodge] = () => new PlayerDodgePacket(),
                [PacketType.PaintBlock] = () => new PaintBlockPacket(),
                [PacketType.PaintWall] = () => new PaintWallPacket(),
                [PacketType.TeleportEntity] = () => new TeleportEntityPacket(),
                [PacketType.HealPlayer] = () => new HealPlayerPacket(),
                [PacketType.ClientUuid] = () => new ClientUuidPacket(),
                [PacketType.RequestOrUpdateChestName] = () => new RequestOrUpdateChestNamePacket(),
                [PacketType.CatchNpc] = () => new CatchNpcPacket(),
                [PacketType.ReleaseNpc] = () => new ReleaseNpcPacket(),
                [PacketType.UpdateTravelingMerchantInventory] = () => new UpdateTravelingMerchantInventoryPacket(),
                [PacketType.TeleportationPotion] = () => new TeleportationPotionPacket(),
                [PacketType.AnglerQuest] = () => new AnglerQuestPacket(),
                [PacketType.CompleteAnglerQuest] = () => new CompleteAnglerQuestPacket(),
                [PacketType.PlayerAnglerQuestsCompleted] = () => new PlayerAnglerQuestsCompletedPacket(),
                [PacketType.CreateTemporaryAnimation] = () => new CreateTemporaryAnimationPacket(),
                [PacketType.InvasionInfo] = () => new InvasionInfoPacket(),
                [PacketType.PlaceObject] = () => new PlaceObjectPacket(),
                [PacketType.UpdateOtherPlayerChest] = () => new UpdateOtherPlayerChestPacket(),
                [PacketType.ShowCombatNumber] = () => new ShowCombatNumberPacket(),
                [PacketType.UpdateNpcKills] = () => new UpdateNpcKillsPacket(),
                [PacketType.PlayerStealth] = () => new PlayerStealthPacket(),
                [PacketType.MoveItemIntoChest] = () => new MoveItemIntoChestPacket(),
                [PacketType.TileEntity] = () => new TileEntityPacket(),
                [PacketType.PlaceTileEntity] = () => new PlaceTileEntityPacket(),
                [PacketType.AlterItem] = () => new AlterItemPacket(),
                [PacketType.PlaceItemFrame] = () => new PlaceItemFramePacket(),
                [PacketType.InstancedItemInfo] = () => new InstancedItemInfoPacket(),
                [PacketType.UpdateEmoteBubble] = () => new UpdateEmoteBubblePacket(),
                [PacketType.NpcStealCoins] = () => new NpcStealCoinsPacket(),
                [PacketType.RemovePortal] = () => new RemovePortalPacket(),
                [PacketType.TeleportPlayerThroughPortal] = () => new TeleportPlayerThroughPortalPacket(),
                [PacketType.NotifyNpcKill] = () => new NotifyNpcKillPacket(),
                [PacketType.NotifyEventProgression] = () => new NotifyEventProgressionPacket(),
                [PacketType.UpdatePlayerMinionTarget] = () => new UpdatePlayerMinionTargetPacket(),
                [PacketType.TeleportNpcThroughPortal] = () => new TeleportNpcThroughPortalPacket(),
                [PacketType.PillarShieldStrengths] = () => new PillarShieldStrengthsPacket(),
                [PacketType.LevelUpNebulaArmor] = () => new LevelUpNebulaArmorPacket(),
                [PacketType.UpdateMoonLordCountdown] = () => new UpdateMoonLordCountdownPacket(),
                [PacketType.NpcShopSlot] = () => new NpcShopSlotPacket(),
                [PacketType.ToggleGemLock] = () => new ToggleGemLockPacket(),
                [PacketType.ShowPoofOfSmoke] = () => new ShowPoofOfSmokePacket(),
                [PacketType.ShowChat] = () => new ShowChatPacket(),
                [PacketType.ShootFromCannon] = () => new ShootFromCannonPacket(),
                [PacketType.RequestMassWireOperation] = () => new RequestMassWireOperationPacket(),
                [PacketType.ConsumeItems] = () => new ConsumeItemsPacket(),
                [PacketType.ToggleBirthdayParty] = () => new ToggleBirthdayPartyPacket(),
                [PacketType.ShowTreeGrowingEffect] = () => new ShowTreeGrowingEffectPacket(),
                [PacketType.StartOldOnesArmy] = () => new StartOldOnesArmyPacket(),
                [PacketType.EndOldOnesArmy] = () => new EndOldOnesArmyPacket(),
                [PacketType.UpdatePlayerMinionTargetNpc] = () => new UpdatePlayerMinionTargetNpcPacket(),
                [PacketType.OldOnesArmyInfo] = () => new OldOnesArmyInfoPacket(),
                [PacketType.HurtPlayer] = () => new HurtPlayerPacket(),
                [PacketType.KillPlayer] = () => new KillPlayerPacket(),
                [PacketType.ShowCombatText] = () => new ShowCombatTextPacket(),
            };

        private static readonly IDictionary<Type, PacketType> PacketTypes = new Dictionary<Type, PacketType>();

        [ExcludeFromCodeCoverage]
        internal static int HeaderLength => sizeof(PacketType) + sizeof(short);

        private protected virtual PacketType Type => throw new NotImplementedException();

        /// <summary>
        /// Reads a packet from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <returns>The packet.</returns>
        public static Packet ReadFromStream(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var reader = new BinaryReader(stream, Encoding.UTF8, true)) {
                var position = stream.Position;
                var packetLength = reader.ReadUInt16();
                var packetType = (PacketType)reader.ReadByte();
                var packetCtor =
                    PacketConstructors.TryGetValue(packetType, out var f) ? f : () => new UnknownPacket(packetType);
                var packet = packetCtor();
                packet.ReadFromReader(reader, packetLength);
                
                Debug.Assert(stream.Position - position == packetLength, "Packet should be fully consumed.");

                return packet;
            }
        }

        /// <summary>
        /// Writes the packet to the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">The packet cannot be written due to its length.</exception>
        public void WriteToStream(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true)) {
                var startPosition = stream.Position;

                writer.Write((ushort)0);
                writer.Write((byte)Type);
                WriteToWriter(writer);

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

        private protected abstract void ReadFromReader(BinaryReader reader, ushort packetLength);
        private protected abstract void WriteToWriter(BinaryWriter writer);
    }
}
