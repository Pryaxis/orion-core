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

namespace Orion.Networking.World {
    /// <summary>
    /// Represents an invasion that is transmitted over the network.
    /// </summary>
    public sealed class NetworkInvasion {
#pragma warning disable 1591
        public static readonly NetworkInvasion MoonLord = new NetworkInvasion(-8);
        public static readonly NetworkInvasion Martians = new NetworkInvasion(-7);
        public static readonly NetworkInvasion Eclipse = new NetworkInvasion(-6);
        public static readonly NetworkInvasion FrostMoon = new NetworkInvasion(-5);
        public static readonly NetworkInvasion PumpkinMoon = new NetworkInvasion(-4);
        public static readonly NetworkInvasion Pirates = new NetworkInvasion(-3);
        public static readonly NetworkInvasion FrostLegion = new NetworkInvasion(-2);
        public static readonly NetworkInvasion Goblins = new NetworkInvasion(-1);
#pragma warning restore 1591

        private const int ArrayOffset = 8;
        private const int ArraySize = ArrayOffset + 0;
        private static readonly NetworkInvasion[] Teams = new NetworkInvasion[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the network invasion's ID.
        /// </summary>
        public short Id { get; }

        static NetworkInvasion() {
            foreach (var field in typeof(NetworkInvasion).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var networkInvasion = (NetworkInvasion)field.GetValue(null);
                Teams[ArrayOffset + networkInvasion.Id] = networkInvasion;
                Names[ArrayOffset + networkInvasion.Id] = field.Name;
            }
        }

        private NetworkInvasion(short id) {
            Id = id;
        }

        /// <summary>
        /// Returns a network invasion converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The network invasion, or <c>null</c> if none exists.</returns>
        public static NetworkInvasion FromId(short id) =>
            ArrayOffset + (uint)id < ArraySize ? Teams[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
