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
using Orion.Networking.Packets.Projectiles;
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
                [PacketType.PlayerData] = () => new PlayerDataPacket(),
                [PacketType.PlayerInventorySlot] = () => new PlayerInventorySlotPacket(),
                [PacketType.FinishConnecting] = () => new FinishConnectingPacket(),
                [PacketType.WorldInfo] = () => new WorldInfoPacket(),
                [PacketType.RequestSection] = () => new RequestSectionPacket(),
                [PacketType.ClientStatus] = () => new ClientStatusPacket(),
                [PacketType.Section] = () => new SectionPacket(),
                [PacketType.SyncTileFrames] = () => new SyncTileFramesPacket(),
                [PacketType.SpawnPlayer] = () => new SpawnPlayerPacket(),
                [PacketType.PlayerInfo] = () => new PlayerInfoPacket(),
                [PacketType.PlayerStatus] = () => new PlayerStatusPacket(),
                [PacketType.PlayerHealth] = () => new PlayerHealthPacket(),
                [PacketType.ModifyTile] = () => new ModifyTilePacket(),
                [PacketType.Time] = () => new TimePacket(),
                [PacketType.ToggleDoor] = () => new ToggleDoorPacket(),
                [PacketType.SquareOfTiles] = () => new SquareOfTilesPacket(),
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
                [PacketType.ShowHealEffect] = () => new ShowHealEffectPacket(),
                [PacketType.PlayerZones] = () => new PlayerZonesPacket(),
                [PacketType.RequestPassword] = () => new RequestPasswordPacket(),
                [PacketType.PasswordResponse] = () => new PasswordResponsePacket(),
                [PacketType.RemoveItemOwner] = () => new RemoveItemOwnerPacket(),
                [PacketType.PlayerTalkingToNpc] = () => new PlayerTalkingToNpcPacket(),
                [PacketType.PlayerItemAnimation] = () => new PlayerItemAnimationPacket(),
                [PacketType.PlayerMana] = () => new PlayerManaPacket(),
                [PacketType.ShowManaEffect] = () => new ShowManaEffectPacket(),
                [PacketType.PlayerTeam] = () => new PlayerTeamPacket(),
                [PacketType.RequestSign] = () => new RequestSignPacket(),
                [PacketType.SignText] = () => new SignTextPacket(),
                [PacketType.Liquid] = () => new LiquidPacket(),
                [PacketType.EnterWorld] = () => new EnterWorldPacket(),
                [PacketType.PlayerBuffs] = () => new PlayerBuffsPacket(),
                [PacketType.PerformMiscAction] = () => new PerformMiscActionPacket(),
                [PacketType.UnlockObject] = () => new UnlockObjectPacket(),
                [PacketType.AddBuffToNpc] = () => new AddBuffToNpcPacket(),
                [PacketType.NpcBuffs] = () => new NpcBuffsPacket(),
                [PacketType.AddBuffToPlayer] = () => new AddBuffToPlayerPacket(),
                [PacketType.NpcName] = () => new NpcNamePacket(),
                [PacketType.BiomeStats] = () => new BiomeStatsPacket(),
                [PacketType.PlayerHarpNote] = () => new PlayerHarpNotePacket(),
                [PacketType.ActivateWire] = () => new ActivateWirePacket(),
                [PacketType.NpcHome] = () => new NpcHomePacket(),
                [PacketType.SummonBossOrInvasion] = () => new SummonBossOrInvasionPacket(),
                [PacketType.PlayerDodge] = () => new PlayerDodgePacket(),
                [PacketType.PaintBlock] = () => new PaintBlockPacket(),
                [PacketType.PaintWall] = () => new PaintWallPacket(),
                [PacketType.EntityTeleport] = () => new EntityTeleportPacket(),
                [PacketType.HealPlayer] = () => new HealPlayerPacket(),
                [PacketType.ClientUuid] = () => new ClientUuidPacket(),
                [PacketType.ChestName] = () => new ChestNamePacket(),
                [PacketType.CatchNpc] = () => new CatchNpcPacket(),
                [PacketType.ReleaseNpc] = () => new ReleaseNpcPacket(),
                [PacketType.TravelingMerchantShop] = () => new TravelingMerchantShopPacket(),
                [PacketType.TeleportationPotion] = () => new TeleportationPotionPacket(),
                [PacketType.AnglerQuest] = () => new AnglerQuestPacket(),
                [PacketType.CompleteAnglerQuest] = () => new CompleteAnglerQuestPacket(),
                [PacketType.PlayerAnglerQuestsCompleted] = () => new PlayerAnglerQuestsCompletedPacket(),
                [PacketType.ShowTileAnimation] = () => new ShowTileAnimationPacket(),
                [PacketType.InvasionInfo] = () => new InvasionInfoPacket(),
                [PacketType.PlaceObject] = () => new PlaceObjectPacket(),
                [PacketType.SyncPlayerChest] = () => new SyncPlayerChestPacket(),
                [PacketType.ShowCombatNumber] = () => new ShowCombatNumberPacket(),
                [PacketType.NpcKillCount] = () => new NpcKillCountPacket(),
                [PacketType.PlayerStealth] = () => new PlayerStealthPacket(),
                [PacketType.MoveIntoChest] = () => new MoveIntoChestPacket(),
                [PacketType.TileEntity] = () => new TileEntityPacket(),
                [PacketType.PlaceTileEntity] = () => new PlaceTileEntityPacket(),
                [PacketType.AlterItem] = () => new AlterItemPacket(),
                [PacketType.ItemFrame] = () => new ItemFramePacket(),
                [PacketType.InstancedItemInfo] = () => new InstancedItemInfoPacket(),
                [PacketType.EmoteBubble] = () => new EmoteBubblePacket(),
                [PacketType.NpcStealCoins] = () => new NpcStealCoinsPacket(),
                [PacketType.RemovePortal] = () => new RemovePortalPacket(),
                [PacketType.TeleportPlayerPortal] = () => new TeleportPlayerPortalPacket(),
                [PacketType.NpcKilledEvent] = () => new NpcKilledEventPacket(),
                [PacketType.NotifyEventProgression] = () => new ProgressionEventPacket(),
                [PacketType.PlayerMinionPosition] = () => new PlayerMinionPositionPacket(),
                [PacketType.TeleportNpcPortal] = () => new TeleportNpcPortalPacket(),
                [PacketType.PillarShieldStrengths] = () => new PillarShieldStrengthsPacket(),
                [PacketType.SpreadNebulaBuff] = () => new SpreadNebulaBuffPacket(),
                [PacketType.MoonLordCountdown] = () => new MoonLordCountdownPacket(),
                [PacketType.NpcShopSlot] = () => new NpcShopSlotPacket(),
                [PacketType.ToggleGemLock] = () => new ToggleGemLockPacket(),
                [PacketType.ShowPoofOfSmoke] = () => new ShowPoofOfSmokePacket(),
                [PacketType.ShowChat] = () => new ShowChatPacket(),
                [PacketType.CannonShot] = () => new CannonShotPacket(),
                [PacketType.RequestMassWireOperation] = () => new RequestMassWireOperationPacket(),
                [PacketType.ConsumeItems] = () => new ConsumeItemsPacket(),
                [PacketType.ToggleBirthdayParty] = () => new ToggleBirthdayPartyPacket(),
                [PacketType.ShowTreeGrowingEffect] = () => new ShowTreeGrowingEffectPacket(),
                [PacketType.StartOldOnesArmy] = () => new StartOldOnesArmyPacket(),
                [PacketType.EndOldOnesArmy] = () => new EndOldOnesArmyPacket(),
                [PacketType.PlayerMinionNpc] = () => new PlayerMinionNpcPacket(),
                [PacketType.OldOnesArmyInfo] = () => new OldOnesArmyInfoPacket(),
                [PacketType.DamagePlayer] = () => new DamagePlayerPacket(),
                [PacketType.KillPlayer] = () => new KillPlayerPacket(),
                [PacketType.ShowCombatText] = () => new ShowCombatTextPacket(),
            };

        [ExcludeFromCodeCoverage]
        internal static int HeaderLength => sizeof(PacketType) + sizeof(short);

        private protected abstract PacketType Type { get; }

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
                var packet = PacketConstructors[packetType]();
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
