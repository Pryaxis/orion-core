// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

using JetBrains.Annotations;
using Orion.Events;
using Orion.Events.Entities;
using Orion.Events.Packets;
using Orion.Utils;

namespace Orion.Entities {
    /// <summary>
    /// Represents a player service. Provides access to player-related events and methods.
    /// </summary>
    [PublicAPI]
    public interface IPlayerService : IReadOnlyArray<IPlayer>, IService {
        /// <summary>
        /// Gets or sets the event handlers that run when receiving a packet. This event can be canceled.
        /// </summary>
        [CanBeNull]
        EventHandlerCollection<PacketReceiveEventArgs> PacketReceive { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when sending a packet. This event can be canceled.
        /// </summary>
        [CanBeNull]
        EventHandlerCollection<PacketSendEventArgs> PacketSend { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when a player connects. This event can be canceled.
        /// </summary>
        [CanBeNull]
        EventHandlerCollection<PlayerConnectEventArgs> PlayerConnect { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when a player sends data. This event can be canceled.
        /// </summary>
        [CanBeNull]
        EventHandlerCollection<PlayerDataEventArgs> PlayerData { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when a player sends an inventory slot. This event can be canceled.
        /// </summary>
        [CanBeNull]
        EventHandlerCollection<PlayerInventorySlotEventArgs> PlayerInventorySlot { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when a player joins. This event can be canceled.
        /// </summary>
        [CanBeNull]
        EventHandlerCollection<PlayerJoinEventArgs> PlayerJoin { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that run when a player disconnects.
        /// </summary>
        [CanBeNull]
        EventHandlerCollection<PlayerDisconnectedEventArgs> PlayerDisconnected { get; set; }
    }
}
