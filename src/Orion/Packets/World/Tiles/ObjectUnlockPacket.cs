// Copyright (c) 2019 Pryaxis & Orion Contributors
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

using System.IO;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to unlock an object (chest, door, etc.). See <see cref="UnlockableObjectType"/> for a list of
    /// unlockable object types.
    /// </summary>
    public sealed class ObjectUnlockPacket : Packet {
        private UnlockableObjectType _objectType;
        private short _x;
        private short _y;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ObjectUnlock;

        /// <summary>
        /// Gets or sets the unlockable object type.
        /// </summary>
        /// <value>The unlockable object type.</value>
        public UnlockableObjectType ObjectType {
            get => _objectType;
            set {
                _objectType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the object's X coordinate.
        /// </summary>
        /// <value>The object's X coordinate.</value>
        public short X {
            get => _x;
            set {
                _x = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the object's Y coordinate.
        /// </summary>
        /// <value>The object's Y coordinate.</value>
        public short Y {
            get => _y;
            set {
                _y = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _objectType = (UnlockableObjectType)reader.ReadByte();
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)_objectType);
            writer.Write(_x);
            writer.Write(_y);
        }
    }
}
