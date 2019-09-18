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
    /// Packet sent to toggle the state of a door.
    /// </summary>
    public sealed class ToggleDoorPacket : Packet {
        private DoorAction _toggleDoorAction;
        private short _doorX;
        private short _doorY;
        private bool _toggleDirection;

        /// <inheritdoc />
        public override PacketType Type => PacketType.ToggleDoor;

        /// <summary>
        /// Gets or sets the door action.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public DoorAction ToggleDoorAction {
            get => _toggleDoorAction;
            set {
                _toggleDoorAction = value ?? throw new ArgumentNullException(nameof(value));
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

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ToggleDoorAction} @ ({DoorX}, {DoorY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ToggleDoorAction = DoorAction.FromId(reader.ReadByte()) ??
                               throw new PacketException("Door action is invalid.");
            DoorX = reader.ReadInt16();
            DoorY = reader.ReadInt16();
            ToggleDirection = reader.ReadByte() == 1;
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ToggleDoorAction.Id);
            writer.Write(DoorX);
            writer.Write(DoorY);
            writer.Write(ToggleDirection);
        }

        /// <summary>
        /// Represents the door action in a <see cref="ToggleDoorPacket"/>.
        /// </summary>
        public sealed class DoorAction {
#pragma warning disable 1591
            public static readonly DoorAction OpenDoor = new DoorAction(0);
            public static readonly DoorAction CloseDoor = new DoorAction(1);
            public static readonly DoorAction OpenTrapdoor = new DoorAction(2);
            public static readonly DoorAction CloseTrapdoor = new DoorAction(3);
            public static readonly DoorAction OpenTallGate = new DoorAction(4);
            public static readonly DoorAction CloseTallGate = new DoorAction(5);
#pragma warning restore 1591

            private static readonly DoorAction[] Types = new DoorAction[6];
            private static readonly string[] Names = new string[6];

            /// <summary>
            /// Gets the door action's ID.
            /// </summary>
            public byte Id { get; }

            static DoorAction() {
                foreach (var field in typeof(DoorAction).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                    var doorAction = (DoorAction)field.GetValue(null);
                    Types[doorAction.Id] = doorAction;
                    Names[doorAction.Id] = field.Name;
                }
            }

            private DoorAction(byte id) {
                Id = id;
            }

            /// <summary>
            /// Returns a door action converted from the given ID.
            /// </summary>
            /// <param name="id">The ID.</param>
            /// <returns>The door action, or <c>null</c> if none exists.</returns>
            public static DoorAction FromId(byte id) => id < Types.Length ? Types[id] : null;

            /// <inheritdoc />
            public override string ToString() => Names[Id];
        }
    }
}
