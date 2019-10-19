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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace Orion.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to show a poof of smoke.
    /// </summary>
    public sealed class PoofOfSmokePacket : Packet {
        private Vector2 _position;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PoofOfSmoke;

        /// <summary>
        /// Gets or sets the smoke's position. The components are pixels.
        /// </summary>
        /// <value>The smoke's position.</value>
        public Vector2 Position {
            get => _position;
            set {
                _position = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _position = new HalfVector2 { PackedValue = reader.ReadUInt32() }.ToVector2();

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            writer.Write(new HalfVector2(Position).PackedValue);
    }
}
