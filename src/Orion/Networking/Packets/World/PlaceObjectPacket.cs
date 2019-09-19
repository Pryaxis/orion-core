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
using Orion.World.Tiles;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to place an object.
    /// </summary>
    public sealed class PlaceObjectPacket : Packet {
        private short _objectX;
        private short _objectY;
        private BlockType _objectType;
        private short _objectStyle;
        private byte _data;
        private sbyte _objectRandomState;
        private bool _objectDirection;

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlaceObject;

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

        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public BlockType ObjectType {
            get => _objectType;
            set {
                _objectType = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the object style.
        /// </summary>
        public short ObjectStyle {
            get => _objectStyle;
            set {
                _objectStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the object's random state.
        /// </summary>
        public sbyte ObjectRandomState {
            get => _objectRandomState;
            set {
                _objectRandomState = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the object direction.
        /// </summary>
        public bool ObjectDirection {
            get => _objectDirection;
            set {
                _objectDirection = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ObjectType}_{ObjectStyle} @ ({ObjectX}, {ObjectY}), ...]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ObjectX = reader.ReadInt16();
            ObjectY = reader.ReadInt16();
            ObjectType = BlockType.FromId(reader.ReadUInt16()) ?? throw new PacketException("Object type is invalid.");
            ObjectStyle = reader.ReadInt16();

            // This byte is basically useless, but we'll keep track of it anyways.
            _data = reader.ReadByte();
            ObjectRandomState = reader.ReadSByte();
            ObjectDirection = reader.ReadBoolean();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ObjectX);
            writer.Write(ObjectY);
            writer.Write(ObjectType.Id);
            writer.Write(ObjectStyle);
            writer.Write(_data);
            writer.Write(ObjectRandomState);
            writer.Write(ObjectDirection);
        }
    }
}
