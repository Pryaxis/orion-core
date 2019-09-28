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

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent from the server to the client to synchronize tile frames. This is sent following every
    /// <see cref="SectionPacket"/>.
    /// </summary>
    public sealed class SectionFramesPacket : Packet {
        private short _startSectionX;
        private short _startSectionY;
        private short _endSectionX;
        private short _endSectionY;

        /// <inheritdoc />
        public override PacketType Type => PacketType.SectionFrames;

        /// <summary>
        /// Gets or sets the starting section's X index.
        /// </summary>
        public short StartSectionX {
            get => _startSectionX;
            set {
                _startSectionX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the starting section's Y index.
        /// </summary>
        public short StartSectionY {
            get => _startSectionY;
            set {
                _startSectionY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the ending section's X index.
        /// </summary>
        public short EndSectionX {
            get => _endSectionX;
            set {
                _endSectionX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the ending section's Y index.
        /// </summary>
        public short EndSectionY {
            get => _endSectionY;
            set {
                _endSectionY = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[({StartSectionX}, {StartSectionY}) to ({EndSectionX}, {EndSectionY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _startSectionX = reader.ReadInt16();
            _startSectionY = reader.ReadInt16();
            _endSectionX = reader.ReadInt16();
            _endSectionY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_startSectionX);
            writer.Write(_startSectionY);
            writer.Write(_endSectionX);
            writer.Write(_endSectionY);
        }
    }
}
