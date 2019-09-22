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
using JetBrains.Annotations;
using Orion.Networking.Packets.Entities;

namespace Orion.Networking.Entities {
    /// <summary>
    /// Represents a player dodge in a <see cref="PlayerDodgePacket"/>.
    /// </summary>
    [PublicAPI]
    public sealed class PlayerDodge {
#pragma warning disable 1591
        public static readonly PlayerDodge NinjaDodge = new PlayerDodge(1);
        public static readonly PlayerDodge ShadowDodge = new PlayerDodge(2);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 3;
        private static readonly PlayerDodge[] Dodges = new PlayerDodge[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the player dodge's ID.
        /// </summary>
        public byte Id { get; }

        static PlayerDodge() {
            foreach (var field in typeof(PlayerDodge).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var dodgeType = (PlayerDodge)field.GetValue(null);
                Dodges[ArrayOffset + dodgeType.Id] = dodgeType;
                Names[ArrayOffset + dodgeType.Id] = field.Name;
            }
        }

        private PlayerDodge(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns a player dodge converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The player dodge, or <c>null</c> if none exists.</returns>
        public static PlayerDodge FromId(byte id) => ArrayOffset + id < ArraySize ? Dodges[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
