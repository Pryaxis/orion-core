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
using Orion.Framework;
using Orion.Packets;
using Orion.Packets.DataStructures;
using Orion.Packets.Server;

namespace Orion.Players {
    /// <summary>
    /// Represents a player service. Provides access to player-related properties and methods.
    /// </summary>
    [Service(ServiceScope.Singleton)]
    public interface IPlayerService {
        /// <summary>
        /// Gets the players.
        /// </summary>
        /// <value>The players.</value>
        IReadOnlyList<IPlayer> Players { get; }
    }

    /// <summary>
    /// Provides extensions for the <see cref="IPlayerService"/> interface.
    /// </summary>
    public static class PlayerServiceExtensions {
        /// <summary>
        /// Broadcasts the given <paramref name="packet"/> reference to all active players.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="playerService">The player service.</param>
        /// <param name="packet">The packet reference. <b>This must be on the stack!</b></param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="playerService"/> is <see langword="null"/>.
        /// </exception>
        public static void BroadcastPacket<TPacket>(this IPlayerService playerService, ref TPacket packet)
                where TPacket : struct, IPacket {
            if (playerService is null) {
                throw new ArgumentNullException(nameof(playerService));
            }

            var players = playerService.Players;
            for (var i = 0; i < players.Count; ++i) {
                players[i].SendPacket(ref packet);
            }
        }

        /// <summary>
        /// Broadcasts the given <paramref name="packet"/> to all active players. This overload is provided for
        /// convenience, but is slightly less efficient due to a struct copy.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="playerService">The player service.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="playerService"/> is <see langword="null"/>.
        /// </exception>
        public static void BroadcastPacket<TPacket>(this IPlayerService playerService, TPacket packet)
                where TPacket : struct, IPacket {
            if (playerService is null) {
                throw new ArgumentNullException(nameof(playerService));
            }
            
            var players = playerService.Players;
            for (var i = 0; i < players.Count; ++i) {
                players[i].SendPacket(ref packet);
            }
        }

        /// <summary>
        /// Broadcasts the given <paramref name="message"/> to all active players using the specified
        /// <paramref name="color"/>.
        /// </summary>
        /// <param name="playerService">The player service.</param>
        /// <param name="message">The message.</param>
        /// <param name="color">The color.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="playerService"/> or <paramref name="message"/> are <see langword="null"/>.
        /// </exception>
        public static void BroadcastMessage(this IPlayerService playerService, NetworkText message, Color3 color) {
            if (playerService is null) {
                throw new ArgumentNullException(nameof(playerService));
            }

            if (message is null) {
                throw new ArgumentNullException(nameof(message));
            }

            var packet = new ServerChatPacket { Color = color, Message = message, LineWidth = -1 };
            playerService.BroadcastPacket(ref packet);
        }
    }
}
