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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace Orion.Entities {
    /// <summary>
    /// Represents a player team.
    /// </summary>
    public sealed class PlayerTeam {
#pragma warning disable 1591
        public static readonly PlayerTeam None = new PlayerTeam(0);
        public static readonly PlayerTeam Red = new PlayerTeam(1);
        public static readonly PlayerTeam Green = new PlayerTeam(2);
        public static readonly PlayerTeam Blue = new PlayerTeam(3);
        public static readonly PlayerTeam Yellow = new PlayerTeam(4);
        public static readonly PlayerTeam Pink = new PlayerTeam(5);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 6;
        private static readonly PlayerTeam[] Teams = new PlayerTeam[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        private static readonly Color[] Colors = {
            Color.White, new Color(218, 59, 59), new Color(59, 218, 85), new Color(59, 149, 218),
            new Color(242, 221, 100), new Color(224, 100, 242)
        };

        /// <summary>
        /// Gets the player team's ID.
        /// </summary>
        public byte Id { get; }

        /// <summary>
        /// Gets the player team's color.
        /// </summary>
        public Color Color => Colors[ArrayOffset + Id];

        static PlayerTeam() {
            foreach (var field in typeof(PlayerTeam).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var playerTeam = (PlayerTeam)field.GetValue(null);
                Teams[ArrayOffset + playerTeam.Id] = playerTeam;
                Names[ArrayOffset + playerTeam.Id] = field.Name;
            }
        }

        private PlayerTeam(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns a player team converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The player team, or <c>null</c> if none exists.</returns>
        public static PlayerTeam FromId(byte id) =>
            ArrayOffset + (uint)id < ArraySize ? Teams[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
