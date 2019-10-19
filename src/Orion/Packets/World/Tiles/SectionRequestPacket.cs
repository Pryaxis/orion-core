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
    /// Packet sent from the client to the server to request a section of the world.
    /// </summary>
    public sealed class SectionRequestPacket : Packet {
        private int _x;
        private int _y;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.SectionRequest;

        /// <summary>
        /// Gets or sets the section's X index. If negative, the spawn section will be sent.
        /// </summary>
        /// <value>The section's X index.</value>
        public int X {
            get => _x;
            set {
                _x = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the section's Y index. If negative, the spawn section will be sent.
        /// </summary>
        /// <value>The section's Y index.</value>
        public int Y {
            get => _y;
            set {
                _y = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _x = reader.ReadInt32();
            _y = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_x);
            writer.Write(_y);
        }
    }
}
