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
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent from the server to the client to show a poof of smoke.
    /// </summary>
    public sealed class PoofOfSmokePacket : Packet {
        /// <summary>
        /// Gets or sets the smoke's position.
        /// </summary>
        public HalfVector2 SmokePosition { get; set; }

        internal override PacketType Type => PacketType.PoofOfSmoke;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[({SmokePosition})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            SmokePosition = new HalfVector2 {
                PackedValue = reader.ReadUInt32()
            };
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(SmokePosition.PackedValue);
        }
    }
}
