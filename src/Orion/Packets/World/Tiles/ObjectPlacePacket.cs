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
using Orion.World.Tiles;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to place an object.
    /// </summary>
    public sealed class ObjectPlacePacket : Packet {
        private short _x;
        private short _y;
        private BlockType _objectType;
        private short _style;
        private byte _data;
        private sbyte _randomState;
        private bool _direction;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ObjectPlace;

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

        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        /// <value>The object type.</value>
        public BlockType ObjectType {
            get => _objectType;
            set {
                _objectType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the object's style.
        /// </summary>
        /// <value>The object's style.</value>
        public short Style {
            get => _style;
            set {
                _style = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the object's random state.
        /// </summary>
        /// <value>The object's random state.</value>
        public sbyte RandomState {
            get => _randomState;
            set {
                _randomState = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the object direction. This is object-specific.
        /// </summary>
        /// <value>A value indicating the object direction.</value>
        public bool Direction {
            get => _direction;
            set {
                _direction = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            _objectType = (BlockType)reader.ReadUInt16();
            _style = reader.ReadInt16();

            // This byte is basically useless, but we'll keep track of it anyways.
            _data = reader.ReadByte();
            _randomState = reader.ReadSByte();
            _direction = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_x);
            writer.Write(_y);
            writer.Write((ushort)_objectType);
            writer.Write(_style);
            writer.Write(_data);
            writer.Write(_randomState);
            writer.Write(_direction);
        }
    }
}
