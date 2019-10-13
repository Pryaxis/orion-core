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
using System.Diagnostics.Contracts;
using System.IO;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to toggle the state of a door. See <see cref="Tiles.ToggleDoorAction"/> for a list
    /// of toggle door actions.
    /// </summary>
    public sealed class ToggleDoorPacket : Packet {
        private ToggleDoorAction _toggleDoorAction;
        private short _doorX;
        private short _doorY;
        private bool _toggleDirection;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ToggleDoor;

        /// <summary>
        /// Gets or sets the toggle door action.
        /// </summary>
        public ToggleDoorAction ToggleDoorAction {
            get => _toggleDoorAction;
            set {
                _toggleDoorAction = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the door's X coordinate.
        /// </summary>
        public short DoorX {
            get => _doorX;
            set {
                _doorX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the door's Y coordinate.
        /// </summary>
        public short DoorY {
            get => _doorY;
            set {
                _doorY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the direction of the toggle.
        /// </summary>
        public bool ToggleDirection {
            get => _toggleDirection;
            set {
                _toggleDirection = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ToggleDoorAction} @ ({DoorX}, {DoorY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _toggleDoorAction = (ToggleDoorAction)reader.ReadByte();
            _doorX = reader.ReadInt16();
            _doorY = reader.ReadInt16();
            _toggleDirection = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)_toggleDoorAction);
            writer.Write(_doorX);
            writer.Write(_doorY);
            writer.Write(_toggleDirection);
        }
    }
}
