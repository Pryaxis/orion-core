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
using Orion.Core.World.Signs;

namespace Orion.Core.Events.World.Signs
{
    /// <summary>
    /// An event that occurs when a player is reading a sign. This event can be canceled.
    /// </summary>
    [Event("sign-read")]
    public sealed class SignReadEvent : SignEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignReadEvent"/> class with the specified
        /// <paramref name="sign"/> and <paramref name="player"/>.
        /// </summary>
        /// <param name="sign">The sign being read.</param>
        /// <param name="player">The player reading the sign.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sign"/> or <paramref name="player"/> are <see langword="null"/>.
        /// </exception>
        public SignReadEvent(ISign sign, IPlayer player) : base(sign)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }

        /// <summary>
        /// Gets the player reading the sign.
        /// </summary>
        /// <value>The player reading the sign.</value>
        public IPlayer Player { get; }
    }
}
