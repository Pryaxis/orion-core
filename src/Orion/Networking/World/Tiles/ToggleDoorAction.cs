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
using Orion.Networking.Packets.World.Tiles;

namespace Orion.Networking.World.Tiles {
    /// <summary>
    /// Represents a toggle door action in a <see cref="ToggleDoorPacket"/>.
    /// </summary>
    public sealed class ToggleDoorAction {
#pragma warning disable 1591
        public static readonly ToggleDoorAction OpenDoor = new ToggleDoorAction(0);
        public static readonly ToggleDoorAction CloseDoor = new ToggleDoorAction(1);
        public static readonly ToggleDoorAction OpenTrapdoor = new ToggleDoorAction(2);
        public static readonly ToggleDoorAction CloseTrapdoor = new ToggleDoorAction(3);
        public static readonly ToggleDoorAction OpenTallGate = new ToggleDoorAction(4);
        public static readonly ToggleDoorAction CloseTallGate = new ToggleDoorAction(5);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 6;
        private static readonly ToggleDoorAction[] Actions = new ToggleDoorAction[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the toggle door action's ID.
        /// </summary>
        public byte Id { get; }

        static ToggleDoorAction() {
            foreach (var field in typeof(ToggleDoorAction).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var toggleDoorAction = (ToggleDoorAction)field.GetValue(null);
                Actions[ArrayOffset + toggleDoorAction.Id] = toggleDoorAction;
                Names[ArrayOffset + toggleDoorAction.Id] = field.Name;
            }
        }

        private ToggleDoorAction(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns a toggle door action converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The toggle door action, or <c>null</c> if none exists.</returns>
        public static ToggleDoorAction FromId(byte id) => ArrayOffset + id < ArraySize ? Actions[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
