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
using Orion.Core.Events.Packets;
using Orion.Core.Events.Players;
using Orion.Core.Packets;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Packets.Server;
using Orion.Core.Packets.World.Tiles;
using Orion.Core.Utils;
using Orion.Core.World.Tiles;

namespace Orion.Core.Players
{
    /// <summary>
    /// Represents a player service. Provides access to players and publishes packet and player-related events.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe.
    /// 
    /// The player service is responsible for publishing the following packet and player-related events:
    /// <list type="bullet">
    /// <item><description><see cref="PacketReceiveEvent{TPacket}"/></description></item>
    /// <item><description><see cref="PacketSendEvent{TPacket}"/></description></item>
    /// <item><description><see cref="PlayerTickEvent"/></description></item>
    /// <item><description><see cref="PlayerQuitEvent"/></description></item>
    /// <item><description><see cref="PlayerJoinEvent"/></description></item>
    /// <item><description><see cref="PlayerHealthEvent"/></description></item>
    /// <item><description><see cref="PlayerPvpEvent"/></description></item>
    /// <item><description><see cref="PlayerPasswordEvent"/></description></item>
    /// <item><description><see cref="PlayerManaEvent"/></description></item>
    /// <item><description><see cref="PlayerTeamEvent"/></description></item>
    /// <item><description><see cref="PlayerUuidEvent"/></description></item>
    /// <item><description><see cref="PlayerChatEvent"/></description></item>
    /// </list>
    /// </remarks>
    [Service(ServiceScope.Singleton)]
    public interface IPlayerService : IReadOnlyList<IPlayer>
    {
    }

    /// <summary>
    /// Provides extensions for the <see cref="IPlayerService"/> interface.
    /// </summary>
    public static class IPlayerServiceExtensions
    {
        /// <summary>
        /// Broadcasts the given <paramref name="packet"/> to all active players.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="players">The player service.</param>
        /// <param name="packet">The packet to broadcast.</param>
        /// <exception cref="ArgumentNullException"><paramref name="players"/> is <see langword="null"/>.</exception>
        public static void BroadcastPacket<TPacket>(this IPlayerService players, TPacket packet) where TPacket : IPacket
        {
            if (players is null)
            {
                throw new ArgumentNullException(nameof(players));
            }

            for (var i = 0; i < players.Count; ++i)
            {
                players[i].SendPacket(packet);
            }
        }

        /// <summary>
        /// Broadcasts the given <paramref name="message"/> with the specified <paramref name="color"/> to all active
        /// players.
        /// </summary>
        /// <param name="players">The player service.</param>
        /// <param name="message">The message to broadcast.</param>
        /// <param name="color">The color to broadcast the message as.</param>
        /// <exception cref="ArgumentNullException"><paramref name="players"/> is <see langword="null"/>.</exception>
        public static void BroadcastMessage(this IPlayerService players, NetworkText message, Color3 color)
        {
            if (players is null)
            {
                throw new ArgumentNullException(nameof(players));
            }

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var packet = new ServerChat { Color = color, Message = message, LineWidth = -1 };
            for (var i = 0; i < players.Count; ++i)
            {
                players[i].SendPacket(packet);
            }
        }

        /// <summary>
        /// Broadcasts the given <paramref name="tiles"/> at the specified coordinates to all active players.
        /// </summary>
        /// <param name="players">The player service.</param>
        /// <param name="x">The top-left tile's X coordinate.</param>
        /// <param name="y">The top-left tile's Y coordinate.</param>
        /// <param name="tiles">The tiles to broadcast.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="players"/> or <paramref name="tiles"/> are <see langword="null"/>.
        /// </exception>
        /// <exception cref="NotSupportedException"><paramref name="tiles"/> is not square.</exception>
        public static void BroadcastTiles(this IPlayerService players, int x, int y, ITileSlice tiles)
        {
            if (players is null)
            {
                throw new ArgumentNullException(nameof(players));
            }

            if (tiles is null)
            {
                throw new ArgumentNullException(nameof(tiles));
            }

            if (tiles.Width != tiles.Height)
            {
                // TODO: implement this when the section packet is implemented.
                throw new NotSupportedException("Tiles is not square");
            }

            var packet = new TileSquare { X = (short)x, Y = (short)y, Tiles = tiles };
            for (var i = 0; i < players.Count; ++i)
            {
                players[i].SendPacket(packet);
            }
        }
    }
}
