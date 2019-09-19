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
    /// Represents a player difficulty.
    /// </summary>
    public sealed class PlayerDifficulty {
#pragma warning disable 1591
        public static readonly PlayerDifficulty Softcore = new PlayerDifficulty(0);
        public static readonly PlayerDifficulty Mediumcore = new PlayerDifficulty(1);
        public static readonly PlayerDifficulty Hardcore = new PlayerDifficulty(2);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 3;
        private static readonly PlayerDifficulty[] Difficulties = new PlayerDifficulty[ArraySize];
        private static readonly string[] Names = new string[ArraySize];
        private static readonly Color[] Colors = {Color.White, new Color(125, 125, 255), new Color(200, 125, 255)};

        /// <summary>
        /// Gets the player difficulty's ID.
        /// </summary>
        public byte Id { get; }

        /// <summary>
        /// Gets the player difficulty's color.
        /// </summary>
        public Color Color => Colors[ArrayOffset + Id];

        static PlayerDifficulty() {
            foreach (var field in typeof(PlayerDifficulty).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var playerDifficulty = (PlayerDifficulty)field.GetValue(null);
                Difficulties[ArrayOffset + playerDifficulty.Id] = playerDifficulty;
                Names[ArrayOffset + playerDifficulty.Id] = field.Name;
            }
        }

        private PlayerDifficulty(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns a player difficulty converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The player difficulty, or <c>null</c> if none exists.</returns>
        public static PlayerDifficulty FromId(byte id) =>
            ArrayOffset + (uint)id < ArraySize ? Difficulties[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
