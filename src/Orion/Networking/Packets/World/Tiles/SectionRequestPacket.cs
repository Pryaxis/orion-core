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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;

namespace Orion.Networking.Packets.World.Tiles {
    /// <summary>
    /// Packet sent from the client to the server to request a section of the world.
    /// </summary>
    [PublicAPI]
    public sealed class SectionRequestPacket : Packet {
        private int _sectionX;
        private int _sectionY;

        /// <inheritdoc />
        public override PacketType Type => PacketType.SectionRequest;

        /// <summary>
        /// Gets or sets the section's X index. An invalid value results in only the spawn section being sent.
        /// </summary>
        public int SectionX {
            get => _sectionX;
            set {
                _sectionX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the section's Y index. An invalid value results in only the spawn section being sent.
        /// </summary>
        public int SectionY {
            get => _sectionY;
            set {
                _sectionY = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[({SectionX}, {SectionY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _sectionX = reader.ReadInt32();
            _sectionY = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_sectionX);
            writer.Write(_sectionY);
        }
    }
}
