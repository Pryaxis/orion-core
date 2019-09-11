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

using Orion.Hooks;
using Orion.Players.Events;
using Orion.Utils;

namespace Orion.Players {
    /// <summary>
    /// Represents a service that manages players. Provides player-related hooks and methods.
    /// </summary>
    public interface IPlayerService : IReadOnlyArray<IPlayer>, IService {
        /// <summary>
        /// Gets or sets the hook handlers that occur when a player is being greeted.
        /// </summary>
        HookHandlerCollection<GreetingPlayerEventArgs> GreetingPlayer { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a player is being updated. This hook can be handled.
        /// </summary>
        HookHandlerCollection<UpdatingPlayerEventArgs> UpdatingPlayer { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a player is updated.
        /// </summary>
        HookHandlerCollection<UpdatedPlayerEventArgs> UpdatedPlayer { get; set; }
    }
}
