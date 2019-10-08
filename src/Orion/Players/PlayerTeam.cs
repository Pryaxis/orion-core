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

using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Orion.Players {
    /// <summary>
    /// Specifies a player team.
    /// </summary>
    public enum PlayerTeam : byte {
        /// <summary>
        /// Indicates no team.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates the red team.
        /// </summary>
        Red = 1,

        /// <summary>
        /// Indicates the green team.
        /// </summary>
        Green = 2,

        /// <summary>
        /// Indicates the blue team.
        /// </summary>
        Blue = 3,

        /// <summary>
        /// Indicates the yellow team.
        /// </summary>
        Yellow = 4,

        /// <summary>
        /// Indicates the pink team.
        /// </summary>
        Pink = 5
    }

    /// <summary>
    /// Provides extensions for the <see cref="PlayerTeam"/> enumeration.
    /// </summary>
    public static class PlayerTeamExtensions {
        private static readonly IDictionary<PlayerTeam, Color> _colors = new Dictionary<PlayerTeam, Color> {
            [PlayerTeam.Red] = new Color(0xda, 0x3b, 0x3b),
            [PlayerTeam.Green] = new Color(0x3b, 0xda, 0x55),
            [PlayerTeam.Blue] = new Color(0x3b, 0x95, 0xda),
            [PlayerTeam.Yellow] = new Color(0xf2, 0xdd, 0x64),
            [PlayerTeam.Pink] = new Color(0xe0, 0x64, 0xf2)
        };

        /// <summary>
        /// Returns the color for the <paramref name="team"/>.
        /// </summary>
        /// <param name="team">The team.</param>
        /// <returns>The color.</returns>
        public static Color Color(this PlayerTeam team) =>
            _colors.TryGetValue(team, out var color) ? color : Microsoft.Xna.Framework.Color.White;
    }
}
