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
using Orion.Networking.Packets.Misc;

namespace Orion.Networking.Misc {
    /// <summary>
    /// Represents a misc action in a <see cref="MiscActionPacket"/>.
    /// </summary>
    public sealed class MiscAction {
#pragma warning disable 1591
        public static readonly MiscAction SpawnSkeletron = new MiscAction(1);
        public static readonly MiscAction GrappleSound = new MiscAction(2);
        public static readonly MiscAction UseSundial = new MiscAction(3);
        public static readonly MiscAction CreateMimicSmoke = new MiscAction(4);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 5;
        private static readonly MiscAction[] Actions = new MiscAction[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the misc action's ID.
        /// </summary>
        public byte Id { get; }

        static MiscAction() {
            foreach (var field in typeof(MiscAction).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var miscAction = (MiscAction)field.GetValue(null);
                Actions[ArrayOffset + miscAction.Id] = miscAction;
                Names[ArrayOffset + miscAction.Id] = field.Name;
            }
        }

        private MiscAction(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns a misc action converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The misc action, or <c>null</c> if none exists.</returns>
        public static MiscAction FromId(byte id) => ArrayOffset + id < ArraySize ? Actions[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
