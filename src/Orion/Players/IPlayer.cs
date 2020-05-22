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
    /// Represents a Terraria player.
    /// </summary>
    public interface IPlayer : IEntity, IWrapping<Terraria.Player> {
        /// <summary>
        /// Gets or sets a value indicating whether the player is in PvP.
        /// </summary>
        /// <value><see langword="true"/> if the player is in PvP; otherwise, <see langword="false"/>.</value>
        bool IsInPvp { get; set; }

        /// <summary>
        /// Gets or sets the player's team.
        /// </summary>
        /// <value>The player's team.</value>
        PlayerTeam Team { get; set; }

        /// <summary>
        /// Sends the given <paramref name="packet"/> reference to the player.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="packet">The packet reference.</param>
        void SendPacket<TPacket>(ref TPacket packet) where TPacket : struct, IPacket;
    }

    /// <summary>
    /// Provides extensions for the <see cref="IPlayer"/> interface.
    /// </summary>
    public static class PlayerExtensions {
        /// <summary>
        /// Sends the given <paramref name="packet"/> to the <paramref name="player"/>. This "overload" is provided for
        /// convenience, but is slightly less efficient due to a struct copy.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is <see langword="null"/>.</exception>
        public static void SendPacket<TPacket>(this IPlayer player, TPacket packet) where TPacket : struct, IPacket {
            if (player is null) {
                throw new ArgumentNullException(nameof(player));
            }

            player.SendPacket(ref packet);
        }
    }
}
