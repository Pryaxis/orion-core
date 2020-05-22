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
using Orion.Entities;
using Orion.Packets;

namespace Orion.Players {
    /// <summary>
    /// Represents a player service. Provides access to player-related properties, methods, and events.
    /// </summary>
    public interface IPlayerService {
        /// <summary>
        /// Gets the array of players.
        /// </summary>
        /// <value>The array of players.</value>
        IReadOnlyArray<IPlayer> Players { get; }
    }

    /// <summary>
    /// Provides extensions for the <see cref="IPlayerService"/> interface.
    /// </summary>
    public static class PlayerServiceExtensions {
        /// <summary>
        /// Broadcasts the given <paramref name="packet"/> reference to all active players.
        /// </summary>
        /// <param name="playerService">The player service.</param>
        /// <param name="packet">The packet reference.</param>
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
        /// <param name="playerService">The player service.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="playerService"/> is <see langword="null"/>.
        /// </exception>
        public static void BroadcastPacket<TPacket>(this IPlayerService playerService, TPacket packet)
                where TPacket : struct, IPacket {
            playerService.BroadcastPacket(ref packet);
        }
    }
}
