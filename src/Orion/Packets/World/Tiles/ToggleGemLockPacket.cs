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
    /// Packet sent to toggle a gem lock.
    /// </summary>
    public sealed class ToggleGemLockPacket : Packet {
        private short _x;
        private short _y;
        private bool _isLocked;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ToggleGemLock;

        /// <summary>
        /// Gets or sets the gem lock's X coordinate.
        /// </summary>
        /// <value>The gem lock's X coordinate.</value>
        public short X {
            get => _x;
            set {
                _x = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the gem lock's Y coordinate.
        /// </summary>
        /// <value>The gem lock's Y coordinate.</value>
        public short Y {
            get => _y;
            set {
                _y = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the gem lock is locked.
        /// </summary>
        /// <value><see langword="true"/> if the gem lock is locked; otherwise, <see langword="false"/>.</value>
        public bool IsLocked {
            get => _isLocked;
            set {
                _isLocked = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            _isLocked = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_x);
            writer.Write(_y);
            writer.Write(_isLocked);
        }
    }
}
