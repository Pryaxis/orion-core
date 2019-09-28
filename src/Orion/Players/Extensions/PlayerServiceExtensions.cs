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
using Orion.Packets;

namespace Orion.Players.Extensions {
    /// <summary>
    /// Provides extensions for the <see cref="IPlayerService"/> interface.
    /// </summary>
    public static class PlayerServiceExtensions {
        /// <summary>
        /// Broadcasts the given packet.
        /// </summary>
        /// <param name="playerService">The player service.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="playerService"/> or <paramref name="packet"/> are <c>null</c>.
        /// </exception>
        public static void BroadcastPacket(this IPlayerService playerService, Packet packet) {
            if (playerService is null) throw new ArgumentNullException(nameof(playerService));
            if (packet is null) throw new ArgumentNullException(nameof(packet));

            for (var i = 0; i < playerService.Count; ++i) {
                playerService[i].SendPacket(packet);
            }
        }
    }
}
