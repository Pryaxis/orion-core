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
using Orion.Events;
using Orion.Events.Packets;
using Orion.Events.Players;
using Orion.Packets;
using Orion.Utils;

namespace Orion.Players {
    /// <summary>
    /// Represents a player service. Provides access to player-related events and methods, and in a thread-safe manner
    /// if not specified otherwise.
    /// </summary>
    public interface IPlayerService {
        /// <summary>
        /// Gets the players in the world. All players are returned, regardless of whether or not they are actually
        /// active.
        /// </summary>
        IReadOnlyArray<IPlayer> Players { get; }

        /// <summary>
        /// Gets the event handlers that run when receiving a packet. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PacketReceiveEventArgs> PacketReceive { get; }

        /// <summary>
        /// Gets the event handlers that run when sending a packet. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PacketSendEventArgs> PacketSend { get; }

        /// <summary>
        /// Gets the event handlers that run when a player connects. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PlayerConnectEventArgs> PlayerConnect { get; }

        /// <summary>
        /// Gets the event handlers that run when a player sends their player data: e.g., clothing colors, name,
        /// etc. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PlayerDataEventArgs> PlayerData { get; }

        /// <summary>
        /// Gets the event handlers that run when a player sends an inventory slot. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PlayerInventorySlotEventArgs> PlayerInventorySlot { get; }

        /// <summary>
        /// Gets the event handlers that run when a player joins. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PlayerJoinEventArgs> PlayerJoin { get; }

        /// <summary>
        /// Gets the event handlers that run when a player chats. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PlayerChatEventArgs> PlayerChat { get; }

        /// <summary>
        /// Gets the event handlers that run when a player disconnects.
        /// </summary>
        EventHandlerCollection<PlayerDisconnectedEventArgs> PlayerDisconnected { get; }
    }

    /// <summary>
    /// Provides extensions for the <see cref="IPlayerService"/> interface.
    /// </summary>
    public static class PlayerServiceExtensions {
        /// <summary>
        /// Broadcasts a <paramref name="packet"/> to all players.
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
        /// Broadcasts a <paramref name="message"/> to all players with the given <paramref name="color"/>.
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
