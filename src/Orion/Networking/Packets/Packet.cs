using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Orion.Networking.Packets.Connections;
using Orion.Networking.Packets.Players;

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
                [PacketType.FinishConnection] = () => new FinishConnectionPacket(),
                [PacketType.UpdateWorldInfo] = () => new UpdateWorldInfoPacket(),
                [PacketType.RequestWorldSection] = () => new RequestWorldSectionPacket(),
                [PacketType.UpdateClientStatus] = () => new UpdateClientStatusPacket(),
                [PacketType.UpdateWorldSection] = () => new UpdateWorldSectionPacket(),
                [PacketType.SyncTileFrames] = () => new SyncTileFramesPacket(),
                [PacketType.SpawnPlayer] = () => new SpawnPlayerPacket(),
                [PacketType.UpdatePlayer] = () => new UpdatePlayerPacket(),
                [PacketType.UpdatePlayerStatus] = () => new UpdatePlayerStatusPacket(),
                [PacketType.PlayerHealth] = () => new PlayerHealthPacket(),
                [PacketType.ModifyTile] = () => new ModifyTilePacket(),
                [PacketType.UpdateTime] = () => new UpdateTimePacket(),
                [PacketType.ToggleDoor] = () => new ToggleDoorPacket(),
                [PacketType.UpdateSquareOfTiles] = () => new UpdateSquareOfTilesPacket(),
                [PacketType.UpdateItem] = () => new UpdateItemPacket(),
                [PacketType.UpdateItemOwner] = () => new UpdateItemOwnerPacket(),
                [PacketType.UpdateNpc] = () => new UpdateNpcPacket(),
                [PacketType.DamageNpcWithSelectedItem] = () => new DamageNpcWithSelectedItemPacket(),
                [PacketType.UpdateProjectile] = () => new UpdateProjectilePacket(),
                [PacketType.DamageNpc] = () => new DamageNpcPacket(),
                [PacketType.RemoveProjectile] = () => new RemoveProjectilePacket(),
                [PacketType.PlayerPvp] = () => new PlayerPvpPacket(),
                [PacketType.RequestChestContents] = () => new RequestChestContentsPacket(),
                [PacketType.UpdateChestContentsSlot] = () => new UpdateChestContentsSlotPacket(),
                [PacketType.UpdatePlayerChest] = () => new UpdatePlayerChestPacket(),
                [PacketType.ModifyChest] = () => new ModifyChestPacket(),
                [PacketType.ShowHealEffect] = () => new ShowHealEffectPacket(),
                [PacketType.UpdatePlayerZones] = () => new UpdatePlayerZonesPacket(),
                [PacketType.RequestPassword] = () => new RequestPasswordPacket(),
                [PacketType.PasswordResponse] =
                    () => new PasswordResponsePacket(),
                [PacketType.RemoveItemOwner] = () => new RemoveItemOwnerPacket(),
                [PacketType.UpdatePlayerTalkingToNpc] = () => new UpdatePlayerTalkingToNpcPacket(),
                [PacketType.UpdatePlayerItemAnimation] = () => new UpdatePlayerItemAnimationPacket(),
                [PacketType.PlayerMana] = () => new PlayerManaPacket(),
                [PacketType.ShowManaEffect] = () => new ShowManaEffectPacket(),
                [PacketType.PlayerTeam] = () => new PlayerTeamPacket(),
                [PacketType.RequestSign] = () => new RequestSignPacket(),
                [PacketType.UpdateSign] = () => new UpdateSignPacket(),
                [PacketType.UpdateLiquid] = () => new UpdateLiquidPacket(),
                [PacketType.FirstSpawnPlayer] = () => new FirstSpawnPlayerPacket(),
                [PacketType.PlayerBuffs] = () => new PlayerBuffsPacket(),
                [PacketType.PerformAction] = () => new PerformActionPacket(),
                [PacketType.UnlockTile] = () => new UnlockTilePacket(),
                [PacketType.AddNpcBuff] = () => new AddNpcBuffPacket(),
                [PacketType.UpdateNpcBuffs] = () => new UpdateNpcBuffsPacket(),
                [PacketType.AddPlayerBuff] = () => new AddPlayerBuffPacket(),
                [PacketType.UpdateNpcName] = () => new UpdateNpcNamePacket(),
                [PacketType.UpdateBiomeStats] = () => new UpdateBiomeStatsPacket(),
                [PacketType.PlayHarpNote] = () => new PlayHarpNotePacket(),
                [PacketType.ActivateWiring] = () => new ActivateWiringPacket(),
                [PacketType.UpdateNpcHome] = () => new UpdateNpcHomePacket(),
                [PacketType.SummonBossOrInvasion] = () => new SummonBossOrInvasionPacket(),
                [PacketType.ShowPlayerDodge] = () => new ShowPlayerDodgePacket(),
                [PacketType.PaintBlock] = () => new PaintBlockPacket(),
                [PacketType.PaintWall] = () => new PaintWallPacket(),
                [PacketType.TeleportEntity] = () => new TeleportEntityPacket(),
                [PacketType.HealOtherPlayer] = () => new HealOtherPlayerPacket(),
                [PacketType.UpdateUuid] = () => new UpdateUuidPacket(),
                [PacketType.RequestOrUpdateChestName] = () => new RequestOrUpdateChestNamePacket(),
                [PacketType.CatchNpc] = () => new CatchNpcPacket(),
                [PacketType.ReleaseNpc] = () => new ReleaseNpcPacket(),
                [PacketType.UpdateTravelingMerchantInventory] = () => new UpdateTravelingMerchantInventoryPacket(),
                [PacketType.PerformTeleportationPotion] = () => new PerformTeleportationPotionPacket(),
                [PacketType.UpdateAnglerQuest] = () => new UpdateAnglerQuestPacket(),
                [PacketType.CompleteAnglerQuest] = () => new CompleteAnglerQuestPacket(),
                [PacketType.UpdateAnglerQuestsCompleted] = () => new UpdateAnglerQuestsCompletedPacket(),
                [PacketType.CreateTemporaryAnimation] = () => new CreateTemporaryAnimationPacket(),
                [PacketType.UpdateInvasion] = () => new UpdateInvasionPacket(),
                [PacketType.PlaceObject] = () => new PlaceObjectPacket(),
                [PacketType.UpdateOtherPlayerChest] = () => new UpdateOtherPlayerChestPacket(),
                [PacketType.CreateCombatText] = () => new CreateCombatTextPacket(),
                [PacketType.UpdateNpcKills] = () => new UpdateNpcKillsPacket(),
                [PacketType.UpdatePlayerStealth] = () => new UpdatePlayerStealthPacket(),
                [PacketType.MoveItemIntoChest] = () => new MoveItemIntoChestPacket(),
                [PacketType.UpdateTileEntity] = () => new UpdateTileEntityPacket(),
                [PacketType.PlaceTileEntity] = () => new PlaceTileEntityPacket(),
                [PacketType.AlterItem] = () => new AlterItemPacket(),
                [PacketType.PlaceItemFrame] = () => new PlaceItemFramePacket(),
                [PacketType.UpdateInstancedItem] = () => new UpdateInstancedItemPacket(),
                [PacketType.UpdateEmoteBubble] = () => new UpdateEmoteBubblePacket(),
                [PacketType.IncrementNpcCoins] = () => new IncrementNpcCoinsPacket(),
                [PacketType.RemovePortal] = () => new RemovePortalPacket(),
                [PacketType.TeleportPlayerThroughPortal] = () => new TeleportPlayerThroughPortalPacket(),
                [PacketType.NotifyNpcKill] = () => new NotifyNpcKillPacket(),
                [PacketType.NotifyEventProgression] = () => new NotifyEventProgressionPacket(),
                [PacketType.UpdatePlayerMinionTarget] = () => new UpdatePlayerMinionTargetPacket(),
                [PacketType.TeleportNpcThroughPortal] = () => new TeleportNpcThroughPortalPacket(),
                [PacketType.UpdatePillarShields] = () => new UpdatePillarShieldsPacket(),
                [PacketType.LevelUpNebulaArmor] = () => new LevelUpNebulaArmorPacket(),
                [PacketType.UpdateMoonLordCountdown] = () => new UpdateMoonLordCountdownPacket(),
                [PacketType.UpdateNpcShopSlot] = () => new UpdateNpcShopSlotPacket(),
                [PacketType.ToggleGemLock] = () => new ToggleGemLockPacket(),
                [PacketType.ShowPoofOfSmoke] = () => new ShowPoofOfSmokePacket(),
                [PacketType.ShowChat] = () => new ShowChatPacket(),
                [PacketType.ShootFromCannon] = () => new ShootFromCannonPacket(),
                [PacketType.RequestMassWireOperation] = () => new RequestMassWireOperationPacket(),
                [PacketType.ConsumeWires] = () => new ConsumeWiresPacket(),
                [PacketType.ToggleBirthdayParty] = () => new ToggleBirthdayPartyPacket(),
                [PacketType.ShowTreeEffect] = () => new ShowTreeEffectPacket(),
                [PacketType.StartOldOnesArmyInvasion] = () => new StartOldOnesArmyInvasionPacket(),
                [PacketType.EndOldOnesArmyInvasion] = () => new EndOldOnesArmyInvasionPacket(),
                [PacketType.UpdatePlayerMinionTargetNpc] = () => new UpdatePlayerMinionTargetNpcPacket(),
                [PacketType.UpdateOldOnesArmyInvasion] = () => new UpdateOldOnesArmyInvasionPacket(),
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

                // This is a hack until I focus down on a better implementation.
                var type = GetType();
                if (PacketTypes.TryGetValue(type, out var packetType)) {
                    writer.Write((byte)packetType);
                } else {
                    if (Enum.TryParse(type.Name.Replace("Packet", ""), out packetType)) {
                        PacketTypes[type] = packetType;
                        writer.Write((byte)packetType);
                    } else {
                        Debug.Assert(this is UnknownPacket, "Packet should be an UnknownPacket.");
                    }
                }

                WriteToWriter(writer);

                var finalPosition = stream.Position;
                var packetLength = finalPosition - startPosition;

                /*
                 * Ideally we would have thrown this exception a long time ago, when the packet is actually being
                 * modified. Unfortunately, this is a major pain to implement properly.
                 */
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
