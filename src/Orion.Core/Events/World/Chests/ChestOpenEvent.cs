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
using Orion.Core.Players;
using Orion.Core.World.Chests;

namespace Orion.Core.Events.World.Chests
{
    /// <summary>
    /// An event that occurs when a player is opening a chest. This event can be canceled.
    /// </summary>
    [Event("chest-open")]
    public sealed class ChestOpenEvent : ChestEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChestOpenEvent"/> class with the specified
        /// <paramref name="chest"/> and <paramref name="player"/>.
        /// </summary>
        /// <param name="chest">The chest being opened.</param>
        /// <param name="player">The player opening the chest.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="chest"/> or <paramref name="player"/> are <see langword="null"/>.
        /// </exception>
        public ChestOpenEvent(IChest chest, IPlayer player) : base(chest)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }

        /// <summary>
        /// Gets the player opening the chest.
        /// </summary>
        /// <value>The player opening the chest.</value>
        public IPlayer Player { get; }
    }
}
