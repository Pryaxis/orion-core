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

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to unlock an object (chest, door, etc.).
    /// </summary>
    public sealed class UnlockObjectPacket : Packet {
        private ObjectType _unlockObjectType;
        private short _objectX;
        private short _objectY;

        /// <inheritdoc />
        public override PacketType Type => PacketType.UnlockObject;

        /// <summary>
        /// Gets or sets the unlock object type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public ObjectType UnlockObjectType {
            get => _unlockObjectType;
            set {
                _unlockObjectType = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the object's X coordinate.
        /// </summary>
        public short ObjectX {
            get => _objectX;
            set {
                _objectX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the object's Y coordinate.
        /// </summary>
        public short ObjectY {
            get => _objectY;
            set {
                _objectY = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{UnlockObjectType} @ ({ObjectX}, {ObjectY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            UnlockObjectType = ObjectType.FromId(reader.ReadByte()) ??
                               throw new PacketException("Object type is invalid.");
            ObjectX = reader.ReadInt16();
            ObjectY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(UnlockObjectType.Id);
            writer.Write(ObjectX);
            writer.Write(ObjectY);
        }

        /// <summary>
        /// Represents an object type in an <see cref="UnlockObjectPacket"/>.
        /// </summary>
        public sealed class ObjectType {
#pragma warning disable 1591
            public static readonly ObjectType Chest = new ObjectType(1);
            public static readonly ObjectType Door = new ObjectType(2);
#pragma warning restore 1591

            private const int ArrayOffset = 0;
            private const int ArraySize = ArrayOffset + 3;
            private static readonly ObjectType[] Objects = new ObjectType[ArraySize];
            private static readonly string[] Names = new string[ArraySize];

            /// <summary>
            /// Gets the object type's ID.
            /// </summary>
            public byte Id { get; }

            static ObjectType() {
                foreach (var field in typeof(ObjectType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                    var objectType = (ObjectType)field.GetValue(null);
                    Objects[ArrayOffset + objectType.Id] = objectType;
                    Names[ArrayOffset + objectType.Id] = field.Name;
                }
            }

            private ObjectType(byte id) {
                Id = id;
            }

            /// <summary>
            /// Returns an object type converted from the given ID.
            /// </summary>
            /// <param name="id">The ID.</param>
            /// <returns>The object type, or <c>null</c> if none exists.</returns>
            public static ObjectType FromId(byte id) => ArrayOffset + id < ArraySize ? Objects[ArrayOffset + id] : null;

            /// <inheritdoc />
            public override string ToString() => Names[ArrayOffset + Id];
        }
    }
}
