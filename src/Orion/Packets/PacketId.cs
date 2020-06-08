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
using Orion.Packets.Client;
using Orion.Packets.Players;
using Orion.Packets.Server;
using Orion.Packets.World.Signs;
using Orion.Packets.World.Tiles;

namespace Orion.Packets {
    /// <summary>
    /// Specifies a packet ID.
    /// </summary>
    public enum PacketId : byte {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        ClientConnect = 1,
        ClientDisconnect = 2,
        PlayerJoin = 6,
        PlayerHealth = 16,
        TileModify = 17,
        TileSquare = 20,
        PlayerPvp = 30,
        PlayerMana = 42,
        PlayerTeam = 45,
        SignRead = 46,
        BlockPaint = 63,
        WallPaint = 64,
        ClientUuid = 68,
        Module = 82,
        ServerChat = 107
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// Provides extensions for the <see cref="PacketId"/> enumeration.
    /// </summary>
    public static class PacketIdExtensions {
        private static readonly IDictionary<PacketId, Type> _types = new Dictionary<PacketId, Type> {
            [PacketId.ClientConnect] = typeof(ClientConnectPacket),
            [PacketId.ClientDisconnect] = typeof(ClientDisconnectPacket),
            [PacketId.PlayerJoin] = typeof(PlayerJoinPacket),
            [PacketId.PlayerHealth] = typeof(PlayerHealthPacket),
            [PacketId.TileModify] = typeof(TileModifyPacket),
            [PacketId.TileSquare] = typeof(TileSquarePacket),
            [PacketId.PlayerMana] = typeof(PlayerManaPacket),
            [PacketId.PlayerPvp] = typeof(PlayerPvpPacket),
            [PacketId.PlayerTeam] = typeof(PlayerTeamPacket),
            [PacketId.SignRead] = typeof(SignReadPacket),
            [PacketId.BlockPaint] = typeof(BlockPaintPacket),
            [PacketId.WallPaint] = typeof(WallPaintPacket),
            [PacketId.ClientUuid] = typeof(ClientUuidPacket),
            [PacketId.ServerChat] = typeof(ServerChatPacket)
        };

        /// <summary>
        /// Gets the corresponding type for the packet <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The packet ID.</param>
        /// <returns>The corresponding type for the packet <paramref name="id"/>.</returns>
        [Pure]
        public static Type Type(this PacketId id) =>
            _types.TryGetValue(id, out var type) ? type : typeof(UnknownPacket);
    }
}
