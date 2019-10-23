// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Microsoft.Xna.Framework;
using Orion.Packets;
using Orion.Utils;

namespace Orion.Players {
    /// <summary>
    /// Represents a player service. Provides access to player-related properties and methods in a thread-safe manner
    /// if not specified otherwise.
    /// </summary>
    public interface IPlayerService {
        /// <summary>
        /// Gets the players. All players are returned, regardless of whether or not they are actually active.
        /// </summary>
        /// <value>The players.</value>
        IReadOnlyArray<IPlayer> Players { get; }
    }

    /// <summary>
    /// Provides extensions for the <see cref="IPlayerService"/> interface.
    /// </summary>
    public static class PlayerServiceExtensions {
        /// <summary>
        /// Broadcasts a <paramref name="packet"/> to all active players.
        /// </summary>
        /// <param name="playerService">The player service.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="playerService"/> or <paramref name="packet"/> are <see langword="null"/>.
        /// </exception>
        public static void BroadcastPacket(this IPlayerService playerService, Packet packet) {
            if (playerService is null) {
                throw new ArgumentNullException(nameof(playerService));
            }

            if (packet is null) {
                throw new ArgumentNullException(nameof(packet));
            }

            var players = playerService.Players;
            for (var i = 0; i < players.Count; ++i) {
                players[i].SendPacket(packet);
            }
        }

        /// <summary>
        /// Broadcasts a <paramref name="message"/> to all active players with the given <paramref name="color"/>.
        /// </summary>
        /// <param name="playerService">The player service.</param>
        /// <param name="message">The message.</param>
        /// <param name="color">The color. The alpha component is ignored.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="playerService"/> or <paramref name="message"/> are <see langword="null"/>.
        /// </exception>
        public static void BroadcastMessage(this IPlayerService playerService, string message, Color color) {
            if (playerService is null) {
                throw new ArgumentNullException(nameof(playerService));
            }

            if (message is null) {
                throw new ArgumentNullException(nameof(message));
            }

            var players = playerService.Players;
            for (var i = 0; i < players.Count; ++i) {
                players[i].SendMessage(message, color);
            }
        }
    }
}
