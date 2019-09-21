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
using Orion.Networking.Packets.World;

namespace Orion.Networking.World {
    /// <summary>
    /// Represents an unlockable object type in an <see cref="UnlockObjectPacket"/>.
    /// </summary>
    public sealed class UnlockableObjectType {
#pragma warning disable 1591
        public static readonly UnlockableObjectType Chest = new UnlockableObjectType(1);
        public static readonly UnlockableObjectType Door = new UnlockableObjectType(2);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 3;
        private static readonly UnlockableObjectType[] Objects = new UnlockableObjectType[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the unlockable object type's ID.
        /// </summary>
        public byte Id { get; }

        static UnlockableObjectType() {
            foreach (var field in typeof(UnlockableObjectType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var unlockableObjectType = (UnlockableObjectType)field.GetValue(null);
                Objects[ArrayOffset + unlockableObjectType.Id] = unlockableObjectType;
                Names[ArrayOffset + unlockableObjectType.Id] = field.Name;
            }
        }

        private UnlockableObjectType(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns an unlockable object type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The unlock object type, or <c>null</c> if none exists.</returns>
        public static UnlockableObjectType FromId(byte id) => ArrayOffset + id < ArraySize ? Objects[ArrayOffset + id] : null;

        /// <inheritdoc />
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
