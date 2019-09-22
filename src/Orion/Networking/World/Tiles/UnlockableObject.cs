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

using System.Reflection;
using JetBrains.Annotations;
using Orion.Networking.Packets.World.Tiles;

namespace Orion.Networking.World.Tiles {
    /// <summary>
    /// Represents an unlockable object in an <see cref="UnlockObjectPacket"/>.
    /// </summary>
    [PublicAPI]
    public sealed class UnlockableObject {
#pragma warning disable 1591
        public static readonly UnlockableObject Chest = new UnlockableObject(1);
        public static readonly UnlockableObject Door = new UnlockableObject(2);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 3;
        private static readonly UnlockableObject[] Objects = new UnlockableObject[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the unlockable object's ID.
        /// </summary>
        public byte Id { get; }

        static UnlockableObject() {
            foreach (var field in typeof(UnlockableObject).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var unlockableObjectType = (UnlockableObject)field.GetValue(null);
                Objects[ArrayOffset + unlockableObjectType.Id] = unlockableObjectType;
                Names[ArrayOffset + unlockableObjectType.Id] = field.Name;
            }
        }

        private UnlockableObject(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns an unlockable object converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The unlockable object, or <c>null</c> if none exists.</returns>
        public static UnlockableObject FromId(byte id) => ArrayOffset + id < ArraySize ? Objects[ArrayOffset + id] : null;

        /// <inheritdoc />
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
