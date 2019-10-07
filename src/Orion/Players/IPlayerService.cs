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
    /// Represents a player service. Provides access to player-related events and methods.
    /// </summary>
    public interface IPlayerService : IService {
        /// <summary>
        /// Gets the players in the world. All players are returned, regardless of whether or not they are actually
        /// active.
        /// </summary>
        IReadOnlyArray<IPlayer> Players { get; }

        /// <summary>
        /// Gets or sets the event handlers that run when receiving a packet. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PacketReceiveEventArgs>? PacketReceive { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when sending a packet. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PacketSendEventArgs>? PacketSend { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when a player connects. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PlayerConnectEventArgs>? PlayerConnect { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when a player sends their player data: e.g., clothing colors, name,
        /// etc. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PlayerDataEventArgs>? PlayerData { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when a player sends an inventory slot. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PlayerInventorySlotEventArgs>? PlayerInventorySlot { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when a player joins. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PlayerJoinEventArgs>? PlayerJoin { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when a player chats. This event can be canceled.
        /// </summary>
        EventHandlerCollection<PlayerChatEventArgs>? PlayerChat { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when a player disconnects.
        /// </summary>
        EventHandlerCollection<PlayerDisconnectedEventArgs>? PlayerDisconnected { get; set; }

        /// <summary>
        /// Broadcasts a <paramref name="packet"/> to all players.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException"><paramref name="packet"/> is <see langword="null"/>.</exception>
        void BroadcastPacket(Packet packet) {
            if (packet is null) {
                throw new ArgumentNullException(nameof(packet));
            }

            for (var i = 0; i < Players.Count; ++i) {
                Players[i].SendPacket(packet);
            }
        }

        /// <summary>
        /// Broadcasts a <paramref name="message"/> to all players with the given <paramref name="color"/>.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="color">The color.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
        void BroadcastMessage(string message, Color color) {
            if (message is null) {
                throw new ArgumentNullException(nameof(message));
            }

            for (var i = 0; i < Players.Count; ++i) {
                Players[i].SendMessage(message, color);
            }
        }
    }
}
