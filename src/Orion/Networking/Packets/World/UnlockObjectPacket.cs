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
using Orion.Networking.World;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to unlock an object (chest, door, etc.).
    /// </summary>
    public sealed class UnlockObjectPacket : Packet {
        private UnlockableObject _unlockableObject;
        private short _objectX;
        private short _objectY;

        /// <inheritdoc />
        public override PacketType Type => PacketType.UnlockObject;

        /// <summary>
        /// Gets or sets the unlockable object type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public UnlockableObject UnlockableObject {
            get => _unlockableObject;
            set {
                _unlockableObject = value ?? throw new ArgumentNullException(nameof(value));
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
        public override string ToString() => $"{Type}[{UnlockableObject} @ ({ObjectX}, {ObjectY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            UnlockableObject = UnlockableObject.FromId(reader.ReadByte()) ??
                               throw new PacketException("Object type is invalid.");
            ObjectX = reader.ReadInt16();
            ObjectY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(UnlockableObject.Id);
            writer.Write(ObjectX);
            writer.Write(ObjectY);
        }
    }
}
