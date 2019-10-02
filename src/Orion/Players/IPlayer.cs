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
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Orion.Entities;
using Orion.Packets;
using Orion.Packets.Players;
using Orion.Packets.World;
using Orion.Utils;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    /// <summary>
    /// Represents a Terraria player.
    /// </summary>
    [PublicAPI]
    public interface IPlayer : IEntity, IWrapping<TerrariaPlayer> {
        /// <summary>
        /// Gets or sets the player's team.
        /// </summary>
        PlayerTeam Team { get; set; }

        /// <summary>
        /// Gets the player's statistics.
        /// </summary>
        IPlayerStats Stats { get; }

        /// <summary>
        /// Gets the player's inventory.
        /// </summary>
        IPlayerInventory Inventory { get; }

        /// <summary>
        /// Sends a packet to the player.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException"><paramref name="packet"/> is <see langword="null" />.</exception>
        void SendPacket(Packet packet);

        /// <summary>
        /// Disconnects the player with the given reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reason"/> is <see langword="null" />.</exception>
        void Disconnect(string reason) {
            if (reason is null) throw new ArgumentNullException(nameof(reason));

            SendPacket(new PlayerDisconnectPacket {PlayerDisconnectReason = reason});
        }

        /// <summary>
        /// Sends a message to the player with the given color.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="color">The color.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null" />.</exception>
        void SendMessage(string message, Color color) {
            SendPacket(new ChatPacket {
                ChatColor = color,
                ChatLineWidth = -1,
                ChatText = message ?? throw new ArgumentNullException(nameof(message))
            });
        }
    }
}
