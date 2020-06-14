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

using System.Collections.Generic;
using Orion.Core.Events.World.Signs;
using Orion.Core.Framework;

namespace Orion.Core.World.Signs {
    /// <summary>
    /// Represents a sign service. Provides access to signs and publishes sign-related events.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe.
    /// 
    /// The sign service is responsible for publishing the following sign-related events:
    /// <list type="bullet">
    /// <item><description><see cref="SignReadEvent"/></description></item>
    /// </list>
    /// </remarks>
    [Service(ServiceScope.Singleton)]
    public interface ISignService {
        /// <summary>
        /// Gets the signs.
        /// </summary>
        /// <value>The signs.</value>
        IReadOnlyList<ISign> Signs { get; }
    }
}
