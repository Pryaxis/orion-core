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

using Orion.Core.Events;
using Orion.Core.Events.Server;
using Orion.Core.Framework;

namespace Orion.Core
{
    /// <summary>
    /// Represents the server. Provides access to Orion extensions and events and publishes server-related events.
    /// </summary>
    /// <remarks>
    /// The server is responsible for publishing the following server-related events:
    /// <list type="bullet">
    /// <item><description><see cref="ServerInitializeEvent"/></description></item>
    /// <item><description><see cref="ServerStartEvent"/></description></item>
    /// <item><description><see cref="ServerTickEvent"/></description></item>
    /// <item><description><see cref="ServerCommandEvent"/></description></item>
    /// </list>
    /// </remarks>
    public interface IServer
    {
        /// <summary>
        /// Gets the extension manager.
        /// </summary>
        /// <value>The extension manager.</value>
        IExtensionManager Extensions { get; }

        /// <summary>
        /// Gets the event manager.
        /// </summary>
        /// <value>The event manager.</value>
        IEventManager Events { get; }
    }
}
