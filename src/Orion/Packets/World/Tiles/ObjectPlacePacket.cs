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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.World.Tiles;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to place an object.
    /// </summary>
    public sealed class ObjectPlacePacket : Packet {
        private short _objectX;
        private short _objectY;
        private BlockType _objectType;
        private short _objectStyle;
        private byte _data;
        private sbyte _objectRandomState;
        private bool _objectDirection;

        /// <inheritdoc />
        public override PacketType Type => PacketType.ObjectPlace;

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
        public BlockType ObjectType {
            get => _objectType;
            set {
                _objectType = value;
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
            _objectX = reader.ReadInt16();
            _objectY = reader.ReadInt16();
            _objectType = (BlockType)reader.ReadUInt16();
            _objectStyle = reader.ReadInt16();

            // This byte is basically useless, but we'll keep track of it anyways.
            _data = reader.ReadByte();
            _objectRandomState = reader.ReadSByte();
            _objectDirection = reader.ReadBoolean();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_objectX);
            writer.Write(_objectY);
            writer.Write((ushort)_objectType);
            writer.Write(_objectStyle);
            writer.Write(_data);
            writer.Write(_objectRandomState);
            writer.Write(_objectDirection);
        }
    }
}
