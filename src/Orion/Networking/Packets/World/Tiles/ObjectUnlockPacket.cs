﻿// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using JetBrains.Annotations;
using Orion.Networking.World.Tiles;

namespace Orion.Networking.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to unlock an object (chest, door, etc.). See
    /// <see cref="Networking.World.Tiles.UnlockableObjectType"/> for a list of unlockable object types.
    /// </summary>
    [PublicAPI]
    public sealed class ObjectUnlockPacket : Packet {
        private UnlockableObjectType _unlockableObjectType;
        private short _objectX;
        private short _objectY;

        /// <inheritdoc />
        public override PacketType Type => PacketType.ObjectUnlock;

        /// <summary>
        /// Gets or sets the unlockable object type.
        /// </summary>
        public UnlockableObjectType UnlockableObjectType {
            get => _unlockableObjectType;
            set {
                _unlockableObjectType = value;
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
        public override string ToString() => $"{Type}[{UnlockableObjectType} @ ({ObjectX}, {ObjectY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _unlockableObjectType = (UnlockableObjectType)reader.ReadByte();
            _objectX = reader.ReadInt16();
            _objectY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)_unlockableObjectType);
            writer.Write(_objectX);
            writer.Write(_objectY);
        }
    }
}