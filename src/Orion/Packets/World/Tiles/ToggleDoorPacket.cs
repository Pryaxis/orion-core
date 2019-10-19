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
    /// Packet sent to toggle the state of a door. See <see cref="Tiles.ToggleDoorAction"/> for a list
    /// of toggle door actions.
    /// </summary>
    public sealed class ToggleDoorPacket : Packet {
        private ToggleDoorAction _action;
        private short _x;
        private short _y;
        private bool _direction;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ToggleDoor;

        /// <summary>
        /// Gets or sets the toggle door action.
        /// </summary>
        /// <value>The toggle door action.</value>
        public ToggleDoorAction Action {
            get => _action;
            set {
                _action = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the door's X coordinate.
        /// </summary>
        /// <value>The door's X coordinate.</value>
        public short X {
            get => _x;
            set {
                _x = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the door's Y coordinate.
        /// </summary>
        /// <value>The door's Y coordinate.</value>
        public short Y {
            get => _y;
            set {
                _y = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the direction of the toggle. This is action-specific.
        /// </summary>
        /// <value>A value indicating the direction of the toggle.</value>
        public bool Direction {
            get => _direction;
            set {
                _direction = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _action = (ToggleDoorAction)reader.ReadByte();
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            _direction = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)_action);
            writer.Write(_x);
            writer.Write(_y);
            writer.Write(_direction);
        }
    }
}
