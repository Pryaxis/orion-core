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
    /// Packet sent from the server to the client to synchronize tile frames.
    /// </summary>
    /// <remarks>This is sent following a <see cref="SectionPacket"/>.</remarks>
    public sealed class SectionFramesPacket : Packet {
        private short _startX;
        private short _startY;
        private short _endX;
        private short _endY;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.SectionFrames;

        /// <summary>
        /// Gets or sets the starting section's X index.
        /// </summary>
        /// <value>The starting section's X index.</value>
        public short StartX {
            get => _startX;
            set {
                _startX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the starting section's Y index.
        /// </summary>
        /// <value>The starting section's Y index.</value>
        public short StartY {
            get => _startY;
            set {
                _startY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the ending section's X index.
        /// </summary>
        /// <value>The ending section's X index.</value>
        public short EndX {
            get => _endX;
            set {
                _endX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the ending section's Y index.
        /// </summary>
        /// <value>The ending section's Y index.</value>
        public short EndY {
            get => _endY;
            set {
                _endY = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _startX = reader.ReadInt16();
            _startY = reader.ReadInt16();
            _endX = reader.ReadInt16();
            _endY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_startX);
            writer.Write(_startY);
            writer.Write(_endX);
            writer.Write(_endY);
        }
    }
}
