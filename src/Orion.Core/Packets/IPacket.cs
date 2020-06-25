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
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Orion.Core.Packets.Client;
using Orion.Core.Packets.Items;
using Orion.Core.Packets.Modules;
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
    /// Represents a packet, the main form of communication between the server and its clients.
    /// </summary>
    public interface IPacket
    {
        private static readonly IDictionary<PacketId, Func<IPacket>> _constructors =
            new Dictionary<PacketId, Func<IPacket>>
            {
                [PacketId.ClientConnect] = () => new ClientConnectPacket(),
                [PacketId.ServerDisconnect] = () => new ServerDisconnectPacket(),
                [PacketId.ServerIndex] = () => new ServerIndexPacket(),
                [PacketId.PlayerCharacter] = () => new PlayerCharacterPacket(),
                [PacketId.PlayerInventory] = () => new PlayerInventoryPacket(),
                [PacketId.PlayerJoin] = () => new PlayerJoinPacket(),
                [PacketId.ClientStatus] = () => new ClientStatusPacket(),
                [PacketId.ServerActivity] = () => new ServerActivityPacket(),
                [PacketId.PlayerHealth] = () => new PlayerHealthPacket(),
                [PacketId.TileModify] = () => new TileModifyPacket(),
                [PacketId.TileSquare] = () => new TileSquarePacket(),
                [PacketId.ItemInfo] = () => new ItemInfoPacket(),
                [PacketId.ItemOwner] = () => new ItemOwnerPacket(),
                [PacketId.PlayerMana] = () => new PlayerManaPacket(),
                [PacketId.PlayerManaEffect] = () => new PlayerManaEffectPacket(),
                [PacketId.NpcDamage] = () => new NpcDamagePacket(),
                [PacketId.PlayerPvp] = () => new PlayerPvpPacket(),
                [PacketId.ChestOpen] = () => new ChestOpenPacket(),
                [PacketId.ChestInventory] = () => new ChestInventoryPacket(),
                [PacketId.PlayerHealthEffect] = () => new PlayerHealthEffectPacket(),
                [PacketId.ServerPassworded] = () => new ServerPasswordedPacket(),
                [PacketId.ClientPassword] = () => new ClientPasswordPacket(),
                [PacketId.ItemDisown] = () => new ItemDisownPacket(),
                [PacketId.PlayerTeam] = () => new PlayerTeamPacket(),
                [PacketId.SignRead] = () => new SignReadPacket(),
                [PacketId.TileLiquid] = () => new TileLiquidPacket(),
                [PacketId.PlayerBuffs] = () => new PlayerBuffsPacket(),
                [PacketId.ObjectUnlock] = () => new ObjectUnlockPacket(),
                [PacketId.NpcBuff] = () => new NpcBuffPacket(),
                [PacketId.PlayerBuff] = () => new PlayerBuffPacket(),
                [PacketId.NpcName] = () => new NpcNamePacket(),
                [PacketId.WireActivate] = () => new WireActivatePacket(),
                [PacketId.PlayerDodge] = () => new PlayerDodgePacket(),
                [PacketId.BlockPaint] = () => new BlockPaintPacket(),
                [PacketId.WallPaint] = () => new WallPaintPacket(),
                [PacketId.PlayerHeal] = () => new PlayerHealPacket(),
                [PacketId.ClientUuid] = () => new ClientUuidPacket(),
                [PacketId.ChestName] = () => new ChestNamePacket(),
                [PacketId.NpcCatch] = () => new NpcCatchPacket(),
                [PacketId.NpcRelease] = () => new NpcReleasePacket(),
                [PacketId.PlayerTeleport] = () => new PlayerTeleportPacket(),
                [PacketId.AnglerQuestInfo] = () => new AnglerQuestInfoPacket(),
                [PacketId.AnglerQuestComplete] = () => new AnglerQuestCompletePacket(),
                [PacketId.ServerCombatNumber] = () => new ServerCombatNumberPacket(),
                [PacketId.Module] = () => new ModulePacket(),
                [PacketId.PlayerStealth] = () => new PlayerStealthPacket(),
                [PacketId.TileEntityPlace] = () => new TileEntityPlacePacket(),
                [PacketId.ItemFrameInfo] = () => new ItemFrameInfoPacket(),
                [PacketId.InstancedItemInfo] = () => new InstancedItemInfoPacket(),
                [PacketId.PlayerMinionPosition] = () => new PlayerMinionPositionPacket(),
                [PacketId.PlayerNebulaBuff] = () => new PlayerNebulaBuffPacket(),
                [PacketId.MoonLordInfo] = () => new MoonLordInfoPacket(),
                [PacketId.GemLockToggle] = () => new GemLockTogglePacket(),
                [PacketId.ServerChat] = () => new ServerChatPacket(),
                [PacketId.WireOperations] = () => new WireOperationsPacket(),
                [PacketId.PartyToggle] = () => new PartyTogglePacket(),
                [PacketId.OldOnesArmyStart] = () => new OldOnesArmyStartPacket(),
                [PacketId.OldOnesArmyEnd] = () => new OldOnesArmyEndPacket(),
                [PacketId.PlayerMinionTarget] = () => new PlayerMinionTargetPacket(),
                [PacketId.OldOnesArmyInfo] = () => new OldOnesArmyInfoPacket(),
                [PacketId.ServerCombatText] = () => new ServerCombatTextPacket(),
                [PacketId.MannequinInventory] = () => new MannequinInventoryPacket(),
                [PacketId.WeaponRackInfo] = () => new WeaponRackInfoPacket(),
                [PacketId.HatRackInventory] = () => new HatRackInventoryPacket(),
                [PacketId.NpcFish] = () => new NpcFishPacket(),
                [PacketId.PlateInfo] = () => new PlateInfoPacket()
            };

        /// <summary>
        /// The packet header size.
        /// </summary>
        public const int HeaderSize = sizeof(ushort) + sizeof(PacketId);

        /// <summary>
        /// Gets the packet's ID.
        /// </summary>
        /// <value>The packet's ID.</value>
        public PacketId Id { get; }

        /// <summary>
        /// Reads the packet's body from the given <paramref name="span"/> with the specified packet
        /// <paramref name="context"/>, mutating this instance. Returns the number of bytes read from the
        /// <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="context">The packet context to use when reading.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        /// <remarks>
        /// Implementations may not perform any bounds checking on <paramref name="span"/>.
        /// </remarks>
        public int ReadBody(Span<byte> span, PacketContext context);

        /// <summary>
        /// Writes the packet's body to the given <paramref name="span"/> with the specified packet
        /// <paramref name="context"/>. Returns the number of bytes written to the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        /// <remarks>
        /// Implementations may not perform any bounds checking on <paramref name="span"/>.
        /// </remarks>
        public int WriteBody(Span<byte> span, PacketContext context);

        /// <summary>
        /// Reads a packet from the given <paramref name="span"/> in the specified <paramref name="context"/>. Returns
        /// the number of bytes read from the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="context">The packet context to use when reading.</param>
        /// <param name="packet">The resulting packet.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        public static int Read(Span<byte> span, PacketContext context, out IPacket packet)
        {
            // Since we know that the span must have space for the packet header, we read it with zero bounds checking.
            var packetLength = Unsafe.ReadUnaligned<ushort>(ref MemoryMarshal.GetReference(span));
            var id = Unsafe.ReadUnaligned<PacketId>(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), 2));

            packet = _constructors.TryGetValue(id, out var ctor) ? ctor() : new UnknownPacket(packetLength, id);
            var packetBodyLength = packet.ReadBody(span[HeaderSize..], context);
            Debug.Assert(packetBodyLength + HeaderSize == packetLength);

            return packetLength;
        }
    }

    /// <summary>
    /// Provides extensions for the <see cref="IPacket"/> interface.
    /// </summary>
    public static class IPacketExtensions
    {
        /// <summary>
        /// Writes the packet to the given <paramref name="span"/> in the specified <paramref name="context"/>.
        /// Returns the number of bytes written to the <paramref name="span"/>.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        public static int Write(this IPacket packet, Span<byte> span, PacketContext context)
        {
            if (packet is null)
            {
                throw new ArgumentNullException(nameof(packet));
            }

            var packetLength = IPacket.HeaderSize + packet.WriteBody(span[IPacket.HeaderSize..], context);

            // Since we know that the span must have space for the packet header, we write it with zero bounds checking.
            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(span), (ushort)packetLength);
            Unsafe.WriteUnaligned(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), 2), packet.Id);

            return packetLength;
        }
    }
}
